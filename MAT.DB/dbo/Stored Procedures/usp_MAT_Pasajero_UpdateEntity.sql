CREATE PROCEDURE [dbo].[usp_MAT_Pasajero_UpdateEntity]
    @PasajeroID UNIQUEIDENTIFIER,
    @Pasaporte VARCHAR (100) = NULL,
    @VencimientoPasaporte DATE = NULL,
    @EmisionPasaporte DATE = NULL,
    @PaisOrigen VARCHAR (50) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Pasajero (NetTiers F7 - reemplaza PasajeroService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Pasajero
    SET Pasaporte = @Pasaporte,
        VencimientoPasaporte = @VencimientoPasaporte,
        EmisionPasaporte = @EmisionPasaporte,
        PaisOrigen = @PaisOrigen
    WHERE PasajeroID = @PasajeroID;
END
