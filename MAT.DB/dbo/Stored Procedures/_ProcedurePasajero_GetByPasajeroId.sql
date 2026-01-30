
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Pasajero table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePasajero_GetByPasajeroId]
(

	@PasajeroId uniqueidentifier   
)
AS


				SELECT
					[PasajeroID],
					[Pasaporte],
					[VencimientoPasaporte],
					[EmisionPasaporte],
					[PaisOrigen]
				FROM
					[dbo].[Pasajero]
				WHERE
					[PasajeroID] = @PasajeroId
				SELECT @@ROWCOUNT
					
			



