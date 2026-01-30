CREATE PROCEDURE [dbo].[usp_TransHotelHabitacionViaje_GetDistribucionHab] (@ViajeID UNIQUEIDENTIFIER,
																		   @HotelID UNIQUEIDENTIFIER,
																		   @Fecha DATE
																			)
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2019-05-20 
  -- Description:  get distribucion habitaciones

  History:
	18/09/2024 Ruben T. Get Precio and Descripcion
  -- ===========================================*/
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		select  TransHotelHabitacionViajeID = th.Id,
		        h.HabitacionID,
				h.NroHabitacion,
				h.Nombre,
				Tipo = ht.Descripcion,
				h.Capacidad,
				h.Precio as HabPrecio,
				h.Descripcion as HabDescripcion
		from dbo.TransHotelHabitacionViaje th
		inner join dbo.Habitacion h
			on th.HabitacionID = h.HabitacionID
		inner join dbo.HabitacionTipo ht
			on h.Tipo = ht.Id
		where th.ViajeID = @ViajeID
			and th.HotelID = @HotelID
			and th.Fecha = @Fecha

  END