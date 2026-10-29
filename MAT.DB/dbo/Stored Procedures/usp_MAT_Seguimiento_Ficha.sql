CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_Ficha]
(
    @ClienteID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Ficha de seguimiento de un cliente.
  --              RS1: datos + métricas (fn_MAT_Seguimiento_Clientes).
  --              RS2: viajes como pasajero o pagados para otros (sin Disponible ni Anulado), más reciente primero.
  --              RS3: historial de contactos.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    -- Hora argentina: el servidor está en UTC y desde las 21:00 "hoy" sería mañana.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.ClienteID = @ClienteID;

    -- Viajes como pasajero o pagados por el cliente para otros (un renglón por viaje)
    SELECT v.FechaSalida,
           v.FechaRegreso,
           v.Descripcion AS Viaje,
           MAX(CASE WHEN p.PasajeroID = @ClienteID THEN e.Descripcion END) AS EstadoPasaje,
           CASE WHEN MAX(CASE WHEN p.PasajeroID = @ClienteID THEN 1 ELSE 0 END) = 1 THEN 'Viajó' ELSE 'Pagó para otros' END AS Rol,
           COUNT(*) AS Pasajes
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    INNER JOIN dbo.EstadoPasaje e ON e.ID = p.EstadoPasaje
    LEFT JOIN dbo.Factura f ON f.FacturaID = p.FacturaID
    WHERE (p.PasajeroID = @ClienteID OR f.ClienteID = @ClienteID)
      AND p.EstadoPasaje NOT IN (1, 7)
    GROUP BY v.ViajeID, v.FechaSalida, v.FechaRegreso, v.Descripcion
    ORDER BY v.FechaSalida DESC;

    SELECT cc.Fecha,
           cc.Resultado,
           cc.Observacion,
           COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = @ClienteID
    ORDER BY cc.Fecha DESC;
END
