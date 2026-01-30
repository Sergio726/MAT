
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePais_Delete]
(

	@PaisId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pais] WITH (ROWLOCK) 
				WHERE
					[PaisID] = @PaisId
					
			



