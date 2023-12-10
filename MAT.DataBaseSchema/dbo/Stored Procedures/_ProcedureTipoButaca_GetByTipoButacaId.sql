
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the TipoButaca table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoButaca_GetByTipoButacaId
(

	@TipoButacaId int   
)
AS


				SELECT
					[TipoButacaID],
					[Descripcion]
				FROM
					[dbo].[TipoButaca]
				WHERE
					[TipoButacaID] = @TipoButacaId
				SELECT @@ROWCOUNT
					
			

