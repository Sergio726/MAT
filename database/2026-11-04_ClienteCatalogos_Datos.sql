-- =============================================================================
-- 2026-11-04 — Módulo Clientes F4 (2 de 2): normalización y deduplicación de datos
-- Author: Sebastian Garcia
--
-- QUÉ HACE (todo en una transacción; si algo falla no queda nada a medias):
--   1. Nacionalidad y país de residencia → código ISO (ARGENTINA / ARGENTINO / ARG. → ARG, BOLIVIANA → BOL…).
--      Valores que no son un país (domicilios cargados por error, "EXT") → NULL.
--   2. Ocupación → nombre neutro del catálogo (JUBILADA / JUBILADO → Jubilado/a…).
--   3. Salta: departamentos duplicados (GRL.GUEMES, 4 variantes de San Martín, LOA ANDES, ROSARIO) se unifican.
--   4. Salta: localidades duplicadas se unifican (SALTA → CAPITAL, que pasa a llamarse "SALTA (CAPITAL)";
--      GENERAL GUEMES ×6, EL BORDO ×2, CAMPO SANTO ×2, AGUARAY ×2, SALVADOR MAZZA ×2, ANTONIO QUIJARRO ×2,
--      SAN ANTONIO DE LOS COBRES ×2); SAN LOREAZO → SAN LORENZO.
--   5. Persona.Provincia = provincia de su localidad (8 inconsistentes en Prod).
--
-- RESPALDO: dbo.Catalogos_Limpieza_20261104_Persona (PersonaID + valores anteriores de las columnas tocadas),
--           _Localidad, _Departamento, _Paquete, _Proveedor. Para revertir, ver el bloque al final.
-- SEGURIDAD: cada equivalencia lleva el nombre esperado; si el ID no tiene ese nombre en esta base, se saltea.
--            Se puede correr dos veces: la segunda no encuentra nada que cambiar.
-- REQUIERE: 2026-11-04_ClienteCatalogos.sql (Pais.Codigo y tabla Ocupacion).
-- ANTES DE PROD: revisar con el negocio la tabla de equivalencias de localidades (bloque 4) y correr en DEV.
-- =============================================================================
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF COL_LENGTH('dbo.Pais', 'Codigo') IS NULL OR OBJECT_ID('dbo.Ocupacion', 'U') IS NULL
BEGIN
    RAISERROR('Falta correr antes 2026-11-04_ClienteCatalogos.sql', 16, 1);
    SET NOEXEC ON;
END
GO

-- ---------------------------------------------------------------- Conteos antes
SELECT 'ANTES' AS momento,
       (SELECT COUNT(*) FROM dbo.Persona) AS personas,
       (SELECT COUNT(*) FROM dbo.Persona WHERE LocalidadID IS NOT NULL) AS personas_con_localidad,
       (SELECT COUNT(*) FROM dbo.Persona WHERE Nacionalidad = 'ARG') AS nacionalidad_arg,
       (SELECT COUNT(*) FROM dbo.Departamento WHERE idProvincia = 16) AS deptos_salta,
       (SELECT COUNT(*) FROM dbo.Localidad l JOIN dbo.Departamento d ON d.ID = l.idDepartamento WHERE d.idProvincia = 16) AS localidades_salta;
GO

BEGIN TRANSACTION;

-- ---------------------------------------------------------------- Respaldo
IF OBJECT_ID('dbo.Catalogos_Limpieza_20261104_Persona', 'U') IS NULL
    SELECT PersonaID, Nacionalidad, PaisResidencia, Ocupacion, LocalidadID, Provincia, SYSDATETIME() AS Respaldado
    INTO dbo.Catalogos_Limpieza_20261104_Persona
    FROM dbo.Persona;
IF OBJECT_ID('dbo.Catalogos_Limpieza_20261104_Localidad', 'U') IS NULL
    SELECT * INTO dbo.Catalogos_Limpieza_20261104_Localidad FROM dbo.Localidad;
IF OBJECT_ID('dbo.Catalogos_Limpieza_20261104_Departamento', 'U') IS NULL
    SELECT * INTO dbo.Catalogos_Limpieza_20261104_Departamento FROM dbo.Departamento;
IF OBJECT_ID('dbo.Catalogos_Limpieza_20261104_Paquete', 'U') IS NULL
    SELECT PaqueteID, DestinoID INTO dbo.Catalogos_Limpieza_20261104_Paquete FROM dbo.Paquete;
IF OBJECT_ID('dbo.Catalogos_Limpieza_20261104_Proveedor', 'U') IS NULL
    SELECT ProveedorID, LocalidadID INTO dbo.Catalogos_Limpieza_20261104_Proveedor FROM dbo.Proveedor;

-- ---------------------------------------------------------------- 1. Nacionalidad y país de residencia
DECLARE @Pais TABLE (Valor VARCHAR(60) COLLATE Latin1_General_CI_AI PRIMARY KEY, Codigo CHAR(3) NULL);
INSERT INTO @Pais (Valor, Codigo) VALUES
    ('ARG', 'ARG'), ('ARG.', 'ARG'), ('ARGENTINA', 'ARG'), ('ARGENTINO', 'ARG'), ('ARGENTINA.', 'ARG'), ('SALTA', 'ARG'),
    ('BOL', 'BOL'), ('BOLIVIA', 'BOL'), ('BOLIVIANA', 'BOL'), ('BOLIVIANO', 'BOL'),
    ('BRA', 'BRA'), ('BRASIL', 'BRA'), ('BRASILEÑA', 'BRA'), ('BRASILEÑO', 'BRA'),
    ('CHL', 'CHL'), ('CHILE', 'CHL'), ('CHILENA', 'CHL'), ('CHILENO', 'CHL'),
    ('PRY', 'PRY'), ('PARAGUAY', 'PRY'), ('PARAGUAYA', 'PRY'), ('PARAGUAYO', 'PRY'),
    ('PER', 'PER'), ('PERU', 'PER'), ('PERUANA', 'PER'), ('PERUANO', 'PER'),
    ('URY', 'URY'), ('URUGUAY', 'URY'), ('URUGUAYA', 'URY'), ('URUGUAYO', 'URY'),
    ('DOM', 'DOM'), ('DOMINICANA', 'DOM'), ('DOMINICANO', 'DOM'),
    ('ESP', 'ESP'), ('ESPAÑA', 'ESP'), ('ESPAÑOLA', 'ESP'), ('ESPAÑOL', 'ESP'),
    ('ITA', 'ITA'), ('ITALIA', 'ITA'), ('ITALIANA', 'ITA'), ('ITALIANO', 'ITA'),
    ('USA', 'USA'), ('ESTADOS UNIDOS', 'USA'), ('ESTADOUNIDENSE', 'USA'),
    ('MEX', 'MEX'), ('MEXICO', 'MEX'), ('MEXICANA', 'MEX'), ('MEXICANO', 'MEX'),
    ('HUN', 'HUN'), ('HUNGRIA', 'HUN'), ('HUNGARA', 'HUN'), ('HUNGARO', 'HUN'),
    ('DNK', 'DNK'), ('DINAMARCA', 'DNK'), ('DANESA', 'DNK'), ('DANES', 'DNK'),
    ('DEU', 'DEU'), ('ALEMANIA', 'DEU'), ('ALEMANA', 'DEU'), ('ALEMAN', 'DEU'),
    ('COL', 'COL'), ('COLOMBIA', 'COL'), ('COLOMBIANA', 'COL'), ('COLOMBIANO', 'COL'),
    ('NLD', 'NLD'), ('PAISES BAJOS', 'NLD'), ('HOLANDA', 'NLD'), ('HOLANDESA', 'NLD'), ('HOLANDES', 'NLD'),
    -- No son un país: se vacían (el valor queda en el respaldo)
    ('EXT', NULL), ('EDIN 217', NULL), ('DR ADOLFO GUEMES 755 2 8', NULL), ('BLOCK 43 S/N 1 7 , Bº LIMACHE', NULL), ('DAMIAN TORINO 964', NULL);

UPDATE pe SET Nacionalidad = m.Codigo
FROM dbo.Persona pe
INNER JOIN @Pais m ON m.Valor = LTRIM(RTRIM(pe.Nacionalidad)) COLLATE Latin1_General_CI_AI
WHERE ISNULL(pe.Nacionalidad, '') COLLATE Latin1_General_CS_AS <> ISNULL(m.Codigo, '');

UPDATE pe SET PaisResidencia = m.Codigo
FROM dbo.Persona pe
INNER JOIN @Pais m ON m.Valor = LTRIM(RTRIM(pe.PaisResidencia)) COLLATE Latin1_General_CI_AI
WHERE ISNULL(pe.PaisResidencia, '') COLLATE Latin1_General_CS_AS <> ISNULL(m.Codigo, '');

-- Vacíos → NULL
UPDATE dbo.Persona SET Nacionalidad = NULL WHERE Nacionalidad IS NOT NULL AND LTRIM(RTRIM(Nacionalidad)) = '';
UPDATE dbo.Persona SET PaisResidencia = NULL WHERE PaisResidencia IS NOT NULL AND LTRIM(RTRIM(PaisResidencia)) = '';

-- ---------------------------------------------------------------- 2. Ocupación
DECLARE @Ocupacion TABLE (Valor VARCHAR(60) COLLATE Latin1_General_CI_AI PRIMARY KEY, Nombre VARCHAR(50) NOT NULL);
INSERT INTO @Ocupacion (Valor, Nombre) VALUES
    ('JUBILADA', 'Jubilado/a'), ('JUBILADO', 'Jubilado/a'),
    ('EMPLEADA', 'Empleado/a'), ('EMPLEADO', 'Empleado/a'), ('PRIVADO', 'Empleado/a'),
    ('EMPLEADA PUBLICA', 'Empleado/a público/a'), ('EMPLEADO PUBLICO', 'Empleado/a público/a'),
    ('EMPLEADO DE COMERCIO', 'Empleado/a de comercio'), ('EMPLEADA DE COMERCIO', 'Empleado/a de comercio'),
    ('DOCENTE', 'Docente'), ('ESTUDIANTE', 'Estudiante'), ('PROFESIONAL', 'Profesional'),
    ('T INDEP', 'Trabajador/a independiente'), ('COMERCIANTE', 'Comerciante'), ('AMA DE CASA', 'Ama/o de casa'),
    ('PENSIONADA', 'Pensionado/a'), ('PENSIONADO', 'Pensionado/a'), ('SIN OCUPACION', 'Sin ocupación'),
    ('ABOGADA', 'Abogado/a'), ('ABOGADO', 'Abogado/a'), ('FARMACEUTICO', 'Farmacéutico/a'), ('FARMACEUTICA', 'Farmacéutico/a'),
    ('ODONTOLOGA', 'Odontólogo/a'), ('ODONTOLOGO', 'Odontólogo/a'), ('PERIODISTA', 'Periodista');

UPDATE pe SET Ocupacion = m.Nombre
FROM dbo.Persona pe
INNER JOIN @Ocupacion m ON m.Valor = LTRIM(RTRIM(pe.Ocupacion)) COLLATE Latin1_General_CI_AI
WHERE pe.Ocupacion COLLATE Latin1_General_CS_AS <> m.Nombre COLLATE Latin1_General_CS_AS;

UPDATE dbo.Persona SET Ocupacion = NULL WHERE Ocupacion IS NOT NULL AND LTRIM(RTRIM(Ocupacion)) = '';

-- ---------------------------------------------------------------- 3. Departamentos de Salta
-- Duplicado → canónico. Se verifica el nombre de los dos lados.
DECLARE @Depto TABLE (DupID INT, DupNombre NVARCHAR(250), CanonID INT, CanonNombre NVARCHAR(250));
INSERT INTO @Depto VALUES
    (439, N'GRL.GUEMES',              447, N'GENERAL GUEMES'),
    (433, N'GRAL.JOSE DE SAN MARTIN', 444, N'GRL.JOSE DE SAN MARTIN'),
    (446, N'GRL.SAN MARTIN',          444, N'GRL.JOSE DE SAN MARTIN'),
    (430, N'SAN MARTIN',              444, N'GRL.JOSE DE SAN MARTIN'),
    (456, N'LOA ANDES',               443, N'LOS ANDES'),
    -- Su única localidad (Ingeniero Maury) es del departamento Rosario de Lerma
    (453, N'ROSARIO',                 441, N'ROSARIO DE LERMA');

-- Los que en esta base no coinciden se descartan (y se informan)
SELECT 'Departamento salteado (ya unificado, o el nombre no coincide en esta base)' AS aviso, x.*
FROM @Depto x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Departamento d WHERE d.ID = x.DupID AND d.Nombre = x.DupNombre AND d.idProvincia = 16)
   OR NOT EXISTS (SELECT 1 FROM dbo.Departamento d WHERE d.ID = x.CanonID AND (d.Nombre = x.CanonNombre OR d.Nombre = N'GENERAL JOSE DE SAN MARTIN') AND d.idProvincia = 16);
DELETE x FROM @Depto x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Departamento d WHERE d.ID = x.DupID AND d.Nombre = x.DupNombre AND d.idProvincia = 16)
   OR NOT EXISTS (SELECT 1 FROM dbo.Departamento d WHERE d.ID = x.CanonID AND (d.Nombre = x.CanonNombre OR d.Nombre = N'GENERAL JOSE DE SAN MARTIN') AND d.idProvincia = 16);

UPDATE l SET idDepartamento = x.CanonID
FROM dbo.Localidad l INNER JOIN @Depto x ON x.DupID = l.idDepartamento;

DELETE d FROM dbo.Departamento d
WHERE d.ID IN (SELECT DupID FROM @Depto)
  AND NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.idDepartamento = d.ID);

UPDATE dbo.Departamento SET Nombre = N'GENERAL JOSE DE SAN MARTIN' WHERE ID = 444 AND Nombre = N'GRL.JOSE DE SAN MARTIN' AND idProvincia = 16;

-- ---------------------------------------------------------------- 4. Localidades de Salta
-- REVISAR CON EL NEGOCIO antes de Prod. Duplicado → canónico (mismo departamento después del paso 3).
DECLARE @Loc TABLE (DupID INT, DupNombre NVARCHAR(250), CanonID INT, CanonNombre NVARCHAR(250));
INSERT INTO @Loc VALUES
    -- Capital (decisión 8.2: SALTA y CAPITAL son la misma)
    (7794, N'SALTA',                                5445, N'CAPITAL'),
    (5451, N'CENTRO',                               5445, N'CAPITAL'),
    -- General Güemes
    (7795, N'GENERAL GUEMES',                       5456, N'GENERAL GUEMES'),
    (6458, N'GENERAL GUEMES',                       5456, N'GENERAL GUEMES'),
    (3817, N'GENERAL GUEMES ,EST.GUEMES',           5456, N'GENERAL GUEMES'),
    (7789, N'GRAL GUEMES',                          5456, N'GENERAL GUEMES'),
    (7796, N'GENERAL GUEMES',                       5456, N'GENERAL GUEMES'),
    (5453, N'EL BORDO',                             3793, N'EL BORDO'),
    (5447, N'CAMPO SANTO',                          3763, N'CAMPO SANTO'),
    -- General José de San Martín
    (3731, N'AGUARAY ,EST.',                        7790, N'AGUARAY'),
    (3914, N'PROFESOR SALVADOR MAZZA ,EST.POCITOS', 5452, N'SALVADOR MAZZA'),
    (3923, N'QUIJARRO, ANTONIO',                    3744, N'ANTONIO QUIJARRO'),
    -- Los Andes
    (7791, N'SAN ANTONIO DE LOS COBRES',            3937, N'SAN ANTONIO DE LOS COBRES');

SELECT 'Localidad salteada (ya unificada, o el nombre no coincide en esta base)' AS aviso, x.*
FROM @Loc x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = x.DupID AND l.Nombre = x.DupNombre)
   OR NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = x.CanonID AND (l.Nombre = x.CanonNombre OR (x.CanonID = 5445 AND l.Nombre = N'SALTA (CAPITAL)')));
DELETE x FROM @Loc x
WHERE NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = x.DupID AND l.Nombre = x.DupNombre)
   OR NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = x.CanonID AND (l.Nombre = x.CanonNombre OR (x.CanonID = 5445 AND l.Nombre = N'SALTA (CAPITAL)')));

UPDATE pe SET LocalidadID = x.CanonID FROM dbo.Persona pe INNER JOIN @Loc x ON x.DupID = pe.LocalidadID;
UPDATE pq SET DestinoID = x.CanonID FROM dbo.Paquete pq INNER JOIN @Loc x ON x.DupID = pq.DestinoID;
UPDATE pv SET LocalidadID = x.CanonID FROM dbo.Proveedor pv INNER JOIN @Loc x ON x.DupID = pv.LocalidadID;

DELETE l FROM dbo.Localidad l
WHERE l.ID IN (SELECT DupID FROM @Loc)
  AND NOT EXISTS (SELECT 1 FROM dbo.Persona pe WHERE pe.LocalidadID = l.ID)
  AND NOT EXISTS (SELECT 1 FROM dbo.Paquete pq WHERE pq.DestinoID = l.ID)
  AND NOT EXISTS (SELECT 1 FROM dbo.Proveedor pv WHERE pv.LocalidadID = l.ID);

UPDATE dbo.Localidad SET Nombre = N'SALTA (CAPITAL)' WHERE ID = 5445 AND Nombre = N'CAPITAL';
UPDATE dbo.Localidad SET Nombre = N'SAN LORENZO' WHERE ID = 5449 AND Nombre = N'SAN LOREAZO';

-- ---------------------------------------------------------------- 5. Provincia de la persona = la de su localidad
UPDATE pe SET Provincia = d.idProvincia
FROM dbo.Persona pe
INNER JOIN dbo.Localidad l ON l.ID = pe.LocalidadID
INNER JOIN dbo.Departamento d ON d.ID = l.idDepartamento
WHERE ISNULL(pe.Provincia, -1) <> d.idProvincia;

COMMIT TRANSACTION;
GO

-- ---------------------------------------------------------------- Conteos después y controles
SELECT 'DESPUES' AS momento,
       (SELECT COUNT(*) FROM dbo.Persona) AS personas,
       (SELECT COUNT(*) FROM dbo.Persona WHERE LocalidadID IS NOT NULL) AS personas_con_localidad,
       (SELECT COUNT(*) FROM dbo.Persona WHERE Nacionalidad = 'ARG') AS nacionalidad_arg,
       (SELECT COUNT(*) FROM dbo.Departamento WHERE idProvincia = 16) AS deptos_salta,
       (SELECT COUNT(*) FROM dbo.Localidad l JOIN dbo.Departamento d ON d.ID = l.idDepartamento WHERE d.idProvincia = 16) AS localidades_salta;

-- Personas que apuntan a una localidad inexistente (tiene que dar 0)
SELECT COUNT(*) AS personas_sin_localidad_valida
FROM dbo.Persona pe WHERE pe.LocalidadID IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = pe.LocalidadID);

-- Valores que quedaron fuera del catálogo (revisar a mano; el formulario los conserva)
SELECT 'Nacionalidad' AS campo, Nacionalidad AS valor, COUNT(*) AS n FROM dbo.Persona
WHERE Nacionalidad IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Pais pa WHERE pa.Codigo = Nacionalidad) GROUP BY Nacionalidad
UNION ALL
SELECT 'PaisResidencia', PaisResidencia, COUNT(*) FROM dbo.Persona
WHERE PaisResidencia IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Pais pa WHERE pa.Codigo = PaisResidencia) GROUP BY PaisResidencia
UNION ALL
SELECT 'Ocupacion', Ocupacion, COUNT(*) FROM dbo.Persona
WHERE Ocupacion IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.Ocupacion o WHERE o.Nombre = Ocupacion) GROUP BY Ocupacion;
GO

SET NOEXEC OFF;
GO

/* ---------------------------------------------------------------- REVERTIR (solo si hace falta)
BEGIN TRANSACTION;
SET IDENTITY_INSERT dbo.Departamento ON;
INSERT INTO dbo.Departamento (ID, idProvincia, Nombre, mg_DepartmentId)
SELECT b.ID, b.idProvincia, b.Nombre, b.mg_DepartmentId FROM dbo.Catalogos_Limpieza_20261104_Departamento b
WHERE NOT EXISTS (SELECT 1 FROM dbo.Departamento d WHERE d.ID = b.ID);
SET IDENTITY_INSERT dbo.Departamento OFF;
UPDATE d SET Nombre = b.Nombre FROM dbo.Departamento d JOIN dbo.Catalogos_Limpieza_20261104_Departamento b ON b.ID = d.ID;
SET IDENTITY_INSERT dbo.Localidad ON;
INSERT INTO dbo.Localidad (ID, idDepartamento, Nombre, mg_LocationId)
SELECT b.ID, b.idDepartamento, b.Nombre, b.mg_LocationId FROM dbo.Catalogos_Limpieza_20261104_Localidad b
WHERE NOT EXISTS (SELECT 1 FROM dbo.Localidad l WHERE l.ID = b.ID);
SET IDENTITY_INSERT dbo.Localidad OFF;
UPDATE l SET Nombre = b.Nombre, idDepartamento = b.idDepartamento FROM dbo.Localidad l JOIN dbo.Catalogos_Limpieza_20261104_Localidad b ON b.ID = l.ID;
UPDATE pe SET Nacionalidad = b.Nacionalidad, PaisResidencia = b.PaisResidencia, Ocupacion = b.Ocupacion, LocalidadID = b.LocalidadID, Provincia = b.Provincia
FROM dbo.Persona pe JOIN dbo.Catalogos_Limpieza_20261104_Persona b ON b.PersonaID = pe.PersonaID;
UPDATE pq SET DestinoID = b.DestinoID FROM dbo.Paquete pq JOIN dbo.Catalogos_Limpieza_20261104_Paquete b ON b.PaqueteID = pq.PaqueteID;
UPDATE pv SET LocalidadID = b.LocalidadID FROM dbo.Proveedor pv JOIN dbo.Catalogos_Limpieza_20261104_Proveedor b ON b.ProveedorID = pv.ProveedorID;
COMMIT TRANSACTION;
------------------------------------------------------------------------------- */
