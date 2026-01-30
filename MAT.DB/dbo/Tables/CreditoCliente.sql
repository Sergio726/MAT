CREATE TABLE [dbo].[CreditoCliente] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [NotaCreditoID] UNIQUEIDENTIFIER NULL,
    [VendedorID]    UNIQUEIDENTIFIER NOT NULL,
    [ClienteID]     UNIQUEIDENTIFIER NOT NULL,
    [Monto]         MONEY            NOT NULL,
    [Descripcion]   VARCHAR (100)    NOT NULL,
    [Fecha]         DATETIME         CONSTRAINT [DF_CreditoCliente_Fecha] DEFAULT (getdate()) NOT NULL,
    [IsInput]       BIT              CONSTRAINT [DF_CreditoCliente_IsInput] DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_CreditoCliente_Monto_IsInput] CHECK ([Monto]>=(0) AND [IsInput]=(1) OR [Monto]<(0) AND [IsInput]=(0)),
    CONSTRAINT [FK_CreditoCliente_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_CreditoCliente_Nota] FOREIGN KEY ([NotaCreditoID]) REFERENCES [dbo].[Nota] ([NotaID]),
    CONSTRAINT [FK_CreditoCliente_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);

