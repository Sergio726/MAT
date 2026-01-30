
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Departamento table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureDepartamento_Insert]
(

	@Id int    OUTPUT,

	@IdProvincia int   ,

	@Nombre nvarchar (250)  
)
AS


				
				INSERT INTO [dbo].[Departamento]
					(
					[idProvincia]
					,[Nombre]
					)
				VALUES
					(
					@IdProvincia
					,@Nombre
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			



