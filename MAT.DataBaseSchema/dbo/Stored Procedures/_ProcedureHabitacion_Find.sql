
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Habitacion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHabitacion_Find
(

	@SearchUsingOR bit   = null ,

	@HabitacionId uniqueidentifier   = null ,

	@NroHabitacion varchar (50)  = null ,

	@Tipo int   = null ,

	@HotelId uniqueidentifier   = null ,

	@Estado int   = null ,

	@Capacidad int   = null ,

	@Ocupacion int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [HabitacionID]
	, [NroHabitacion]
	, [Tipo]
	, [HotelID]
	, [Estado]
	, [Capacidad]
	, [Ocupacion]
    FROM
	[dbo].[Habitacion]
    WHERE 
	 ([HabitacionID] = @HabitacionId OR @HabitacionId IS NULL)
	AND ([NroHabitacion] = @NroHabitacion OR @NroHabitacion IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
	AND ([Capacidad] = @Capacidad OR @Capacidad IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [HabitacionID]
	, [NroHabitacion]
	, [Tipo]
	, [HotelID]
	, [Estado]
	, [Capacidad]
	, [Ocupacion]
    FROM
	[dbo].[Habitacion]
    WHERE 
	 ([HabitacionID] = @HabitacionId AND @HabitacionId is not null)
	OR ([NroHabitacion] = @NroHabitacion AND @NroHabitacion is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	OR ([Capacidad] = @Capacidad AND @Capacidad is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	SELECT @@ROWCOUNT			
  END
				

