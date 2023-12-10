
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Voucher table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVoucher_Find
(

	@SearchUsingOR bit   = null ,

	@VoucherId uniqueidentifier   = null ,

	@NroVoucher bigint   = null ,

	@FechaEmision datetime   = null ,

	@VendedorId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [VoucherID]
	, [NroVoucher]
	, [FechaEmision]
	, [VendedorID]
    FROM
	[dbo].[Voucher]
    WHERE 
	 ([VoucherID] = @VoucherId OR @VoucherId IS NULL)
	AND ([NroVoucher] = @NroVoucher OR @NroVoucher IS NULL)
	AND ([FechaEmision] = @FechaEmision OR @FechaEmision IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [VoucherID]
	, [NroVoucher]
	, [FechaEmision]
	, [VendedorID]
    FROM
	[dbo].[Voucher]
    WHERE 
	 ([VoucherID] = @VoucherId AND @VoucherId is not null)
	OR ([NroVoucher] = @NroVoucher AND @NroVoucher is not null)
	OR ([FechaEmision] = @FechaEmision AND @FechaEmision is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	SELECT @@ROWCOUNT			
  END