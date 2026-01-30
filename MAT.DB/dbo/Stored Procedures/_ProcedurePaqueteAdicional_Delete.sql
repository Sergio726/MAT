
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaqueteAdicional_Delete]
(

	@PaqueteAdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PaqueteAdicional] WITH (ROWLOCK) 
				WHERE
					[PaqueteAdicionalID] = @PaqueteAdicionalId
					
			



