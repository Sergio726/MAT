CREATE PROCEDURE [dbo].[usp_MAT_MovimientoCuenta_GetByPagoId]
    @PagoID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Movimientos de cuenta de un pago, filas de entidad (NetTiers F6 -
 --              reemplaza MovimientoCuentaService.GetByPagoId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        MovimientoID,
        PagoID,
        FacturaID,
        FechaRegistro,
        CuentaID,
        NotaID,
        CuentaCorrienteID,
        DebitoID
    FROM dbo.MovimientoCuenta
    WHERE PagoID = @PagoID;
END
