
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquete_Delete]
(

	@PaqueteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Paquete] WITH (ROWLOCK) 
				WHERE
					[PaqueteID] = @PaqueteId
					
			



