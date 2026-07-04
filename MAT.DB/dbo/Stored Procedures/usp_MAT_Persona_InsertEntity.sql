CREATE PROCEDURE [dbo].[usp_MAT_Persona_InsertEntity]
    @PersonaID UNIQUEIDENTIFIER,
    @Apellido VARCHAR (100) = NULL,
    @Nombre VARCHAR (100) = NULL,
    @TipoDocumento INT = NULL,
    @NroDocumento VARCHAR (50) = NULL,
    @Celular VARCHAR (50) = NULL,
    @Telefono VARCHAR (50) = NULL,
    @Email VARCHAR (50) = NULL,
    @FechaNacimiento DATE = NULL,
    @LocalidadID INT = NULL,
    @UserId INT = NULL,
    @Domicilio VARCHAR (100) = NULL,
    @Sexo INT = NULL,
    @Ocupacion VARCHAR (50) = NULL,
    @Nacionalidad VARCHAR (50) = NULL,
    @PaisResidencia VARCHAR (50) = NULL,
    @Provincia INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Insert de la entidad Persona (NetTiers F7 - reemplaza PersonaService.Insert)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Persona
    (
        PersonaID, Apellido, Nombre, TipoDocumento, NroDocumento, Celular, Telefono, Email,
        FechaNacimiento, LocalidadID, UserId, Domicilio, Sexo, Ocupacion, Nacionalidad,
        PaisResidencia, Provincia
    )
    VALUES
    (
        @PersonaID, @Apellido, @Nombre, @TipoDocumento, @NroDocumento, @Celular, @Telefono, @Email,
        @FechaNacimiento, @LocalidadID, @UserId, @Domicilio, @Sexo, @Ocupacion, @Nacionalidad,
        @PaisResidencia, @Provincia
    );
END
