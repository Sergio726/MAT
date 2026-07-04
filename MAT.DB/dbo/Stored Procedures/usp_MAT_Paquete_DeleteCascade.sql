CREATE PROCEDURE [dbo].[usp_MAT_Paquete_DeleteCascade]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Elimina un paquete y sus vinculos (PaqueteServicio, PaqueteExcursion,
 --              PaquetePrecio, PaqueteAdicional) en una transaccion (NetTiers F4 -
 --              reemplaza la cascada manual de PaqueteController.Delete).
 --              Falla si el paquete tiene viajes vinculados.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        IF EXISTS (SELECT 1 FROM dbo.Viaje WHERE PaqueteID = @PaqueteID)
        BEGIN
            RAISERROR ('El paquete posee viajes vinculados y no puede eliminarse.', 16, 1);
        END

        BEGIN TRAN;

        DELETE FROM dbo.PaqueteServicio WHERE PaqueteID = @PaqueteID;
        DELETE FROM dbo.PaqueteExcursion WHERE PaqueteID = @PaqueteID;
        DELETE FROM dbo.PaquetePrecio WHERE PaqueteID = @PaqueteID;
        DELETE FROM dbo.PaqueteAdicional WHERE PaqueteID = @PaqueteID;
        DELETE FROM dbo.Paquete WHERE PaqueteID = @PaqueteID;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
