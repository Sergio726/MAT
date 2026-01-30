CREATE PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_NuevaReserva](@HabitacionID	uniqueidentifier,
															    @PasajeID	uniqueidentifier,
																@PasajeroID	uniqueidentifier,
															    @Desde datetime = null,
																@Hasta datetime = null,
																@HoraIngreso varchar(10) = null,
																@HoraSalida varchar(10) = null,
																@Expiro bit,
																@ViajeID uniqueidentifier,
																@UserID uniqueidentifier
																)
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 09/04/2017
  -- Description:  New room reservation

  2017-04-18	Garcia Sergio:	update state pasaje
  2017-04-19	Garcia Sergio: set estate pasajes
  2018-03-13	Garcia Sergio: validate that not exists ReservaHabitacion by @PasajeID 
  2019-04-04	Garcia Sergio: register audit and add @UserID parameter
  2019-06-21	Garcia Sergio: add contiction @ViajeID 
  2019-07-22	Garcia Sergio: add condition @Desde
  -- ============================================= */
  SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
  BEGIN 
      
		BEGIN TRY 
			BEGIN TRAN
			DECLARE @ReservaHabitacionID UNIQUEIDENTIFIER, 
						@HabOcupacion        INT, 
						@HabCapacidad        INT, 
						@HabEstado           INT, 
						@EstadoPasaje        INT,
						@HotelID			 UNIQUEIDENTIFIER

				
				SELECT @HotelID = HotelID FROM dbo.Habitacion where HabitacionID = @HabitacionID
				
			if not exists (select * 
						   from dbo.ReservaHabitacion rh 
						   inner join dbo.Habitacion hab
							on rh.HabitacionID = hab.HabitacionID
						   where rh.PasajeID = @PasajeID
							AND rh.PasajeroID = @PasajeroID
							AND rh.ViajeID = @ViajeID
							AND rh.Desde = @Desde)
			begin
			/*insert ReservaHabitacion*/
				
				SELECT @ReservaHabitacionID = Newid() 
				INSERT INTO dbo.ReservaHabitacion 
							(reservahabitacionid, 
							 habitacionid, 
							 pasajeid, 
							 fechareserva, 
							 desde, 
							 hasta, 
							 expiro, 
							 horaingreso, 
							 horasalida, 
							 pasajeroid, 
							 viajeid) 
				VALUES      (@ReservaHabitacionID, 
							 @HabitacionID, 
							 @PasajeID, 
							 Getdate(), 
							 @Desde, 
							 @Hasta, 
							 @Expiro, 
							 @HoraIngreso, 
							 @HoraSalida, 
							 @PasajeroID, 
							 @ViajeID ) 

				/*ValidarHabitacion*/
				SELECT @HabOcupacion = Isnull(h.ocupacion, 0), 
					   @HabCapacidad = Isnull(h.capacidad, 0), 
					   @HabEstado = 0 
				FROM   habitacion h 
				WHERE  h.habitacionid = @HabitacionID 

				IF ( @HabOcupacion = @HabCapacidad ) 
				  BEGIN 
					  SELECT @HabEstado = 1 
				  END 
				ELSE 
				  BEGIN 
					  IF ( @HabOcupacion < @HabCapacidad ) 
						SELECT @HabEstado = 0 
				  END 

				UPDATE habitacion 
				SET    estado = @HabEstado 
				WHERE  habitacionid = @HabitacionID 

				/*Actualizo el estado del Pasaje*/ 
				SELECT @EstadoPasaje = p.estadopasaje 
				FROM   pasaje p 
				WHERE  pasajeid = @PasajeID 

				/*
					1	Disponible
					2	Reservado
					3	Señado
					4	Pagado
					5	Pre-reserva
					6	Reserva-Hotel
					7	Anulado
					8	Pasaje y Hotel prereservados
					9	Pasaje pre-reserva con hotel
				*/
				update dbo.Pasaje
				set EstadoPasaje = case @EstadoPasaje
										when 4 then 6
										when 3 then 8
										when 5 then 9
										when 6 then 6
										when 8 then 8
										when 9 then 9
										end
				where PasajeID = @PasajeID

				SELECT @ReservaHabitacionID AS ReservaHabitacionID, 
					   'Done.'            AS Result 
				
				--Audit
				exec usp_AuditReservaHabitacion_Insert @ReservaHabitacionID,@HabitacionID,@PasajeID,@ViajeID,@UserID,'INSERT';

				COMMIT TRAN; 
			end
			else
			begin
				/*si ya existe la reserva, solo actualiza el estado del pasaje*/
				SELECT @EstadoPasaje = p.estadopasaje 
				FROM   pasaje p 
				WHERE  pasajeid = @PasajeID 

				/*
					1	Disponible
					2	Reservado
					3	Señado
					4	Pagado
					5	Pre-reserva
					6	Reserva-Hotel
					7	Anulado
					8	Pasaje y Hotel prereservados
					9	Pasaje pre-reserva con hotel
				*/
				update dbo.Pasaje
				set EstadoPasaje = case @EstadoPasaje
										when 4 then 6
										when 3 then 8
										when 5 then 9
										when 6 then 6
										when 8 then 8
										when 9 then 9
										end
				where PasajeID = @PasajeID


				SELECT '' AS ReservaHabitacionID, 
					   'Done.'            AS Result 
				COMMIT TRAN; 
			end
		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			select '' as ReservaHabitacionID, @errmsg AS Result
			
		END CATCH




     

  END