/*
----------------------------------------------------------------------------------------------------
-- Created By: Sistema MAT
-- Create date: 2026-01-XX
-- Purpose: Actualiza el estado de un presupuesto y lo vincula con una factura
-- Description: Marca un presupuesto como cerrado y registra la factura y vendedor de cierre
----------------------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_UpdateEstado]
(
    @PresupuestoId    UNIQUEIDENTIFIER,
    @Estado           INT,
    @FacturaId        UNIQUEIDENTIFIER = NULL,
    @VendedorIdCierre UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[Presupuesto]
        SET 
            [Estado] = @Estado,
            [FacturaId] = ISNULL(@FacturaId, [FacturaId]),
            [VendedorIdCierre] = ISNULL(@VendedorIdCierre, [VendedorIdCierre])
        WHERE 
            [PresupuestoID] = @PresupuestoId;

        IF @@ROWCOUNT = 0
            RAISERROR('Presupuesto no encontrado', 16, 1);

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

GO

