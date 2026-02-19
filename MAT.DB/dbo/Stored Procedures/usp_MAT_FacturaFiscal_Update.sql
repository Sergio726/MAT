CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_Update]
(
    @FacturaFiscalID    UNIQUEIDENTIFIER,
    @Tipo               INT,
    @TipoComprobante    INT,
    @PuntoVenta         INT,
    @Numero             BIGINT,
    @FechaEmision       DATE,
    @FechaVencimiento   DATE = NULL,
    @ProveedorID        UNIQUEIDENTIFIER = NULL,
    @ClienteID          UNIQUEIDENTIFIER = NULL,
    @Cuit               VARCHAR(13),
    @CondicionIva       INT,
    @Neto               DECIMAL(18,2) = 0,
    @Iva                DECIMAL(18,2) = 0,
    @OtrosImpuestos     DECIMAL(18,2) = 0,
    @Total              DECIMAL(18,2) = 0,
    @Moneda             INT = 1,
    @CAE                VARCHAR(20) = NULL,
    @ArchivoAdjunto     VARCHAR(500) = NULL,
    @Estado             INT = 1,
    @AlicuotaIva        INT = 4,
    @Percepciones       DECIMAL(18,2) = 0,
    @CondicionVenta     VARCHAR(100) = NULL,
    @Observaciones      VARCHAR(500) = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Updates an existing record in the FacturaFiscal table
-- Description: Actualiza una factura fiscal existente
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[FacturaFiscal]
        SET
            [Tipo]              = @Tipo,
            [TipoComprobante]   = @TipoComprobante,
            [PuntoVenta]        = @PuntoVenta,
            [Numero]            = @Numero,
            [FechaEmision]      = @FechaEmision,
            [FechaVencimiento]  = @FechaVencimiento,
            [ProveedorID]       = @ProveedorID,
            [ClienteID]         = @ClienteID,
            [Cuit]              = @Cuit,
            [CondicionIva]      = @CondicionIva,
            [Neto]              = @Neto,
            [Iva]               = @Iva,
            [OtrosImpuestos]    = @OtrosImpuestos,
            [Total]             = @Total,
            [Moneda]            = @Moneda,
            [CAE]               = @CAE,
            [ArchivoAdjunto]    = @ArchivoAdjunto,
            [Estado]            = @Estado,
            [AlicuotaIva]       = @AlicuotaIva,
            [Percepciones]      = @Percepciones,
            [CondicionVenta]    = @CondicionVenta,
            [Observaciones]     = @Observaciones,
            [UpdatedAt]         = GETDATE()
        WHERE [FacturaFiscalID] = @FacturaFiscalID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
