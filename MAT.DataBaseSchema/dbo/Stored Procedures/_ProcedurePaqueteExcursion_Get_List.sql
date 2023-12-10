
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PaqueteExcursion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteExcursion_Get_List

AS


				
				SELECT
					[PaqueteExcursionID],
					[ExcursionID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteExcursion]
					
				SELECT @@ROWCOUNT