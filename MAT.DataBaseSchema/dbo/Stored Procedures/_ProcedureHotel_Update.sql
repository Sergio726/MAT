
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_Update
(

	@HotelId uniqueidentifier   ,

	@OriginalHotelId uniqueidentifier   ,

	@Nombre varchar (50)  ,

	@Direccion varchar (50)  ,

	@Cp varchar (50)  ,

	@Telefono varchar (50)  ,

	@Email varchar (50)  ,

	@Contacto varchar (50)  ,

	@CantidadHabitaciones int   ,

	@Categoria int   ,

	@Child1 varchar (50)  ,

	@Child2 varchar (50)  ,

	@ChildHabitacion int   ,

	@CheckIn varchar (8)  ,

	@CheckOut varchar (8)  ,

	@GoogleMapHtml varchar (200)  ,

	@LocalidadId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Hotel]
				SET
					[HotelID] = @HotelId
					,[Nombre] = @Nombre
					,[Direccion] = @Direccion
					,[CP] = @Cp
					,[Telefono] = @Telefono
					,[Email] = @Email
					,[Contacto] = @Contacto
					,[CantidadHabitaciones] = @CantidadHabitaciones
					,[Categoria] = @Categoria
					,[Child1] = @Child1
					,[Child2] = @Child2
					,[ChildHabitacion] = @ChildHabitacion
					,[CheckIn] = @CheckIn
					,[CheckOut] = @CheckOut
					,[GoogleMapHtml] = @GoogleMapHtml
					,[LocalidadID] = @LocalidadId
				WHERE
[HotelID] = @OriginalHotelId 
				
			

