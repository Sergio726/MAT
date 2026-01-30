CREATE TABLE [dbo].[PlanillaServicioItem] (
    [PlanillaServicioItemID] UNIQUEIDENTIFIER NOT NULL,
    [PlanillaID]             UNIQUEIDENTIFIER NOT NULL,
    [ServicioID]             UNIQUEIDENTIFIER NOT NULL,
    [Cantidad]               INT              NOT NULL,
    [Subtotal]               FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_PlanillaServicioItem] PRIMARY KEY CLUSTERED ([PlanillaServicioItemID] ASC),
    CONSTRAINT [FK_PlanillaServicioItem_Planilla] FOREIGN KEY ([PlanillaID]) REFERENCES [dbo].[Planilla] ([PlanillaID])
);

