CREATE PROCEDURE [dbo].[usp_MAT_Precio_DeleteCascade]
    @PrecioID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Elimina un precio en transaccion: borra vinculos PaquetePrecio,
 --              desasocia los pasajes (Pasaje.PrecioID = NULL) y borra el precio
 --              (NetTiers F4 - reemplaza la cascada manual de PrecioController.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DELETE FROM dbo.PaquetePrecio WHERE PrecioID = @PrecioID;

        UPDATE dbo.Pasaje
        SET PrecioID = NULL
        WHERE PrecioID = @PrecioID;

        DELETE FROM dbo.Precio WHERE PrecioID = @PrecioID;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
