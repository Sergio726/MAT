CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetAllEntities]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todos los viajes, filas de entidad (NetTiers F5 - reemplaza ViajeService.GetAll)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ViajeID,
        PaqueteID,
        Origen,
        FechaSalida,
        HoraSalida,
        PaisOrigen,
        PaisDestino,
        Paso,
        Medio,
        BusID,
        FechaRegreso,
        HoraRegreso,
        Descripcion,
        PrecioSemicama,
        PrecioCama,
        PrecioPromocional,
        FechaPromocion,
        nDias,
        nNoches
    FROM dbo.Viaje;
END
