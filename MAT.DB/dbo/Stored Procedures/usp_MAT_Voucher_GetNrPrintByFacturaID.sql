CREATE PROCEDURE [dbo].[usp_MAT_Voucher_GetNrPrintByFacturaID](@FacturaID	uniqueidentifier)
AS 
 /* -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 08/21/2017
  -- Description:  get nrPrint of the voucher
  History
 
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT TOP 1  v.NrPrint	
		FROM   dbo.pasaje pj 
			   INNER JOIN dbo.voucher v 
					ON PJ.VoucherID = V.VoucherID
		WHERE  pj.FacturaID = @FacturaID 

END


