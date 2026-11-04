CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_Clientes]
(
    @Categoria          VARCHAR(20)  = NULL,
    @Texto              VARCHAR(100) = NULL,
    @IncluirNoContactar BIT          = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Listado de seguimiento de clientes por categoría y/o texto (apellido, nombre o DNI).
  --              Por defecto excluye a los marcados "No volver a contactar".
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    -- Hora argentina: el servidor está en UTC y desde las 21:00 "hoy" sería mañana.
    DECLARE @Hoy DATE = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATE);
    DECLARE @Busqueda VARCHAR(102) = '%' + NULLIF(LTRIM(RTRIM(@Texto)), '') + '%';

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE (@Categoria IS NULL OR s.Categoria = @Categoria)
      AND (@Busqueda IS NULL
           OR s.Apellido LIKE @Busqueda
           OR s.Nombre LIKE @Busqueda
           OR (s.Apellido + ' ' + s.Nombre) LIKE @Busqueda
           OR REPLACE(s.NroDocumento, '.', '') LIKE REPLACE(@Busqueda, '.', ''))
      AND (@IncluirNoContactar = 1 OR s.NoContactar = 0)
    ORDER BY s.ViajesTotales DESC, s.UltimoViaje DESC, s.Apellido, s.Nombre;
END
