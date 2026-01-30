
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureServicio_Delete]
(

	@ServicioId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Servicio] WITH (ROWLOCK) 
				WHERE
					[ServicioID] = @ServicioId
					
			



