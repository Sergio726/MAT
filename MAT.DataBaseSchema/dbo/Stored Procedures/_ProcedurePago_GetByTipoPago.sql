
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Pago table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePago_GetByTipoPago
(

	@TipoPago int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago]
				FROM
					[dbo].[Pago]
				WHERE
					[TipoPago] = @TipoPago
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

