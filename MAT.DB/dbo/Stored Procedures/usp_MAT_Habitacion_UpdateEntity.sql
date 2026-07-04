CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_UpdateEntity]
    @HabitacionID UNIQUEIDENTIFIER,
    @NroHabitacion INT = NULL,
    @Tipo INT,
    @HotelID UNIQUEIDENTIFIER = NULL,
    @Estado INT,
    @Capacidad INT,
    @Ocupacion INT,
    @Nombre VARCHAR (100) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Habitacion (NetTiers F5 - reemplaza
 --              HabitacionService.Update). Actualiza solo columnas conocidas
 --              por la entidad; no toca Precio ni Descripcion.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Habitacion
    SET NroHabitacion = @NroHabitacion,
        Tipo = @Tipo,
        HotelID = @HotelID,
        Estado = @Estado,
        Capacidad = @Capacidad,
        Ocupacion = @Ocupacion,
        Nombre = @Nombre
    WHERE HabitacionID = @HabitacionID;
END
