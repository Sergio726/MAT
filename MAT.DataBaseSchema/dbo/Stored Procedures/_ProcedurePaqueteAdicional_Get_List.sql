
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PaqueteAdicional table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteAdicional_Get_List

AS


				
				SELECT
					[PaqueteAdicionalID],
					[PaqueteID],
					[AdicionalID]
				FROM
					[dbo].[PaqueteAdicional]
					
				SELECT @@ROWCOUNT