CREATE PROCEDURE [dbo].[usp_MAT_Habitacion_GetByHotelId](@HotelId uniqueidentifier)

AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 06/17/2017
  -- Description: get habitacion by HotelId
  --History
  --2017-06-17  Garcia Sergio: created
    2017-06-23	Garcia Sergio: add HabitacionTipo
	2018-11-06	Garcia Sergio:  add Hotel column
  -- ============================================= */

  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT 
			   h.nrohabitacion,
			   h.nombre,
			   tipo = ht.Descripcion, 
			   Hotel = ho.Nombre,
			   estado = iif (h.estado = 0,'LIBRE','OCUPADO'),
			   h.capacidad, 
			   h.ocupacion, 
			   h.habitacionid, 
			   h.hotelid
		FROM   dbo.Habitacion h 
		INNER JOIN dbo.HabitacionTipo ht
			on h.Tipo = ht.Id
		INNER JOIN dbo.Hotel ho
			on h.HotelID = ho.HotelID
		WHERE  h.HotelID = @HotelId

       
  END