
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the ReservaHabitacion table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureReservaHabitacion_GetByPasajeId]
(

	@PasajeId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[PasajeID] = @PasajeId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			



