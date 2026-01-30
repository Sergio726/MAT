
CREATE PROCEDURE [dbo].[usp_MAT_Reserva_ActualizarEstados]
(
	@FacturaID UNIQUEIDENTIFIER
)
AS
BEGIN
	begin try
		declare @Saldo money,
				@TotalFactura money
		declare @tmpVoucher as table (VoucherID UNIQUEIDENTIFIER)
		/*Actualizar estado de factura y pasaje*/ 
				/*calcular saldo de factura*/ 
				SELECT @Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)
				select @TotalFactura = sum(df.Precio * df.Cantidad) from dbo.DetalleFactura df where df.FacturaID = @FacturaID

				begin tran
					IF((@Saldo < @TotalFactura) and (@Saldo > 0)) 
					  BEGIN 
						--3  Señado 
						  delete @tmpVoucher

						  insert into @tmpVoucher
						  select vo.VoucherID
						  from dbo.Voucher vo
						  inner join dbo.Pasaje pj
							on pj.VoucherID = vo.VoucherID
						  where pj.FacturaID = @FacturaID

						  UPDATE dbo.Factura 
						  SET    Estado = 3 
						  WHERE  FacturaID = @FacturaID 


						  UPDATE p 
						  SET    p.estadopasaje = (case p.estadopasaje
													when 6 then 8 -- Pagado - Con Hotel ==>Señado-ConHotel
													when 8 then 8
													else 3 
												   end ),
								 p.VoucherID = null
						  FROM   pasaje p 
						  WHERE  p.facturaid = @FacturaID 
				  	
						  /*delete voucher*/
						  delete vo
						  from dbo.Voucher vo
						  inner join @tmpVoucher tmp
							on vo.VoucherID = tmp.VoucherID
						  
					  END 
					  else if (@Saldo = @TotalFactura)
					  begin
						  
						   delete @tmpVoucher

						  insert into @tmpVoucher
						  select vo.VoucherID
						  from dbo.Voucher vo
						  inner join dbo.Pasaje pj
							on pj.VoucherID = vo.VoucherID
						  where pj.FacturaID = @FacturaID

						  UPDATE dbo.Factura 
						  SET    Estado = 2 --Pre reserva
						  WHERE  FacturaID = @FacturaID 


						  UPDATE p 
						  SET    p.estadopasaje = ( CASE p.estadopasaje 
													  WHEN 6 THEN 9 --Pagado - Con Hotel ==> Pre-Reserva - Con Hotel
													  WHEN 8 THEN 9 --Señado - Con Hotel ==> Pre-Reserva - Con Hotel
													  WHEN 9 THEN 9 --Pre-Reserva - Con Hotel ==> Pre-Reserva - Con Hotel
													  ELSE 5 --Pre-reserva
													END ),
								 p.VoucherID = null 
						  FROM   dbo.Pasaje p 
						  WHERE  p.FacturaID = @FacturaID 


						  /*delete voucher*/
						 /*delete voucher*/
						  delete vo
						  from dbo.Voucher vo
						  inner join @tmpVoucher tmp
							on vo.VoucherID = tmp.VoucherID
					 end
					 ELSE IF( @Saldo <= 0 ) 
					 BEGIN
						  declare @NroFactura varchar(50),
								  @VendedorId uniqueidentifier;
						  select @NroFactura = NroFactura,
								 @VendedorId = VendedorID
						  from dbo.Factura where FacturaID = @FacturaID

						 --4  Pagado 
						  UPDATE factura 
						  SET    estado = 4, 
								 nrofactura = @NroFactura 
						  WHERE  facturaid = @FacturaID 

						  SELECT Newid()     AS VoucherID, 
								 GETDATE()      AS FechaEmision, 
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

				 commit;
			/*-------------End Actualizar estado de factura y pasaje------------*/
	end try
	begin catch
		IF @@TRANCOUNT > 0 
			ROLLBACK TRAN 

			DECLARE @errmsg   AS NVARCHAR (2048), 
					@errState INT,
					@sFacturaID AS NVARCHAR (36)

			SELECT @errmsg = Error_message() + Error_line(), 
				   @errState = Error_state(),
				   @sFacturaID = CONVERT(NVARCHAR(36), @FacturaID)
			
			
			
			RAISERROR (N'Error al actualizar estados de la reserva, FacturaID: %s . Error: %s',16,34,@sFacturaID,@errmsg);

	end catch
END;

