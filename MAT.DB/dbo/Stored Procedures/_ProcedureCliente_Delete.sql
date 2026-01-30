
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureCliente_Delete]
(

	@ClienteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Cliente] WITH (ROWLOCK) 
				WHERE
					[ClienteID] = @ClienteId
					
			



