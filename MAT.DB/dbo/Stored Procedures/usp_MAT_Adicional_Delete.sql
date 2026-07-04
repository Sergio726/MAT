CREATE PROCEDURE [dbo].[usp_MAT_Adicional_Delete]
    @AdicionalID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Elimina un adicional y sus vinculos PaqueteAdicional en transaccion
 --              (NetTiers F4 - reemplaza la cascada manual de AdicionalController.Delete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DELETE FROM dbo.PaqueteAdicional WHERE AdicionalID = @AdicionalID;
        DELETE FROM dbo.Adicional WHERE AdicionalID = @AdicionalID;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
