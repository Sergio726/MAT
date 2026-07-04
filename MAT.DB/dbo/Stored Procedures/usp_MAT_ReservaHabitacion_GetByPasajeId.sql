CREATE PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_GetByPasajeId]
    @PasajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Reservas de habitacion por pasaje, filas de entidad (NetTiers F5 -
 --              reemplaza ReservaHabitacionService.GetByPasajeId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ReservaHabitacionID,
        HabitacionID,
        PasajeID,
        FechaReserva,
        Desde,
        Hasta,
        Expiro,
        HoraIngreso,
        HoraSalida,
        PasajeroID,
        ViajeID
    FROM dbo.ReservaHabitacion
    WHERE PasajeID = @PasajeID;
END
