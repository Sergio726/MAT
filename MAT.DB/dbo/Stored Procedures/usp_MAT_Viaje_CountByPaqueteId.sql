CREATE PROCEDURE [dbo].[usp_MAT_Viaje_CountByPaqueteId]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Cantidad de viajes vinculados a un paquete (NetTiers F4 -
 --              reemplaza ViajeService.GetByPaqueteId().Count en el delete de paquete)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT COUNT(*) AS Cantidad
    FROM dbo.Viaje
    WHERE PaqueteID = @PaqueteID;
END
