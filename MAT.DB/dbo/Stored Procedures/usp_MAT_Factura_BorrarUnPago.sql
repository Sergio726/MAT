CREATE PROCEDURE [dbo].[usp_MAT_Factura_BorrarUnPago]( @MovimientoID uniqueidentifier,
												   @VendedorID uniqueidentifier )
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio
  -- Create date: 13/04/2017
  -- Description:  Delete a payment
  -- 09/24/2017	Garcia Sergio	 update source of @ClienteId	
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
			DECLARE @Saldo        FLOAT, 
					@FacturaID    UNIQUEIDENTIFIER, 
					@HistorialID  UNIQUEIDENTIFIER, 
					@PagoMonto    FLOAT, 
					@Fecha        DATETIME, 
					@ClienteID    UNIQUEIDENTIFIER, 
					@PagoID       UNIQUEIDENTIFIER, 
					@FacturaMonto FLOAT 

			SELECT @HistorialID = Newid(), 
				   @Fecha = Getdate() 

			SELECT @PagoMonto = p.Monto, 
				   @FacturaID = mc.FacturaID, 
				   @PagoID = p.PagoID 
			FROM   dbo.MovimientoCuenta mc 
				   INNER JOIN dbo.Pago p 
						   ON mc.pagoid = p.pagoid 
			WHERE  mc.MovimientoID = @MovimientoID 

			select @ClienteID = f.ClienteID from dbo.Factura f where f.FacturaID = @FacturaID

			SELECT @FacturaMonto = f.monto 
			FROM   dbo.Factura f 
			WHERE  f.facturaid = @FacturaID 

			/*Guardar Historial*/
			
			INSERT INTO dbo.Historial 
						(HistorialID, 
						 Tabla, 
						 Operacion, 
						 FechaHoraRegistro, 
						 Cliente, 
						 Vendedor, 
						 Observaciones, 
						 Monto) 
			VALUES      ( @HistorialID, 
						  1,--TablaPago 
						  2,--Baja 
						  @Fecha, 
						  @ClienteID, 
						  @VendedorID, 
						  'PagoBorrado', 
						  @PagoMonto ) 
			
			/*Eliminar Movimiento y Pago*/
			DELETE dbo.MovimientoCuenta 
			WHERE  movimientoid = @MovimientoID 

			DELETE dbo.Pago 
			WHERE  pagoid = @PagoID 

			/*Actualizar estado de factura y pasaje*/ 
			/*calcular saldo de factura*/ 
			
			SELECT @Saldo = ( f.monto - Sum(p.monto) ) 
			FROM   dbo.Factura f 
				   INNER JOIN dbo.MovimientoCuenta mc 
						   ON f.FacturaID = mc.FacturaID 
				   INNER JOIN dbo.Pago p 
						   ON mc.PagoID = p.PagoID 
			WHERE  f.FacturaID = @FacturaID
			group by f.Monto


			IF( @Saldo > 0 ) 
			  BEGIN 
				   --3  Señado 
				 UPDATE dbo.Factura 
				 SET    estado = 3 
				 WHERE  facturaid = @FacturaID 

				 UPDATE p 
				 SET    p.estadopasaje = ( CASE p.estadopasaje 
										  WHEN 6 THEN 8 --Pasaje y Hotel prereservados 
										  ELSE 3 
										END ) 
				 FROM   dbo.Pasaje p 
				 WHERE  p.facturaid = @FacturaID 
				  
				 
				 UPDATE p
				 SET p.VoucherID = null
				 FROM   dbo.Voucher v 
				 		INNER JOIN pasaje p 
				 				ON v.voucherid = p.voucherid 
				 WHERE  p.facturaid = @FacturaID 


				 DELETE v 
				 FROM   dbo.Voucher v 
				 		INNER JOIN pasaje p 
				 				ON v.voucherid = p.voucherid 
				 WHERE  p.facturaid = @FacturaID 
			
			  END 
			  ELSE
			  BEGIN
					IF (@Saldo = @FacturaMonto)
					BEGIN
						 --5 Pre-reserva
					  UPDATE factura 
					  SET    estado = 5
					  WHERE  facturaid = @FacturaID 

					  UPDATE p 
					  SET    p.estadopasaje = ( CASE p.estadopasaje 
												  WHEN 8 THEN 9 -- Pasaje pre-reserva con hotel
												  ELSE 5 
												END ) 
					  FROM   dbo.Pasaje p 
					  WHERE  p.facturaid = @FacturaID 
					END
			  END
			

			SELECT 1 AS ID, 
				   'Done.'  AS Result 
	
			COMMIT TRAN; 
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			select -1 as ID, @errmsg AS Result
			
		END CATCH
		
  END

