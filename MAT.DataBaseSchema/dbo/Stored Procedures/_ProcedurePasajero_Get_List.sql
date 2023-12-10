
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajero_Get_List

AS


				
				SELECT
					[PasajeroID],
					[Pasaporte],
					[VencimientoPasaporte],
					[EmisionPasaporte],
					[PaisOrigen]
				FROM
					[dbo].[Pasajero]
					
				SELECT @@ROWCOUNT
			

