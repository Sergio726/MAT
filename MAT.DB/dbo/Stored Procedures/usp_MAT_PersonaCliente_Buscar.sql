CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_Buscar]
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
