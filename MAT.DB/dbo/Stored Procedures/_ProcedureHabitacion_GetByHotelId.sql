
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Habitacion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureHabitacion_GetByHotelId]
(

	@HotelId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[HotelID] = @HotelId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			



