CREATE TABLE [dbo].[Factura] (
    [FacturaID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Factura_FacturaID] DEFAULT (newid()) NOT NULL,
    [NroFactura]     VARCHAR (50)     NULL,
    [Monto]          FLOAT (53)       NULL,
    [Fecha]          DATETIME         NULL,
    [Tipo]           INT              NULL,
    [Estado]         INT              NULL,
    [ClienteID]      UNIQUEIDENTIFIER NOT NULL,
    [VendedorID]     UNIQUEIDENTIFIER NOT NULL,
    [Observaciones]  VARCHAR (MAX)    NULL,
    [DescuentoAplicado] FLOAT (53)    CONSTRAINT [DF_Factura_DescuentoAplicado] DEFAULT ((0)) NOT NULL,
    [DiasPreReserva] INT              DEFAULT ((0)) NULL,
    [MonedaTipo]     INT              CONSTRAINT [DF_Factura_MonedaTipo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED ([FacturaID] ASC),
    CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_Factura_MonedaTipo] FOREIGN KEY ([MonedaTipo]) REFERENCES [dbo].[MonedaTipo] ([Id]),
    CONSTRAINT [FK_Factura_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Factura_ClienteID]
    ON [dbo].[Factura]([ClienteID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Factura_Estado]
    ON [dbo].[Factura]([Estado] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Factura_VendedorID]
    ON [dbo].[Factura]([VendedorID] ASC);

