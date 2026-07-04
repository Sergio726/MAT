CREATE PROCEDURE [dbo].[usp_MAT_Adicional_GetById]
    @AdicionalID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Adicional por ID (NetTiers F4 - reemplaza AdicionalService.GetByAdicionalId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        AdicionalID,
        Monto,
        Descripcion
    FROM dbo.Adicional
    WHERE AdicionalID = @AdicionalID;
END
