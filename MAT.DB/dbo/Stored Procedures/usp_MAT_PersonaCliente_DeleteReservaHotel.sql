
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
  -- 2026-02-15: Permite eliminar por PasajeroID+HabitacionID+ViajeID sin requerir PasajeID en Pasaje
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		BEGIN try 
			BEGIN TRAN 

			DECLARE @PasajeID UNIQUEIDENTIFIER = NULL,
					@ReservaHabitacionID uniqueidentifier = NULL,
					@DeleteCount INT = 0

			DECLARE @DeleteIds table (ReservaHabitacionID uniqueidentifier, PasajeID uniqueidentifier) 

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

			-- Eliminar reserva y capturar ReservaHabitacionID y PasajeID de la fila eliminada
			DELETE dbo.ReservaHabitacion 
			OUTPUT deleted.ReservaHabitacionID, deleted.PasajeID INTO @DeleteIds
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
			
			SELECT @ReservaHabitacionID = ReservaHabitacionID, @PasajeID = PasajeID FROM @DeleteIds

			-- Audit: solo si tenemos PasajeID (AuditReservaHabitacion requiere PasajeId NOT NULL)
			IF @ReservaHabitacionID IS NOT NULL AND @PasajeID IS NOT NULL
			BEGIN
				BEGIN TRY
					EXEC usp_AuditReservaHabitacion_Insert @ReservaHabitacionID, @HabitacionID, @PasajeID, @ViajeID, @UserID, 'DELETE';
				END TRY
				BEGIN CATCH
					PRINT 'WARN: Auditoría falló - ' + ERROR_MESSAGE()
				END CATCH
			END
			
			-- Actualizar estado del pasaje solo si existe PasajeID y ya no tiene habitaciones asignadas
			IF @PasajeID IS NOT NULL 
			   AND NOT EXISTS(SELECT 1 
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