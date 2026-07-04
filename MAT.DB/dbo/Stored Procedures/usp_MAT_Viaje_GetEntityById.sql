CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetEntityById]
    @ViajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Viaje por ID, fila de entidad (NetTiers F5 - reemplaza ViajeService.GetByViajeId;
 --              el usp_MAT_Viaje_GetViajeByViajeID existente devuelve un DTO joineado y se conserva)
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
    FROM dbo.Viaje
    WHERE ViajeID = @ViajeID;
END
