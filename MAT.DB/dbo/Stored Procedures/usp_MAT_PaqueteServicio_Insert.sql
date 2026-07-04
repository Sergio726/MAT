CREATE PROCEDURE [dbo].[usp_MAT_PaqueteServicio_Insert]
    @PaqueteServicioID UNIQUEIDENTIFIER,
    @ServicioID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vincula un servicio a un paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.PaqueteServicio (PaqueteServicioID, ServicioID, PaqueteID)
    VALUES (@PaqueteServicioID, @ServicioID, @PaqueteID);
END
