CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_ResumenCategorias]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Cantidad de clientes por categoría de seguimiento (sin los "No volver a contactar").
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    SELECT s.Categoria, COUNT(*) AS Cantidad
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.NoContactar = 0
    GROUP BY s.Categoria;
END
