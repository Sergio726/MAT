CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_CheckDuplicate]
(
    @Cuit               VARCHAR(13),
    @TipoComprobante    INT,
    @PuntoVenta         INT,
    @Numero             BIGINT,
    @ExcludeID          UNIQUEIDENTIFIER = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Checks if a duplicate comprobante exists in FacturaFiscal
-- Description: Verifica si ya existe una factura con el mismo CUIT, tipo, punto de venta y numero
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Duplicados
    FROM [dbo].[FacturaFiscal]
    WHERE [Cuit] = @Cuit
      AND [TipoComprobante] = @TipoComprobante
      AND [PuntoVenta] = @PuntoVenta
      AND [Numero] = @Numero
      AND (@ExcludeID IS NULL OR [FacturaFiscalID] <> @ExcludeID);
END
