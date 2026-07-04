CREATE PROCEDURE [dbo].[usp_MAT_Viaje_UpdateEntity]
    @ViajeID UNIQUEIDENTIFIER,
    @PaqueteID UNIQUEIDENTIFIER = NULL,
    @Origen VARCHAR (50) = NULL,
    @FechaSalida DATE = NULL,
    @HoraSalida VARCHAR (50) = NULL,
    @PaisOrigen VARCHAR (50) = NULL,
    @PaisDestino VARCHAR (50) = NULL,
    @Paso VARCHAR (50) = NULL,
    @Medio VARCHAR (50) = NULL,
    @BusID UNIQUEIDENTIFIER = NULL,
    @FechaRegreso DATE = NULL,
    @HoraRegreso VARCHAR (50) = NULL,
    @Descripcion VARCHAR (200) = NULL,
    @PrecioSemicama FLOAT (53) = NULL,
    @PrecioCama FLOAT (53) = NULL,
    @PrecioPromocional FLOAT (53) = NULL,
    @FechaPromocion DATETIME = NULL,
    @nDias INT = NULL,
    @nNoches INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Viaje (NetTiers F5 - reemplaza ViajeService.Update).
 --              Actualiza solo columnas conocidas por la entidad; no toca
 --              TiempoConsentracion, Observaciones, MonedaTipo ni IsPublicWeb.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Viaje
    SET PaqueteID = @PaqueteID,
        Origen = @Origen,
        FechaSalida = @FechaSalida,
        HoraSalida = @HoraSalida,
        PaisOrigen = @PaisOrigen,
        PaisDestino = @PaisDestino,
        Paso = @Paso,
        Medio = @Medio,
        BusID = @BusID,
        FechaRegreso = @FechaRegreso,
        HoraRegreso = @HoraRegreso,
        Descripcion = @Descripcion,
        PrecioSemicama = @PrecioSemicama,
        PrecioCama = @PrecioCama,
        PrecioPromocional = @PrecioPromocional,
        FechaPromocion = @FechaPromocion,
        nDias = @nDias,
        nNoches = @nNoches
    WHERE ViajeID = @ViajeID;
END
