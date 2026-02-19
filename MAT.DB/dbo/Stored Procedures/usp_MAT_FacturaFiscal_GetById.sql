CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_GetById]
(
    @FacturaFiscalID UNIQUEIDENTIFIER
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Retrieves a single FacturaFiscal record by its ID
-- Description: Obtiene una factura fiscal por su identificador con datos relacionados
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
    WHERE ff.[FacturaFiscalID] = @FacturaFiscalID;
END
