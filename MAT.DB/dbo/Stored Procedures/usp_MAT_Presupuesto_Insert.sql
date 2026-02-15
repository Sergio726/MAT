CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_Insert]
(
    @PresupuestoId      UNIQUEIDENTIFIER,
    @DniCliente         VARCHAR(50) = NULL,
    @NombreCliente      VARCHAR(200) = NULL,
    @TelefonoCliente    VARCHAR(50) = NULL,
    @EmailCliente       VARCHAR(100) = NULL,
    @VendedorIdOrigen   UNIQUEIDENTIFIER,
    @MontoPactado       FLOAT,
    @ViajeId            UNIQUEIDENTIFIER = NULL,
    @FechaExpiracion    DATETIME = NULL,
    @Observaciones      VARCHAR(500) = NULL,
    @CodigoSeguimiento  VARCHAR(50) OUTPUT
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-01-11
-- Purpose: Inserts a record into the Presupuesto table
-- Description: Crea un nuevo presupuesto con código único generado automáticamente
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Generar nuevo GUID si no se proporciona
        IF @PresupuestoId IS NULL
            SET @PresupuestoId = NEWID();

        -- Generar código único MAT-XXXX
        DECLARE @Secuencial INT;
        DECLARE @FechaActual VARCHAR(8) = CONVERT(VARCHAR(8), GETDATE(), 112); -- YYYYMMDD
        
        -- Obtener el siguiente número secuencial del día
        SELECT @Secuencial = ISNULL(MAX(CAST(SUBSTRING(CodigoSeguimiento, 13, LEN(CodigoSeguimiento)) AS INT)), 0) + 1
        FROM [dbo].[Presupuesto]
        WHERE CodigoSeguimiento LIKE 'MAT-' + @FechaActual + '-%';
        
        -- Formato: MAT-YYYYMMDD-XXXX
        SET @CodigoSeguimiento = 'MAT-' + @FechaActual + '-' + RIGHT('0000' + CAST(@Secuencial AS VARCHAR(4)), 4);

        -- Si no se proporciona fecha de expiración, usar 48 horas por defecto
        IF @FechaExpiracion IS NULL
            SET @FechaExpiracion = DATEADD(HOUR, 48, GETDATE());

        -- Insertar presupuesto
        INSERT INTO [dbo].[Presupuesto]
        (
            [PresupuestoID],
            [DniCliente],
            [NombreCliente],
            [TelefonoCliente],
            [EmailCliente],
            [VendedorIdOrigen],
            [CodigoSeguimiento],
            [MontoPactado],
            [ViajeId],
            [Estado],
            [FechaCreacion],
            [FechaExpiracion],
            [Observaciones]
        )
        VALUES
        (
            @PresupuestoId,
            ISNULL(NULLIF(LTRIM(RTRIM(@DniCliente)), ''), NULL),
            NULLIF(LTRIM(RTRIM(@NombreCliente)), ''),
            NULLIF(LTRIM(RTRIM(@TelefonoCliente)), ''),
            NULLIF(LTRIM(RTRIM(@EmailCliente)), ''),
            @VendedorIdOrigen,
            @CodigoSeguimiento,
            @MontoPactado,
            @ViajeId,
            1, -- Estado: 1 = Pendiente
            GETDATE(),
            @FechaExpiracion,
            @Observaciones
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