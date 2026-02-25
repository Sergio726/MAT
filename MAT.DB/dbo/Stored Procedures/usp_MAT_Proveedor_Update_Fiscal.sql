CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_Update_Fiscal]
(
    @ProveedorID    UNIQUEIDENTIFIER,
    @RazonSocial    VARCHAR(50),
    @Cuit           VARCHAR(50),
    @CondicionIva   INT = NULL,
    @Domicilio      VARCHAR(200) = NULL,
    @Email          VARCHAR(50) = NULL,
    @Telefono       VARCHAR(50) = NULL,
    @Estado         INT = 1,
    @Web            VARCHAR(50) = NULL,
    @Fax            VARCHAR(50) = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Updates an existing Proveedor record for the fiscal module
-- Description: Actualiza un proveedor existente validando unicidad de CUIT excluyendo el registro actual
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- Verificar que el CUIT no exista en otro proveedor
        IF EXISTS (SELECT 1 FROM [dbo].[Proveedor] WHERE [Cuit] = @Cuit AND [ProveedorID] <> @ProveedorID)
        BEGIN
            RAISERROR('Ya existe otro proveedor con el CUIT ingresado.', 16, 1);
        END

        UPDATE [dbo].[Proveedor]
        SET
            [RazonSocial]   = @RazonSocial,
            [Cuit]          = @Cuit,
            [CondicionIva]  = @CondicionIva,
            [Domicilio]     = @Domicilio,
            [Email]         = @Email,
            [Telefono]      = @Telefono,
            [Estado]        = @Estado,
            [Web]           = @Web,
            [Fax]           = @Fax
        WHERE [ProveedorID] = @ProveedorID;

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
