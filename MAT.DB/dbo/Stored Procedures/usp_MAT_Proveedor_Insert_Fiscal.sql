CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_Insert_Fiscal]
(
    @ProveedorID    UNIQUEIDENTIFIER,
    @RazonSocial    VARCHAR(50),
    @Cuit           VARCHAR(50),
    @CondicionIva   INT = NULL,
    @Domicilio      VARCHAR(200) = NULL,
    @Email          VARCHAR(50) = NULL,
    @Telefono       VARCHAR(50) = NULL,
    @Estado         INT = 1
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Inserts a new Proveedor record for the fiscal module
-- Description: Crea un nuevo proveedor validando unicidad de CUIT
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Verificar que el CUIT no exista
        IF EXISTS (SELECT 1 FROM [dbo].[Proveedor] WHERE [Cuit] = @Cuit)
        BEGIN
            RAISERROR('Ya existe un proveedor con el CUIT ingresado.', 16, 1);
        END

        INSERT INTO [dbo].[Proveedor]
        (
            [ProveedorID],
            [RazonSocial],
            [Cuit],
            [CondicionIva],
            [Domicilio],
            [Email],
            [Telefono],
            [Estado]
        )
        VALUES
        (
            @ProveedorID,
            @RazonSocial,
            @Cuit,
            @CondicionIva,
            @Domicilio,
            @Email,
            @Telefono,
            @Estado
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
