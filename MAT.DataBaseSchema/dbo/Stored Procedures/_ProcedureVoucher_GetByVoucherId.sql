
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Voucher table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_GetByVoucherId
(

	@VoucherId uniqueidentifier   
)
AS


				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
				WHERE
					[VoucherID] = @VoucherId
				SELECT @@ROWCOUNT