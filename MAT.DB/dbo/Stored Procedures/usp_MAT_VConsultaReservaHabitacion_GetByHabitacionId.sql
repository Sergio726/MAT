CREATE PROCEDURE [dbo].[usp_MAT_VConsultaReservaHabitacion_GetByHabitacionId]
    @HabitacionID UNIQUEIDENTIFIER,
    @Expiro BIT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Consulta de reservas por habitacion sobre la vista
 --              vConsultaReservaHabitacion, con filtro opcional de expiradas
 --              (NetTiers F5 - reemplaza VConsultaReservaHabitacionService.GetAll + LINQ)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ReservaHabitacionID,
        HotelID,
        Expiro,
        HabitacionID,
        Capacidad,
        Ocupacion,
        Estado,
        Desde,
        Hasta
    FROM dbo.vConsultaReservaHabitacion
    WHERE HabitacionID = @HabitacionID
      AND (@Expiro IS NULL OR Expiro = @Expiro);
END
