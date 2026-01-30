CREATE PROCEDURE [dbo].[usp_MAT_Reserva_Reservar]
(
	@ReservaId uniqueidentifier,
	@ViajeId    uniqueidentifier,
	@VendedorID	uniqueidentifier,
	@ClienteID	uniqueidentifier,
	@Observaciones	varchar(8000) = null,
	@Condicion  varchar(50) = null,
	@MonedaTipo int,
	@DescuentoDetalle varchar(200) = null,
	@DescuentoMonto money,
	@DescuentoIsDescuento bit = 1,

	@PagoMonto	money	= 0	,
	@PagoMontoRecibidoMonedaTipo int =1,
	@PagoMontoEquivalente money = null,
	@PagoMontoEquivalenteMonedaTipo int = null,
	@PagoMontoEquivalenteCotizacion money = null,--cotizacion del momento	
	@PagoNroRecibo	varchar(50) = '',
	@PagoTransaccionId varchar(50) = null,
	@PagoTipoPago int,
	@PagoNroFactura	varchar(50) = null,

	@ExpirationMinutes int = 15
)
AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 29/10/2024
Description:  Realizar el pago de la reserva
Pasos
1. Insertar Factura: [usp_MAT_Reserva_NuevaReserva]
2. Registrar pasajero como prereserva: usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher
3. Registrar detalle de factura: usp_MAT_Reserva_DetalleFactura
4. Agregar descuento: usp_MAT_DetalleFactura_AgregarDescuentoRecargo
5. Asignar Habitaciones: usp_MAT_ReservaHabitacion_NuevaReserva
6. Registrar el pago: usp_MAT_RegistroPago_NuevoPago
7. Vincular menores: usp_MAT_Reserva_VincularMenorByViajeID
History:

2024-11-04 Ruben Tejerina Replace @Pasaje tvp_pasaje with @SessionId
	The data are previously inserted and identified with session Id
	in the new table PasajeSeleccionado

2024-11-11 Ruben Tejerina Add return result, FacturaId, FacturaEstadoId, FacturaEstado
2024-11-20 Ruben Tejerina Replace SessionId by ReservaId
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    BEGIN TRY 
		BEGIN TRAN
			
			DECLARE @FacturaID			UNIQUEIDENTIFIER, 
				@FechaHoy				DATETIME,
				@PrecioFinal			float	(8),
				@DiasPreReserva			int = 0,
										
				@EstadoFactura			INT,
				@EstadoPasaje			INT,
				@ExpirationOn			DATETIME

			DECLARE @tempPasajes table(
				PasajeId			UNIQUEIDENTIFIER,
				PasajeroId			UNIQUEIDENTIFIER,
				ButacaId			UNIQUEIDENTIFIER,
				ButacaCodigo		VARCHAR(4),
				ButacaPrecio		FLOAT(53),
				AdicionalesIds		VARCHAR(max),
				HabitacionId		UNIQUEIDENTIFIER,
				PasajeroAdultoId	UNIQUEIDENTIFIER,

				VoucherId			UNIQUEIDENTIFIER,
				Edad				TinyInt,
				ReservaHabId		UNIQUEIDENTIFIER
			)

			IF (@Condicion = 'Cuenta Corriente')
				SET @DiasPreReserva = 7

			SELECT @FacturaID = Newid(), 
				@FechaHoy = Getdate(),
				@EstadoFactura = 2, --prereserva
				@EstadoPasaje  = 5,
				@ExpirationOn = DATEADD(minute, @ExpirationMinutes, @FechaHoy),
				--fix parameters
				@PagoNroRecibo = case when @PagoNroRecibo is null then '' else @PagoNroRecibo end
			
			-- Save pedido de Reserva
			INSERT INTO PedidoReserva(
				[Id]
				,[ViajeId],[VendedorId],[ClienteId]
				,[Observaciones],[Condicion],[MonedaTipo]
				,[DescuentoDetalle],[DescuentoMonto],[DescuentoIsDescuento]
				,[PagoMonto],[PagoMontoRecibidoMonedaTipo],[PagoMontoEquivalente]			
				,[PagoMontoEquivalenteMonedaTipo],[PagoMontoEquivalenteCotizacion],[PagoNroRecibo]
				,[PagoTransaccionId],[PagoTipoPago],[PagoNroFactura]
				,[FacturaId],[CreatedOn],[ExpirationOn]					
			)
			SELECT 
				@ReservaId
				,@ViajeId, @VendedorID, @ClienteID
				,@Observaciones, @Condicion, @MonedaTipo
				,@DescuentoDetalle, @DescuentoMonto, @DescuentoIsDescuento
				,@PagoMonto, @PagoMontoRecibidoMonedaTipo, @PagoMontoEquivalente
				,@PagoMontoEquivalenteMonedaTipo, @PagoMontoEquivalenteCotizacion, @PagoNroRecibo
				,@PagoTransaccionId, @PagoTipoPago, @PagoNroFactura
				,@FacturaID, @FechaHoy, @ExpirationOn
				

			INSERT INTO @tempPasajes (
				PasajeId,
				PasajeroId,
				ButacaId,
				ButacaCodigo,
				ButacaPrecio,	
				AdicionalesIds,
				HabitacionId,
				PasajeroAdultoId,
				VoucherId,
				Edad,
				ReservaHabId
			)
			SELECT 
				p.PasajeId,				
				p.PasajeroId,
				p.ButacaId,
				p.ButacaCodigo,
				p.ButacaPrecio,	
				p.AdicionalesIds,
				p.HabitacionId,
				p.PasajeroAdultoId,
				NEWID(),
				datediff(year,per.FechaNacimiento,@FechaHoy),
				case when p.HabitacionId is not null then NEWID() else null end			
			FROM dbo.PasajeSeleccionado p
			inner join dbo.Persona per on per.PersonaID = p.PasajeroId
			where p.ReservaId = @ReservaId
			 
			if( exists(
				select 1
				from @tempPasajes p
				where
					p.Edad < 3
					and p.ButacaId is null
					and p.PasajeroAdultoId is null
			))
				RAISERROR('Hay menores sin asignar adultos.',16,1)

			
			/***** Insert factura *****/ 
			INSERT INTO dbo.Factura 
				(FacturaID, 
				Monto, 
				Fecha, 
				Estado, 
				ClienteID, 
				VendedorID, 
				Observaciones,
				DiasPreReserva,
				MonedaTipo) 
			VALUES (@FacturaID,
				@PrecioFinal, 
				@FechaHoy, 
				@EstadoFactura,  --2 preserva - ccte
				@ClienteID, 
				@VendedorID, 
				@Observaciones,
				@DiasPreReserva,
				@MonedaTipo)
		
			/*Audit Factura*/	
			CREATE TABLE #TEMP(Result VARCHAR(2048))
			INSERT INTO #TEMP
			EXEC usp_Factura_Audit @FacturaID, @ClienteID, @VendedorID, 'Insert', 'Nueva Reserva'
			DROP TABLE #TEMP
				  
			

			/*** [usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher] ***/
			update p
			set 
				p.FacturaID = @FacturaID,
				p.PasajeroID = pas.PasajeroId,
				p.EstadoPasaje = @EstadoPasaje, --@EstadoFactura INT,  //=> le pasa 5 desde el codigo
				p.VoucherID =  case when @EstadoPasaje = 4 then pas.VoucherId else null end
			FROM @tempPasajes pas
			inner join dbo.Pasaje p on p.PasajeID = pas.PasajeId
			where pas.PasajeId is not null
						
			INSERT INTO dbo.PasajeAdicional 
					(PasajeAdicionalID,
					PasajeID, 
					AdicionalID,
					PasajeroId
					) 
			SELECT 
				Newid() AS PasajeAdicionalID, 
				pas.PasajeId,
				tAdicionales.AdicionalId,
				tAdicionales.PasajeroId
			from @tempPasajes pas
			cross apply(
				select 
					a.Item as AdicionalId,
					pas.PasajeroId  
				from dbo.Split(pas.AdicionalesIds, ',') a
				where len(pas.AdicionalesIds) > 0

				union 

				select 
					tAdicional_1.AdicionalId,
					auxPas.PasajeroId
				from @tempPasajes auxPas
				cross apply (
					select a_1.Item as AdicionalId
					from dbo.Split(auxPas.AdicionalesIds, ',') a_1
					where len(auxPas.AdicionalesIds) > 0
				) tAdicional_1
				where auxPas.PasajeroAdultoId = pas.PasajeroId

			 ) tAdicionales
			 where
				pas.PasajeId is not null

			IF ( @EstadoPasaje = 4 ) 
			BEGIN 
				INSERT INTO dbo.Voucher 
					(VoucherID, 
					FechaEmision, 
					VendedorID) 
				SELECT p.VoucherId,
					@FechaHoy,
					@VendedorID
				FROM @tempPasajes p
				where p.PasajeId is not null
			END

			/************************************/
			/*** [usp_MAT_Reserva_DetalleFactura] ***/

			--Butacas
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
			select @FacturaID, 
					'Butaca ' + ISNULL(b.CodigoButaca,''),
					pas.ButacaPrecio,
					1
			from @tempPasajes pas			
			inner join dbo.Butaca b on b.ButacaID = pas.ButacaId

			--Habitaciones
			;with tHabitaciones as (
				select t.HabitacionId
				from @tempPasajes t
				where t.PasajeId is not null
				group by t.HabitacionId
			)
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
			select @FacturaID,
				   'Habitacion ' + ISNULL(h.Descripcion,''),
				   h.Precio,
				   1
			from tHabitaciones hab
			inner join dbo.Habitacion h on h.HabitacionID = hab.HabitacionId
			
			--Adicionales				
			;with tAdicionales as (
				select ax.Item as AdicionalId, 
					count(*) as Cantidad
				from @tempPasajes pas
				cross apply(
					select a.Item
					from dbo.Split(pas.AdicionalesIds, ',') a
					where len(pas.AdicionalesIds) > 0

					union

					select tAdicional1.AdicionalId
					from @tempPasajes auxPas
					cross apply (
						select a1.Item as AdicionalId
						from dbo.Split(auxPas.AdicionalesIds, ',') a1
						where len(auxPas.AdicionalesIds) > 0
					) tAdicional1
					where auxPas.PasajeroAdultoId = pas.PasajeroId

				) ax
				where pas.PasajeId is not null
				group by ax.item
			)
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad,AdicionalID)
			select @FacturaID,
				a.Descripcion, 
				a.Monto,
				adi.Cantidad,
				a.AdicionalID
			from dbo.Adicional a
			inner join tAdicionales adi on adi.AdicionalId = a.AdicionalID

			
			/***************************************/
			/***** usp_MAT_DetalleFactura_AgregarDescuentoRecargo  *****/
			if(@DescuentoIsDescuento = 1)
			BEGIN
				insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
				values (@FacturaID,@DescuentoDetalle,-@DescuentoMonto,1)
			END

			/*****************************************/

			/*****  usp_MAT_ReservaHabitacion_NuevaReserva  ********/
			
			declare @tempOutput table (
				ActionType varchar(50),
				Id int,
				HabitacionId uniqueidentifier
			)

			declare @tHabitaciones table(
				Id				int identity(1,1),
				Desde			date, 
				Hasta			date,
				HoraIngreso		varchar(10),
				HoraSalida		varchar(10),
				HotelId			uniqueidentifier,
				HabitacionId	uniqueidentifier,
				PasajeroId		uniqueidentifier,
				PasajeId		uniqueidentifier,
				HabIdReservada  uniqueidentifier
			)

			SET DATEFORMAT DMY; 
			-- El formato en ViajeHotel es dd/mm/aaaa, el sistema requiere aaaa-mm-dd

			insert into @tHabitaciones
			(
				Desde			,
				Hasta			,
				HoraIngreso		,
				HoraSalida		,
				HotelId			,
				HabitacionId    ,
				PasajeroId		,
				PasajeId		,
				HabIdReservada	
			)
			select 
				convert(date,vh.Desde), 
				convert(date,vh.Hasta),
				vh.HoraIngreso, 
				vh.HoraSalida,
				vh.HotelID,
				p.HabitacionId,
				p.PasajeroId,
				p.PasajeId,
				p.ReservaHabId
			from  dbo.ViajeHotel vh
			inner join TransHotelHabitacionViaje t on t.ViajeID = vh.ViajeID and t.HotelID = vh.HotelID
			inner join @tempPasajes p on p.HabitacionId = t.HabitacionID
			where 
				vh.ViajeID = @ViajeId
				and p.PasajeId is not null
			
			merge dbo.ReservaHabitacion as t
			using @tHabitaciones as s
				on s.PasajeroId = t.PasajeroID
				and s.PasajeId = t.PasajeID
				and s.Desde = t.Desde
				and t.ViajeID = @ViajeId
			when not matched then
				insert ( reservahabitacionid, habitacionid, pasajeid, 
						fechareserva, desde, hasta, expiro, horaingreso, 
						horasalida, pasajeroid, viajeid)
				values (s.HabIdReservada, s.HabitacionId, s.PasajeId,
						@FechaHoy, s.Desde, s.Hasta, 1, s.HoraIngreso,
						s.HoraSalida, s.PasajeroId, @ViajeId)
			OUTPUT $action, s.Id, s.HabitacionId
			into @tempOutput(ActionType,Id, HabitacionId);
			
			--actualizar estado habitacion
			update h
			set estado = 1
			from dbo.Habitacion h
			inner join @tempOutput t on t.HabitacionId = h.HabitacionID
			where t.ActionType = 'INSERT'

			--Audit
			INSERT INTO AuditReservaHabitacion (ReservaHabitacionId, HabitacionId, PasajeId, ViajeId, UserId, [Action] ) 
			select 
				h.HabIdReservada,
				h.HabitacionId,
				h.PasajeId,
				@ViajeId,
				@VendedorID,
				'INSERT'
			from @tHabitaciones h
			inner join @tempOutput t on t.Id = h.Id
			where t.ActionType = 'INSERT'
			
			--actualizar estado pasaje
			update p
				set EstadoPasaje = case EstadoPasaje
										when 4 then 6
										when 3 then 8
										when 5 then 9
										when 6 then 6
										when 8 then 8
										when 9 then 9
								   end
			from dbo.Pasaje p
			inner join @tempPasajes t on t.PasajeId = p.PasajeID
	
			/*****************************************/
						
			/*** usp_MAT_Reserva_VincularMenorByViajeID ***/
 
			declare @tMenores table(
				MenorId			UNIQUEIDENTIFIER,
				AdultoId		UNIQUEIDENTIFIER,
				AdultoPasajeId	UNIQUEIDENTIFIER
			)

			insert into @tMenores(MenorId, AdultoId, AdultoPasajeId)
			select p.PasajeroId, pAdulto.PasajeroId, pAdulto.PasajeId
			from @tempPasajes p
			inner join @tempPasajes pAdulto on pAdulto.PasajeroId = p.PasajeroAdultoId
			where p.Edad < 3 --bebes
				and p.ButacaId is null --sin butaca
				and p.PasajeroAdultoId is not null -- con adulto asignado

			INSERT INTO dbo.pasajeromenor 
                ( 
					pasajeid, 
					pasajeroid, 
					menorid
				) 
			Select
				t.AdultoPasajeId,
				t.AdultoId,
				t.MenorId
			from @tMenores t
			left join dbo.PasajeroMenor pm on 
				pm.pasajeid = t.AdultoPasajeId
				and pm.pasajeroid = t.AdultoId
				and pm.menorid = t.MenorId
			where pm.id is null
			
			/*******************************************/

			/*****  usp_MAT_RegistroPago_NuevoPago ********/
			-- solo se registra pago, si es mayor que cero
			create table #tPago (
				FacturaId UNIQUEIDENTIFIER,
				PagoId UNIQUEIDENTIFIER,
				Saldo MONEY,
				TotalFactura MONEY)
			insert into #tPago
			exec usp_MAT_Reserva_RegistrarPago @ReservaId, @PagoMonto

			/*******************************************/

			SELECT 
				@ReservaId as ReservaId,
				@ExpirationOn as ExpirationOn

			--SELECT 
			--	'Done.' AS Result,
			--	@ReservaId as ReservaId,
			--	f.FacturaID as FacturaId,
			--	f.Estado as FacturaEstadoId,
			--	ef.Descripcion as FacturaEstado,
			--	@ExpirationOn as ExpirationOn
			--	p.Saldo,
			--	p.TotalFactura
			--FROM dbo.Factura f 
			--INNER JOIN dbo.EstadoFactura ef on f.Estado = ef.ID
			--INNER JOIN #tPago p on p.FacturaId = f.FacturaID
			--WHERE f.FacturaID = @FacturaID

			/*
			-- Select de pruebas
			select  'PedidoReserva', *
			from PedidoReserva

			select '@tempPasajes', * 
			from @tempPasajes t

			select 'Pasaje', p.* 
			from @tempPasajes t
			inner join dbo.Pasaje p on p.PasajeID = t.PasajeId

			select 'PasajeAdicional', *
			from @tempPasajes t
			inner join dbo.PasajeAdicional pa on pa.PasajeID = t.PasajeId

			select 'Voucher', *
			from Voucher v
			where v.FechaEmision = @FechaHoy

			select 'Factura', *
			from dbo.Factura f
			where f.FacturaID = @FacturaID

			select 'DetalleFactura', *
			from dbo.DetalleFactura d
			where d.FacturaID = @FacturaID

			select 'movimientocuenta', * 
			from movimientocuenta m
			where m.FacturaID = @FacturaID

			select 'pago', * 
			FROM   dbo.MovimientoCuenta mc 
			inner join dbo.Pago p
				on mc.PagoID = p.PagoID
			WHERE  mc.facturaid = @FacturaID
	
			select 'AuditReservaHabitacion', * 
			from AuditReservaHabitacion a
			inner join @tHabitaciones h on h.HabIdReservada = a.ReservaHabitacionId
		
			*/


			/******************************************************************/
			 
		COMMIT TRAN; 
		--ROLLBACK TRAN;
	END TRY
	BEGIN CATCH
		
		-- Deshacemos la transación
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN;

		DECLARE @errmsg   AS NVARCHAR (2048)
		SELECT @errmsg = 'Error in usp_MAT_Reserva_Reservar. Message:' + Error_message() 
			+ ' Error Line:' + STR(ERROR_LINE())
			+ ' ReservaId: ' + convert(varchar(50),@ReservaId)

		RAISERROR(@errmsg,16,1)
		--select 
		--	'Error'		AS Result,
		--	null		as ReservaId,
		--	null		as FacturaId,
		--	null		as FacturaEstadoId,
		--	@errmsg		as FacturaEstado,
		--	null		as ExpirationOn,
		--	null		as Saldo,
		--	null		as TotalFactura

	END CATCH
END