CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_GetByPasajeroId]
    @PasajeroID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pasajes de un pasajero, filas de entidad (NetTiers F6 -
 --              reemplaza PasajeService.GetByPasajeroId)
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
    WHERE PasajeroID = @PasajeroID;
END
