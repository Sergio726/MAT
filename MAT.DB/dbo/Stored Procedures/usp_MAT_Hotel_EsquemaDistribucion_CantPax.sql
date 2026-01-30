
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_EsquemaDistribucion_CantPax](@ViajeId uniqueidentifier,
														          @HotelId uniqueidentifier, 
																  @Fecha date)
AS
 /* -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2017-08-24
  -- Description:  EsquemaDistribucion Cant Pax
  History

  2019-06-22	Garcia Sergio S add @Fecha
  
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

			select COUNT(iif(rh.PasajeroID is not null,1,0))
			from  dbo.Habitacion hab
			INNER JOIN dbo.ReservaHabitacion rh
			on rh.HabitacionID = hab.HabitacionID
			INNER JOIN dbo.TransHotelHabitacionViaje th
				on th.HabitacionID = hab.HabitacionID
				and rh.ViajeID = th.ViajeID
				and th.HotelID = @HotelId
			where 
			rh.ViajeID = @ViajeId
			and rh.Desde = @Fecha

			

END