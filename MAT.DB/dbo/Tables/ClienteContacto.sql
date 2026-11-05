CREATE TABLE [dbo].[ClienteContacto] (
    [ClienteContactoID] UNIQUEIDENTIFIER CONSTRAINT [DF_ClienteContacto_ClienteContactoID] DEFAULT (newid()) NOT NULL,
    [ClienteID]         UNIQUEIDENTIFIER NOT NULL,
    [VendedorID]        UNIQUEIDENTIFIER NULL,
    [Usuario]           VARCHAR (100)    NULL,
    [Fecha]             DATETIME         CONSTRAINT [DF_ClienteContacto_Fecha] DEFAULT (getdate()) NOT NULL,
    [Resultado]         INT              NOT NULL,
    [Observacion]       VARCHAR (500)    NULL,
    -- 1 WhatsApp, 2 Llamada, 3 Email, 4 Presencial (módulo Clientes F5, 2026-11-04). NULL en los registros anteriores.
    [Canal]             TINYINT          NULL,
    CONSTRAINT [PK_ClienteContacto] PRIMARY KEY CLUSTERED ([ClienteContactoID] ASC),
    CONSTRAINT [CK_ClienteContacto_Canal] CHECK ([Canal] >= 1 AND [Canal] <= 4),
    CONSTRAINT [FK_ClienteContacto_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_ClienteContacto_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [CK_ClienteContacto_Resultado] CHECK ([Resultado] >= 1 AND [Resultado] <= 8)
);


GO
CREATE NONCLUSTERED INDEX [IX_ClienteContacto_ClienteID_Fecha]
    ON [dbo].[ClienteContacto]([ClienteID] ASC, [Fecha] DESC)
    INCLUDE([Resultado], [VendedorID], [Usuario]);
