
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureReservaHabitacion_Update
(

	@ReservaHabitacionId uniqueidentifier   ,

	@OriginalReservaHabitacionId uniqueidentifier   ,

	@HabitacionId uniqueidentifier   ,

	@PasajeId uniqueidentifier   ,

	@FechaReserva date   ,

	@Desde date   ,

	@Hasta date   ,

	@Expiro bit   ,

	@HoraIngreso varchar (10)  ,

	@HoraSalida varchar (10)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[ReservaHabitacion]
				SET
					[ReservaHabitacionID] = @ReservaHabitacionId
					,[HabitacionID] = @HabitacionId
					,[PasajeID] = @PasajeId
					,[FechaReserva] = @FechaReserva
					,[Desde] = @Desde
					,[Hasta] = @Hasta
					,[Expiro] = @Expiro
					,[HoraIngreso] = @HoraIngreso
					,[HoraSalida] = @HoraSalida
				WHERE
[ReservaHabitacionID] = @OriginalReservaHabitacionId