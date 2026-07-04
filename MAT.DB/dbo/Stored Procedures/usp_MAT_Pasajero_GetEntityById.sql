CREATE PROCEDURE [dbo].[usp_MAT_Pasajero_GetEntityById]
    @PasajeroID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pasajero por ID, fila de entidad (NetTiers F7 - reemplaza PasajeroService.Get / GetByPasajeroId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PasajeroID,
        Pasaporte,
        VencimientoPasaporte,
        EmisionPasaporte,
        PaisOrigen
    FROM dbo.Pasajero
    WHERE PasajeroID = @PasajeroID;
END
