CREATE PROCEDURE [dbo].[usp_MAT_AdminUsuarioPreferencia_SetOnboarding]
    @UserId     INT,
    @Completado BIT
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-06-19
  -- Description: Marca el onboarding del panel Admin como completado o pendiente (upsert por UserId).
  ============================================= */
BEGIN
    SET NOCOUNT ON;

    IF @UserId IS NULL OR @UserId <= 0
    BEGIN
        RAISERROR('UserId inválido.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM dbo.AdminUsuarioPreferencia WHERE UserId = @UserId)
    BEGIN
        UPDATE dbo.AdminUsuarioPreferencia
        SET AdminOnboardingCompletado = @Completado,
            FechaOnboardingCompletado = CASE WHEN @Completado = 1 THEN ISNULL(FechaOnboardingCompletado, SYSDATETIME()) ELSE NULL END,
            FechaModificacion = SYSDATETIME()
        WHERE UserId = @UserId;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.AdminUsuarioPreferencia (UserId, AdminOnboardingCompletado, FechaOnboardingCompletado, FechaModificacion)
        VALUES (@UserId, @Completado, CASE WHEN @Completado = 1 THEN SYSDATETIME() ELSE NULL END, SYSDATETIME());
    END
END
