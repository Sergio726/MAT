
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureReservaHabitacion_Insert
(

	@ReservaHabitacionId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[ReservaHabitacion]
					(
					[ReservaHabitacionID]
					,[HabitacionID]
					,[PasajeID]
					,[FechaReserva]
					,[Desde]
					,[Hasta]
					,[Expiro]
					,[HoraIngreso]
					,[HoraSalida]
					)
				VALUES
					(
					@ReservaHabitacionId
					,@HabitacionId
					,@PasajeId
					,@FechaReserva
					,@Desde
					,@Hasta
					,@Expiro
					,@HoraIngreso
					,@HoraSalida
					)