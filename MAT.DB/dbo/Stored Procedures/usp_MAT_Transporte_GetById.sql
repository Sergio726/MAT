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
