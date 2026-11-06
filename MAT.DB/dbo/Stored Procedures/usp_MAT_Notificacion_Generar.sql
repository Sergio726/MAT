CREATE PROCEDURE [dbo].[usp_MAT_Notificacion_Generar]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-06
  -- Description: Crea los avisos de la campanita que todavía no existen (Notificacion.Clave evita repetirlos).
  --              Lo llama la aplicación cada pocos minutos; correrlo de más no duplica nada.
  --              - ViajeProximo:  viaje que sale en 7 días o menos con lugares libres (uno por viaje).
  --              - UltimosLugares: viaje de los próximos 40 días con 1 a 5 lugares libres (uno por viaje).
  --              - ViajeCompleto: viaje de los próximos 40 días sin lugares libres (uno por viaje).
  --              - Cumpleanios:   clientes activos que cumplen años hoy (uno por día).
  --              Libres = pasajes en estado Disponible (1); total = todos menos Anulado (7). Hora argentina.
  --              También borra los avisos vencidos hace más de 30 días.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);
    DECLARE @Hoy DATE = CAST(@Ahora AS DATE);

    -- Ocupación de los viajes de los próximos 40 días
    SELECT v.ViajeID,
           LTRIM(RTRIM(v.Descripcion)) AS Descripcion,
           CAST(v.FechaSalida AS DATE) AS FechaSalida,
           DATEDIFF(DAY, @Hoy, CAST(v.FechaSalida AS DATE)) AS DiasFaltan,
           SUM(CASE WHEN p.EstadoPasaje <> 7 THEN 1 ELSE 0 END) AS Total,
           SUM(CASE WHEN p.EstadoPasaje = 1 THEN 1 ELSE 0 END) AS Libres
    INTO #Viajes
    FROM dbo.Viaje v
    INNER JOIN dbo.Pasaje p ON p.ViajeID = v.ViajeID
    WHERE v.FechaSalida >= @Hoy AND v.FechaSalida < DATEADD(DAY, 41, @Hoy)
    GROUP BY v.ViajeID, v.Descripcion, v.FechaSalida;

    DECLARE @Nuevas TABLE (Clave VARCHAR(150), Tipo VARCHAR(30), Titulo NVARCHAR(200), Detalle NVARCHAR(400), Url VARCHAR(300), Vence DATETIME);

    INSERT INTO @Nuevas
    SELECT 'viaje-proximo:' + CAST(ViajeID AS VARCHAR(36)), 'ViajeProximo',
           -- Fecha fija en el título: "sale en 3 días" quedaría falso al día siguiente
           LEFT(Descripcion, 120) + N' sale el ' + CONVERT(NVARCHAR(5), FechaSalida, 103),
           N'Quedan ' + CAST(Libres AS NVARCHAR(10)) + N' lugares libres de ' + CAST(Total AS NVARCHAR(10)) + N'.',
           '/Reserva/Index?viajeid=' + CAST(ViajeID AS VARCHAR(36)), DATEADD(DAY, 1, CAST(FechaSalida AS DATETIME))
    FROM #Viajes WHERE DiasFaltan <= 7 AND Libres > 0 AND Total > 0;

    INSERT INTO @Nuevas
    SELECT 'viaje-ultimos:' + CAST(ViajeID AS VARCHAR(36)), 'UltimosLugares',
           N'Últimos lugares: ' + LEFT(Descripcion, 120),
           N'Quedan ' + CAST(Libres AS NVARCHAR(10)) + N' de ' + CAST(Total AS NVARCHAR(10)) + N'. Sale el ' + CONVERT(NVARCHAR(10), FechaSalida, 103) + N'.',
           '/Reserva/Index?viajeid=' + CAST(ViajeID AS VARCHAR(36)), DATEADD(DAY, 1, CAST(FechaSalida AS DATETIME))
    FROM #Viajes WHERE Libres BETWEEN 1 AND 5;

    INSERT INTO @Nuevas
    SELECT 'viaje-completo:' + CAST(ViajeID AS VARCHAR(36)), 'ViajeCompleto',
           N'Completo: ' + LEFT(Descripcion, 120),
           N'Se vendieron los ' + CAST(Total AS NVARCHAR(10)) + N' lugares. Sale el ' + CONVERT(NVARCHAR(10), FechaSalida, 103) + N'.',
           '/Reserva/Index?viajeid=' + CAST(ViajeID AS VARCHAR(36)), DATEADD(DAY, 1, CAST(FechaSalida AS DATETIME))
    FROM #Viajes WHERE Libres = 0 AND Total > 0;

    -- Cumpleaños de hoy (clientes activos; el 29/02 se saluda el 28/02 en años no bisiestos)
    DECLARE @Cumplen INT = (
        SELECT COUNT(*)
        FROM dbo.Cliente c
        INNER JOIN dbo.Persona pe ON pe.PersonaID = c.ClienteID
        WHERE c.Activo = 1 AND pe.FechaNacimiento IS NOT NULL AND YEAR(pe.FechaNacimiento) >= 1910
          AND (  (MONTH(pe.FechaNacimiento) = MONTH(@Hoy) AND DAY(pe.FechaNacimiento) = DAY(@Hoy))
              OR (MONTH(pe.FechaNacimiento) = 2 AND DAY(pe.FechaNacimiento) = 29 AND MONTH(@Hoy) = 2 AND DAY(@Hoy) = 28
                  AND DAY(DATEADD(DAY, 1, @Hoy)) = 1)));
    IF (@Cumplen > 0)
        INSERT INTO @Nuevas
        VALUES ('cumple:' + CONVERT(VARCHAR(10), @Hoy, 23), 'Cumpleanios',
                CASE WHEN @Cumplen = 1 THEN N'Hoy cumple años 1 cliente' ELSE N'Hoy cumplen años ' + CAST(@Cumplen AS NVARCHAR(10)) + N' clientes' END,
                N'Saludalos por WhatsApp desde Seguimiento.', '/Seguimiento/Index', DATEADD(DAY, 1, CAST(@Hoy AS DATETIME)));

    INSERT INTO dbo.Notificacion (Clave, Tipo, Titulo, Detalle, Url, Fecha, Vence)
    SELECT n.Clave, n.Tipo, n.Titulo, n.Detalle, n.Url, @Ahora, n.Vence
    FROM @Nuevas n
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Notificacion x WHERE x.Clave = n.Clave);

    DELETE FROM dbo.Notificacion WHERE Vence < DATEADD(DAY, -30, @Ahora);
END
