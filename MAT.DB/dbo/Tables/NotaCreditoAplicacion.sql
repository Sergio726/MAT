CREATE TABLE [dbo].[NotaCreditoAplicacion] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [NotaCreditoID]   UNIQUEIDENTIFIER NOT NULL,
    [PagoID]          UNIQUEIDENTIFIER NOT NULL,
    [FacturaID]       UNIQUEIDENTIFIER NOT NULL,
    [MontoAplicado]   MONEY            NOT NULL,
    [Fecha]           DATETIME         CONSTRAINT [DF_NotaCreditoAplicacion_Fecha] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_NotaCreditoAplicacion] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NotaCreditoAplicacion_Nota] FOREIGN KEY ([NotaCreditoID]) REFERENCES [dbo].[Nota] ([NotaID]),
    CONSTRAINT [FK_NotaCreditoAplicacion_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID]),
    CONSTRAINT [FK_NotaCreditoAplicacion_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID])
);

GO
CREATE NONCLUSTERED INDEX [IX_NotaCreditoAplicacion_NotaCreditoID]
    ON [dbo].[NotaCreditoAplicacion]([NotaCreditoID] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_NotaCreditoAplicacion_PagoID]
    ON [dbo].[NotaCreditoAplicacion]([PagoID] ASC);
