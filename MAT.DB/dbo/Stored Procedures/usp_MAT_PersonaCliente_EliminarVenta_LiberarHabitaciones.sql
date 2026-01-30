CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitaciones]( @FacturaID	VARCHAR(36))
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 15/05/2017
  -- Description:  release rooms

  --03/20/2020 Garcia Sergio: delete ReservaHabitacion according to PasajeID
  --21/11/2024 Ruben Tejerina: update status of hotel, set Status = 0 (Libre)
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
		
			UPDATE h
			SET
				h.Estado = 0
			FROM Pasaje p
			INNER JOIN ReservaHabitacion r on r.PasajeID = p.PasajeID
			INNER JOIN Habitacion h on h.HabitacionID = r.HabitacionID
			where
				p.FacturaID = @FacturaID

			DELETE rh 
			FROM   ReservaHabitacion rh 
				   INNER JOIN Pasaje p 
						   ON rh.PasajeID = p.PasajeID 
			WHERE  p.FacturaID = @FacturaID 

			if (@@rowcount > 0)
				SELECT 1 AS ID,'Done.' AS Result
			
			COMMIT TRAN; 
			
			

		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			ROLLBACK TRAN


			DECLARE @errmsg   AS NVARCHAR (2048)
			SELECT @errmsg = Error_message()

			SELECT -1 AS ID,@errmsg AS Result
			
		END CATCH
  END
