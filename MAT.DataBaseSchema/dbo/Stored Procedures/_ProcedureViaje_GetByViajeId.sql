
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Viaje table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_GetByViajeId
(

	@ViajeId uniqueidentifier   
)
AS


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
					[ViajeID] = @ViajeId
				SELECT @@ROWCOUNT
					
			

