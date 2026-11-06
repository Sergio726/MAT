-- =============================================================================
-- 2026-11-06 — Inicio: panel "Próximas salidas"
-- Author: Sebastian Garcia
-- usp_MAT_Dashboard_ProximasSalidas: viajes de hoy a 40 días, ordenados, con libres/vendidos/pre-reservas por estado
-- y hora argentina. Reemplaza en el inicio al recuadro flotante (usp_MAT_Dashboard_Viajes_LugaresDisponibles,
-- que se deja como está por si otra pantalla lo usa).
-- Paridad con MAT.DB. Idempotente: CREATE OR ALTER. No modifica datos.
-- =============================================================================
SET NOCOUNT ON;
GO
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Dashboard_ProximasSalidas]
(
    @Dias INT = 40
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-06
  -- Description: Panel "Próximas salidas" del inicio. Reemplaza para el inicio a
  --              usp_MAT_Dashboard_Viajes_LugaresDisponibles, que:
  --              - no ordenaba (mostraba 7 viajes cualesquiera, no los más próximos),
  --              - dejaba afuera los viajes que salen hoy (comparaba con la hora actual del servidor, en UTC),
  --              - no mostraba los viajes completos,
  --              - contaba "sin pasajero" en vez del estado Disponible (al liberar un pasaje se pone EstadoPasaje = 1).
  --              Una fila por viaje que sale entre hoy (hora argentina) y @Dias días, ordenadas por salida.
  --              Libres = Disponible (1). Vendidos = Señado/Pagado con o sin hotel (3, 4, 6, 8).
  --              PreReservas = Reservado/Pre-reserva (2, 5, 9). Total = todos menos Anulado (7).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Dias IS NULL OR @Dias < 1) SET @Dias = 40;
    IF (@Dias > 180) SET @Dias = 180;

    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);

    SELECT v.ViajeID,
           LTRIM(RTRIM(v.Descripcion)) AS Descripcion,
           LTRIM(RTRIM(pq.Descripcion)) AS Paquete,
           CAST(v.FechaSalida AS DATE) AS FechaSalida,
           v.HoraSalida,
           DATEDIFF(DAY, @Hoy, CAST(v.FechaSalida AS DATE)) AS DiasFaltan,
           SUM(CASE WHEN p.EstadoPasaje <> 7 THEN 1 ELSE 0 END) AS Total,
           SUM(CASE WHEN p.EstadoPasaje = 1 THEN 1 ELSE 0 END) AS Libres,
           SUM(CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) THEN 1 ELSE 0 END) AS Vendidos,
           SUM(CASE WHEN p.EstadoPasaje IN (2, 5, 9) THEN 1 ELSE 0 END) AS PreReservas
    FROM dbo.Viaje v
    LEFT JOIN dbo.Paquete pq ON pq.PaqueteID = v.PaqueteID
    LEFT JOIN dbo.Pasaje p ON p.ViajeID = v.ViajeID
    WHERE v.FechaSalida >= @Hoy
      AND v.FechaSalida < DATEADD(DAY, @Dias + 1, @Hoy)
    GROUP BY v.ViajeID, v.Descripcion, pq.Descripcion, v.FechaSalida, v.HoraSalida
    ORDER BY v.FechaSalida, v.HoraSalida, v.Descripcion;
END
GO
