CREATE PROCEDURE [dbo].[usp_MAT_Excursion_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todas las excursiones (NetTiers F4 - reemplaza ExcursionService.GetAll)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ExcursionID,
        Descripcion,
        Costo,
        Observaciones,
        ProveedorID
    FROM dbo.Excursion;
END
