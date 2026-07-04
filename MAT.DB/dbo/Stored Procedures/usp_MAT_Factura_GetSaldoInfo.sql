CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetSaldoInfo]
    @FacturaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Monto de la factura, total de pagos y total de debitos en una sola
 --              consulta set-based (NetTiers F6 - reemplaza los N+1 de MATContext.Saldo
 --              y FacturaModel.CalcularSaldo). La resta se hace en C# por call-site:
 --              MATContext.Saldo NO resta debitos; CalcularSaldo si.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        f.Monto,
        TotalPagos = ISNULL((
            SELECT SUM(p.Monto)
            FROM dbo.MovimientoCuenta mc
            INNER JOIN dbo.Pago p ON p.PagoID = mc.PagoID
            WHERE mc.FacturaID = f.FacturaID), 0),
        TotalDebitos = ISNULL((
            SELECT SUM(d.MontoDebito)
            FROM dbo.MovimientoCuenta mc
            INNER JOIN dbo.Debito d ON d.DebitoID = mc.DebitoID
            WHERE mc.FacturaID = f.FacturaID), 0)
    FROM dbo.Factura f
    WHERE f.FacturaID = @FacturaID;
END
