
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Pago table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePago_Get_List]

AS


				
				SELECT
					[PagoID],
					[FechaPago],
					[Monto],
					[TipoPago],
					[VendedorId],
					[NroRecibo]
				FROM
					[dbo].[Pago]
					
				SELECT @@ROWCOUNT

