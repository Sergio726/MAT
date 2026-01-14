
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Transporte table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTransporte_Delete
(

	@TransporteId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Transporte] WITH (ROWLOCK) 
				WHERE
					[TransporteID] = @TransporteId
					
			

