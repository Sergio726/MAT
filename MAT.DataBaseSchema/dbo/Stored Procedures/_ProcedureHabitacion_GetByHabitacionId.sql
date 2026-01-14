
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Habitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHabitacion_GetByHabitacionId
(

	@HabitacionId uniqueidentifier   
)
AS


				SELECT
					[HabitacionID],
					[NroHabitacion],
					[Tipo],
					[HotelID],
					[Estado],
					[Capacidad],
					[Ocupacion]
				FROM
					[dbo].[Habitacion]
				WHERE
					[HabitacionID] = @HabitacionId
				SELECT @@ROWCOUNT
					
			

