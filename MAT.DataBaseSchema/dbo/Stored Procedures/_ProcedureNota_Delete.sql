
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Nota table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_Delete
(

	@NotaId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Nota] WITH (ROWLOCK) 
				WHERE
					[NotaID] = @NotaId