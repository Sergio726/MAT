-- =============================================================================
-- 2026-11-04 — Módulo Clientes F2: listado y búsqueda paginados
-- Author: Sebastian Garcia
-- usp_MAT_PersonaCliente_Buscar (listado paginado; documento y teléfonos por dígitos; sin acentos)
-- usp_MAT_Reportes_ClientesNuevosPagina (Altas por período paginado / todo para Excel)
-- Requiere fn_MAT_Seguimiento_Clientes (database/2026-10-29_SeguimientoClientes.sql).
-- Paridad con MAT.DB (generado desde esos archivos). Idempotente: CREATE OR ALTER. No modifica datos.
-- =============================================================================
SET NOCOUNT ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_Buscar]
(
    @Texto     NVARCHAR (200) = NULL,
    @TipoID    INT            = NULL,
    @Categoria VARCHAR (20)   = NULL,
    @Orden     VARCHAR (20)   = 'apellido',
    @Pagina    INT            = 1,
    @Tamanio   INT            = 25
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
           l.Nombre AS LocalidadNombre, c.TipoID, c.FechaAlta,
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
           pg.LocalidadNombre, pg.TipoID, pg.FechaAlta,
           s.Categoria, ISNULL(s.EsMenor, 0) AS EsMenor,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Factura f WHERE f.ClienteID = pg.PersonaID) THEN 1 ELSE 0 END AS BIT) AS EsTitularFacturas
    FROM #Pagina pg
    LEFT JOIN dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s ON s.ClienteID = pg.PersonaID
    ORDER BY CASE WHEN @Orden = 'alta' THEN pg.FechaAlta END DESC,
             CASE WHEN @Orden = 'documento' THEN RIGHT(REPLICATE('0', 15) + pg.DocumentoDigitos, 15) END,
             LTRIM(pg.Apellido), LTRIM(pg.Nombre), pg.PersonaID;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reportes_ClientesNuevosPagina]
(
    @FechaInicio DATE,
    @FechaFin    DATE,
    @Pagina      INT = 1,
    @Tamanio     INT = 25
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: "Altas por período" del módulo Clientes, paginado (antes se mandaban todas las filas al
  --              navegador: 3.808 entre 2020 y hoy). @Tamanio = 0 devuelve todo (exportación a Excel).
  --              RS1: Total. RS2: la página, más recientes primero.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Pagina IS NULL OR @Pagina < 1) SET @Pagina = 1;
    IF (@Tamanio IS NULL OR @Tamanio < 0) SET @Tamanio = 25;

    SELECT c.ClienteID, c.FechaAlta, c.VendedorID
    INTO #Altas
    FROM dbo.Cliente c
    WHERE c.FechaAlta >= @FechaInicio
      AND c.FechaAlta < DATEADD(DAY, 1, @FechaFin);

    SELECT COUNT(*) AS Total FROM #Altas;

    ;WITH primeraFactura AS (
        SELECT f.ClienteID, MIN(f.Fecha) AS PrimeraFecha
        FROM dbo.Factura f
        INNER JOIN #Altas a ON a.ClienteID = f.ClienteID
        WHERE f.Fecha IS NOT NULL
        GROUP BY f.ClienteID
    )
    SELECT a.ClienteID AS PersonaID,
           LTRIM(RTRIM(ISNULL(p.Apellido, ''))) + ', ' + LTRIM(RTRIM(ISNULL(p.Nombre, ''))) AS ClienteNombre,
           ISNULL(p.NroDocumento, '') AS NroDocumento,
           COALESCE(NULLIF(p.Celular, ''), p.Telefono, '') AS Telefono,
           ISNULL(p.Email, '') AS Email,
           a.FechaAlta,
           pf.PrimeraFecha,
           LTRIM(RTRIM(ISNULL(v.Nombre, '') + ' ' + ISNULL(v.Apellido, ''))) AS VendedorNombre
    FROM #Altas a
    INNER JOIN dbo.Persona p ON p.PersonaID = a.ClienteID
    LEFT JOIN primeraFactura pf ON pf.ClienteID = a.ClienteID
    LEFT JOIN dbo.Persona v ON v.PersonaID = a.VendedorID
    ORDER BY a.FechaAlta DESC, p.Apellido, a.ClienteID
    OFFSET CASE WHEN @Tamanio = 0 THEN 0 ELSE (@Pagina - 1) * @Tamanio END ROWS
    FETCH NEXT CASE WHEN @Tamanio = 0 THEN 2147483647 ELSE @Tamanio END ROWS ONLY;
END
GO
