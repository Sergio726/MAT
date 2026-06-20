-- =============================================
-- Author:    Sebastian Garcia
-- Create date: 2026-06-19
-- Description: Preferencias de panel Admin por usuario (onboarding guiado).
--              Tabla AdminUsuarioPreferencia + SPs Get / SetOnboarding
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminUsuarioPreferencia]') AND type IN (N'U'))
BEGIN
    CREATE TABLE [dbo].[AdminUsuarioPreferencia] (
        [UserId]                        INT            NOT NULL,
        [AdminOnboardingCompletado]     BIT            NOT NULL CONSTRAINT [DF_AdminUsuarioPreferencia_Onboarding] DEFAULT (0),
        [FechaOnboardingCompletado]     DATETIME2 (7)  NULL,
        [FechaModificacion]             DATETIME2 (7)  NOT NULL CONSTRAINT [DF_AdminUsuarioPreferencia_FechaMod] DEFAULT (GETDATE()),
        CONSTRAINT [PK_AdminUsuarioPreferencia] PRIMARY KEY CLUSTERED ([UserId] ASC)
    );
END
GO

IF OBJECT_ID('dbo.usp_MAT_AdminUsuarioPreferencia_Get', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_AdminUsuarioPreferencia_Get]
        @UserId INT
    AS
    /*-- =============================================
      -- Author:    Sebastian Garcia
      -- Create date: 2026-06-19
      -- Description: Obtiene preferencias de panel Admin por UserId.
      ============================================= */
    BEGIN
        SET NOCOUNT ON;
        SELECT UserId, AdminOnboardingCompletado, FechaOnboardingCompletado, FechaModificacion
        FROM dbo.AdminUsuarioPreferencia
        WHERE UserId = @UserId;
    END
    ');
END
GO

IF OBJECT_ID('dbo.usp_MAT_AdminUsuarioPreferencia_SetOnboarding', 'P') IS NULL
BEGIN
    EXEC('
    CREATE PROCEDURE [dbo].[usp_MAT_AdminUsuarioPreferencia_SetOnboarding]
        @UserId     INT,
        @Completado BIT
    AS
    /*-- =============================================
      -- Author:    Sebastian Garcia
      -- Create date: 2026-06-19
      -- Description: Upsert onboarding completado para panel Admin.
      ============================================= */
    BEGIN
        SET NOCOUNT ON;
        IF @UserId IS NULL OR @UserId <= 0
        BEGIN
            RAISERROR(''UserId inválido.'', 16, 1);
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
    ');
END
GO
