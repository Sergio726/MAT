CREATE PROCEDURE [dbo].[usp_MAT_Reportes_RankingCompras_V2]
(
    @To DATE = NULL,
    @From DATE = NULL,
    @ViajeId UNIQUEIDENTIFIER = NULL,
    @ClienteId UNIQUEIDENTIFIER = NULL
)
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-04-15
 -- Description: Dashboard ranking de compras V2.
 --              Devuelve una fila por factura x viaje
 --              con pasajeros distintos; la agregación
 --              para tarjetas KPI se hace en el cliente.
 ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        f.FacturaID,
        f.Fecha,
        f.ClienteID,
        perCli.FullName                  AS ClienteFullName,
        p.ViajeID,
        v.Descripcion                    AS ViajeDescripcion,
        v.FechaSalida                    AS ViajeFechaSalida,
        COUNT(*)                         AS CantidadPasajesXFactura,
        COUNT(DISTINCT p.PasajeroID)     AS CantPasajerosDistintos
    FROM dbo.Factura f
    INNER JOIN dbo.Pasaje p        ON p.FacturaID = f.FacturaID
    INNER JOIN dbo.Viaje v         ON v.ViajeID   = p.ViajeID
    INNER JOIN dbo.Persona perCli  ON perCli.PersonaID = f.ClienteID
    WHERE
        p.PasajeroID IS NOT NULL AND
        (@From      IS NULL OR f.Fecha    >= @From)    AND
        (@To        IS NULL OR f.Fecha    <= @To)      AND
        (@ViajeId   IS NULL OR p.ViajeID   = @ViajeId) AND
        (@ClienteId IS NULL OR f.ClienteID = @ClienteId)
    GROUP BY
        f.FacturaID, f.Fecha, f.ClienteID, perCli.FullName,
        p.ViajeID, v.Descripcion, v.FechaSalida
    ORDER BY
        f.Fecha DESC, perCli.FullName;
END
