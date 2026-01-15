
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Localidad table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureLocalidad_GetById
(

	@Id int   
)
AS


				SELECT
					[ID],
					[idDepartamento],
					[Nombre]
				FROM
					[dbo].[Localidad]
				WHERE
					[ID] = @Id
				SELECT @@ROWCOUNT
					
			

