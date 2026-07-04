CREATE PROCEDURE [dbo].[usp_MAT_Persona_GetEntityById]
    @PersonaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Persona por ID, fila de entidad (NetTiers F7 - reemplaza PersonaService.Get)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PersonaID,
        Apellido,
        Nombre,
        TipoDocumento,
        NroDocumento,
        Celular,
        Telefono,
        Email,
        FechaNacimiento,
        LocalidadID,
        UserId,
        Domicilio,
        Sexo,
        Ocupacion,
        Nacionalidad,
        PaisResidencia,
        Provincia
    FROM dbo.Persona
    WHERE PersonaID = @PersonaID;
END
