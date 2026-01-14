
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Adicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureAdicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SELECT
					[AdicionalID],
					[Monto],
					[Descripcion]
				FROM
					[dbo].[Adicional]
				WHERE
					[AdicionalID] = @AdicionalId
				SELECT @@ROWCOUNT