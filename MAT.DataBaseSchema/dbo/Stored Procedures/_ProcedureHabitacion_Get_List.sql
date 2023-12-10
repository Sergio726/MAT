
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Habitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureHabitacion_Get_List

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
					
				SELECT @@ROWCOUNT
			

