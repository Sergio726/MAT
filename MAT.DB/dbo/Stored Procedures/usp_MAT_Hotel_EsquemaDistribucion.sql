
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_EsquemaDistribucion](@ViajeId uniqueidentifier,
														   @HotelId uniqueidentifier,
														   @Fecha varchar(10),
														   @FechaD varchar(10))
AS
 /* -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2017-08-18
  -- Description:  EsquemaDistribucion
  History
  2019-06-21	Garcia Sergio Sebastian		add @Fecha
  2020-11-13	Garcia Sergio sebastian, chagen date filter @FechaD by @Fecha
  -- ============================================= 
  */
 BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

			select hab.HabitacionID
				,hab.NroHabitacion
				,HabNombre = hab.Nombre 
				,HabTipo = hab.Tipo
				,HabTipoDescripcion = ht.Descripcion
				,pr.PersonaID
				,PersonaApellido = pr.Apellido
				,PersonaNombre = pr.Nombre
				,rh.ViajeID
				,HotelNombre = h.Nombre
				,HotelIngreso = vh.Desde
				,HotelEgreso = vh.Hasta
			from  dbo.TransHotelHabitacionViaje th
			inner join dbo.Habitacion hab
				on th.HabitacionID = hab.HabitacionID
			inner join dbo.HabitacionTipo ht
				on hab.Tipo = ht.Id
			inner join dbo.Hotel h
				on th.HotelID = h.HotelID
			inner join dbo.ViajeHotel vh
				on h.HotelID = vh.HotelID
				--and vh.Desde =  @Fecha
			left join dbo.ReservaHabitacion rh
				on rh.HabitacionID = hab.HabitacionID
				--and rh.Desde = CONVERT(DATE, @Fecha)
			left join dbo.Persona pr
				on rh.PasajeroID = pr.PersonaID
				
			where 
			th.HotelID = @HotelId
			and th.ViajeID = @ViajeId
			and th.Fecha = @Fecha
			and (vh.Desde = @FechaD or vh.Desde = @Fecha)
			

			group by hab.HabitacionID,
					hab.NroHabitacion,
					hab.Nombre,
					hab.Tipo, 
					pr.PersonaID,
					pr.Apellido,
					pr.Nombre,
					rh.ViajeID,
					h.Nombre,
					ht.Descripcion,
					vh.Desde,
					vh.Hasta

			order by hab.NroHabitacion
END