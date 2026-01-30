
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Persona table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePersona_Find]
(

	@SearchUsingOR bit   = null ,

	@PersonaId uniqueidentifier   = null ,

	@Apellido varchar (100)  = null ,

	@Nombre varchar (100)  = null ,

	@TipoDocumento int   = null ,

	@NroDocumento varchar (50)  = null ,

	@Celular varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@FechaNacimiento date   = null ,

	@LocalidadId int   = null ,

	@UserId int   = null ,

	@Domicilio varchar (100)  = null ,

	@Sexo int   = null ,

	@Ocupacion varchar (50)  = null ,

	@Nacionalidad varchar (50)  = null ,

	@PaisResidencia varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PersonaID]
	, [Apellido]
	, [Nombre]
	, [TipoDocumento]
	, [NroDocumento]
	, [Celular]
	, [Telefono]
	, [Email]
	, [FechaNacimiento]
	, [LocalidadID]
	, [UserId]
	, [Domicilio]
	, [Sexo]
	, [Ocupacion]
	, [Nacionalidad]
	, [PaisResidencia]
    FROM
	[dbo].[Persona]
    WHERE 
	 ([PersonaID] = @PersonaId OR @PersonaId IS NULL)
	AND ([Apellido] = @Apellido OR @Apellido IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
	AND ([TipoDocumento] = @TipoDocumento OR @TipoDocumento IS NULL)
	AND ([NroDocumento] = @NroDocumento OR @NroDocumento IS NULL)
	AND ([Celular] = @Celular OR @Celular IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([FechaNacimiento] = @FechaNacimiento OR @FechaNacimiento IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
	AND ([UserId] = @UserId OR @UserId IS NULL)
	AND ([Domicilio] = @Domicilio OR @Domicilio IS NULL)
	AND ([Sexo] = @Sexo OR @Sexo IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
	AND ([Nacionalidad] = @Nacionalidad OR @Nacionalidad IS NULL)
	AND ([PaisResidencia] = @PaisResidencia OR @PaisResidencia IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PersonaID]
	, [Apellido]
	, [Nombre]
	, [TipoDocumento]
	, [NroDocumento]
	, [Celular]
	, [Telefono]
	, [Email]
	, [FechaNacimiento]
	, [LocalidadID]
	, [UserId]
	, [Domicilio]
	, [Sexo]
	, [Ocupacion]
	, [Nacionalidad]
	, [PaisResidencia]
    FROM
	[dbo].[Persona]
    WHERE 
	 ([PersonaID] = @PersonaId AND @PersonaId is not null)
	OR ([Apellido] = @Apellido AND @Apellido is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	OR ([TipoDocumento] = @TipoDocumento AND @TipoDocumento is not null)
	OR ([NroDocumento] = @NroDocumento AND @NroDocumento is not null)
	OR ([Celular] = @Celular AND @Celular is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([FechaNacimiento] = @FechaNacimiento AND @FechaNacimiento is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	OR ([UserId] = @UserId AND @UserId is not null)
	OR ([Domicilio] = @Domicilio AND @Domicilio is not null)
	OR ([Sexo] = @Sexo AND @Sexo is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	OR ([Nacionalidad] = @Nacionalidad AND @Nacionalidad is not null)
	OR ([PaisResidencia] = @PaisResidencia AND @PaisResidencia is not null)
	SELECT @@ROWCOUNT			
  END
				



