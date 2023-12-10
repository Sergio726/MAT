
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_Get_List

AS


				
				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
					
				SELECT @@ROWCOUNT