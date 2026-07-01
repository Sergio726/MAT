CREATE PROCEDURE [dbo].[usp_MAT_Transporte_Update]
    @TransporteID   UNIQUEIDENTIFIER,
    @NroCoche       VARCHAR(50)  = NULL,
    @MaxPasajeros   INT          = NULL,
    @KmRecorridos   INT          = NULL,
    @UltimoService  DATE         = NULL,
    @Matricula      VARCHAR(10)  = NULL,
    @Tipo           NVARCHAR(50) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Actualiza transporte (migración NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        UPDATE dbo.Transporte
        SET NroCoche       = @NroCoche,
            MaxPasajeros   = @MaxPasajeros,
            KmRecorridos   = @KmRecorridos,
            UltimoService  = @UltimoService,
            Matricula      = @Matricula,
            Tipo           = @Tipo
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
