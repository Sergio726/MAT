-- =============================================================================
-- 2026-11-04 — Módulo Clientes F6: baja lógica y auditoría
-- Author: Sebastian Garcia
-- * Cliente.Activo (default 1: todos los clientes actuales quedan activos), FechaBaja, MotivoBaja, BajaUsuario.
-- * Tabla ClienteAudit (alta, modificación, baja, reactivación).
-- * Los dados de baja no aparecen en: listado (usp_MAT_PersonaCliente_Buscar), autocompletado de reservas,
--   presupuestos y facturas (usp_MAT_PersonaCliente_GetTop) ni Seguimiento (fn_MAT_Seguimiento_Clientes).
-- * SPs nuevos: usp_MAT_Cliente_CambiarEstado, usp_MAT_ClienteAudit_Insert, usp_MAT_ClienteAudit_GetByCliente.
-- Paridad con MAT.DB. Idempotente. No modifica datos existentes (solo agrega columnas con valor por defecto).
-- Requiere 2026-11-04_ClienteCatalogos.sql (la ficha usa Pais.Codigo) y 2026-10-29_SeguimientoClientes.sql.
-- =============================================================================
SET NOCOUNT ON;
GO

IF COL_LENGTH('dbo.Cliente', 'Activo') IS NULL
    ALTER TABLE dbo.Cliente ADD Activo BIT CONSTRAINT DF_Cliente_Activo DEFAULT ((1)) NOT NULL;
GO
IF COL_LENGTH('dbo.Cliente', 'FechaBaja') IS NULL
    ALTER TABLE dbo.Cliente ADD FechaBaja DATETIME NULL;
GO
IF COL_LENGTH('dbo.Cliente', 'MotivoBaja') IS NULL
    ALTER TABLE dbo.Cliente ADD MotivoBaja VARCHAR(250) NULL;
GO
IF COL_LENGTH('dbo.Cliente', 'BajaUsuario') IS NULL
    ALTER TABLE dbo.Cliente ADD BajaUsuario VARCHAR(100) NULL;
GO

IF OBJECT_ID('dbo.ClienteAudit', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ClienteAudit (
        ID        INT              IDENTITY (1, 1) NOT NULL,
        ClienteID UNIQUEIDENTIFIER NOT NULL,
        Fecha     DATETIME         NOT NULL,
        Usuario   VARCHAR (100)    NULL,
        Accion    VARCHAR (20)     NOT NULL,
        Detalle   NVARCHAR (MAX)   NULL,
        CONSTRAINT PK_ClienteAudit PRIMARY KEY CLUSTERED (ID ASC),
        CONSTRAINT FK_ClienteAudit_Cliente FOREIGN KEY (ClienteID) REFERENCES dbo.Cliente (ClienteID),
        CONSTRAINT CK_ClienteAudit_Accion CHECK (Accion IN ('Alta', 'Modificacion', 'Baja', 'Reactivacion'))
    );
    CREATE NONCLUSTERED INDEX IX_ClienteAudit_ClienteID_Fecha ON dbo.ClienteAudit (ClienteID ASC, Fecha DESC);
END
GO

CREATE OR ALTER FUNCTION [dbo].[fn_MAT_Seguimiento_Clientes] (@Hoy DATE)
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
  -- 2026-11-04 (Clientes F6): excluye a los clientes dados de baja.
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
) uc
WHERE c.Activo = 1; -- F6: los dados de baja no entran en Seguimiento
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetTop]
(
    @SearchTerm NVARCHAR(200) = '',
    @TopCount INT = 12,
    @ViajeID UNIQUEIDENTIFIER = NULL
)
AS 
/*
----------------------------------------------------------------------------------------------------
-- Author:    Seba Garcia
-- Create date: 2026-01-08
-- Description: Obtiene los TOP N clientes. Sin parámetro de búsqueda: primeros 12 registros.
-- Con búsqueda: filtra por palabras en Apellido, Nombre, NroDocumento, Telefono, Celular o Email.
-- Las palabras pueden estar separadas por espacio, coma (,) o guión (-).
-- @ViajeID opcional: si se informa, excluye clientes ya inscriptos en ese viaje o en otro viaje
--   con la misma FechaSalida (fecha de inicio/salida del viaje).
-- Optimizaciones:
--   - Solo clientes (INNER JOIN Cliente). Filtro por palabras con dbo.Split.
--   - IsTituarFactura con EXISTS. TipoDocumento y Email para autocomplete.
-- 2026-11-04 (Clientes F6): no devuelve clientes dados de baja (Cliente.Activo = 0).
----------------------------------------------------------------------------------------------------
*/
BEGIN 
    SET NOCOUNT, XACT_ABORT ON; 
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 

    -- Normalizar término de búsqueda: trim y tratar coma/guion como delimitadores (unificar con espacio)
    SET @SearchTerm = LTRIM(RTRIM(ISNULL(@SearchTerm, '')));
    SET @SearchTerm = REPLACE(REPLACE(REPLACE(@SearchTerm, ',', ' '), '-', ' '), N'–', ' ');  -- guion normal y en-dash
    -- Colapsar espacios múltiples (evita palabras vacías al hacer split)
    WHILE CHARINDEX('  ', @SearchTerm) > 0
        SET @SearchTerm = REPLACE(@SearchTerm, '  ', ' ');
    SET @SearchTerm = LTRIM(RTRIM(@SearchTerm));

    -- Validar TopCount
    IF @TopCount <= 0 OR @TopCount > 100
        SET @TopCount = 12;

    -- Filtro opcional: solo clientes no inscriptos en el viaje
    -- (cuando @ViajeID se usa desde autocomplete de pasajeros en reserva)
    DECLARE @SoloDisponiblesParaViaje BIT = CASE WHEN @ViajeID IS NOT NULL THEN 1 ELSE 0 END;

    -- Sin parámetro de búsqueda: primeros N registros (solo clientes, opcionalmente disponibles para @ViajeID)
    IF LEN(@SearchTerm) = 0
    BEGIN
        SELECT TOP (@TopCount)
            p.PersonaID,
            p.Apellido,
            p.Nombre,
            p.NroDocumento,
            p.Telefono,
            p.Celular,
            ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
            ISNULL(p.Email, '') AS Email,
            LocalidadNombre = ISNULL(l.Nombre, ''),
            p.Nacionalidad,
            p.PaisResidencia,
            IsTituarFactura = CASE
                WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
                ELSE 0
            END
        FROM dbo.Persona p WITH (NOLOCK)
        INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
        LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
        WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
          AND c.Activo = 1 -- F6: los dados de baja no se ofrecen
        ORDER BY p.Apellido ASC, p.Nombre ASC;
        RETURN;
    END

    -- Con parámetro de búsqueda: filtrar por palabras usando dbo.Split
    SELECT TOP (@TopCount)
        p.PersonaID,
        p.Apellido,
        p.Nombre,
        p.NroDocumento,
        p.Telefono,
        p.Celular,
        ISNULL(p.TipoDocumento, 1) AS TipoDocumento,
        ISNULL(p.Email, '') AS Email,
        LocalidadNombre = ISNULL(l.Nombre, ''),
        p.Nacionalidad,
        p.PaisResidencia,
        IsTituarFactura = CASE
            WHEN EXISTS (SELECT 1 FROM dbo.Factura f WITH (NOLOCK) WHERE f.ClienteID = p.PersonaID) THEN 1
            ELSE 0
        END
    FROM dbo.Persona p WITH (NOLOCK)
    INNER JOIN dbo.Cliente c WITH (NOLOCK) ON c.ClienteID = p.PersonaID
    LEFT JOIN dbo.Localidad l WITH (NOLOCK) ON p.LocalidadID = l.ID
    WHERE (@SoloDisponiblesParaViaje = 0 OR dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0)
    AND c.Activo = 1 -- F6: los dados de baja no se ofrecen
    AND NOT EXISTS (
        SELECT 1
        FROM dbo.Split(CAST(@SearchTerm AS VARCHAR(200)), ' ') AS s
        WHERE LTRIM(RTRIM(s.Item)) <> ''
        AND ISNULL(p.Apellido, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Nombre, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.NroDocumento, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Telefono, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Celular, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
        AND ISNULL(p.Email, '') NOT LIKE '%' + REPLACE(REPLACE(LTRIM(RTRIM(s.Item)), '%', '[%]'), '_', '[_]') + '%'
    )
    ORDER BY p.Apellido ASC, p.Nombre ASC;

END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_Buscar]
(
    @Texto     NVARCHAR (200) = NULL,
    @TipoID    INT            = NULL,
    @Categoria VARCHAR (20)   = NULL,
    @Orden     VARCHAR (20)   = 'apellido',
    @Pagina    INT            = 1,
    @Tamanio   INT            = 25,
    @IncluirInactivos BIT     = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Listado de clientes paginado (módulo Clientes F2). Reemplaza para el listado al SP de
  --              autocompletado usp_MAT_PersonaCliente_GetTop (que sigue usándose en Reservas, DetalleFactura
  --              y Presupuesto), que cortaba en 10 resultados.
  --              @Texto: palabras en cualquier orden, todas deben coincidir. Una palabra numérica se compara
  --              contra los DÍGITOS de documento, teléfono y celular ("11470759" = "11.470.759",
  --              "3874140863" = "3874 14-0863"); el resto, sin acentos ni mayúsculas en apellido, nombre y email.
  --              @Categoria: categoría de Seguimiento (fn_MAT_Seguimiento_Clientes).
  --              @Orden: apellido | alta | documento. @Tamanio: 25, 50 o 100.
  --              RS1: Total. RS2: la página, con la categoría de cada cliente.
  -- 2026-11-04 (F6): sin @IncluirInactivos no aparecen los clientes dados de baja; se devuelve Activo.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Pagina IS NULL OR @Pagina < 1) SET @Pagina = 1;
    IF (@Tamanio IS NULL OR @Tamanio NOT IN (25, 50, 100)) SET @Tamanio = 25;
    IF (@Orden IS NULL OR @Orden NOT IN ('apellido', 'alta', 'documento')) SET @Orden = 'apellido';

    -- Hora argentina: el servidor está en UTC.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);

    -- Palabras de búsqueda (espacio o coma); las numéricas guardan sus dígitos.
    DECLARE @Palabras TABLE (Palabra NVARCHAR (200) NOT NULL, Digitos VARCHAR (200) NULL);
    SET @Texto = LTRIM(RTRIM(REPLACE(ISNULL(@Texto, N''), N',', N' ')));
    IF (@Texto <> N'')
        INSERT INTO @Palabras (Palabra)
        SELECT DISTINCT LTRIM(RTRIM(Item)) FROM dbo.Split(@Texto, ' ') WHERE LTRIM(RTRIM(Item)) <> '';

    UPDATE @Palabras
    SET Digitos = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Palabra, '.', ''), '-', ''), '(', ''), ')', ''), '+', ''), '/', '');
    UPDATE @Palabras SET Digitos = NULL WHERE Digitos = '' OR Digitos LIKE '%[^0-9]%';

    DECLARE @CantidadPalabras INT = (SELECT COUNT(*) FROM @Palabras);

    -- Clientes que cumplen texto y tipo
    SELECT p.PersonaID, p.Apellido, p.Nombre, p.NroDocumento, p.Celular, p.Telefono, p.Email,
           l.Nombre AS LocalidadNombre, c.TipoID, c.FechaAlta, c.Activo,
           d.Documento AS DocumentoDigitos
    INTO #Encontrados
    FROM dbo.Cliente c
    INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
    LEFT JOIN dbo.Localidad l ON l.ID = p.LocalidadID
    CROSS APPLY (
        SELECT REPLACE(REPLACE(REPLACE(ISNULL(p.NroDocumento, ''), '.', ''), '-', ''), ' ', '') AS Documento,
               REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(p.Telefono, ''), ' ', ''), '-', ''), '.', ''), '(', ''), ')', ''), '+', ''), '/', '') AS Telefono,
               REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(p.Celular, ''), ' ', ''), '-', ''), '.', ''), '(', ''), ')', ''), '+', ''), '/', '') AS Celular
    ) d
    WHERE (@TipoID IS NULL OR c.TipoID = @TipoID)
      AND (ISNULL(@IncluirInactivos, 0) = 1 OR c.Activo = 1)
      AND (@CantidadPalabras = 0 OR NOT EXISTS (
            -- ninguna palabra puede quedar sin coincidencia
            SELECT 1 FROM @Palabras w
            WHERE NOT (
                   (w.Digitos IS NOT NULL AND (d.Documento LIKE '%' + w.Digitos + '%'
                                               OR d.Telefono LIKE '%' + w.Digitos + '%'
                                               OR d.Celular LIKE '%' + w.Digitos + '%'))
                OR ISNULL(p.Apellido, '') COLLATE Latin1_General_CI_AI LIKE N'%' + w.Palabra + N'%' COLLATE Latin1_General_CI_AI
                OR ISNULL(p.Nombre, '') COLLATE Latin1_General_CI_AI LIKE N'%' + w.Palabra + N'%' COLLATE Latin1_General_CI_AI
                OR ISNULL(p.Email, '') COLLATE Latin1_General_CI_AI LIKE N'%' + w.Palabra + N'%' COLLATE Latin1_General_CI_AI
            )));

    IF (@Categoria IS NOT NULL)
        DELETE e
        FROM #Encontrados e
        LEFT JOIN (SELECT ClienteID FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) WHERE Categoria = @Categoria) s
               ON s.ClienteID = e.PersonaID
        WHERE s.ClienteID IS NULL;

    SELECT COUNT(*) AS Total FROM #Encontrados;

    SELECT e.*
    INTO #Pagina
    FROM #Encontrados e
    ORDER BY CASE WHEN @Orden = 'alta' THEN e.FechaAlta END DESC,
             CASE WHEN @Orden = 'documento' THEN RIGHT(REPLICATE('0', 15) + e.DocumentoDigitos, 15) END,
             LTRIM(e.Apellido), LTRIM(e.Nombre), e.PersonaID
    OFFSET (@Pagina - 1) * @Tamanio ROWS FETCH NEXT @Tamanio ROWS ONLY;

    SELECT pg.PersonaID, pg.Apellido, pg.Nombre, pg.NroDocumento, pg.Celular, pg.Telefono, pg.Email,
           pg.LocalidadNombre, pg.TipoID, pg.FechaAlta, pg.Activo,
           s.Categoria, ISNULL(s.EsMenor, 0) AS EsMenor,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Factura f WHERE f.ClienteID = pg.PersonaID) THEN 1 ELSE 0 END AS BIT) AS EsTitularFacturas
    FROM #Pagina pg
    LEFT JOIN dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s ON s.ClienteID = pg.PersonaID
    ORDER BY CASE WHEN @Orden = 'alta' THEN pg.FechaAlta END DESC,
             CASE WHEN @Orden = 'documento' THEN RIGHT(REPLICATE('0', 15) + pg.DocumentoDigitos, 15) END,
             LTRIM(pg.Apellido), LTRIM(pg.Nombre), pg.PersonaID;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_Ficha]
(
    @PersonaID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Datos de la ficha del cliente (módulo Clientes F3) en una sola consulta, con los nombres
  --              ya resueltos (localidad, departamento, provincia, vendedor), el estado de la cuenta corriente
  --              y cuántas facturas tiene como titular. Reemplaza a Persona.GetById + Cliente.GetById + una
  --              consulta por cada nombre que hacía la vista.
  -- 2026-11-04 (F4): nombre del país de nacionalidad y de residencia (Pais.Codigo).
  -- 2026-11-04 (F6): estado y datos de la baja.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT p.PersonaID, p.Apellido, p.Nombre, p.TipoDocumento, p.NroDocumento, p.FechaNacimiento, p.Sexo,
           p.Nacionalidad, p.PaisResidencia, p.Ocupacion,
           pn.Descripcion AS NacionalidadNombre, pr2.Descripcion AS PaisResidenciaNombre,
           p.Domicilio, p.Telefono, p.Celular, p.Email,
           l.Nombre AS LocalidadNombre,
           d.Nombre AS DepartamentoNombre,
           COALESCE(pr.Nombre, prl.Nombre) AS ProvinciaNombre,
           c.RazonSocial, c.Cuit, c.CondicionIva, c.FormaPago, c.Empresa, c.Fax, c.Web, c.Idioma, c.Promotor,
           c.Observacion, c.TipoID, c.FechaAlta,
           c.Activo, c.FechaBaja, c.MotivoBaja, c.BajaUsuario,
           LTRIM(RTRIM(ISNULL(v.Apellido, '') + ', ' + ISNULL(v.Nombre, ''))) AS VendedorNombre,
           cu.Estado AS CuentaHabilitada,
           (SELECT COUNT(*) FROM dbo.Factura f WHERE f.ClienteID = c.ClienteID) AS CantidadFacturas
    FROM dbo.Cliente c
    INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
    LEFT JOIN dbo.Localidad l ON l.ID = p.LocalidadID
    LEFT JOIN dbo.Departamento d ON d.ID = l.idDepartamento
    LEFT JOIN dbo.Provincia prl ON prl.ID = d.idProvincia
    LEFT JOIN dbo.Provincia pr ON pr.ID = p.Provincia
    LEFT JOIN dbo.Persona v ON v.PersonaID = c.VendedorID
    -- Nacionalidad y país de residencia se guardan como código ISO (F4)
    LEFT JOIN dbo.Pais pn ON pn.Codigo = p.Nacionalidad
    LEFT JOIN dbo.Pais pr2 ON pr2.Codigo = p.PaisResidencia
    OUTER APPLY (SELECT TOP 1 cu.Estado FROM dbo.Cuenta cu WHERE cu.ClienteID = c.ClienteID) cu
    WHERE c.ClienteID = @PersonaID;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Cliente_CambiarEstado]
(
    @ClienteID UNIQUEIDENTIFIER,
    @Activo    BIT,
    @Motivo    VARCHAR(250) = NULL,
    @Usuario   VARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Baja (@Activo = 0, con motivo) o reactivación (@Activo = 1) de un cliente, con su registro en
  --              ClienteAudit, en una transacción. No borra nada: facturas, pagos y viajes siguen accesibles.
  --              Devuelve 1 si cambió, 0 si ya estaba en ese estado o el cliente no existe.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);
    SET @Motivo = NULLIF(LTRIM(RTRIM(@Motivo)), '');

    BEGIN TRANSACTION;

    UPDATE dbo.Cliente
    SET Activo = @Activo,
        FechaBaja = CASE WHEN @Activo = 0 THEN @Ahora END,
        MotivoBaja = CASE WHEN @Activo = 0 THEN @Motivo END,
        BajaUsuario = CASE WHEN @Activo = 0 THEN @Usuario END
    WHERE ClienteID = @ClienteID AND Activo <> @Activo;

    DECLARE @Cambio INT = @@ROWCOUNT;
    IF (@Cambio = 1)
        INSERT INTO dbo.ClienteAudit (ClienteID, Fecha, Usuario, Accion, Detalle)
        VALUES (@ClienteID, @Ahora, @Usuario, CASE WHEN @Activo = 0 THEN 'Baja' ELSE 'Reactivacion' END,
                CASE WHEN @Activo = 0 THEN N'Motivo: ' + ISNULL(@Motivo, N'(sin motivo)') END);

    COMMIT TRANSACTION;

    SELECT @Cambio AS Cambio;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ClienteAudit_Insert]
(
    @ClienteID UNIQUEIDENTIFIER,
    @Usuario   VARCHAR(100)  = NULL,
    @Accion    VARCHAR(20),
    @Detalle   NVARCHAR(MAX) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Registra un evento en el historial del cliente (alta o modificación; la baja y la
  --              reactivación las registra usp_MAT_Cliente_CambiarEstado). Hora argentina.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    INSERT INTO dbo.ClienteAudit (ClienteID, Fecha, Usuario, Accion, Detalle)
    VALUES (@ClienteID, CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME),
            @Usuario, @Accion, NULLIF(LTRIM(RTRIM(@Detalle)), N''));
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ClienteAudit_GetByCliente]
(
    @ClienteID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Historial de cambios del cliente, más reciente primero (pestaña de la ficha, solo administradores).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT a.Fecha, a.Usuario, a.Accion, a.Detalle
    FROM dbo.ClienteAudit a
    WHERE a.ClienteID = @ClienteID
    ORDER BY a.Fecha DESC, a.ID DESC;
END
GO
