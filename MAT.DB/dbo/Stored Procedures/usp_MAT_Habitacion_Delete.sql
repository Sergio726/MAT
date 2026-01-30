CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_Delete](@HabitacionID uniqueidentifier)

AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/23/2017
  -- Description:  DELETE habitacion
  --History

  -- ============================================= 

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

        BEGIN TRY 
           
				IF NOT EXISTS (SELECT HabitacionID FROM dbo.ReservaHabitacion WHERE HabitacionID = @HabitacionID)
				BEGIN

				   BEGIN TRAN
					DELETE dbo.Habitacion WHERE HabitacionID = @HabitacionID		
				   COMMIT TRAN; 

				END
				ELSE
					RAISERROR (N'La habitacion seleccionada esta en uso, no puede ser eliminada.',11,1);

        END TRY
        BEGIN CATCH

            IF @@TRANCOUNT > 0 
				ROLLBACK TRAN

            DECLARE @errmsg   AS NVARCHAR (2048),
					@errorProc VARCHAR(50),
					@ErrorSeverity INT,
				    @ErrorState INT

		    SELECT 
			    @errmsg = CONCAT('*** ', Error_message()),
				@ErrorSeverity = ERROR_SEVERITY(),
				@ErrorState = ERROR_STATE();
           
			 RAISERROR (@errmsg, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );

        END CATCH
  END 



