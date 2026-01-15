
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasaje_GetByHabitacionId
(

	@HabitacionId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PasajeID],
					[PasajeroID],
					[HabitacionID],
					[ButacaID],
					[FechaReserva],
					[FechaCompra],
					[ViajeID],
					[FacturaID],
					[EstadoPasaje],
					[NroVoucher]
				FROM
					[dbo].[Pasaje]
				WHERE
					[HabitacionID] = @HabitacionId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

