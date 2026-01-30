
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the ReservaHabitacion table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureReservaHabitacion_Get_List]

AS


				
				SELECT
					[ReservaHabitacionID],
					[HabitacionID],
					[PasajeID],
					[FechaReserva],
					[Desde],
					[Hasta],
					[Expiro],
					[HoraIngreso],
					[HoraSalida]
				FROM
					[dbo].[ReservaHabitacion]
					
				SELECT @@ROWCOUNT
			



