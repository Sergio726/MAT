
CREATE PROCEDURE [dbo].[usp_MAT_Viaje_ViajeHotel_GetAll] (@ViajeID varchar(max) = null,
														 @HabitacionID varchar(max) = null,
														 @ViajeHotelID varchar(max) = null,
														 @Fecha varchar(10))
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/12/2016 
  -- Description:  Trae la lista de hoteles vinculados a un viaje 
  --Historial : Garcia Sergio LEFT Join Habitacion 
   --2019/06/20 Garcia Sergio add @Fecha
  -- ============================================= 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      SELECT distinct vh.ViajeHotelID,
             vh.ViajeID,
             vh.HotelID,
			 isnull(convert(varchar(10), vh.Desde, 101),'') as Desde,
			 isnull(vh.HoraIngreso,'') as HoraIngreso,
			 isnull(convert(varchar(10),vh.Hasta,101),'') as Hasta,
			 isnull(vh.HoraSalida, '') as HoraSalida,
             h.Nombre,
			 vh.Comentario
	  FROM dbo.ViajeHotel vh
	  INNER JOIN Hotel h ON h.HotelID = vh.HotelID
	  INNER JOIN dbo.TransHotelHabitacionViaje th
			ON th.HotelID = h.HotelID
	  LEFT Join Habitacion Hab 
			ON th.HabitacionID = hab.HabitacionID
	  WHERE vh.ViajeID = CONVERT(uniqueidentifier, @ViajeID)
			and
			(
				(th.HabitacionID = CONVERT(uniqueidentifier, @HabitacionID) and vh.Desde = @Fecha and @ViajeHotelID is null)
				or
				vh.ViajeHotelID = CONVERT(uniqueidentifier, @ViajeHotelID)
			)
  END