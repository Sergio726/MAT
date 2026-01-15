
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHabitacion_Update
(

	@HabitacionId uniqueidentifier   ,

	@OriginalHabitacionId uniqueidentifier   ,

	@NroHabitacion varchar (50)  ,

	@Tipo int   ,

	@HotelId uniqueidentifier   ,

	@Estado int   ,

	@Capacidad int   ,

	@Ocupacion int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Habitacion]
				SET
					[HabitacionID] = @HabitacionId
					,[NroHabitacion] = @NroHabitacion
					,[Tipo] = @Tipo
					,[HotelID] = @HotelId
					,[Estado] = @Estado
					,[Capacidad] = @Capacidad
					,[Ocupacion] = @Ocupacion
				WHERE
[HabitacionID] = @OriginalHabitacionId 
				
			

