CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_GetEntityById]
    @PasajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pasaje por ID, fila de entidad (NetTiers F6 - reemplaza PasajeService.GetByPasajeId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PasajeID,
        PasajeroID,
        ButacaID,
        FechaReserva,
        FechaCompra,
        ViajeID,
        FacturaID,
        EstadoPasaje,
        VoucherID,
        PrecioID
    FROM dbo.Pasaje
    WHERE PasajeID = @PasajeID;
END
