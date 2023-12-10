
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersona_Delete
(

	@PersonaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Persona] WITH (ROWLOCK) 
				WHERE
					[PersonaID] = @PersonaId
					
			

