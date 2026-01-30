
CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_GetHabitacionDisponibilidad] (@ViajeID uniqueidentifier,
																		 @HotelID uniqueidentifier,
																		 @Fecha date)
AS 
/* =============================================
 Author:		Garcia Sergio
 Create date: 2017-06-30
 Description:	get disponibility of habitacion by viaje and hotel

 Historial:
 2019-06-18	Garcia Sergio	Add @Fecha parameters
 =============================================*/


BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT h.HabitacionID, 
		   NroHabitacion = isnull(h.NroHabitacion,''),
		   h.Tipo, 
		   HabitacionNombre = isnull(h.Nombre,''),
		   HabitacionTipo = t.Descripcion, 
		   HotelNombre = ht.Nombre, 
		   --Disponibilidad = (h.Capacidad - count(rh.HabitacionID))
		   Disponibilidad = (h.Capacidad - x.Ocupacion)
	FROM   
		   dbo.TransHotelHabitacionViaje th
		   INNER JOIN dbo.Habitacion h 
				  ON th.HabitacionID = h.HabitacionID
		   INNER JOIN dbo.HabitacionTipo t
				  ON h.Tipo = t.Id 
		   INNER JOIN dbo.Hotel ht
				  ON th.HotelID = ht.HotelID
		   CROSS APPLY(
				select count(*) Ocupacion 
				from ReservaHabitacion rh 
				where rh.HabitacionID = h.HabitacionID 
			
		   ) x
					 
	WHERE  th.HotelID = @HotelID
	AND	   th.Fecha = @Fecha
	AND    th.ViajeID = @ViajeID
	GROUP  BY h.HabitacionID, 
			  h.NroHabitacion, 
			  h.Tipo, 
			  ht.Nombre, 
			  h.Capacidad, 
			  h.Nombre,
			  t.Descripcion ,x.Ocupacion
	ORDER  BY h.NroHabitacion 

END