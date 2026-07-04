CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetByFacturaId]
    @FacturaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pagos de una factura via MovimientoCuenta, filas de entidad
 --              (NetTiers F6 - reemplaza el N+1 MovimientoCuenta.GetByFacturaId +
 --              PagoService.GetByPagoId de FacturaPagosModel.CalcularSaldo)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        p.PagoID,
        p.FechaPago,
        p.Monto,
        p.TipoPago,
        p.VendedorId,
        p.NroRecibo,
        p.TransaccionID,
        p.ClienteID,
        p.EstadoRendicion,
        p.CuentaCorrienteID
    FROM dbo.MovimientoCuenta mc
    INNER JOIN dbo.Pago p ON p.PagoID = mc.PagoID
    WHERE mc.FacturaID = @FacturaID;
END
