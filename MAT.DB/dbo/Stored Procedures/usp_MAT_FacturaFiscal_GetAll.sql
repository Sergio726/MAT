CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_GetAll]
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
-- Purpose: Retrieves all FacturaFiscal records with optional filters
-- Description: Lista facturas fiscales con filtros opcionales y datos relacionados
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    SELECT
        ff.[FacturaFiscalID],
        ff.[Tipo],
        ff.[TipoComprobante],
        ff.[PuntoVenta],
        ff.[Numero],
        ff.[FechaEmision],
        ff.[FechaVencimiento],
        ff.[ProveedorID],
        ff.[ClienteID],
        ff.[Cuit],
        ff.[CondicionIva],
        ff.[Neto],
        ff.[Iva],
        ff.[OtrosImpuestos],
        ff.[Total],
        ff.[Moneda],
        ff.[CAE],
        ff.[ArchivoAdjunto],
        ff.[Estado],
        ff.[AlicuotaIva],
        ff.[Percepciones],
        ff.[CondicionVenta],
        ff.[Observaciones],
        ff.[CreatedAt],
        ff.[UpdatedAt],
        p.[RazonSocial] AS ProveedorRazonSocial,
        pc.[Apellido] + ' ' + pc.[Nombre] AS ClienteNombre,
        m.[Descripcion] AS MonedaDescripcion
    FROM [dbo].[FacturaFiscal] ff
    LEFT JOIN [dbo].[Proveedor] p ON ff.[ProveedorID] = p.[ProveedorID]
    LEFT JOIN [dbo].[Persona] pc ON ff.[ClienteID] = pc.[PersonaID]
    LEFT JOIN [dbo].[MonedaTipo] m ON ff.[Moneda] = m.[Id]
    WHERE
        (@Tipo IS NULL OR ff.[Tipo] = @Tipo)
        AND (@TipoComprobante IS NULL OR ff.[TipoComprobante] = @TipoComprobante)
        AND (@ProveedorID IS NULL OR ff.[ProveedorID] = @ProveedorID)
        AND (@ClienteID IS NULL OR ff.[ClienteID] = @ClienteID)
        AND (@Cuit IS NULL OR ff.[Cuit] = @Cuit)
        AND (@Estado IS NULL OR ff.[Estado] = @Estado)
        AND (@FechaDesde IS NULL OR ff.[FechaEmision] >= @FechaDesde)
        AND (@FechaHasta IS NULL OR ff.[FechaEmision] <= @FechaHasta)
        AND (@Busqueda IS NULL OR ff.[Cuit] LIKE '%' + @Busqueda + '%' OR p.[RazonSocial] LIKE '%' + @Busqueda + '%' OR ff.[Observaciones] LIKE '%' + @Busqueda + '%')
    ORDER BY ff.[FechaEmision] DESC;
END
