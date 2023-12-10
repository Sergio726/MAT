
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Hotel table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHotel_Find
(

	@SearchUsingOR bit   = null ,

	@HotelId uniqueidentifier   = null ,

	@Nombre varchar (50)  = null ,

	@Direccion varchar (50)  = null ,

	@Cp varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@Contacto varchar (50)  = null ,

	@CantidadHabitaciones int   = null ,

	@Categoria int   = null ,

	@Child1 varchar (50)  = null ,

	@Child2 varchar (50)  = null ,

	@ChildHabitacion int   = null ,

	@CheckIn varchar (8)  = null ,

	@CheckOut varchar (8)  = null ,

	@GoogleMapHtml varchar (200)  = null ,

	@LocalidadId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [HotelID]
	, [Nombre]
	, [Direccion]
	, [CP]
	, [Telefono]
	, [Email]
	, [Contacto]
	, [CantidadHabitaciones]
	, [Categoria]
	, [Child1]
	, [Child2]
	, [ChildHabitacion]
	, [CheckIn]
	, [CheckOut]
	, [GoogleMapHtml]
	, [LocalidadID]
    FROM
	[dbo].[Hotel]
    WHERE 
	 ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([Nombre] = @Nombre OR @Nombre IS NULL)
	AND ([Direccion] = @Direccion OR @Direccion IS NULL)
	AND ([CP] = @Cp OR @Cp IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([Contacto] = @Contacto OR @Contacto IS NULL)
	AND ([CantidadHabitaciones] = @CantidadHabitaciones OR @CantidadHabitaciones IS NULL)
	AND ([Categoria] = @Categoria OR @Categoria IS NULL)
	AND ([Child1] = @Child1 OR @Child1 IS NULL)
	AND ([Child2] = @Child2 OR @Child2 IS NULL)
	AND ([ChildHabitacion] = @ChildHabitacion OR @ChildHabitacion IS NULL)
	AND ([CheckIn] = @CheckIn OR @CheckIn IS NULL)
	AND ([CheckOut] = @CheckOut OR @CheckOut IS NULL)
	AND ([GoogleMapHtml] = @GoogleMapHtml OR @GoogleMapHtml IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [HotelID]
	, [Nombre]
	, [Direccion]
	, [CP]
	, [Telefono]
	, [Email]
	, [Contacto]
	, [CantidadHabitaciones]
	, [Categoria]
	, [Child1]
	, [Child2]
	, [ChildHabitacion]
	, [CheckIn]
	, [CheckOut]
	, [GoogleMapHtml]
	, [LocalidadID]
    FROM
	[dbo].[Hotel]
    WHERE 
	 ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([Nombre] = @Nombre AND @Nombre is not null)
	OR ([Direccion] = @Direccion AND @Direccion is not null)
	OR ([CP] = @Cp AND @Cp is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([Contacto] = @Contacto AND @Contacto is not null)
	OR ([CantidadHabitaciones] = @CantidadHabitaciones AND @CantidadHabitaciones is not null)
	OR ([Categoria] = @Categoria AND @Categoria is not null)
	OR ([Child1] = @Child1 AND @Child1 is not null)
	OR ([Child2] = @Child2 AND @Child2 is not null)
	OR ([ChildHabitacion] = @ChildHabitacion AND @ChildHabitacion is not null)
	OR ([CheckIn] = @CheckIn AND @CheckIn is not null)
	OR ([CheckOut] = @CheckOut AND @CheckOut is not null)
	OR ([GoogleMapHtml] = @GoogleMapHtml AND @GoogleMapHtml is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	SELECT @@ROWCOUNT			
  END
				

