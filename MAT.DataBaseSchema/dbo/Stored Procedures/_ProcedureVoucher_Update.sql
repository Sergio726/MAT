
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_Update
(

	@VoucherId uniqueidentifier   ,

	@OriginalVoucherId uniqueidentifier   ,

	@NroVoucher bigint   ,

	@FechaEmision datetime   ,

	@VendedorId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Voucher]
				SET
					[VoucherID] = @VoucherId
					,[FechaEmision] = @FechaEmision
					,[VendedorID] = @VendedorId
				WHERE
[VoucherID] = @OriginalVoucherId