
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Pais table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePais_GetByPaisId
(

	@PaisId uniqueidentifier   
)
AS


				SELECT
					[PaisID],
					[Descripcion]
				FROM
					[dbo].[Pais]
				WHERE
					[PaisID] = @PaisId
				SELECT @@ROWCOUNT
					
			

