
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureAdicional_Delete]
(

	@AdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Adicional] WITH (ROWLOCK) 
				WHERE
					[AdicionalID] = @AdicionalId
					
			



