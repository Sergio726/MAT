CREATE PROCEDURE [dbo].[usp_MAT_Pasaje_GetEntitiesByViajeId]
    @ViajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Pasajes de un viaje, filas de entidad (NetTiers F6 -
 --              reemplaza PasajeService.GetByViajeId)
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
    WHERE ViajeID = @ViajeID;
END
