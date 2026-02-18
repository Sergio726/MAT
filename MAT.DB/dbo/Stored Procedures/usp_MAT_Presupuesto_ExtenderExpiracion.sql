CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_ExtenderExpiracion]
(
    @PresupuestoId      UNIQUEIDENTIFIER,
    @HorasAdicionales   INT = 24
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-02-15
-- Purpose: Extiende la fecha de expiración de un presupuesto pendiente
-- Description: Suma horas a FechaExpiracion para presupuestos en estado Pendiente
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @HorasAdicionales <= 0
        SET @HorasAdicionales = 24;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[Presupuesto]
        SET [FechaExpiracion] = DATEADD(HOUR, @HorasAdicionales, [FechaExpiracion])
        WHERE 
            [PresupuestoID] = @PresupuestoId
            AND [Estado] = 1  -- Solo pendientes
            AND [FacturaId] IS NULL;

        DECLARE @RowsAffected INT = @@ROWCOUNT;

        COMMIT TRANSACTION;

        SELECT @RowsAffected AS RowsAffected;
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
