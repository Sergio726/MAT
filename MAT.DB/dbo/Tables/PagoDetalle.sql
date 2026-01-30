CREATE TABLE [dbo].[PagoDetalle] (
    [Id]                         INT              IDENTITY (1, 1) NOT NULL,
    [PagoID]                     UNIQUEIDENTIFIER NOT NULL,
    [MontoRecibido]              MONEY            NOT NULL,
    [MontoRecibidoMonedaTipo]    INT              NOT NULL,
    [MontoEquivalente]           MONEY            NOT NULL,
    [MontoEquivalenteMonedaTipo] INT              NOT NULL,
    [MontoEquivalenteCotizacion] MONEY            NOT NULL,
    [Fecha]                      DATETIME         CONSTRAINT [DF_PagoDetale_Fecha] DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PagoDetalle_MontoEquivalente_MonedaTipo] FOREIGN KEY ([MontoEquivalenteMonedaTipo]) REFERENCES [dbo].[MonedaTipo] ([Id]),
    CONSTRAINT [FK_PagoDetalle_MontoRecibido_MonedaTipo] FOREIGN KEY ([MontoRecibidoMonedaTipo]) REFERENCES [dbo].[MonedaTipo] ([Id]),
    CONSTRAINT [FK_PagoDetalle_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID])
);

