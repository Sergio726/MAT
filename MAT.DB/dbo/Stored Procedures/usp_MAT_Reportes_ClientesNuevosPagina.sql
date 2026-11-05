CREATE PROCEDURE [dbo].[usp_MAT_Reportes_ClientesNuevosPagina]
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
