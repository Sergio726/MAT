
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Pasaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasaje_GetByTipoId
(

	@TipoId int   
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
					[TipoID]
				FROM
					[dbo].[Pasaje]
				WHERE
					[TipoID] = @TipoId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

