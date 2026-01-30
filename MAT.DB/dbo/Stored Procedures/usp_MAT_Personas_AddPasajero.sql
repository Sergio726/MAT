CREATE PROCEDURE [dbo].[usp_MAT_Personas_AddPasajero]
(	
	@Apellido varchar (100),
	@Nombre varchar (100),
	@TipoDocumento int = NULL,
	@NroDocumento varchar (50) = NULL,
	@Celular varchar (50) = NULL,
	@Telefono varchar (50) = NULL,
	@Email varchar (50) = NULL,
	@FechaNacimiento date = NULL,
	@LocalidadId int = NULL,
	@UserId int = NULL,
	@Domicilio varchar (100) = NULL,
	@Sexo int = NULL,
	@Ocupacion varchar (50) = NULL,
	@Nacionalidad varchar (50) = NULL,
	@PaisResidencia varchar (50) = NULL 
)
AS
 /*-- ============================================= 
  -- Author: Ruben Tejerina 
  -- Create date: 13/12/2024
  -- Description: Add Pasajero
	 This SP is called by API BACKEND

  -- ============================================= */
BEGIN
	SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted;	

	DECLARE @CurrentPersonaId uniqueidentifier, @NroDoc varchar(50)

	SELECT @NroDoc = REPLACE(@NroDocumento,'.','')


	SELECT @CurrentPersonaId = p.PersonaID
	FROM dbo.Persona p
	where p.NroDocumentoCalc = @NroDoc

	IF (@CurrentPersonaId IS NULL)
	BEGIN
		
		SELECT @CurrentPersonaId = NEWID()

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
					@CurrentPersonaId
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

		INSERT INTO dbo.Pasajero (PasajeroID)
		SELECT @CurrentPersonaId
	END

	declare @Today date = getdate()
	select 
		p.PersonaID,
		p.Nombre,
		p.Apellido,
		p.NroDocumento,
		p.Email,
		datediff(year,p.FechaNacimiento,@Today) as Edad		
	from Persona p
	where
		p.PersonaID = @CurrentPersonaId
END