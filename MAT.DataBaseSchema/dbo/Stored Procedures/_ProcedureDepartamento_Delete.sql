
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDepartamento_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Departamento] WITH (ROWLOCK) 
				WHERE
					[ID] = @Id
					
			

