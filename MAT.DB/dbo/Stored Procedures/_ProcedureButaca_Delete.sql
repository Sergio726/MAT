
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Butaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureButaca_Delete]
(

	@ButacaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Butaca] WITH (ROWLOCK) 
				WHERE
					[ButacaID] = @ButacaId
					
			



