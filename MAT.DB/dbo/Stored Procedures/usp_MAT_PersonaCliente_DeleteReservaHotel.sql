
CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_DeleteReservaHotel](@PasajeroID	uniqueidentifier,
															       @HabitacionID	uniqueidentifier,
																   @ViajeID	uniqueidentifier,
																   @UserID uniqueidentifier
																   )
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 12/04/2017
  -- Description:  Remove registration ReservaHabitacion
  -- 2019-04-05 GARCIA SERGIO: ADD AUDIT
  -- 2026-02-14: Mejora manejo de errores y validaciones
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		BEGIN try 
			BEGIN TRAN 

			DECLARE @PasajeID UNIQUEIDENTIFIER,
					@ExistsRoom BIT,
					@ReservaHabitacionID uniqueidentifier,
					@DeleteCount INT = 0

			DECLARE @DeleteIds table (ReservaHabitacionID uniqueidentifier) 

			-- Validar que exista el pasajero en el viaje
			SELECT @PasajeID = p.PasajeId 
			FROM   dbo.Pasaje p 
			WHERE  p.PasajeroId = @PasajeroID 
				   AND p.ViajeId = @ViajeID 

			IF @PasajeID IS NULL
			BEGIN
				SELECT -1 AS ID, 'No se encontró el pasaje para el pasajero en este viaje.' AS Result
				ROLLBACK TRAN
				RETURN
			END

			-- Validar que exista la reserva de habitación
			IF NOT EXISTS(SELECT 1 FROM dbo.ReservaHabitacion 
						  WHERE PasajeroID = @PasajeroID 
								AND HabitacionID = @HabitacionID 
								AND ViajeID = @ViajeID)
			BEGIN
				SELECT -1 AS ID, 'No se encontró la reserva de habitación para este pasajero.' AS Result
				ROLLBACK TRAN
				RETURN
			END

			-- Eliminar reserva
			DELETE dbo.ReservaHabitacion 
			OUTPUT deleted.ReservaHabitacionID INTO @DeleteIds
			WHERE  PasajeroID = @PasajeroID 
				   AND HabitacionID = @HabitacionID 
				   AND ViajeID = @ViajeID 

			SET @DeleteCount = @@ROWCOUNT

			IF @DeleteCount = 0
			BEGIN
				SELECT -1 AS ID, 'No se pudo eliminar la reserva de habitación.' AS Result
				ROLLBACK TRAN
				RETURN
			END
			
			-- Audit (no crítico: si falla, loguear pero no abortar la operación)
			SELECT @ReservaHabitacionID = ReservaHabitacionID FROM @DeleteIds

			BEGIN TRY
				IF @ReservaHabitacionID IS NOT NULL
				BEGIN
					EXEC usp_AuditReservaHabitacion_Insert @ReservaHabitacionID, @HabitacionID, @PasajeID, @ViajeID, @UserID, 'DELETE';
				END
			END TRY
			BEGIN CATCH
				-- Auditoría falló, pero la operación principal continúa
				PRINT 'WARN: Auditoría falló - ' + ERROR_MESSAGE()
			END CATCH
			
			-- Actualizar estado del pasaje si ya no tiene habitaciones asignadas
			IF NOT EXISTS(SELECT 1 
					  FROM ReservaHabitacion 
					  WHERE PasajeroID = @PasajeroID
							AND ViajeID = @ViajeID 
					  )
			BEGIN
				UPDATE dbo.Pasaje 
				SET    EstadoPasaje = CASE EstadoPasaje 
										WHEN 6  THEN 4 
										WHEN 8 THEN 3 
										WHEN 9 THEN 5
										ELSE Pasaje.EstadoPasaje 
									  END 
				WHERE  PasajeId = @PasajeID 
			END

			SELECT 1       AS ID, 
				   'Done.' AS Result 

			COMMIT TRAN; 
		END try 

		BEGIN catch 
			IF @@TRANCOUNT > 0 
			  ROLLBACK TRAN 

			DECLARE @errmsg AS NVARCHAR(2048)
			SET @errmsg = ERROR_MESSAGE()

			SELECT -1      AS ID, 
				   @errmsg AS Result 
		END catch 

  END