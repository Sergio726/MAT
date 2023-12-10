
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Voucher table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_Insert
(

	@VoucherId uniqueidentifier    OUTPUT,

	@NroVoucher bigint    OUTPUT,

	@FechaEmision datetime   ,

	@VendedorId uniqueidentifier   
)
AS


				
				INSERT INTO [dbo].[Voucher]
					(
					[VoucherID]
					,[FechaEmision]
					,[VendedorID]
					)
				VALUES
					(
					@VoucherId
					,@FechaEmision
					,@VendedorId
					)
				
				-- Get the identity value
				SET @NroVoucher = SCOPE_IDENTITY()