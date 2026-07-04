CREATE PROCEDURE [dbo].[usp_MAT_Pago_GetEntityById]
    @PagoID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pago por ID, fila de entidad (NetTiers F6 - reemplaza PagoService.GetByPagoId)
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
    WHERE PagoID = @PagoID;
END
