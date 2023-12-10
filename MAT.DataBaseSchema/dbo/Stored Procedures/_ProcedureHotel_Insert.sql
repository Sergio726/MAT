
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Hotel table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_Insert
(

	@HotelId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Hotel]
					(
					[HotelID]
					,[Nombre]
					,[Direccion]
					,[CP]
					,[Telefono]
					,[Email]
					,[Contacto]
					,[CantidadHabitaciones]
					,[Categoria]
					,[Child1]
					,[Child2]
					,[ChildHabitacion]
					,[CheckIn]
					,[CheckOut]
					,[GoogleMapHtml]
					,[LocalidadID]
					)
				VALUES
					(
					@HotelId
					,@Nombre
					,@Direccion
					,@Cp
					,@Telefono
					,@Email
					,@Contacto
					,@CantidadHabitaciones
					,@Categoria
					,@Child1
					,@Child2
					,@ChildHabitacion
					,@CheckIn
					,@CheckOut
					,@GoogleMapHtml
					,@LocalidadId
					)
				
									
							
			

