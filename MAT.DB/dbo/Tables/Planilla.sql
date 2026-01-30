CREATE TABLE [dbo].[Planilla] (
    [PlanillaID]    UNIQUEIDENTIFIER NOT NULL,
    [ViajeID]       UNIQUEIDENTIFIER NOT NULL,
    [FechaRegistro] DATE             NOT NULL,
    [Total]         FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_PlanillaServicio] PRIMARY KEY CLUSTERED ([PlanillaID] ASC)
);

