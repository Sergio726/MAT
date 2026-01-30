
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureHabitacion_Insert]
(

	@HabitacionId uniqueidentifier    OUTPUT,

	@NroHabitacion varchar (50)  ,

	@Tipo int   ,

	@HotelId uniqueidentifier   ,

	@Estado int   ,

	@Capacidad int   ,

	@Ocupacion int   
)
AS


				
				INSERT INTO [dbo].[Habitacion]
					(
					[HabitacionID]
					,[NroHabitacion]
					,[Tipo]
					,[HotelID]
					,[Estado]
					,[Capacidad]
					,[Ocupacion]
					)
				VALUES
					(
					@HabitacionId
					,@NroHabitacion
					,@Tipo
					,@HotelId
					,@Estado
					,@Capacidad
					,@Ocupacion
					)
				
									
							
			



