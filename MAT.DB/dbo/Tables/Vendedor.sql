CREATE TABLE [dbo].[Vendedor] (
    [VendedorID]  UNIQUEIDENTIFIER NOT NULL,
    [Descripcion] VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([VendedorID] ASC),
    CONSTRAINT [FK_Vendedor_Persona] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Persona] ([PersonaID])
);

