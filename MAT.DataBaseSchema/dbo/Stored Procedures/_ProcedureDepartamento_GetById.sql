
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Departamento table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDepartamento_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[idProvincia],
					[Nombre]
				FROM
					[dbo].[Departamento]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

