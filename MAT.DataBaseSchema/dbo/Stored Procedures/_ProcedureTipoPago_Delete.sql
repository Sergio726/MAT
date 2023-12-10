
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Deletes a record in the TipoPago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_Delete
(

	@TipoPagoId int   
)
AS


				DELETE FROM [dbo].[TipoPago] WITH (ROWLOCK) 
				WHERE
					[TipoPagoID] = @TipoPagoId
					
			

