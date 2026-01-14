
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the TipoPasaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPasaje_Delete
(

	@TipoId int   
)
AS


				DELETE FROM [dbo].[TipoPasaje] WITH (ROWLOCK) 
				WHERE
					[TipoID] = @TipoId
					
			

