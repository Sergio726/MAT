CREATE PROCEDURE [dbo].[usp_MAT_Viaje_InsertEntity]
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
 -- Description: Alta de entidad Viaje (NetTiers F5 - reemplaza ViajeService.Insert).
 --              MonedaTipo e IsPublicWeb toman los defaults de la tabla.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO dbo.Viaje (
        ViajeID, PaqueteID, Origen, FechaSalida, HoraSalida, PaisOrigen, PaisDestino,
        Paso, Medio, BusID, FechaRegreso, HoraRegreso, Descripcion,
        PrecioSemicama, PrecioCama, PrecioPromocional, FechaPromocion, nDias, nNoches)
    VALUES (
        @ViajeID, @PaqueteID, @Origen, @FechaSalida, @HoraSalida, @PaisOrigen, @PaisDestino,
        @Paso, @Medio, @BusID, @FechaRegreso, @HoraRegreso, @Descripcion,
        @PrecioSemicama, @PrecioCama, @PrecioPromocional, @FechaPromocion, @nDias, @nNoches);
END
