CREATE PROCEDURE [dbo].[usp_MAT_Servicio_Delete]
    @ServicioID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Elimina servicio y vínculos PaqueteServicio en transacción (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DELETE FROM dbo.PaqueteServicio
        WHERE ServicioID = @ServicioID;

        DELETE FROM dbo.Servicio
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
