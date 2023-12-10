
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the PaqueteServicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaqueteServicio_Get_List

AS


				
				SELECT
					[PaqueteServicioID],
					[ServicioID],
					[PaqueteID]
				FROM
					[dbo].[PaqueteServicio]
					
				SELECT @@ROWCOUNT
			

