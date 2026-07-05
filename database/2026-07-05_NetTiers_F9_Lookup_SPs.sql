-- NetTiers F9: SPs para LookupDataAccess / Helper (dropdowns y lookups)
-- Ejecutar en el entorno antes de smoke de Servicio/Excursion/Helper.

IF OBJECT_ID(N'dbo.usp_MAT_Proveedor_GetSelectList', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_Proveedor_GetSelectList AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[usp_MAT_Proveedor_GetSelectList]
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
GO

IF OBJECT_ID(N'dbo.usp_MAT_PrecioServicio_GetActiveByServicioId', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_PrecioServicio_GetActiveByServicioId AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[usp_MAT_PrecioServicio_GetActiveByServicioId]
    @ServicioID UNIQUEIDENTIFIER
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-05
  -- Description: Precio activo de un servicio (NetTiers F9 - reemplaza PrecioServicioService en Helper).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT TOP (1)
        Precio
    FROM dbo.PrecioServicio
    WHERE ServicioID = @ServicioID
      AND Activo = 1
    ORDER BY FechaRegistro DESC;
END
GO
