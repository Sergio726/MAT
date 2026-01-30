CREATE TABLE [dbo].[PlanillaHabitacionItem] (
    [PlanillaHabitacionItemID] UNIQUEIDENTIFIER NOT NULL,
    [PlanillaID]               UNIQUEIDENTIFIER NOT NULL,
    [HabitacionID]             UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]                 INT              NOT NULL,
    [Subtotal]                 FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_PlanillaHabitacionItem] PRIMARY KEY CLUSTERED ([PlanillaHabitacionItemID] ASC),
    CONSTRAINT [FK_PlanillaHabitacionItem_Planilla] FOREIGN KEY ([PlanillaID]) REFERENCES [dbo].[Planilla] ([PlanillaID])
);

