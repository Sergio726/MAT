CREATE PROCEDURE [dbo].[usp_MAT_Butaca_Delete]
    @ButacaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Elimina butaca si no está en uso por pasaje (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM dbo.Pasaje WHERE ButacaID = @ButacaID)
        BEGIN
            RAISERROR('No se puede eliminar la butaca: está asignada a un pasaje.', 16, 1);
        END

        DELETE FROM dbo.Butaca
        WHERE ButacaID = @ButacaID;

        IF @@ROWCOUNT = 0
            RAISERROR('Butaca no encontrada.', 16, 1);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
