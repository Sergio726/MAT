
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the TipoPasaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPasaje_GetByTipoId
(

	@TipoId int   
)
AS


				SELECT
					[TipoID],
					[Descripcion]
				FROM
					[dbo].[TipoPasaje]
				WHERE
					[TipoID] = @TipoId
				SELECT @@ROWCOUNT
					
			

