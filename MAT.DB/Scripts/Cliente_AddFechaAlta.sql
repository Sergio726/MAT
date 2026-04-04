/*
-- Migración: Agregar columna FechaAlta a dbo.Cliente
-- Fecha: 2026-04-04
-- Descripción: Registra la fecha en que el cliente fue dado de alta en el sistema.
--              Se hace backfill histórico usando la fecha de primera factura.
--              Clientes sin ninguna factura quedan con NULL.
-- Ejecutar en bases de datos existentes antes de desplegar la nueva versión.
*/
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cliente') AND name = 'FechaAlta')
BEGIN
    ALTER TABLE [dbo].[Cliente] ADD [FechaAlta] DATETIME NULL;
END
GO

-- Backfill histórico: usar fecha de primera factura como proxy de fecha de alta
UPDATE c
SET c.FechaAlta = pf.PrimeraFecha
FROM dbo.Cliente c
INNER JOIN (
    SELECT ClienteID, MIN(Fecha) AS PrimeraFecha
    FROM dbo.Factura
    WHERE Fecha IS NOT NULL
    GROUP BY ClienteID
) pf ON pf.ClienteID = c.ClienteID
WHERE c.FechaAlta IS NULL;
GO
