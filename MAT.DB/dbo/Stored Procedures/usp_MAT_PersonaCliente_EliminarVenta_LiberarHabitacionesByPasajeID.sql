CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_EliminarVenta_LiberarHabitacionesByPasajeID]( @PasajeID	VARCHAR(36))
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 01/28/2018
  -- Description:  release rooms
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		BEGIN TRY 
			BEGIN TRAN
		

			DELETE rh 
			FROM   ReservaHabitacion rh 
				   INNER JOIN Pasaje p 
						   ON rh.PasajeroID = p.PasajeroID 
			WHERE  p.PasajeID = @PasajeID

			
			---update estado pasaje
			update p
			set p.EstadoPasaje = case p.EstadoPasaje when 6 then 4
													 when 8 then 3
													 when 9 then 5
													 else p.EstadoPasaje end
								
			from dbo.Pasaje p
			WHERE  p.PasajeID = @PasajeID
			

			COMMIT TRAN; 
			
			

		END TRY

		BEGIN CATCH
			IF @@TRANCOUNT > 0 
			begin
				declare @sError varchar(1000);
				select @sError = Error_message();
				throw 51000, @sError , 1;
				ROLLBACK TRAN;
			end

			
		END CATCH

		     

  END