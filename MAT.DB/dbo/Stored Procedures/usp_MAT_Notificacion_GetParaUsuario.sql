CREATE PROCEDURE [dbo].[usp_MAT_Notificacion_GetParaUsuario]
(
    @Usuario  VARCHAR(100),
    @Cantidad INT = 30
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-06
  -- Description: Avisos vigentes de la campanita para un usuario, más recientes primero.
  --              RS1: NoLeidas (para el número de la campanita). RS2: los últimos @Cantidad con su marca de leído.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (@Cantidad IS NULL OR @Cantidad < 1 OR @Cantidad > 100) SET @Cantidad = 30;
    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);

    SELECT COUNT(*) AS NoLeidas
    FROM dbo.Notificacion n
    WHERE (n.Vence IS NULL OR n.Vence > @Ahora)
      AND NOT EXISTS (SELECT 1 FROM dbo.NotificacionLeida l WHERE l.NotificacionID = n.ID AND l.Usuario = @Usuario);

    SELECT TOP (@Cantidad) n.ID, n.Tipo, n.Titulo, n.Detalle, n.Url, n.Fecha,
           CAST(CASE WHEN l.NotificacionID IS NULL THEN 0 ELSE 1 END AS BIT) AS Leida
    FROM dbo.Notificacion n
    LEFT JOIN dbo.NotificacionLeida l ON l.NotificacionID = n.ID AND l.Usuario = @Usuario
    WHERE n.Vence IS NULL OR n.Vence > @Ahora
    ORDER BY n.Fecha DESC, n.ID DESC;
END
