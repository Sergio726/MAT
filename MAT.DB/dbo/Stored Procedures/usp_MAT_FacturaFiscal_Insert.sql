CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_Insert]
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
-- Purpose: Inserts a new record into the FacturaFiscal table
-- Description: Crea una nueva factura fiscal (compra o venta)
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO [dbo].[FacturaFiscal]
        (
            [FacturaFiscalID],
            [Tipo],
            [TipoComprobante],
            [PuntoVenta],
            [Numero],
            [FechaEmision],
            [FechaVencimiento],
            [ProveedorID],
            [ClienteID],
            [Cuit],
            [CondicionIva],
            [Neto],
            [Iva],
            [OtrosImpuestos],
            [Total],
            [Moneda],
            [CAE],
            [ArchivoAdjunto],
            [Estado],
            [AlicuotaIva],
            [Percepciones],
            [CondicionVenta],
            [Observaciones],
            [CreatedAt]
        )
        VALUES
        (
            @FacturaFiscalID,
            @Tipo,
            @TipoComprobante,
            @PuntoVenta,
            @Numero,
            @FechaEmision,
            @FechaVencimiento,
            @ProveedorID,
            @ClienteID,
            @Cuit,
            @CondicionIva,
            @Neto,
            @Iva,
            @OtrosImpuestos,
            @Total,
            @Moneda,
            @CAE,
            @ArchivoAdjunto,
            @Estado,
            @AlicuotaIva,
            @Percepciones,
            @CondicionVenta,
            @Observaciones,
            GETDATE()
        );

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
