CREATE PROCEDURE [dbo].[usp_MAT_Voucher_GetHotelByPasajeID](@PasajeID	uniqueidentifier)
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 10/04/2017
  -- Description:  GET HOTELs BY PASAJE ID
  History
  2017-06-19	Garcia Sergio	Add column NombreHabitacion
  2017-08-03	Garcia Sergio	Add order by Desde ASC
  2019-07-13	Garcia Sergio	join with TransHotelHabitacionViaje
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	SELECT ht.Nombre       ,                    
		   ht.Telefono     ,                    
		   ht.Direccion    ,                    
		   h.NroHabitacion ,
		   h.Nombre AS NombreHabitacion,                
		   CONVERT(VARCHAR(10), rh.desde, 103) AS Desde, 
		   CONVERT(VARCHAR(10), rh.hasta, 103) AS Hasta 
	FROM   dbo.reservahabitacion rh 
		   INNER JOIN dbo.habitacion h 
				   ON rh.habitacionid = h.habitacionid 
		   INNER JOIN dbo.TransHotelHabitacionViaje th
				   ON th.HabitacionID = h.HabitacionID
		   INNER JOIN dbo.hotel ht 
				   ON ht.hotelid = th.HotelID 
	WHERE  rh.pasajeid = @PasajeID 
	ORDER BY rh.Desde ASC

END