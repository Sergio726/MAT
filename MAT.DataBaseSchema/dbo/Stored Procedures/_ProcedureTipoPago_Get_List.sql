
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the TipoPago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoPago_Get_List

AS


				
				SELECT
					[TipoPagoID],
					[Descripcion]
				FROM
					[dbo].[TipoPago]
					
				SELECT @@ROWCOUNT
			

