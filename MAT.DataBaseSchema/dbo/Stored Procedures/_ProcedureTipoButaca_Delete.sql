
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the TipoButaca table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_Delete
(

	@TipoButacaId int   
)
AS


				DELETE FROM [dbo].[TipoButaca] WITH (ROWLOCK) 
				WHERE
					[TipoButacaID] = @TipoButacaId
					
			

