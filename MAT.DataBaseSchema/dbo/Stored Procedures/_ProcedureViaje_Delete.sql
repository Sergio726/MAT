
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Viaje table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureViaje_Delete
(

	@ViajeId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Viaje] WITH (ROWLOCK) 
				WHERE
					[ViajeID] = @ViajeId
					
			

