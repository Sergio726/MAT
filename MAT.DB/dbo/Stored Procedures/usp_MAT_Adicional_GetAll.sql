CREATE PROCEDURE [dbo].[usp_MAT_Adicional_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todos los adicionales (NetTiers F4 - reemplaza AdicionalService.GetAll)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        AdicionalID,
        Monto,
        Descripcion
    FROM dbo.Adicional;
END
