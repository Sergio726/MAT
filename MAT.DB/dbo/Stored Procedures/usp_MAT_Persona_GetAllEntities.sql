CREATE PROCEDURE [dbo].[usp_MAT_Persona_GetAllEntities]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todas las Personas, filas de entidad (NetTiers F7 - reemplaza PersonaService.GetAll)
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
    FROM dbo.Persona;
END
