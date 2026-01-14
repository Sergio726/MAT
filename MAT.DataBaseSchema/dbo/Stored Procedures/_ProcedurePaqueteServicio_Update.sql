
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteServicio_Update
(

	@PaqueteServicioId uniqueidentifier   ,

	@OriginalPaqueteServicioId uniqueidentifier   ,

	@ServicioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[PaqueteServicio]
				SET
					[PaqueteServicioID] = @PaqueteServicioId
					,[ServicioID] = @ServicioId
					,[PaqueteID] = @PaqueteId
				WHERE
[PaqueteServicioID] = @OriginalPaqueteServicioId 
				
			

