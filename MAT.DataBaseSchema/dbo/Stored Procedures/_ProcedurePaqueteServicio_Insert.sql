
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteServicio_Insert
(

	@PaqueteServicioId uniqueidentifier    OUTPUT,

	@ServicioId uniqueidentifier   ,

	@PaqueteId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[PaqueteServicio]
					(
					[PaqueteServicioID]
					,[ServicioID]
					,[PaqueteID]
					)
				VALUES
					(
					@PaqueteServicioId
					,@ServicioId
					,@PaqueteId
					)
				
									
							
			

