CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_UpdateEntity]
    @PasajeID UNIQUEIDENTIFIER,
    @PasajeroID UNIQUEIDENTIFIER = NULL,
    @ButacaID UNIQUEIDENTIFIER = NULL,
    @FechaReserva DATE = NULL,
    @FechaCompra DATE = NULL,
    @ViajeID UNIQUEIDENTIFIER = NULL,
    @FacturaID UNIQUEIDENTIFIER = NULL,
    @EstadoPasaje INT,
    @VoucherID UNIQUEIDENTIFIER = NULL,
    @PrecioID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Pasaje (NetTiers F6 - reemplaza PasajeService.Update)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Pasaje
    SET PasajeroID = @PasajeroID,
        ButacaID = @ButacaID,
        FechaReserva = @FechaReserva,
        FechaCompra = @FechaCompra,
        ViajeID = @ViajeID,
        FacturaID = @FacturaID,
        EstadoPasaje = @EstadoPasaje,
        VoucherID = @VoucherID,
        PrecioID = @PrecioID
    WHERE PasajeID = @PasajeID;
END
