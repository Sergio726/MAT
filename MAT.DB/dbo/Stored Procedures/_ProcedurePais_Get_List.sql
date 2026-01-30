
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Pais table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePais_Get_List]

AS


				
				SELECT
					[PaisID],
					[Descripcion]
				FROM
					[dbo].[Pais]
					
				SELECT @@ROWCOUNT
			



