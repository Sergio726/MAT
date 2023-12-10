
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Excursion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureExcursion_GetByExcursionId
(

	@ExcursionId uniqueidentifier   
)
AS


				SELECT
					[ExcursionID],
					[Descripcion],
					[Costo],
					[Observaciones],
					[ProveedorID]
				FROM
					[dbo].[Excursion]
				WHERE
					[ExcursionID] = @ExcursionId
				SELECT @@ROWCOUNT