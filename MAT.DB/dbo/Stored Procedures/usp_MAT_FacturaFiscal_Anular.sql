CREATE PROCEDURE [dbo].[usp_MAT_FacturaFiscal_Anular]
(
    @FacturaFiscalID UNIQUEIDENTIFIER
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Marks a FacturaFiscal record as voided (Anulada)
-- Description: Anula una factura fiscal activa cambiando su estado a 2 (Anulada)
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[FacturaFiscal]
        SET
            [Estado]    = 2,
            [UpdatedAt] = GETDATE()
        WHERE [FacturaFiscalID] = @FacturaFiscalID
          AND [Estado] = 1;

        SELECT @@ROWCOUNT AS RowsAffected;

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
