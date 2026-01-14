
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the ReservaHabitacion table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureReservaHabitacion_Find
(

	@SearchUsingOR bit   = null ,

	@ReservaHabitacionId uniqueidentifier   = null ,

	@HabitacionId uniqueidentifier   = null ,

	@PasajeId uniqueidentifier   = null ,

	@FechaReserva date   = null ,

	@Desde date   = null ,

	@Hasta date   = null ,

	@Expiro bit   = null ,

	@HoraIngreso varchar (10)  = null ,

	@HoraSalida varchar (10)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ReservaHabitacionID]
	, [HabitacionID]
	, [PasajeID]
	, [FechaReserva]
	, [Desde]
	, [Hasta]
	, [Expiro]
	, [HoraIngreso]
	, [HoraSalida]
    FROM
	[dbo].[ReservaHabitacion]
    WHERE 
	 ([ReservaHabitacionID] = @ReservaHabitacionId OR @ReservaHabitacionId IS NULL)
	AND ([HabitacionID] = @HabitacionId OR @HabitacionId IS NULL)
	AND ([PasajeID] = @PasajeId OR @PasajeId IS NULL)
	AND ([FechaReserva] = @FechaReserva OR @FechaReserva IS NULL)
	AND ([Desde] = @Desde OR @Desde IS NULL)
	AND ([Hasta] = @Hasta OR @Hasta IS NULL)
	AND ([Expiro] = @Expiro OR @Expiro IS NULL)
	AND ([HoraIngreso] = @HoraIngreso OR @HoraIngreso IS NULL)
	AND ([HoraSalida] = @HoraSalida OR @HoraSalida IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ReservaHabitacionID]
	, [HabitacionID]
	, [PasajeID]
	, [FechaReserva]
	, [Desde]
	, [Hasta]
	, [Expiro]
	, [HoraIngreso]
	, [HoraSalida]
    FROM
	[dbo].[ReservaHabitacion]
    WHERE 
	 ([ReservaHabitacionID] = @ReservaHabitacionId AND @ReservaHabitacionId is not null)
	OR ([HabitacionID] = @HabitacionId AND @HabitacionId is not null)
	OR ([PasajeID] = @PasajeId AND @PasajeId is not null)
	OR ([FechaReserva] = @FechaReserva AND @FechaReserva is not null)
	OR ([Desde] = @Desde AND @Desde is not null)
	OR ([Hasta] = @Hasta AND @Hasta is not null)
	OR ([Expiro] = @Expiro AND @Expiro is not null)
	OR ([HoraIngreso] = @HoraIngreso AND @HoraIngreso is not null)
	OR ([HoraSalida] = @HoraSalida AND @HoraSalida is not null)
	SELECT @@ROWCOUNT			
  END