CREATE PROCEDURE [dbo].[usp_MAT_Transporte_Delete]
    @TransporteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Elimina transporte si no tiene butacas vinculadas (NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM dbo.Butaca WHERE TransporteID = @TransporteID)
        BEGIN
            RAISERROR('No se puede eliminar el transporte: tiene butacas vinculadas.', 16, 1);
        END

        DELETE FROM dbo.Transporte
        WHERE TransporteID = @TransporteID;

        IF @@ROWCOUNT = 0
            RAISERROR('Transporte no encontrado.', 16, 1);

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
