-- Fix Transporte/Edit — publicar SPs F3 faltantes en cada entorno (p. ej. mat.viewdns.net)
-- Fuente de verdad: MAT.DB/dbo/Stored Procedures/
-- Requerido para GET /Transporte/Edit/{id} y POST guardar cambios.

IF OBJECT_ID(N'dbo.usp_MAT_Transporte_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Transporte_GetById;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Transporte_GetById]
    @TransporteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Obtiene un transporte por ID (migración NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        TransporteID,
        NroCoche,
        MaxPasajeros,
        KmRecorridos,
        UltimoService,
        Matricula,
        Tipo
    FROM dbo.Transporte
    WHERE TransporteID = @TransporteID;
END
GO

IF OBJECT_ID(N'dbo.usp_MAT_Transporte_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Transporte_Update;
GO

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
GO

-- Verificación post-deploy:
-- SELECT OBJECT_ID('dbo.usp_MAT_Transporte_GetById'), OBJECT_ID('dbo.usp_MAT_Transporte_Update');
-- EXEC usp_MAT_Transporte_GetById @TransporteID = 'a69ba293-4730-4a4d-84ac-54bfff60a642';
