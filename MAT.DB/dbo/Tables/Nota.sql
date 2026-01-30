CREATE TABLE [dbo].[Nota] (
    [NotaID]              UNIQUEIDENTIFIER NOT NULL,
    [PorcentajeRetencion] MONEY            NULL,
    [MontoRetencion]      MONEY            NULL,
    [Fecha]               DATETIME         CONSTRAINT [DF_Nota_Fecha] DEFAULT (getdate()) NOT NULL,
    [Dias]                INT              NULL,
    [ClienteID]           UNIQUEIDENTIFIER NOT NULL,
    [VendedorID]          UNIQUEIDENTIFIER NOT NULL,
    [NroNota]             VARCHAR (50)     NOT NULL,
    [MontoNota]           MONEY            NOT NULL,
    [MontoDevolucion]      MONEY            NULL,
    [Detalle]             VARCHAR (1000)   NULL,
    CONSTRAINT [PK_Nota] PRIMARY KEY CLUSTERED ([NotaID] ASC),
    CONSTRAINT [FK_Nota_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_Nota_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);

