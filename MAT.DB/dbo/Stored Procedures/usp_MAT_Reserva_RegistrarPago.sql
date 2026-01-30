CREATE PROCEDURE [dbo].[usp_MAT_Reserva_RegistrarPago]
(
	@ReservaId UNIQUEIDENTIFIER,
	@PagoMonto MONEY
)

AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 19/11/2024
Description:  Registrar el pago de la reserva
Basado en el SP: usp_MAT_RegistroPago_NuevoPago

History:

2024-11-19 Ruben Tejerina Create SP
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    BEGIN TRY 
		BEGIN TRAN

		DECLARE
			@FacturaId				UNIQUEIDENTIFIER,
			@ClienteId				UNIQUEIDENTIFIER,
			@VendedorId				UNIQUEIDENTIFIER,
			@FechaHoy				DATETIME,
			@PagoMontoRecibidoMonedaTipo int =1,
			@PagoMontoEquivalente money = null,
			@PagoMontoEquivalenteMonedaTipo int = null,
			@PagoMontoEquivalenteCotizacion money = null,--cotizacion del momento	
			@PagoNroRecibo	varchar(50) = '',
			@PagoTransaccionId varchar(50) = null,
			@PagoTipoPago int,
			@PagoNroFactura	varchar(50) = null

		
		DECLARE 
			@PagoID            UNIQUEIDENTIFIER = Newid(), 
			@MovimientoID      UNIQUEIDENTIFIER = Newid(), 
			@CuentaID          UNIQUEIDENTIFIER, 
			@Saldo             FLOAT,
			@ReservaHotel	   bit = 0,
			@TotalFactura      money

		SELECT
			@FechaHoy = GETDATE(),
			@FacturaId = pr.FacturaId,
			@ClienteId = pr.ClienteId,
			@VendedorId = pr.VendedorId,		
			@PagoNroRecibo = pr.PagoNroRecibo,
			@PagoTransaccionId = pr.PagoTransaccionId,
			@PagoTipoPago = pr.PagoTipoPago,
			@PagoMontoEquivalente = pr.PagoMontoEquivalente,
			@PagoMontoRecibidoMonedaTipo = pr.PagoMontoRecibidoMonedaTipo,
			@PagoMontoEquivalenteMonedaTipo = pr.PagoMontoEquivalenteMonedaTipo,
			@PagoMontoEquivalenteCotizacion = pr.PagoMontoEquivalenteCotizacion,
			@PagoNroFactura = pr.PagoNroFactura

		FROM PedidoReserva pr
		WHERE pr.Id = @ReservaId
		
		if(@PagoMonto > 0 )
			BEGIN

				SELECT @CuentaID = c.cuentaid 
				FROM   cuenta c 
				WHERE  c.clienteid = @ClienteId 
			
				/*Actualizacion de pagos*/ 
				INSERT INTO dbo.pago 
				(	pagoid, 
					fechapago, 
					monto, 
					nrorecibo, 
					TransaccionID,
					tipopago, 
					vendedorid) 
				VALUES
					(@PagoID, 
					@FechaHoy, 
					@PagoMonto, 
					@PagoNroRecibo,
					@PagoTransaccionId, 
					@PagoTipoPago, 
					@VendedorId ) 
				
				/*si la moneda en que paga es diferente al peso argentino*/
				if @@ROWCOUNT > 0
				begin
					if @PagoMontoEquivalente is not null 
						insert into dbo.PagoDetalle 
							(PagoID,
							MontoRecibido,
							MontoRecibidoMonedaTipo, 
							MontoEquivalente,
							MontoEquivalenteMonedaTipo,
							MontoEquivalenteCotizacion
							)
						values (
							@PagoID,
							@PagoMonto,
							@PagoMontoRecibidoMonedaTipo,
							@PagoMontoEquivalente,
							@PagoMontoEquivalenteMonedaTipo,
							@PagoMontoEquivalenteCotizacion)

				end
				
				/*Movimiento de Nota de Credito en CreditoCliente*/
				if(@PagoTipoPago = 4) --nota credito
				begin
					insert into dbo.CreditoCliente
					(
						VendedorID,
						ClienteID,
						Monto,
						Descripcion,
						IsInput
					)
					values (
						@VendedorId,
						@ClienteId,
						-@PagoMonto,
						'REALIZACION PAGO. NRO DE RECIBO ' + @PagoNroRecibo
						,0
					)
				end

				/*Impacto Movimiento de Pago en el Historial*/ 
				INSERT INTO dbo.movimientocuenta 
				(	
					movimientoid, 
					cuentaid, 
					pagoid, 
					facturaid, 
					fecharegistro
				) 
				VALUES 
				(
					@MovimientoID, 
					@CuentaID, 
					@PagoID, 
					@FacturaID, 
					@FechaHoy
				) 

				/*Actualizar estado de factura y pasaje*/ 
				/*calcular saldo de factura*/ 
				SELECT @Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)

				
				select 
					@TotalFactura = sum(df.Precio * df.Cantidad) 
				from dbo.DetalleFactura df 
				where df.FacturaID = @FacturaID

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
					SET estado = 4, 
						nrofactura = @PagoNroFactura 
					WHERE facturaid = @FacturaID 

					SELECT Newid()     AS VoucherID, 
						@FechaHoy      AS FechaEmision, 
						@VendedorId AS VendedorID, 
						p.pasajeid, 
						p.estadopasaje 
					INTO #tmpvoucherpasaje 
					FROM pasaje p 
					WHERE p.facturaid = @FacturaID 

					INSERT INTO dbo.voucher 
					(
						voucherid, 
						fechaemision, 
						vendedorid
					) 
					SELECT voucherid,
						fechaemision, 
						vendedorid 
					FROM #tmpvoucherpasaje 

					UPDATE p 
					SET p.estadopasaje = ( 
						case when (hr.HabitacionID is not null) 
							then 6 
							else 4 
						end), --6 pagado con hotel / 4 pagado

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
					WHERE facturaid = @FacturaID 

					UPDATE p 				
					SET  p.estadopasaje = ( 
						case when (hr.HabitacionID is not null) 
							then 8 
							else 3 
						end) --6 Señado - Con Hotel / 3 Señado sin hotel
					FROM pasaje p 
					left join dbo.ReservaHabitacion hr
							 on p.PasajeID = hr.PasajeID
							 and p.ViajeID = hr.ViajeID
					WHERE  p.facturaid = @FacturaID 
				END 


			end

		IF( @Saldo is null)
		BEGIN
			SELECT @Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)

				
			select 
				@TotalFactura = sum(df.Precio * df.Cantidad) 
			from dbo.DetalleFactura df 
			where df.FacturaID = @FacturaID

		END

		SELECT
			@FacturaId as FacturaId,
			@PagoID as PagoId,
			@Saldo as Saldo,
			@TotalFactura as TotalFactura

        COMMIT TRAN; 
		--ROLLBACK TRAN;
	END TRY
	BEGIN CATCH
		
		DECLARE @errmsg   AS NVARCHAR (2048)
		SELECT @errmsg ='Error in usp_MAT_Reserva_RegistrarPago. Message:' + Error_message() + ' Error Line:' + STR(ERROR_LINE())
		print @errmsg
		
		-- Deshacemos la transación
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;

		--DECLARE @errmsg   AS NVARCHAR (2048)
		--SELECT @errmsg ='Error in usp_MAT_Reserva_RegistrarPago. Message:' + Error_message() + ' Error Line:' + STR(ERROR_LINE())

		RAISERROR(@errmsg, 16, 1)

	END CATCH
END