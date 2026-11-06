CREATE PROCEDURE [dbo].[usp_MAT_Notificacion_MarcarLeidas]
(
    @Usuario VARCHAR(100),
    -- IDs separados por coma; NULL o vacío = todas las vigentes
    @IDs     VARCHAR(MAX) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-06
  -- Description: Marca avisos de la campanita como leídos para un usuario. Correrlo dos veces no duplica.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);
    SET @IDs = NULLIF(LTRIM(RTRIM(@IDs)), '');

    INSERT INTO dbo.NotificacionLeida (NotificacionID, Usuario, Fecha)
    SELECT n.ID, @Usuario, @Ahora
    FROM dbo.Notificacion n
    WHERE (n.Vence IS NULL OR n.Vence > @Ahora)
      AND (@IDs IS NULL OR n.ID IN (SELECT TRY_CAST(LTRIM(RTRIM(Item)) AS INT) FROM dbo.Split(@IDs, ',')))
      AND NOT EXISTS (SELECT 1 FROM dbo.NotificacionLeida l WHERE l.NotificacionID = n.ID AND l.Usuario = @Usuario);
END
