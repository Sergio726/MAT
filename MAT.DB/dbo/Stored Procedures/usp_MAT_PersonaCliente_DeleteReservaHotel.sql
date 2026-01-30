
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
  --2019-04-05 GARCIA SERGIO: ADD AUDIT
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN try 
			BEGIN TRAN 

			DECLARE @PasajeID UNIQUEIDENTIFIER,
					@ExistsRoom BIT,
					@ReservaHabitacionID uniqueidentifier
			
			DECLARE @DeleteIds table (ReservaHabitacionID uniqueidentifier) 

			SELECT @PasajeID = p.pasajeid 
			FROM   dbo.pasaje p 
			WHERE  pasajeroid = @PasajeroID 
				   AND viajeid = @ViajeID 

			DELETE dbo.ReservaHabitacion 
			OUTPUT deleted.ReservaHabitacionID INTO @DeleteIds
			WHERE  pasajeroid = @PasajeroID 
				   AND habitacionid = @HabitacionID 
				   AND viajeid = @ViajeID 
				  -- AND pasajeid = @PasajeID 
			
			--Audit
			SELECT @ReservaHabitacionID = ReservaHabitacionID FROM @DeleteIds
			exec usp_AuditReservaHabitacion_Insert @ReservaHabitacionID,@HabitacionID,@PasajeID,@ViajeID,@UserID,'DELETE';
			
			IF NOT EXISTS(SELECT ReservaHabitacionID 
					  FROM ReservaHabitacion 
					  WHERE PasajeroID = @PasajeroID
							AND viajeid = @ViajeID 
					  )
			BEGIN
				
				UPDATE dbo.Pasaje 
				SET    estadopasaje = CASE estadopasaje 
										WHEN 6  THEN 4 
										WHEN 8 THEN 3 
										WHEN 9 THEN 5
										else Pasaje.EstadoPasaje 
									  END 
				WHERE  pasajeid = @PasajeID 

			END
			
			--SELECT * FROM ESTADOPASAJE

			SELECT 1       AS ID, 
				   'Done.' AS Result 

			COMMIT TRAN; 
		END try 

		BEGIN catch 
			IF @@TRANCOUNT > 0 
			  ROLLBACK TRAN 

			DECLARE @errmsg AS NVARCHAR (2048) 

			SELECT @errmsg = Error_message() 

			SELECT -1      AS ID, 
				   @errmsg AS Result 
		END catch 

		     

  END