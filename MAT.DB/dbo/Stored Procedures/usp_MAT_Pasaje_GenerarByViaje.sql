CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_GenerarByViaje]
    @ViajeID UNIQUEIDENTIFIER,
    @TransporteID UNIQUEIDENTIFIER,
    @EstadoPasaje INT,
    @Generados INT OUTPUT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Genera un pasaje por cada butaca del transporte para el viaje dado,
 --              en una transaccion set-based (NetTiers F4 - reemplaza el loop
 --              PasajeService.Insert + TransactionManager de PaqueteModel.GenerarPasajes)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        INSERT INTO dbo.Pasaje (PasajeID, ButacaID, ViajeID, EstadoPasaje)
        SELECT NEWID(), b.ButacaID, @ViajeID, @EstadoPasaje
        FROM dbo.Butaca b
        WHERE b.TransporteID = @TransporteID;

        SET @Generados = @@ROWCOUNT;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
