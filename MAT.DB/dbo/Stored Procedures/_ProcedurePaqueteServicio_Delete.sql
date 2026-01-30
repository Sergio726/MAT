
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaqueteServicio_Delete]
(

	@PaqueteServicioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteServicio] WITH (ROWLOCK) 
				WHERE
					[PaqueteServicioID] = @PaqueteServicioId
					
			



