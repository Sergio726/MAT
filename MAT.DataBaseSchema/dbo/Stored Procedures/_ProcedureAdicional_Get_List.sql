
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Adicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureAdicional_Get_List

AS


				
				SELECT
					[AdicionalID],
					[Monto],
					[Descripcion]
				FROM
					[dbo].[Adicional]
					
				SELECT @@ROWCOUNT