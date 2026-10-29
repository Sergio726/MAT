CREATE FUNCTION [dbo].[fn_MAT_Seguimiento_Clientes] (@Hoy DATE)
RETURNS TABLE
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Una fila por cliente con métricas de viaje, gasto, último contacto y
  --              categoría de seguimiento (docs/DECISIONES_ABIERTAS.md, Seguimiento de clientes).
  --              Viaje realizado = pasaje Señado/Pagado (3,4,6,8) con salida hasta @Hoy.
  --              Categorías (gana la primera): VIP, EnRiesgo, Frecuente, Nuevo, Ocasional, Dormido, SinViajes.
  ============================================= */
RETURN
WITH viajes AS (
    SELECT p.PasajeroID AS ClienteID,
           COUNT(DISTINCT CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN p.ViajeID END) AS ViajesTotales,
           COUNT(DISTINCT CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy
                                    AND v.FechaSalida > DATEADD(MONTH, -12, @Hoy) THEN p.ViajeID END) AS Viajes12Meses,
           MIN(CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN v.FechaSalida END) AS PrimerViaje,
           MAX(CASE WHEN p.EstadoPasaje IN (3, 4, 6, 8) AND v.FechaSalida <= @Hoy THEN v.FechaSalida END) AS UltimoViaje,
           MIN(CASE WHEN p.EstadoPasaje NOT IN (1, 7) AND v.FechaSalida > @Hoy THEN v.FechaSalida END) AS ProximoViaje
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    WHERE p.PasajeroID IS NOT NULL
    GROUP BY p.PasajeroID
),
gasto AS (
    SELECT f.ClienteID,
           SUM(CASE WHEN f.MonedaTipo = 1 THEN f.Monto END) AS GastoARS,
           SUM(CASE WHEN f.MonedaTipo = 3 THEN f.Monto END) AS GastoUSD
    FROM dbo.Factura f
    WHERE f.Estado IN (3, 4)
    GROUP BY f.ClienteID
),
fechasRepetidas AS (
    SELECT FechaNacimiento, COUNT(*) AS Cantidad
    FROM dbo.Persona
    WHERE FechaNacimiento IS NOT NULL
    GROUP BY FechaNacimiento
)
SELECT c.ClienteID,
       pe.Apellido,
       pe.Nombre,
       pe.NroDocumento,
       pe.Celular,
       pe.Telefono,
       pe.Email,
       pe.FechaNacimiento,
       CAST(CASE WHEN fr.Cantidad >= 10 THEN 1 ELSE 0 END AS BIT) AS FechaNacimientoDudosa,
       ISNULL(vj.ViajesTotales, 0) AS ViajesTotales,
       ISNULL(vj.Viajes12Meses, 0) AS Viajes12Meses,
       vj.PrimerViaje,
       vj.UltimoViaje,
       vj.ProximoViaje,
       g.GastoARS,
       g.GastoUSD,
       CASE
           WHEN ISNULL(vj.ViajesTotales, 0) = 0 THEN 'SinViajes'
           WHEN vj.ViajesTotales >= 5 AND vj.UltimoViaje > DATEADD(MONTH, -12, @Hoy) THEN 'VIP'
           WHEN vj.ViajesTotales >= 3 AND vj.UltimoViaje <= DATEADD(MONTH, -12, @Hoy)
                AND vj.UltimoViaje > DATEADD(MONTH, -24, @Hoy) THEN 'EnRiesgo'
           WHEN vj.ViajesTotales BETWEEN 2 AND 4 AND vj.UltimoViaje > DATEADD(MONTH, -12, @Hoy) THEN 'Frecuente'
           WHEN vj.ViajesTotales = 1 AND vj.UltimoViaje > DATEADD(MONTH, -6, @Hoy) THEN 'Nuevo'
           WHEN vj.UltimoViaje > DATEADD(MONTH, -24, @Hoy) THEN 'Ocasional'
           ELSE 'Dormido'
       END AS Categoria,
       uc.Fecha AS UltimoContactoFecha,
       uc.Resultado AS UltimoContactoResultado,
       uc.Quien AS UltimoContactoPor,
       CAST(CASE WHEN uc.Resultado = 8 THEN 1 ELSE 0 END AS BIT) AS NoContactar
FROM dbo.Cliente c
INNER JOIN dbo.Persona pe ON pe.PersonaID = c.ClienteID
LEFT JOIN viajes vj ON vj.ClienteID = c.ClienteID
LEFT JOIN gasto g ON g.ClienteID = c.ClienteID
LEFT JOIN fechasRepetidas fr ON fr.FechaNacimiento = pe.FechaNacimiento
OUTER APPLY (
    SELECT TOP (1) cc.Fecha, cc.Resultado, COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = c.ClienteID
    ORDER BY cc.Fecha DESC
) uc;
