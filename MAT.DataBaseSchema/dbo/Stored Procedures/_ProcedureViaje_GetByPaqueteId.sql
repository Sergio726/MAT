
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Viaje table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_GetByPaqueteId
(

	@PaqueteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[ViajeID],
					[PaqueteID],
					[Origen],
					[FechaSalida],
					[HoraSalida],
					[PaisOrigen],
					[PaisDestino],
					[Paso],
					[Medio],
					[BusID],
					[FechaRegreso],
					[HoraRegreso],
					[Descripcion]
				FROM
					[dbo].[Viaje]
				WHERE
					[PaqueteID] = @PaqueteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

