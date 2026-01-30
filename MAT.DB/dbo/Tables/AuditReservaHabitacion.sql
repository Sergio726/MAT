CREATE TABLE [dbo].[AuditReservaHabitacion] (
    [Id]                  INT              IDENTITY (1, 1) NOT NULL,
    [ReservaHabitacionId] UNIQUEIDENTIFIER NULL,
    [HabitacionId]        UNIQUEIDENTIFIER NOT NULL,
    [PasajeId]            UNIQUEIDENTIFIER NOT NULL,
    [ViajeId]             UNIQUEIDENTIFIER NOT NULL,
    [UserId]              UNIQUEIDENTIFIER NOT NULL,
    [Action]              VARCHAR (50)     NOT NULL,
    [DateTime]            DATETIME         CONSTRAINT [CK_DateTime] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AuditReservaHavitacion_HabitacionId]
    ON [dbo].[AuditReservaHabitacion]([HabitacionId] ASC);

