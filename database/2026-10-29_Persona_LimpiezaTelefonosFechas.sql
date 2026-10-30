-- =============================================================================
-- 2026-10-29 — Limpieza de teléfonos incompletos y fechas de nacimiento imposibles en Persona
-- Author: Sebastian Garcia
--
-- Qué hace:
--   * Celular / Telefono con menos de 7 dígitos (en su mayoría "387", "0387", "0387-") → NULL.
--   * FechaNacimiento anterior a 1910 o futura (incluye 0001-01-01) → NULL.
--   * NO toca las fechas repetidas (pueden ser reales): se verifican con el cliente.
-- Medición en producción (MAT.Intranet, 29/10/2026): 569 celulares, 1.772 teléfonos, 75 fechas.
--
-- Reversible: antes de cambiar guarda PersonaID, campo y valor anterior en
-- dbo.Persona_Limpieza_20261029 (tabla de respaldo, no forma parte de MAT.DB; borrar cuando ya no haga falta).
-- Revertir:
--   UPDATE p SET Celular = b.ValorAnterior FROM dbo.Persona p JOIN dbo.Persona_Limpieza_20261029 b ON b.PersonaID = p.PersonaID AND b.Campo = 'Celular';
--   UPDATE p SET Telefono = b.ValorAnterior FROM dbo.Persona p JOIN dbo.Persona_Limpieza_20261029 b ON b.PersonaID = p.PersonaID AND b.Campo = 'Telefono';
--   UPDATE p SET FechaNacimiento = CONVERT(date, b.ValorAnterior, 23) FROM dbo.Persona p JOIN dbo.Persona_Limpieza_20261029 b ON b.PersonaID = p.PersonaID AND b.Campo = 'FechaNacimiento';
--
-- Requiere SQL Server 2017+ (TRANSLATE). Idempotente: una segunda corrida no encuentra filas.
-- =============================================================================
SET NOCOUNT, XACT_ABORT ON;

IF OBJECT_ID('dbo.Persona_Limpieza_20261029') IS NULL
    CREATE TABLE dbo.Persona_Limpieza_20261029 (
        PersonaID     UNIQUEIDENTIFIER NOT NULL,
        Campo         VARCHAR(30)      NOT NULL,
        ValorAnterior VARCHAR(100)     NULL,
        Fecha         DATETIME         NOT NULL CONSTRAINT DF_Persona_Limpieza_20261029_Fecha DEFAULT (GETDATE())
    );
GO

BEGIN TRANSACTION;

-- Teléfonos: cuenta solo dígitos (TRANSLATE convierte 0-9 en '#')
INSERT INTO dbo.Persona_Limpieza_20261029 (PersonaID, Campo, ValorAnterior)
SELECT PersonaID, 'Celular', Celular FROM dbo.Persona
WHERE LEN(Celular) > 0
  AND LEN(Celular) - LEN(REPLACE(TRANSLATE(Celular, '0123456789', '##########'), '#', '')) < 7;

UPDATE dbo.Persona SET Celular = NULL
WHERE LEN(Celular) > 0
  AND LEN(Celular) - LEN(REPLACE(TRANSLATE(Celular, '0123456789', '##########'), '#', '')) < 7;
PRINT 'Celulares limpiados: ' + CAST(@@ROWCOUNT AS varchar(10));

INSERT INTO dbo.Persona_Limpieza_20261029 (PersonaID, Campo, ValorAnterior)
SELECT PersonaID, 'Telefono', Telefono FROM dbo.Persona
WHERE LEN(Telefono) > 0
  AND LEN(Telefono) - LEN(REPLACE(TRANSLATE(Telefono, '0123456789', '##########'), '#', '')) < 7;

UPDATE dbo.Persona SET Telefono = NULL
WHERE LEN(Telefono) > 0
  AND LEN(Telefono) - LEN(REPLACE(TRANSLATE(Telefono, '0123456789', '##########'), '#', '')) < 7;
PRINT 'Teléfonos limpiados: ' + CAST(@@ROWCOUNT AS varchar(10));

-- Fechas de nacimiento imposibles
INSERT INTO dbo.Persona_Limpieza_20261029 (PersonaID, Campo, ValorAnterior)
SELECT PersonaID, 'FechaNacimiento', CONVERT(varchar(10), FechaNacimiento, 23) FROM dbo.Persona
WHERE YEAR(FechaNacimiento) < 1910 OR FechaNacimiento > CAST(GETDATE() AS date);

UPDATE dbo.Persona SET FechaNacimiento = NULL
WHERE YEAR(FechaNacimiento) < 1910 OR FechaNacimiento > CAST(GETDATE() AS date);
PRINT 'Fechas de nacimiento limpiadas: ' + CAST(@@ROWCOUNT AS varchar(10));

COMMIT TRANSACTION;
GO
