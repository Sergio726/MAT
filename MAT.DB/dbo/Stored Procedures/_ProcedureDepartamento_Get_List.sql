
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureDepartamento_Get_List]

AS


				
				SELECT
					[ID],
					[idProvincia],
					[Nombre]
				FROM
					[dbo].[Departamento]
					
				SELECT @@ROWCOUNT
			



