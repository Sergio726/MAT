CREATE PROCEDURE usp_MAT_Paquete_GetPaquetes
(
    @DateYear  VARCHAR(4)    = '',
    @Search    NVARCHAR(100) = NULL,
    @Temporada INT           = NULL,
    @Moneda    INT           = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Lista de paquetes con filtros para el listado /Paquete.
  --              @DateYear vacio devuelve todos los anios (compat con el selector
  --              de paquetes de Viaje); el listado /Paquete fija el anio actual por
  --              defecto desde el controlador. Filtros opcionales por texto
  --              (Descripcion/Codigo), Temporada y Moneda. Devuelve MonedaCodigo
  --              y el nombre del Destino (Localidad) para el listado.
  --              History:
  --                2017-2025 Garcia Sergio: version original por anio.
  --                2026-07-04 Sebastian Garcia: filtros + destino + MonedaCodigo limpio.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Year INT =
        CASE
            WHEN @DateYear IS NULL OR LTRIM(RTRIM(@DateYear)) = '' THEN NULL
            ELSE TRY_CONVERT(INT, @DateYear)
        END;

    IF @Search IS NOT NULL AND LTRIM(RTRIM(@Search)) = ''
        SET @Search = NULL;

    SELECT
         P.PaqueteID
        ,P.Descripcion
        ,P.Iva
        ,P.Alicuota
        ,P.Temporada
        ,P.Cotizacion
        ,P.Codigo
        ,P.DestinoID
        ,Destino = L.Nombre
        ,P.Foto
        ,FechaCreacion = CONVERT(VARCHAR(10), P.FechaCreacion, 103)
        ,LastUpdate    = CONVERT(VARCHAR(10), P.LastUpdate, 103)
        ,MonedaCodigo  = mt.Codigo
    FROM dbo.Paquete P
    INNER JOIN dbo.MonedaTipo mt
        ON P.Moneda = mt.Id
    LEFT JOIN dbo.Localidad L
        ON P.DestinoID = L.ID
    WHERE (@Year IS NULL OR YEAR(P.FechaCreacion) = @Year)
      AND (@Search IS NULL
           OR P.Descripcion LIKE '%' + @Search + '%'
           OR P.Codigo LIKE '%' + @Search + '%'
           OR L.Nombre LIKE '%' + @Search + '%')
      AND (@Temporada IS NULL OR P.Temporada = @Temporada)
      AND (@Moneda IS NULL OR P.Moneda = @Moneda)
    ORDER BY P.FechaCreacion DESC;
END
