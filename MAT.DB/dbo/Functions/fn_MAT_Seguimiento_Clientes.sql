CREATE FUNCTION [dbo].[fn_MAT_Seguimiento_Clientes] (@Hoy DATE)
RETURNS TABLE
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Una fila por cliente con métricas de viaje, gasto, último contacto y
  --              categoría de seguimiento (docs/DECISIONES_ABIERTAS.md, Seguimiento de clientes).
  --              Viaje = pasaje Señado/Pagado (3,4,6,8) donde el cliente es pasajero o quien lo pagó
  --              (factura a su nombre); realizado si la salida es hasta @Hoy.
  --              Categorías (gana la primera): VIP, EnRiesgo, Frecuente, Nuevo, Ocasional, Dormido, SinViajes.
  --              @Hoy lo calculan los SPs en hora argentina (el servidor está en UTC).
  -- 2026-10-29: viajes pagados para otros, EsMenor y TelefonoCompartido.
  ============================================= */
RETURN
WITH viajesCliente AS (
    SELECT p.PasajeroID AS ClienteID, p.ViajeID, p.EstadoPasaje, v.FechaSalida, CAST(1 AS BIT) AS ComoPasajero
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    WHERE p.PasajeroID IS NOT NULL
    UNION ALL
    SELECT f.ClienteID, p.ViajeID, p.EstadoPasaje, v.FechaSalida, CAST(0 AS BIT)
    FROM dbo.Pasaje p
    INNER JOIN dbo.Factura f ON f.FacturaID = p.FacturaID
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
),
viajes AS (
    SELECT ClienteID,
           COUNT(DISTINCT CASE WHEN EstadoPasaje IN (3, 4, 6, 8) AND FechaSalida <= @Hoy THEN ViajeID END) AS ViajesTotales,
           COUNT(DISTINCT CASE WHEN EstadoPasaje IN (3, 4, 6, 8) AND FechaSalida <= @Hoy AND ComoPasajero = 1 THEN ViajeID END) AS ViajesComoPasajero,
           COUNT(DISTINCT CASE WHEN EstadoPasaje IN (3, 4, 6, 8) AND FechaSalida <= @Hoy
                                    AND FechaSalida > DATEADD(MONTH, -12, @Hoy) THEN ViajeID END) AS Viajes12Meses,
           MIN(CASE WHEN EstadoPasaje IN (3, 4, 6, 8) AND FechaSalida <= @Hoy THEN FechaSalida END) AS PrimerViaje,
           MAX(CASE WHEN EstadoPasaje IN (3, 4, 6, 8) AND FechaSalida <= @Hoy THEN FechaSalida END) AS UltimoViaje,
           MIN(CASE WHEN EstadoPasaje NOT IN (1, 7) AND FechaSalida > @Hoy THEN FechaSalida END) AS ProximoViaje
    FROM viajesCliente
    GROUP BY ClienteID
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
),
celulares AS (
    SELECT pe.PersonaID,
           REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(pe.Celular, ' ', ''), '-', ''), '.', ''), '(', ''), ')', ''), '+', '') AS Digitos
    FROM dbo.Persona pe
    INNER JOIN dbo.Cliente c ON c.ClienteID = pe.PersonaID
    WHERE LEN(pe.Celular) >= 7
),
celularesCompartidos AS (
    SELECT Digitos, COUNT(*) AS Cantidad
    FROM celulares
    GROUP BY Digitos
    HAVING COUNT(*) > 1
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
       CAST(CASE WHEN pe.FechaNacimiento IS NOT NULL AND YEAR(pe.FechaNacimiento) >= 1910
                      AND DATEADD(YEAR, 18, pe.FechaNacimiento) > @Hoy THEN 1 ELSE 0 END AS BIT) AS EsMenor,
       ISNULL(cc.Cantidad - 1, 0) AS TelefonoCompartido,
       ISNULL(vj.ViajesTotales, 0) AS ViajesTotales,
       ISNULL(vj.ViajesComoPasajero, 0) AS ViajesComoPasajero,
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
LEFT JOIN celulares cel ON cel.PersonaID = c.ClienteID
LEFT JOIN celularesCompartidos cc ON cc.Digitos = cel.Digitos
OUTER APPLY (
    SELECT TOP (1) cc2.Fecha, cc2.Resultado, COALESCE(pv.FullName, cc2.Usuario) AS Quien
    FROM dbo.ClienteContacto cc2
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc2.VendedorID
    WHERE cc2.ClienteID = c.ClienteID
    ORDER BY cc2.Fecha DESC
) uc;
