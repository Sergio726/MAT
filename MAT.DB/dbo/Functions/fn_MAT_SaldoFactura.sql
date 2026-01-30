CREATE FUNCTION [dbo].[fn_MAT_SaldoFactura]
(
	@FacturaID uniqueidentifier
)
RETURNS MONEY
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 09-14-2017
-- Description:  CALCULATE SALDO FACTURA
-- =============================================
AS
BEGIN
	
	DECLARE @Saldo money = 0,
			@Total money = 0,
			@Pagado money = 0
	
	select @Total = sum(df.precio*df.Cantidad)  from dbo.DetalleFactura df where FacturaID = @FacturaID
	select @Pagado = sum(p.Monto)
	FROM   dbo.MovimientoCuenta mc 
	inner join dbo.Pago p
		on mc.PagoID = p.PagoID
	WHERE  mc.facturaid = @FacturaID
	
	SELECT @Saldo = isnull(@Total,0) - isnull(@Pagado,0)

	RETURN @Saldo
END

