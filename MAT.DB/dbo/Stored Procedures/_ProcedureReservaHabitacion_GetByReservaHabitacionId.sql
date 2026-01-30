
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the ReservaHabitacion table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureReservaHabitacion_GetByReservaHabitacionId]
(

	@ReservaHabitacionId uniqueidentifier   
)
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
				WHERE
					[ReservaHabitacionID] = @ReservaHabitacionId
				SELECT @@ROWCOUNT
					
			



