
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the PasajeAdicional table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajeAdicional_GetByAdicionalId
(

	@AdicionalId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
				WHERE
					[AdicionalID] = @AdicionalId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON