
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PasajeAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajeAdicional_Get_List]

AS


				
				SELECT
					[PasajeAdicionalID],
					[PasajeID],
					[AdicionalID]
				FROM
					[dbo].[PasajeAdicional]
					
				SELECT @@ROWCOUNT
			



