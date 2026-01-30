CREATE PROCEDURE [dbo].[usp_MAT_Factura_DeleteFactura](@FacturaID	VARCHAR(36),
													   @VendedorID VARCHAR(36))
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 05/22/2017
  -- Description:  DELETE FACTURA
  -- 2017-09-17	Garcia Sergio	Delete DetalleFactura
  -- 2017-09-28	Garcia Sergio	Nota Credito
  -- 2018-04-29 Garcia Sergio	delete PagoDetalle
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
			
			
			CREATE TABLE #TempNotasPagosID(PagoID uniqueidentifier,
									       NotaID uniqueidentifier)
			DECLARE @PersonaID VARCHAR(36),
					@Descripcion VARCHAR(150)
			
			/****************GET DATA************/
				SELECT @PersonaID = f.ClienteID, 
					   @Descripcion = p.Descripcion + ' ' + CONVERT(VARCHAR(10), v.FechaSalida, 103) 
				FROM   Factura f 
				  INNER JOIN Pasaje pje 
				  ON pje.FacturaID = f.FacturaID 
				  INNER JOIN Viaje v 
				  ON pje.ViajeID = v.ViajeID 
				  INNER JOIN Paquete p 
				  ON v.PaqueteID = p.PaqueteID 
				WHERE  f.FacturaID = @FacturaID 
			/***********************************/

			--DELETE MOVIMIENTO CUENTA
			IF EXISTS(SELECT * FROM MovimientoCuenta WHERE FacturaID = @FacturaID)
			BEGIN
				INSERT INTO #TempNotasPagosID
				SELECT PagoID, NotaID
				FROM MovimientoCuenta 
				WHERE FacturaID = @FacturaID

				DELETE MovimientoCuenta WHERE FacturaID = @FacturaID

			END

			--DELETE DETALLE FACTURA
			IF EXISTS(SELECT * FROM dbo.DetalleFactura WHERE FacturaID = @FacturaID)
			BEGIN
				
				DELETE dbo.DetalleFactura WHERE FacturaID = @FacturaID

			END

			--RETURN CREDIT
			IF EXISTS(SELECT * 
					  FROM dbo.Pago p 
					  INNER JOIN dbo.MovimientoCuenta mc
						ON mc.PagoID = p.PagoID
					  WHERE p.TipoPago = 4 
					  AND mc.FacturaID = @FacturaID)
			BEGIN

				INSERT INTO dbo.CreditoCliente (VendedorID, ClienteID, Monto, Descripcion, IsInput) 
				  SELECT  p.VendedorId,c.ClienteID,p.Monto, concat('PAGO CANCELADO - NRO RECIBO ' , p.NroRecibo),1
					FROM   dbo.Pago p 
					INNER JOIN dbo.MovimientoCuenta mc
						on p.PagoID = mc.PagoID
					INNER JOIN dbo.Cuenta c
						on mc.CuentaID = c.CuentaID
					WHERE  mc.FacturaID = @FacturaID
			END

			--DELETE PAGO
			IF EXISTS(SELECT PagoID FROM  #TempNotasPagosID)
			BEGIN
				delete pd
				from dbo.PagoDetalle pd
				inner join dbo.Pago p
					on pd.PagoID = p.PagoID
				inner join #TempNotasPagosID tp
					on tp.PagoID = p.PagoID

				DELETE p
				FROM   Pago p 
				   INNER JOIN #TempNotasPagosID tp 
						   ON p.PagoID = tp.PagoID 

			END

			--DELETE NOTAS DE CREDITO
			IF EXISTS(SELECT NotaID FROM  #TempNotasPagosID WHERE NotaID IS NOT NULL)
			BEGIN
				DELETE n
				FROM   Nota n 
				   INNER JOIN #TempNotasPagosID tp 
						   ON n.NotaID = tp.NotaID

			END

			--CLEAR PASAJES
			IF EXISTS(SELECT * FROM Pasaje WHERE FacturaID = @FacturaID)
			BEGIN 
				UPDATE p
				SET p.PasajeroID = null,
					p.FechaReserva = null,
					p.FechaCompra = null,
					p.FacturaID = null,
					p.EstadoPasaje = 1, --disponible
					p.VoucherID = null
				FROM dbo.Pasaje p
				INNER JOIN dbo.Factura f
					ON p.FacturaID = f.FacturaID
					AND p.FacturaID = @FacturaID
			END

			--CHECK AND DELETE VOUCHER
			IF EXISTS(
				SELECT * 
				FROM Voucher v
				left join Pasaje p
					on v.VoucherID = p.VoucherID
				where p.VoucherID is null
			)
			BEGIN 
				DELETE v
				FROM   Voucher v 
					   LEFT JOIN Pasaje p 
							  ON v.VoucherID = p.VoucherID 
				WHERE  p.VoucherID IS NULL 
			END
	
			--CHECK AND DELETE PASAJE ADICIONAL 
			--IF EXISTS(
			--	SELECT * 
			--	FROM dbo.Pasaje p
			--	LEFT JOIN dbo.PasajeAdicional pa
			--		ON pa.PasajeID = p.PasajeID
			--	WHERE p.PasajeID IS NULL
			--)
			--BEGIN 
			--	DELETE pa
			--	FROM dbo.Pasaje p
			--	LEFT JOIN dbo.PasajeAdicional pa
			--		ON pa.PasajeID = p.PasajeID
			--	WHERE p.PasajeID IS NULL
			--END

			/****/
			
			
			delete pd
			from PaymentDetail pd
			inner join Payment p on p.Id = pd.PaymentId
			inner join PedidoReserva pr on pr.Id = p.ReservaId
			where pr.FacturaId = @FacturaID

			delete p
			from Payment p
			inner join PedidoReserva pr on pr.Id = p.ReservaId
			where pr.FacturaId = @FacturaID

			delete ps
			from PasajeSeleccionado ps
			inner join  PedidoReserva pr on ps.ReservaId = pr.Id
			where pr.FacturaId = @FacturaID

			delete pr
			from PedidoReserva pr
			where pr.FacturaId = @FacturaID


			/****/


			--DELETE FACTURA
			IF EXISTS(SELECT * FROM Factura WHERE FacturaID = @FacturaID)
			BEGIN
				
				DELETE Factura WHERE FacturaID = @FacturaID
							
				CREATE TABLE #TempAuditFactura(Result VARCHAR(2048))
				SELECT @Descripcion = 'Baja de Factura - ' + @Descripcion

				INSERT INTO #TempAuditFactura
				EXEC usp_Factura_Audit @FacturaID, @PersonaID,@VendedorID,'Delete', @Descripcion
				DROP TABLE #TempAuditFactura


			END

			DROP TABLE #TempNotasPagosID


			COMMIT TRAN; 
			
			SELECT 'Done.' AS Result

		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			SELECT @errmsg AS Result
			
		END CATCH
     

  END
