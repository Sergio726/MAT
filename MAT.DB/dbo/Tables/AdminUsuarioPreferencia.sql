CREATE TABLE [dbo].[AdminUsuarioPreferencia] (
    [UserId]                        INT            NOT NULL,
    [AdminOnboardingCompletado]     BIT            NOT NULL CONSTRAINT [DF_AdminUsuarioPreferencia_Onboarding] DEFAULT (0),
    [FechaOnboardingCompletado]     DATETIME2 (7)  NULL,
    [FechaModificacion]             DATETIME2 (7)  NOT NULL CONSTRAINT [DF_AdminUsuarioPreferencia_FechaMod] DEFAULT (GETDATE()),
    CONSTRAINT [PK_AdminUsuarioPreferencia] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
