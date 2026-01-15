
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Localidad table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureLocalidad_Insert
(

	@Id int    OUTPUT,

	@IdDepartamento int   ,

	@Nombre nvarchar (250)  
)
AS


				
				INSERT INTO [dbo].[Localidad]
					(
					[idDepartamento]
					,[Nombre]
					)
				VALUES
					(
					@IdDepartamento
					,@Nombre
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

