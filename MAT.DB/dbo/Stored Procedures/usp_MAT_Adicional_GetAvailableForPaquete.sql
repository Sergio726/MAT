CREATE PROCEDURE [dbo].[usp_MAT_Adicional_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Adicionales disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  --              Reemplaza GetAllAdicionales + loop GetById + Except en memoria
  --              del RenderGridAdicionales.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         a.AdicionalID
        ,a.Monto
        ,a.Descripcion
    FROM dbo.Adicional a
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaqueteAdicional pa
              WHERE pa.AdicionalID = a.AdicionalID
                AND pa.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR a.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY a.Descripcion;
END
