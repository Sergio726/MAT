
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_Delete
(

	@PagoId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Pago] WITH (ROWLOCK) 
				WHERE
					[PagoID] = @PagoId
					
			

