CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_GetTotales]
(
    @Tipo               INT = NULL,
    @TipoComprobante    INT = NULL,
    @ProveedorID        UNIQUEIDENTIFIER = NULL,
    @ClienteID          UNIQUEIDENTIFIER = NULL,
    @Cuit               VARCHAR(13) = NULL,
    @Estado             INT = NULL,
    @FechaDesde         DATE = NULL,
    @FechaHasta         DATE = NULL,
    @Busqueda           VARCHAR(100) = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Returns aggregated totals for FacturaFiscal records matching the given filters
-- Description: Obtiene totales sumarizados de facturas fiscales con los mismos filtros que GetAll
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    SELECT
        SUM(ff.[Neto]) AS TotalNeto,
        SUM(ff.[Iva]) AS TotalIva,
        SUM(ff.[OtrosImpuestos]) AS TotalOtrosImpuestos,
        SUM(ff.[Total]) AS TotalGeneral,
        COUNT(*) AS CantidadRegistros
    FROM [dbo].[FacturaFiscal] ff
    LEFT JOIN [dbo].[Proveedor] p ON ff.[ProveedorID] = p.[ProveedorID]
    WHERE
        (@Tipo IS NULL OR ff.[Tipo] = @Tipo)
        AND (@TipoComprobante IS NULL OR ff.[TipoComprobante] = @TipoComprobante)
        AND (@ProveedorID IS NULL OR ff.[ProveedorID] = @ProveedorID)
        AND (@ClienteID IS NULL OR ff.[ClienteID] = @ClienteID)
        AND (@Cuit IS NULL OR ff.[Cuit] = @Cuit)
        AND (@Estado IS NULL OR ff.[Estado] = @Estado)
        AND (@FechaDesde IS NULL OR ff.[FechaEmision] >= @FechaDesde)
        AND (@FechaHasta IS NULL OR ff.[FechaEmision] <= @FechaHasta)
        AND (@Busqueda IS NULL OR ff.[Cuit] LIKE '%' + @Busqueda + '%' OR p.[RazonSocial] LIKE '%' + @Busqueda + '%' OR ff.[Observaciones] LIKE '%' + @Busqueda + '%');
END
