CREATE PROCEDURE [dbo].[usp_MAT_Servicio_GetAvailableForPaquete]
(
    @PaqueteID UNIQUEIDENTIFIER,
    @Filter    NVARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-04
  -- Description: Servicios disponibles para vincular a un paquete (los que aun
  --              no estan vinculados), con filtro opcional por descripcion.
  --              Reemplaza el patron N+1 de ServicioMethod.GetAllEntities +
  --              Except en memoria del RenderGridServicios.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF @Filter IS NOT NULL AND LTRIM(RTRIM(@Filter)) = ''
        SET @Filter = NULL;

    SELECT TOP (100)
         s.ServicioID
        ,s.Descripcion
    FROM dbo.Servicio s
    WHERE NOT EXISTS (
              SELECT 1
              FROM dbo.PaqueteServicio ps
              WHERE ps.ServicioID = s.ServicioID
                AND ps.PaqueteID = @PaqueteID)
      AND (@Filter IS NULL OR s.Descripcion LIKE '%' + @Filter + '%')
    ORDER BY s.Descripcion;
END
