CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetByVendedorId]
    @VendedorID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pagos registrados por un vendedor, filas de entidad (NetTiers F6 -
 --              reemplaza PagoService.GetByVendedorId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PagoID,
        FechaPago,
        Monto,
        TipoPago,
        VendedorId,
        NroRecibo,
        TransaccionID,
        ClienteID,
        EstadoRendicion,
        CuentaCorrienteID
    FROM dbo.Pago
    WHERE VendedorId = @VendedorID;
END
