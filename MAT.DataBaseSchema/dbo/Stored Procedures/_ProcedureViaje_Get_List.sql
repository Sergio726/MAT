
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_Get_List

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
					
				SELECT @@ROWCOUNT
			

