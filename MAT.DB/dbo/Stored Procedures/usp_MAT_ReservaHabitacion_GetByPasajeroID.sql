
  CREATE PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_GetByPasajeroID] (@PasajeroID uniqueidentifier, 
                                                                    @ViajeID uniqueidentifier,
																	@Fecha date) 
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 03-12-2016 
  -- Description:  Trae la habitacion y el hotel que el pasajero esta ocupando 
  History:

  2017-07-01	Garcia Sergio: add param @ViajeID
  2019-06-21	Garcia Sergio: add param @Fecha and delete @HotelID
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      SELECT rh.HabitacionID, 
             hab.HotelID 
      FROM   dbo.ReservaHabitacion rh 
             INNER JOIN dbo.Habitacion hab 
                     ON rh.HabitacionID = hab.HabitacionID
      WHERE  rh.ViajeID = @ViajeID
			 AND rh.PasajeroID = @PasajeroID
             AND rh.Desde = @Fecha
  END