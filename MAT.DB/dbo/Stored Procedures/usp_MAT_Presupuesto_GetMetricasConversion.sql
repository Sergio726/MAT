CREATE PROCEDURE [dbo].[usp_MAT_Presupuesto_GetMetricasConversion]
(
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Created By: Seba Garcia
-- Create date: 2026-02-15
-- Purpose: Metricas de conversion de presupuestos
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    IF @FechaDesde IS NULL
        SET @FechaDesde = DATEADD(MONTH, -6, GETDATE());
    IF @FechaHasta IS NULL
        SET @FechaHasta = GETDATE();

    -- Resumen global
    SELECT 
        COUNT(CASE WHEN Estado = 1 THEN 1 END) AS TotalPendientes,
        COUNT(CASE WHEN Estado = 2 THEN 1 END) AS TotalExpirados,
        COUNT(CASE WHEN Estado = 3 THEN 1 END) AS TotalCerrados,
        COUNT(CASE WHEN Estado = 4 THEN 1 END) AS TotalRechazados,
        COUNT(CASE WHEN Estado = 5 THEN 1 END) AS TotalCancelados,
        COUNT(*) AS Total,
        CASE 
            WHEN SUM(CASE WHEN Estado IN (2, 3, 4, 5) THEN 1 ELSE 0 END) > 0 
            THEN CAST(SUM(CASE WHEN Estado = 3 THEN 1 ELSE 0 END) AS FLOAT) 
                 / SUM(CASE WHEN Estado IN (2, 3, 4, 5) THEN 1 ELSE 0 END) * 100
            ELSE 0 
        END AS TasaConversionPorcentaje
    FROM [dbo].[Presupuesto]
    WHERE FechaCreacion >= @FechaDesde
      AND FechaCreacion <= DATEADD(DAY, 1, @FechaHasta);

    -- Por vendedor
    SELECT 
        p.VendedorIdOrigen AS VendedorId,
        ISNULL(RTRIM(v.Apellido + ' ' + v.Nombre), v.Descripcion) AS VendedorNombre,
        COUNT(*) AS TotalCreados,
        SUM(CASE WHEN p.Estado = 3 THEN 1 ELSE 0 END) AS TotalCerrados,
        SUM(CASE WHEN p.Estado = 2 THEN 1 ELSE 0 END) AS TotalExpirados,
        SUM(CASE WHEN p.Estado IN (4, 5) THEN 1 ELSE 0 END) AS TotalRechazadosCancelados,
        CASE 
            WHEN SUM(CASE WHEN p.Estado IN (2, 3, 4, 5) THEN 1 ELSE 0 END) > 0 
            THEN CAST(SUM(CASE WHEN p.Estado = 3 THEN 1 ELSE 0 END) AS FLOAT) 
                 / SUM(CASE WHEN p.Estado IN (2, 3, 4, 5) THEN 1 ELSE 0 END) * 100
            ELSE 0 
        END AS TasaConversionPorcentaje
    FROM [dbo].[Presupuesto] p
    LEFT JOIN [dbo].[PersonaVendedor] v ON p.VendedorIdOrigen = v.VendedorID
    WHERE p.FechaCreacion >= @FechaDesde
      AND p.FechaCreacion <= DATEADD(DAY, 1, @FechaHasta)
    GROUP BY p.VendedorIdOrigen, v.Apellido, v.Nombre, v.Descripcion
    ORDER BY TotalCreados DESC;

    -- Tendencia mensual (ultimos 12 meses)
    SELECT 
        YEAR(FechaCreacion) AS Anio,
        MONTH(FechaCreacion) AS Mes,
        COUNT(*) AS TotalCreados,
        SUM(CASE WHEN Estado = 3 THEN 1 ELSE 0 END) AS TotalCerrados,
        SUM(CASE WHEN Estado = 2 THEN 1 ELSE 0 END) AS TotalExpirados
    FROM [dbo].[Presupuesto]
    WHERE FechaCreacion >= DATEADD(MONTH, -12, GETDATE())
    GROUP BY YEAR(FechaCreacion), MONTH(FechaCreacion)
    ORDER BY Anio DESC, Mes DESC;
END
