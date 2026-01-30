
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Voucher table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureVoucher_GetByVendedorId]
(

	@VendedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[VoucherID],
					[NroVoucher],
					[FechaEmision],
					[VendedorID]
				FROM
					[dbo].[Voucher]
				WHERE
					[VendedorID] = @VendedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			



