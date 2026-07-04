CREATE PROCEDURE [dbo].[usp_MAT_PaqueteAdicional_GetByPaqueteId]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vinculos paquete-adicional por paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PaqueteAdicionalID,
        PaqueteID,
        AdicionalID
    FROM dbo.PaqueteAdicional
    WHERE PaqueteID = @PaqueteID;
END
