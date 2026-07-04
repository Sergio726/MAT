CREATE PROCEDURE [dbo].[usp_MAT_PaquetePrecio_Insert]
    @PaquetePrecioID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER,
    @PrecioID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vincula un precio a un paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.PaquetePrecio (PaquetePrecioID, PaqueteID, PrecioID)
    VALUES (@PaquetePrecioID, @PaqueteID, @PrecioID);
END
