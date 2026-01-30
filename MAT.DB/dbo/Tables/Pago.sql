CREATE TABLE [dbo].[Pago] (
    [PagoID]        UNIQUEIDENTIFIER CONSTRAINT [DF_Pago_PagoID] DEFAULT (newid()) NOT NULL,
    [FechaPago]     DATETIME         NOT NULL,
    [Monto]         MONEY            NOT NULL,
    [TipoPago]      INT              NOT NULL,
    [VendedorId]    UNIQUEIDENTIFIER NOT NULL,
    [NroRecibo]     VARCHAR (50)     NOT NULL,
    [TransaccionID] VARCHAR (50)     NULL,
    CONSTRAINT [PK_Pago] PRIMARY KEY CLUSTERED ([PagoID] ASC),
    CONSTRAINT [FK_Pago_TipoPago] FOREIGN KEY ([TipoPago]) REFERENCES [dbo].[PagoTipo] ([Id]),
    CONSTRAINT [FK_Pago_Vendedor] FOREIGN KEY ([VendedorId]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Pago__FechaPago]
    ON [dbo].[Pago]([FechaPago] ASC)
    INCLUDE([VendedorId], [Monto], [NroRecibo], [TransaccionID], [TipoPago]);


GO
CREATE NONCLUSTERED INDEX [IX_Pago_PagoID]
    ON [dbo].[Pago]([PagoID] ASC)
    INCLUDE([Monto]);

