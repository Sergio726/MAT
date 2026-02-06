CREATE TABLE [dbo].[CreditoCliente] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [NotaCreditoID] UNIQUEIDENTIFIER NULL,
    [VendedorID]    UNIQUEIDENTIFIER NOT NULL,
    [ClienteID]     UNIQUEIDENTIFIER NOT NULL,
    [Monto]         MONEY            NOT NULL,
    [Descripcion]   VARCHAR (100)    NOT NULL,
    [Fecha]         DATETIME         CONSTRAINT [DF_CreditoCliente_Fecha] DEFAULT (getdate()) NOT NULL,
    [IsInput]       BIT              CONSTRAINT [DF_CreditoCliente_IsInput] DEFAULT ((0)) NOT NULL,
    [PagoID]        UNIQUEIDENTIFIER NULL,
    [FacturaID]     UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_CreditoCliente_Monto_IsInput] CHECK ([Monto]>=(0) AND [IsInput]=(1) OR [Monto]<(0) AND [IsInput]=(0)),
    CONSTRAINT [FK_CreditoCliente_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_CreditoCliente_Nota] FOREIGN KEY ([NotaCreditoID]) REFERENCES [dbo].[Nota] ([NotaID]),
    CONSTRAINT [FK_CreditoCliente_Vendedor] FOREIGN KEY ([VendedorID]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_CreditoCliente_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID]),
    CONSTRAINT [FK_CreditoCliente_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID])
);

GO
-- Movimientos por cliente (GetMovimientoNotaCreditoByClienteID: WHERE ClienteID, ORDER BY Id DESC)
CREATE NONCLUSTERED INDEX [IX_CreditoCliente_ClienteID_Id]
    ON [dbo].[CreditoCliente]([ClienteID] ASC, [Id] DESC);

GO
-- Agregado por cliente (PersonaCliente_CreditoCliente_GetAll: GROUP BY ClienteID, SUM/MAX)
CREATE NONCLUSTERED INDEX [IX_CreditoCliente_ClienteID_Fecha]
    ON [dbo].[CreditoCliente]([ClienteID] ASC)
    INCLUDE ([Monto], [Fecha]);

GO
-- Allocate: JOIN por ClienteID, IsInput=1, NotaCreditoID
CREATE NONCLUSTERED INDEX [IX_CreditoCliente_ClienteID_IsInput_NotaCreditoID]
    ON [dbo].[CreditoCliente]([ClienteID] ASC, [IsInput] ASC, [NotaCreditoID] ASC);

