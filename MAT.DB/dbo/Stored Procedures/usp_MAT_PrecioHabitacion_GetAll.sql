CREATE PROCEDURE [dbo].[usp_MAT_PrecioHabitacion_GetAll]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Todos los precios de habitacion (NetTiers F4 - reemplaza
 --              PrecioHabitacionService.GetAll usado por PlanillaHotelModel)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PrecioHabitacionID,
        TipoHabitacion,
        HotelID,
        FechaRegistro,
        Activo,
        Precio
    FROM dbo.PrecioHabitacion;
END
