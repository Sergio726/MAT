CREATE PROCEDURE [dbo].[usp_MAT_PaquetePrecio_DeleteByPrecioAndPaquete]
    @PrecioID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Desvincula un precio de un paquete (NetTiers F4 - reemplaza
 --              GetAll().Where(...) + Delete(PK) de PaqueteController.DesvincularPrecio)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DELETE FROM dbo.PaquetePrecio
    WHERE PrecioID = @PrecioID
      AND PaqueteID = @PaqueteID;

    SELECT @@ROWCOUNT AS Eliminados;
END
