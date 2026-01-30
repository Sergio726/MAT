
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Pasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasaje_Delete]
(

	@PasajeId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pasaje] WITH (ROWLOCK) 
				WHERE
					[PasajeID] = @PasajeId
					
			



