CREATE PROCEDURE [dbo].[usp_MAT_PaqueteServicio_DeleteByServicioAndPaquete]
    @ServicioID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Desvincula un servicio de un paquete (NetTiers F4 - reemplaza
 --              GetAll().Where(...) + Delete(PK) de PaqueteController.DesvincularServicio)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.PaqueteServicio
    WHERE ServicioID = @ServicioID
      AND PaqueteID = @PaqueteID;

    SELECT @@ROWCOUNT AS Eliminados;
END
