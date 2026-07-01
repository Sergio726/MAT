CREATE PROCEDURE [dbo].[usp_MAT_Servicio_Update]
    @ServicioID         UNIQUEIDENTIFIER,
    @Descripcion        VARCHAR(250),
    @Precio             FLOAT(53)        = NULL,
    @Moneda             VARCHAR(50)      = NULL,
    @Iva                VARCHAR(50)      = NULL,
    @Alicuota           FLOAT(53)        = NULL,
    @Validez            DATE             = NULL,
    @VisibilidadTarifa  INT              = NULL,
    @ProveedorID        UNIQUEIDENTIFIER = NULL,
    @TransporteID       UNIQUEIDENTIFIER = NULL,
    @HotelID            UNIQUEIDENTIFIER = NULL,
    @TipoServicio       INT              = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Actualiza servicio (migración NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        UPDATE dbo.Servicio
        SET Descripcion        = @Descripcion,
            Precio             = @Precio,
            Moneda             = @Moneda,
            Iva                = @Iva,
            Alicuota           = @Alicuota,
            Validez            = @Validez,
            VisibilidadTarifa  = @VisibilidadTarifa,
            ProveedorID        = @ProveedorID,
            TransporteID       = @TransporteID,
            HotelID            = @HotelID,
            TipoServicio       = @TipoServicio
        WHERE ServicioID = @ServicioID;

        IF @@ROWCOUNT = 0
            RAISERROR('Servicio no encontrado.', 16, 1);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
