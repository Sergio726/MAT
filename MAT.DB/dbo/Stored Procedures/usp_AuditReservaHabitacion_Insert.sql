CREATE PROCEDURE [dbo].[usp_AuditReservaHabitacion_Insert]( @ReservaHabitacionId uniqueidentifier = null,
															@HabitacionId uniqueidentifier,
															@PasajeId uniqueidentifier,
															@ViajeId uniqueidentifier,
															@UserId uniqueidentifier,
															@Action varchar(50)
															)
  AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2019-04-04
  -- Description:  Audit table AuditReservaHabitacion
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
		BEGIN TRAN

		INSERT INTO AuditReservaHabitacion (ReservaHabitacionId, HabitacionId, PasajeId, ViajeId, UserId, [Action] ) 
		VALUES     (@ReservaHabitacionId, @HabitacionId, @PasajeId, @ViajeId, @UserId, @Action ) 

		COMMIT TRAN; 
		

		END TRY

		BEGIN CATCH
		IF @@TRANCOUNT > 0 
		ROLLBACK TRAN
		

		DECLARE @errmsg   AS NVARCHAR (2048), 
                @errState INT 

        SELECT @errmsg = Error_message() + Error_line(), 
                @errState = Error_state() 

			RAISERROR (N'Error al guardar auditoria de ReservaHabitacion. MSG: %s',16,@errState,@errmsg);
		END CATCH
		
  END
