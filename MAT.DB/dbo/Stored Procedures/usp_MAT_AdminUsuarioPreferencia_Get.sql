CREATE PROCEDURE [dbo].[usp_MAT_AdminUsuarioPreferencia_Get]
    @UserId INT
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-06-19
  -- Description: Obtiene preferencias de panel Admin por UserId (SimpleMembership).
  --              Si no existe fila, el caller interpreta onboarding no completado.
  ============================================= */
BEGIN
    SET NOCOUNT ON;

    SELECT
        UserId,
        AdminOnboardingCompletado,
        FechaOnboardingCompletado,
        FechaModificacion
    FROM dbo.AdminUsuarioPreferencia
    WHERE UserId = @UserId;
END
