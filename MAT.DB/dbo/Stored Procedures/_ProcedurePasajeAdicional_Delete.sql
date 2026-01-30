
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajeAdicional_Delete]
(

	@PasajeAdicionalId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[PasajeAdicional] WITH (ROWLOCK) 
				WHERE
					[PasajeAdicionalID] = @PasajeAdicionalId
					
			



