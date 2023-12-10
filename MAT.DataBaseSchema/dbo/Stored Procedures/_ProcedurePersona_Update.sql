
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersona_Update
(

	@PersonaId uniqueidentifier   ,

	@OriginalPersonaId uniqueidentifier   ,

	@Apellido varchar (100)  ,

	@Nombre varchar (100)  ,

	@TipoDocumento int   ,

	@NroDocumento varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@FechaNacimiento date   ,

	@LocalidadId int   ,

	@UserId int   ,

	@Domicilio varchar (100)  ,

	@Sexo int   ,

	@Ocupacion varchar (50)  ,

	@Nacionalidad varchar (50)  ,

	@PaisResidencia varchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Persona]
				SET
					[PersonaID] = @PersonaId
					,[Apellido] = @Apellido
					,[Nombre] = @Nombre
					,[TipoDocumento] = @TipoDocumento
					,[NroDocumento] = @NroDocumento
					,[Telefono] = @Telefono
					,[Email] = @Email
					,[FechaNacimiento] = @FechaNacimiento
					,[LocalidadID] = @LocalidadId
					,[UserId] = @UserId
					,[Domicilio] = @Domicilio
					,[Sexo] = @Sexo
					,[Ocupacion] = @Ocupacion
					,[Nacionalidad] = @Nacionalidad
					,[PaisResidencia] = @PaisResidencia
				WHERE
[PersonaID] = @OriginalPersonaId 
				
			

