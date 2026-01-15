
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the TipoCliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_Delete
(

	@TipoId int   
)
AS


				DELETE FROM [dbo].[TipoCliente] WITH (ROWLOCK) 
				WHERE
					[TipoID] = @TipoId
					
			

