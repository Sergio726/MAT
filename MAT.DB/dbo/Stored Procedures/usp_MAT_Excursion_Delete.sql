CREATE PROCEDURE [dbo].[usp_MAT_Excursion_Delete]
    @ExcursionID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Elimina una excursion y sus vinculos PaqueteExcursion en transaccion
 --              (NetTiers F4 - reemplaza la cascada manual de ExcursionController.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DELETE FROM dbo.PaqueteExcursion WHERE ExcursionID = @ExcursionID;
        DELETE FROM dbo.Excursion WHERE ExcursionID = @ExcursionID;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
