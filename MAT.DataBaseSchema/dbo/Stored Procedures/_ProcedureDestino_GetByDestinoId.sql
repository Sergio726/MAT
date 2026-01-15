
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Destino table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_GetByDestinoId
(

	@DestinoId uniqueidentifier   
)
AS


				SELECT
					[DestinoID],
					[LocalidadID]
				FROM
					[dbo].[Destino]
				WHERE
					[DestinoID] = @DestinoId
				SELECT @@ROWCOUNT
					
			

