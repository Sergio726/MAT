
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureLocalidad_Get_List

AS


				
				SELECT
					[ID],
					[idDepartamento],
					[Nombre]
				FROM
					[dbo].[Localidad]
					
				SELECT @@ROWCOUNT
			

