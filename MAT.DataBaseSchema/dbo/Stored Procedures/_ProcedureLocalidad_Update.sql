
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureLocalidad_Update
(

	@Id int   ,

	@IdDepartamento int   ,

	@Nombre nvarchar (250)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Localidad]
				SET
					[idDepartamento] = @IdDepartamento
					,[Nombre] = @Nombre
				WHERE
[ID] = @Id 
				
			

