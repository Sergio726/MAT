CREATE TABLE [dbo].[Presupuesto] (
    [PresupuestoID]     UNIQUEIDENTIFIER CONSTRAINT [DF_Presupuesto_PresupuestoID] DEFAULT (newid()) NOT NULL,
    [DniCliente]        VARCHAR (50)     NOT NULL,
    [VendedorIdOrigen]  UNIQUEIDENTIFIER NOT NULL,
    [CodigoSeguimiento] VARCHAR (50)     NOT NULL,
    [MontoPactado]      FLOAT (53)       NOT NULL,
    [ViajeId]           UNIQUEIDENTIFIER NULL,
    [Estado]            INT              NOT NULL,
    [FechaCreacion]     DATETIME         CONSTRAINT [DF_Presupuesto_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [FechaExpiracion]   DATETIME         NOT NULL,
    [FacturaId]         UNIQUEIDENTIFIER NULL,
    [VendedorIdCierre]  UNIQUEIDENTIFIER NULL,
    [Observaciones]     VARCHAR (500)    NULL,
    CONSTRAINT [PK_Presupuesto] PRIMARY KEY CLUSTERED ([PresupuestoID] ASC),
    CONSTRAINT [FK_Presupuesto_Factura] FOREIGN KEY ([FacturaId]) REFERENCES [dbo].[Factura] ([FacturaID]),
    CONSTRAINT [FK_Presupuesto_VendedorCierre] FOREIGN KEY ([VendedorIdCierre]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_Presupuesto_VendedorOrigen] FOREIGN KEY ([VendedorIdOrigen]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_Presupuesto_Viaje] FOREIGN KEY ([ViajeId]) REFERENCES [dbo].[Viaje] ([ViajeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Presupuesto_FacturaId]
    ON [dbo].[Presupuesto]([FacturaId] ASC) WHERE ([FacturaId] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Presupuesto_VendedorIdOrigen]
    ON [dbo].[Presupuesto]([VendedorIdOrigen] ASC)
    INCLUDE([PresupuestoID], [Estado], [FechaCreacion]);


GO
CREATE NONCLUSTERED INDEX [IX_Presupuesto_Estado_FechaExpiracion]
    ON [dbo].[Presupuesto]([Estado] ASC, [FechaExpiracion] ASC)
    INCLUDE([PresupuestoID], [DniCliente]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Presupuesto_CodigoSeguimiento]
    ON [dbo].[Presupuesto]([CodigoSeguimiento] ASC) WHERE ([CodigoSeguimiento] IS NOT NULL);


GO
CREATE NONCLUSTERED INDEX [IX_Presupuesto_DniCliente]
    ON [dbo].[Presupuesto]([DniCliente] ASC)
    INCLUDE([PresupuestoID], [Estado], [FechaExpiracion], [CodigoSeguimiento]);

