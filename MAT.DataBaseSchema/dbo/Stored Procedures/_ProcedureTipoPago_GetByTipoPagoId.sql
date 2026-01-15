
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the TipoPago table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_GetByTipoPagoId
(

	@TipoPagoId int   
)
AS


				SELECT
					[TipoPagoID],
					[Descripcion]
				FROM
					[dbo].[TipoPago]
				WHERE
					[TipoPagoID] = @TipoPagoId
				SELECT @@ROWCOUNT
					
			

