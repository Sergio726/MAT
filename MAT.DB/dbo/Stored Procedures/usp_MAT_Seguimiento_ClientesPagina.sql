CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_ClientesPagina]
(
    @Categoria          VARCHAR(20)  = NULL,
    @Texto              VARCHAR(100) = NULL,
    @IncluirNoContactar BIT          = 0,
    @Orden              VARCHAR(20)  = 'viajes',
    @Desc               BIT          = 1,
    @Inicio             INT          = 0,
    @Cantidad           INT          = 25
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Seguimiento por categoría o búsqueda, paginado en la base (módulo Clientes F5).
  --              Antes usp_MAT_Seguimiento_Clientes mandaba la categoría entera al navegador (Dormido: más de 6.000).
  --              @Orden: cliente | viajes | ultimoViaje | proximoViaje | ultimoContacto. @Cantidad: 10 a 100.
  --              RS1: Total. RS2: la página (mismas columnas que fn_MAT_Seguimiento_Clientes).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Inicio IS NULL OR @Inicio < 0) SET @Inicio = 0;
    IF (@Cantidad IS NULL OR @Cantidad < 10) SET @Cantidad = 10;
    IF (@Cantidad > 100) SET @Cantidad = 100;
    IF (@Orden IS NULL OR @Orden NOT IN ('cliente', 'viajes', 'ultimoViaje', 'proximoViaje', 'ultimoContacto')) SET @Orden = 'viajes';
    SET @Desc = ISNULL(@Desc, 1);

    -- Hora argentina: el servidor está en UTC y desde las 21:00 "hoy" sería mañana.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);
    DECLARE @Busqueda VARCHAR(102) = '%' + NULLIF(LTRIM(RTRIM(@Texto)), '') + '%';

    SELECT s.*
    INTO #Lista
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE (@Categoria IS NULL OR s.Categoria = @Categoria)
      AND (@Busqueda IS NULL
           OR s.Apellido LIKE @Busqueda
           OR s.Nombre LIKE @Busqueda
           OR (s.Apellido + ' ' + s.Nombre) LIKE @Busqueda
           OR REPLACE(s.NroDocumento, '.', '') LIKE REPLACE(@Busqueda, '.', ''))
      AND (@IncluirNoContactar = 1 OR s.NoContactar = 0);

    SELECT COUNT(*) AS Total FROM #Lista;

    SELECT *
    FROM #Lista l
    ORDER BY
        CASE WHEN @Orden = 'cliente' AND @Desc = 0 THEN LTRIM(l.Apellido) + ' ' + LTRIM(l.Nombre) END ASC,
        CASE WHEN @Orden = 'cliente' AND @Desc = 1 THEN LTRIM(l.Apellido) + ' ' + LTRIM(l.Nombre) END DESC,
        CASE WHEN @Orden = 'viajes' AND @Desc = 0 THEN l.ViajesTotales END ASC,
        CASE WHEN @Orden = 'viajes' AND @Desc = 1 THEN l.ViajesTotales END DESC,
        CASE WHEN @Orden = 'ultimoViaje' AND @Desc = 0 THEN l.UltimoViaje END ASC,
        CASE WHEN @Orden = 'ultimoViaje' AND @Desc = 1 THEN l.UltimoViaje END DESC,
        CASE WHEN @Orden = 'proximoViaje' AND @Desc = 0 THEN l.ProximoViaje END ASC,
        CASE WHEN @Orden = 'proximoViaje' AND @Desc = 1 THEN l.ProximoViaje END DESC,
        CASE WHEN @Orden = 'ultimoContacto' AND @Desc = 0 THEN l.UltimoContactoFecha END ASC,
        CASE WHEN @Orden = 'ultimoContacto' AND @Desc = 1 THEN l.UltimoContactoFecha END DESC,
        l.UltimoViaje DESC, l.Apellido, l.Nombre, l.ClienteID
    OFFSET @Inicio ROWS FETCH NEXT @Cantidad ROWS ONLY;
END
