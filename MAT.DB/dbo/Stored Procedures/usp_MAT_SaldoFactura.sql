CREATE PROCEDURE [dbo].[usp_MAT_SaldoFactura]
(
	@FacturaID uniqueidentifier
)

-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 09-14-2017
-- Description:  CALCULATE SALDO FACTURA
-- =============================================
AS
BEGIN

	SELECT Saldo = [dbo].[fn_MAT_SaldoFactura](@FacturaID)
	
END

