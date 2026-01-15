
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDepartamento_Update
(

	@Id int   ,

	@IdProvincia int   ,

	@Nombre nvarchar (250)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Departamento]
				SET
					[idProvincia] = @IdProvincia
					,[Nombre] = @Nombre
				WHERE
[ID] = @Id 
				
			

