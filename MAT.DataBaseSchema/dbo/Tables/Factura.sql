CREATE TABLE [dbo].[Factura] (
    [FacturaID]         UNIQUEIDENTIFIER CONSTRAINT [DF_Factura_FacturaID] DEFAULT (newid()) NOT NULL,
    [NroFactura]        VARCHAR (50)     NULL,
    [Monto]             FLOAT (53)       NULL,
    [Fecha]             DATETIME         NULL,
    [Tipo]              INT              NULL,
    [Estado]            INT              NULL,
    [ClienteID]         UNIQUEIDENTIFIER NULL,
    [VendedorID]        UNIQUEIDENTIFIER NULL,
    [DescuentoAplicado] FLOAT (53)       NULL,
    CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED ([FacturaID] ASC),
    CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_Factura_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);







