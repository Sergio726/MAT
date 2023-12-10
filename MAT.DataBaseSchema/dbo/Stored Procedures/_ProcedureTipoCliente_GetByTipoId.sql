
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the TipoCliente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_GetByTipoId
(

	@TipoId int   
)
AS


				SELECT
					[TipoID],
					[Descripcion]
				FROM
					[dbo].[TipoCliente]
				WHERE
					[TipoID] = @TipoId
				SELECT @@ROWCOUNT
					
			

