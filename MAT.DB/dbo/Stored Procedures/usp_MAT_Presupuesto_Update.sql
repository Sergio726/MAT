CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_Update]
(
    @PresupuestoId    UNIQUEIDENTIFIER,
    @NombreCliente    VARCHAR(200) = NULL,
    @TelefonoCliente  VARCHAR(50) = NULL,
    @EmailCliente     VARCHAR(100) = NULL,
    @MontoPactado     FLOAT,
    @FechaExpiracion  DATETIME,
    @Observaciones    VARCHAR(500) = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-02-15
-- Purpose: Actualiza un presupuesto pendiente (solo campos editables)
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE [dbo].[Presupuesto]
        SET 
            [NombreCliente] = NULLIF(LTRIM(RTRIM(@NombreCliente)), ''),
            [TelefonoCliente] = NULLIF(LTRIM(RTRIM(@TelefonoCliente)), ''),
            [EmailCliente] = NULLIF(LTRIM(RTRIM(@EmailCliente)), ''),
            [MontoPactado] = @MontoPactado,
            [FechaExpiracion] = @FechaExpiracion,
            [Observaciones] = ISNULL(@Observaciones, '')
        WHERE 
            [PresupuestoID] = @PresupuestoId
            AND [Estado] = 1  -- Solo pendientes
            AND [FacturaId] IS NULL;

        IF @@ROWCOUNT = 0
            RAISERROR('Presupuesto no encontrado o no es editable (debe estar pendiente)', 16, 1);

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
