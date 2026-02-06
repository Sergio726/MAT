CREATE PROCEDURE [dbo].[usp_MAT_RegistroPago_NuevoPago](@Monto	money	= 0	,
														@MontoRecibidoMonedaTipo int =1,
														@MontoEquivalente money = null,
														@MontoEquivalenteMonedaTipo int = null,
														@MontoEquivalenteCotizacion money = null,--cotizacion del momento
														@ClienteID	uniqueidentifier,
														@NroRecibo	varchar(50) = '',
														@TransaccionId varchar(50) = null,
														@TipoPago int,
														@VendedorId	uniqueidentifier = null,
														@FacturaID uniqueidentifier,
														@NroFactura	varchar(50) = null
																											   )
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 04/04/2017
  -- Description:  registra un nuevo pago 
  -- 09-14-2017:   optimize query, delete columns unnecessary, calculate Monto of the table DetalleFactura
  -- 2017-09-29	Garcia Sergio	join EstadoFactura, Pago con nota de credito dbo.CreditoCliente
  -- 2018-03-16	Garcia Sergio	fix logic of the update dbo.Pasaje
  -- 2018-04-19 Garcia Sergio	insert into PagoDetalle
  -- 2019-08-03	Garcia Sergio	upgrate procedure
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  if (@Monto >= 0)
	  begin
		BEGIN TRY 
			BEGIN TRAN
			DECLARE @Fecha             DATETIME = Getdate(), 
					@PagoID            UNIQUEIDENTIFIER = Newid(), 
					@MovimientoID      UNIQUEIDENTIFIER = Newid(), 
					@CuentaID          UNIQUEIDENTIFIER, 
					@Saldo             FLOAT ,
					@ReservaHotel	   bit = 0

			SELECT @CuentaID = c.cuentaid 
			FROM   cuenta c 
			WHERE  c.clienteid = @ClienteID 

						
			/*Actualizacion de pagos*/ 
			INSERT INTO dbo.pago 
						(pagoid, 
						 fechapago, 
						 monto, 
						 nrorecibo, 
						 TransaccionID,
						 tipopago, 
						 vendedorid) 
			VALUES      (@PagoID, 
						 @Fecha, 
						 @Monto, 
						 @NroRecibo,
						 @TransaccionId, 
						 @TipoPago, 
						 @VendedorId ) 
			/*si la moneda en que paga es diferente al peso argentino*/
			if @@ROWCOUNT > 0
				if @MontoEquivalente is not null 
					insert into dbo.PagoDetalle (PagoID,MontoRecibido,MontoRecibidoMonedaTipo, MontoEquivalente,MontoEquivalenteMonedaTipo,MontoEquivalenteCotizacion)
					values (@PagoID,@Monto,@MontoRecibidoMonedaTipo,@MontoEquivalente,@MontoEquivalenteMonedaTipo,@MontoEquivalenteCotizacion)


			/*Movimiento de Nota de Credito en CreditoCliente + trazabilidad (PagoID/FacturaID) y aplicacion por nota (FIFO)*/
			if(@TipoPago = 4) --nota credito
			begin
				insert into dbo.CreditoCliente (VendedorID,ClienteID,Monto,Descripcion,IsInput,PagoID,FacturaID)
				values (@VendedorId,@ClienteID,-@Monto,'REALIZACION PAGO. NRO DE RECIBO ' + @NroRecibo,0,@PagoID,@FacturaID)
				EXEC dbo.usp_MAT_NotaCreditoAplicacion_Allocate @ClienteID = @ClienteID, @PagoID = @PagoID, @FacturaID = @FacturaID, @Monto = @Monto
			end

			/*Impacto Movimiento de Pago en el Historial*/ 
			INSERT INTO dbo.movimientocuenta 
						(movimientoid, 
						 cuentaid, 
						 pagoid, 
						 facturaid, 
						 fecharegistro) 
			VALUES      ( @MovimientoID, 
						  @CuentaID, 
						  @PagoID, 
						  @FacturaID, 
						  @Fecha ) 

			/*Actualizar estado de factura y pasaje*/ 
			/*calcular saldo de factura*/ 
			SELECT @Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)

			declare @TotalFactura  money
			select @TotalFactura = sum(df.Precio * df.Cantidad) from dbo.DetalleFactura df where df.FacturaID = @FacturaID

			IF ((@Saldo >= @TotalFactura) and (@Saldo > 0))
			BEGIN
				 UPDATE factura 
					  SET    estado = 2 --PRE RESERVA
					  WHERE  facturaid = @FacturaID 

					  UPDATE p 
					  SET    p.estadopasaje = 5 --PRE RESERVA 
					  FROM   pasaje p 
					  WHERE  p.facturaid = @FacturaID 
			END
				ELSE IF( @Saldo <= 0 ) 
				  BEGIN 
					  --4  Pagado 
					  UPDATE factura 
					  SET    estado = 4, 
							 nrofactura = @NroFactura 
					  WHERE  facturaid = @FacturaID 

					  SELECT Newid()     AS VoucherID, 
							 @Fecha      AS FechaEmision, 
							 @VendedorId AS VendedorID, 
							 p.pasajeid, 
							 p.estadopasaje 
					  INTO   #tmpvoucherpasaje 
					  FROM   pasaje p 
					  WHERE  p.facturaid = @FacturaID 

					  INSERT INTO dbo.voucher 
								  (voucherid, 
								   fechaemision, 
								   vendedorid) 
					  SELECT voucherid, 
							 fechaemision, 
							 vendedorid 
					  FROM   #tmpvoucherpasaje 

					  UPDATE p 
					  SET    p.estadopasaje = ( case when (hr.HabitacionID is not null) then 6 else 4 end), --6 pagado con hotel / 4 pagado
												  												  
												--  WHEN 8 THEN 6 --Pagado - Con Hotel 
												--  WHEN 9 THEN 6 --Pagado - Con Hotel
												--  WHEN 7 THEN 7 --Anulado 
												--  ELSE 4 
												--END ),--Pagado 
							 p.voucherid = t.voucherid 
					  FROM   dbo.pasaje p 
							 INNER JOIN #tmpvoucherpasaje t 
									 ON p.pasajeid = t.pasajeid 
							 left join dbo.ReservaHabitacion hr
									 on p.PasajeID = hr.PasajeID
									 and p.ViajeID = hr.ViajeID
					  where p.EstadoPasaje <> 7 --anulado

					  DROP TABLE #tmpvoucherpasaje 
			
				  END 
				ELSE 
				  BEGIN 
					  --3  Señado 
					  UPDATE factura 
					  SET    estado = 3 
					  WHERE  facturaid = @FacturaID 

					  UPDATE p 
					  --SET    p.estadopasaje = iif(p.estadopasaje = 9,8,3) --si es Pre-Reserva - Con Hotel ==> Señado - Con Hotel, sino ==> Señado
					  SET    p.estadopasaje = ( case when (hr.HabitacionID is not null) then 8 else 3 end) --6 Señado - Con Hotel / 3 Señado sin hotel
					  FROM   pasaje p 
					  left join dbo.ReservaHabitacion hr
									 on p.PasajeID = hr.PasajeID
									 and p.ViajeID = hr.ViajeID
					  WHERE  p.facturaid = @FacturaID 
				  END 



			SELECT	  ef.Descripcion EstadoFactura ,
				     'Done.'            AS Result
			FROM dbo.Factura f 
			INNER JOIN dbo.EstadoFactura ef
				ON f.Estado = ef.ID
			WHERE f.FacturaID = @FacturaID
	
			COMMIT TRAN; 
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			select '' AS EstadoFactura , @errmsg AS Result
			
		END CATCH


	  end
	  
	  else
		 SELECT	  'Pre-reserva' EstadoFactura,
				   'Done.'            AS Result

     

  END

