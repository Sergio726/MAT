CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_GetForExport]
(
    @Tipo           INT,
    @FechaDesde     DATE,
    @FechaHasta     DATE
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Retrieves active FacturaFiscal records for export within a date range
-- Description: Obtiene facturas fiscales activas para exportacion filtradas por tipo y rango de fechas
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
    WHERE ff.[Tipo] = @Tipo
      AND ff.[FechaEmision] >= @FechaDesde
      AND ff.[FechaEmision] <= @FechaHasta
      AND ff.[Estado] = 1
    ORDER BY ff.[FechaEmision] ASC;
END
