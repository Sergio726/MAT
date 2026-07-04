CREATE PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_UpdateEntity]
    @ReservaHabitacionID UNIQUEIDENTIFIER,
    @HabitacionID UNIQUEIDENTIFIER = NULL,
    @PasajeID UNIQUEIDENTIFIER = NULL,
    @FechaReserva DATE = NULL,
    @Desde DATE = NULL,
    @Hasta DATE = NULL,
    @Expiro BIT = NULL,
    @HoraIngreso VARCHAR (10) = NULL,
    @HoraSalida VARCHAR (10) = NULL,
    @PasajeroID UNIQUEIDENTIFIER = NULL,
    @ViajeID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad ReservaHabitacion (NetTiers F5 - reemplaza
 --              ReservaHabitacionService.Update). No toca TransHotelHabitacionViajeID
 --              (columna desconocida por la entidad).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.ReservaHabitacion
    SET HabitacionID = @HabitacionID,
        PasajeID = @PasajeID,
        FechaReserva = @FechaReserva,
        Desde = @Desde,
        Hasta = @Hasta,
        Expiro = @Expiro,
        HoraIngreso = @HoraIngreso,
        HoraSalida = @HoraSalida,
        PasajeroID = @PasajeroID,
        ViajeID = @ViajeID
    WHERE ReservaHabitacionID = @ReservaHabitacionID;
END
