CREATE PROCEDURE [dbo].[usp_MAT_Pasajero_InsertEntity]
    @PasajeroID UNIQUEIDENTIFIER,
    @Pasaporte VARCHAR (100) = NULL,
    @VencimientoPasaporte DATE = NULL,
    @EmisionPasaporte DATE = NULL,
    @PaisOrigen VARCHAR (50) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Insert de la entidad Pasajero (NetTiers F7 - reemplaza PasajeroService.Save)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Pasajero (PasajeroID, Pasaporte, VencimientoPasaporte, EmisionPasaporte, PaisOrigen)
    VALUES (@PasajeroID, @Pasaporte, @VencimientoPasaporte, @EmisionPasaporte, @PaisOrigen);
END
