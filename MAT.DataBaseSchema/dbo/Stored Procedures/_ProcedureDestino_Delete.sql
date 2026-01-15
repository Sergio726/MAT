
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Destino table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureDestino_Delete
(

	@DestinoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Destino] WITH (ROWLOCK) 
				WHERE
					[DestinoID] = @DestinoId
					
			

