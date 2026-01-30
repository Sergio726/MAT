
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PasajeAdicional table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajeAdicional_GetByPasajeAdicionalId]
(

	@PasajeAdicionalId uniqueidentifier   
)
AS


				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
				WHERE
					[PasajeAdicionalID] = @PasajeAdicionalId
				SELECT @@ROWCOUNT
					
			



