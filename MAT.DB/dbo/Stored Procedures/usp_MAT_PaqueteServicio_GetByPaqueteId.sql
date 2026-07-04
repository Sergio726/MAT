CREATE PROCEDURE [dbo].[usp_MAT_PaqueteServicio_GetByPaqueteId]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Vinculos paquete-servicio por paquete (NetTiers F4)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PaqueteServicioID,
        ServicioID,
        PaqueteID
    FROM dbo.PaqueteServicio
    WHERE PaqueteID = @PaqueteID;
END
