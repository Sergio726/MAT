
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Deletes a record in the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_Delete
(

	@VoucherId uniqueidentifier   
)
AS


				DELETE FROM [dbo].[Voucher] WITH (ROWLOCK) 
				WHERE
					[VoucherID] = @VoucherId