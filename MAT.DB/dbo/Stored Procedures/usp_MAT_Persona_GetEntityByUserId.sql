CREATE PROCEDURE [dbo].[usp_MAT_Persona_GetEntityByUserId]
    @UserId INT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Persona por UserId de membership, fila de entidad (NetTiers F7 -
 --              reemplaza PersonaService.GetAll().Where(UserId) de MATContext.CurrentVendedor)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT TOP 1
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
    WHERE UserId = @UserId;
END
