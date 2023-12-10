CREATE TABLE [dbo].[Nota] (
    [NotaID]              UNIQUEIDENTIFIER NOT NULL,
    [PorcentajeRetencion] FLOAT (53)       NULL,
    [MontoRetencion]      FLOAT (53)       NULL,
    [Fecha]               DATE             NULL,
    [Dias]                INT              NULL,
    [ClienteID]           UNIQUEIDENTIFIER NULL,
    [VendedorID]          UNIQUEIDENTIFIER NULL,
    [NroNota]             VARCHAR (50)     NULL,
    CONSTRAINT [PK_Nota] PRIMARY KEY CLUSTERED ([NotaID] ASC),
    CONSTRAINT [FK_Nota_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_Nota_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);

