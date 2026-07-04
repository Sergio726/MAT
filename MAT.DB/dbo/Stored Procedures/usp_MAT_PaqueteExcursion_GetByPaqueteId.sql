CREATE PROCEDURE [dbo].[usp_MAT_PaqueteExcursion_GetByPaqueteId]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vinculos paquete-excursion por paquete, filas de entidad (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PaqueteExcursionID,
        ExcursionID,
        PaqueteID,
        IsOpcional
    FROM dbo.PaqueteExcursion
    WHERE PaqueteID = @PaqueteID;
END
