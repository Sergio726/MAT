CREATE PROCEDURE [dbo].[usp_MAT_PaquetePrecio_GetByPaqueteId]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vinculos paquete-precio por paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PaquetePrecioID,
        PaqueteID,
        PrecioID
    FROM dbo.PaquetePrecio
    WHERE PaqueteID = @PaqueteID;
END
