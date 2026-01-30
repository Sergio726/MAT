
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Provincia table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProvincia_Get_List]

AS


				
				SELECT
					[ID],
					[Nombre]
				FROM
					[dbo].[Provincia]
					
				SELECT @@ROWCOUNT
			



