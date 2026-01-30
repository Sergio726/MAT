CREATE TABLE [dbo].[DetalleFactura] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [FacturaID]   UNIQUEIDENTIFIER NOT NULL,
    [Detalle]     VARCHAR (300)    NOT NULL,
    [Precio]      MONEY            NOT NULL,
    [Cantidad]    INT              NOT NULL,
    [Fecha]       DATETIME         CONSTRAINT [DF_DetalleFactura_Fecha] DEFAULT (getdate()) NULL,
    [AdicionalID] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID]),
    CONSTRAINT [FK_DetalleFactura_Adicional] FOREIGN KEY ([AdicionalID]) REFERENCES [dbo].[Adicional] ([AdicionalID])
);




GO
CREATE NONCLUSTERED INDEX [IX_DetalleFactura_FacturaID]
    ON [dbo].[DetalleFactura]([FacturaID] ASC)
    INCLUDE([Precio]);


GO
CREATE NONCLUSTERED INDEX [IX_DetalleFactura_FacturaID_Include_Items]
    ON [dbo].[DetalleFactura]([FacturaID] ASC)
    INCLUDE([Precio], [Fecha], [Detalle], [Cantidad]);

