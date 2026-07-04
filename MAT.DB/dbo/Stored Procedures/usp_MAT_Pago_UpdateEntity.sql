CREATE PROCEDURE [dbo].[usp_MAT_Pago_UpdateEntity]
    @PagoID UNIQUEIDENTIFIER,
    @FechaPago DATETIME,
    @Monto MONEY,
    @TipoPago INT,
    @VendedorID UNIQUEIDENTIFIER,
    @NroRecibo VARCHAR (50),
    @TransaccionID VARCHAR (50) = NULL,
    @ClienteID UNIQUEIDENTIFIER = NULL,
    @EstadoRendicion INT = NULL,
    @CuentaCorrienteID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Pago (NetTiers F6 - reemplaza PagoService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Pago
    SET FechaPago = @FechaPago,
        Monto = @Monto,
        TipoPago = @TipoPago,
        VendedorId = @VendedorID,
        NroRecibo = @NroRecibo,
        TransaccionID = @TransaccionID,
        ClienteID = @ClienteID,
        EstadoRendicion = @EstadoRendicion,
        CuentaCorrienteID = @CuentaCorrienteID
    WHERE PagoID = @PagoID;
END
