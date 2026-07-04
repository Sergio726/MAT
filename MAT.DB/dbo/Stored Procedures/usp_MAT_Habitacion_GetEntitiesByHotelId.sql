CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_GetEntitiesByHotelId]
    @HotelID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Habitaciones de un hotel, filas de entidad con tipos nativos
 --              (NetTiers F5 - reemplaza HabitacionService.GetByHotelId;
 --              el usp_MAT_Habitacion_GetByHotelId existente devuelve un DTO
 --              display con tipo/estado como texto y se conserva)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        habitacionid = h.HabitacionID,
        nrohabitacion = h.NroHabitacion,
        tipo = h.Tipo,
        h.Estado,
        h.Capacidad,
        h.Ocupacion,
        nombre = h.Nombre,
        h.HotelID
    FROM dbo.Habitacion h
    WHERE h.HotelID = @HotelID;
END
