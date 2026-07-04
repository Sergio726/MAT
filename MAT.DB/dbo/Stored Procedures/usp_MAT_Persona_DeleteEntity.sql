CREATE PROCEDURE [dbo].[usp_MAT_Persona_DeleteEntity]
    @PersonaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Delete de la entidad Persona por ID (NetTiers F7 - reemplaza PersonaService.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.Persona
    WHERE PersonaID = @PersonaID;
END
