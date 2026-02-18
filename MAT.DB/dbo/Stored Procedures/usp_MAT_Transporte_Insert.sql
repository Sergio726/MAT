CREATE PROCEDURE [dbo].[usp_MAT_Transporte_Insert]
    @NroCoche        VARCHAR(50)  = NULL,
    @MaxPasajeros    INT          = NULL,
    @KmRecorridos    INT          = NULL,
    @UltimoService   DATE         = NULL,
    @Matricula       VARCHAR(10)  = NULL
AS
-- =============================================
-- Author:    Seba Garcia
-- Create date: 2026
-- Description: Inserta un nuevo transporte
-- =============================================
SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    BEGIN TRY
        BEGIN TRAN;

        INSERT INTO dbo.Transporte (
            NroCoche,
            MaxPasajeros,
            KmRecorridos,
            UltimoService,
            Matricula
        )
        VALUES (
            @NroCoche,
            @MaxPasajeros,
            @KmRecorridos,
            @UltimoService,
            @Matricula
        );

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg  NVARCHAR(2048),
                @errState INT;
        SELECT @errmsg = ERROR_MESSAGE() + ' Line: ' + CAST(ERROR_LINE() AS VARCHAR(10)),
               @errState = ERROR_STATE();
        RAISERROR(@errmsg, 16, @errState);
    END CATCH
END
