
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Persona table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePersona_Insert]
(

	@PersonaId uniqueidentifier   ,

	@Apellido varchar (100)  ,

	@Nombre varchar (100)  ,

	@TipoDocumento int   ,

	@NroDocumento varchar (50)  ,

	@Celular varchar (50)  ,

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


				
				INSERT INTO [dbo].[Persona]
					(
					[PersonaID]
					,[Apellido]
					,[Nombre]
					,[TipoDocumento]
					,[NroDocumento]
					,[Celular]
					,[Telefono]
					,[Email]
					,[FechaNacimiento]
					,[LocalidadID]
					,[UserId]
					,[Domicilio]
					,[Sexo]
					,[Ocupacion]
					,[Nacionalidad]
					,[PaisResidencia]
					)
				VALUES
					(
					@PersonaId
					,@Apellido
					,@Nombre
					,@TipoDocumento
					,@NroDocumento
					,@Celular
					,@Telefono
					,@Email
					,@FechaNacimiento
					,@LocalidadId
					,@UserId
					,@Domicilio
					,@Sexo
					,@Ocupacion
					,@Nacionalidad
					,@PaisResidencia
					)
				
									
							
			



