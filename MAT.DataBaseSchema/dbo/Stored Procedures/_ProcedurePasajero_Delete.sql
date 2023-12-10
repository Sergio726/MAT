
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Pasajero table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePasajero_Delete
(

	@PasajeroId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pasajero] WITH (ROWLOCK) 
				WHERE
					[PasajeroID] = @PasajeroId
					
			

