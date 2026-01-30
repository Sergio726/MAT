CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_GetHabitacionDisponiblesByViaje] 
(
	@ViajeID uniqueidentifier,
	@HotelID uniqueidentifier = null,
	@Fecha date = null
)
AS 
/* =============================================
 Author: Ruben Tejerina
 Create date: 2024/10/08
 Description:	get disponibility of habitacion by viaje
 This SP is called by API BACKEND

 2024-10-31 Ruben Tejerina Add condition Estado = 0 

 =============================================*/


BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	SELECT 
		ht.HotelID,
		h.HabitacionID, 
		NroHabitacion = isnull(h.NroHabitacion,''),
		h.Tipo, 
		HabitacionNombre = isnull(h.Nombre,''),
		HabitacionTipo = t.Descripcion, 
		HotelNombre = ht.Nombre, 		
		Disponibilidad = (h.Capacidad - x.Ocupacion)
		,h.Capacidad
		,th.Fecha
	    ,h.Precio
	    ,HabitacionDescripcion = h.Descripcion
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
					 
	WHERE
		th.ViajeID = @ViajeID
		and h.Estado = 0
	
	ORDER  BY h.NroHabitacion 

END