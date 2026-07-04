CREATE PROCEDURE [dbo].[usp_MAT_Precio_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Precios disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  --              Reemplaza GetAllPrecios + loop GetById + Except en memoria
  --              del RenderGridPrecios.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         p.PrecioID
        ,p.Monto
        ,p.Vigencia
        ,p.Descripcion
        ,p.Mes
    FROM dbo.Precio p
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaquetePrecio pp
              WHERE pp.PrecioID = p.PrecioID
                AND pp.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR p.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY p.Descripcion;
END
