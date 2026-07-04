CREATE PROCEDURE [dbo].[usp_MAT_Persona_UpdateEntity]
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
 -- Description: Update de la entidad Persona (NetTiers F7 - reemplaza PersonaService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Persona
    SET Apellido = @Apellido,
        Nombre = @Nombre,
        TipoDocumento = @TipoDocumento,
        NroDocumento = @NroDocumento,
        Celular = @Celular,
        Telefono = @Telefono,
        Email = @Email,
        FechaNacimiento = @FechaNacimiento,
        LocalidadID = @LocalidadID,
        UserId = @UserId,
        Domicilio = @Domicilio,
        Sexo = @Sexo,
        Ocupacion = @Ocupacion,
        Nacionalidad = @Nacionalidad,
        PaisResidencia = @PaisResidencia,
        Provincia = @Provincia
    WHERE PersonaID = @PersonaID;
END
