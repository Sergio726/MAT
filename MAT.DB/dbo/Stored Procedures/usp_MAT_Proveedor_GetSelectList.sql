CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_GetSelectList]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-05
  -- Description: Lista ProveedorID + RazonSocial para dropdowns Helper (NetTiers F9).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ProveedorID,
        RazonSocial
    FROM dbo.Proveedor
    ORDER BY RazonSocial;
END
