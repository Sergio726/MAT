/*
----------------------------------------------------------------------------------------------------
-- Created By: Sistema MAT
-- Create date: 2026-01-XX
-- Purpose: Marca presupuestos expirados como tal
-- Description: Actualiza el estado de presupuestos que han pasado su fecha de expiración
----------------------------------------------------------------------------------------------------
*/

CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_Expirados]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[Presupuesto]
        SET [Estado] = 2 -- Estado: 2 = Expirado
        WHERE 
            [Estado] = 1 -- Solo pendientes
            AND [FechaExpiracion] < GETDATE()
            AND [FacturaId] IS NULL; -- Solo los que no se cerraron

        DECLARE @RowsAffected INT = @@ROWCOUNT;

        COMMIT TRANSACTION;

        SELECT @RowsAffected AS PresupuestosExpirados;
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

