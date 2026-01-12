CREATE TABLE [dbo].[Presupuesto] (
    [PresupuestoID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Presupuesto_PresupuestoID] DEFAULT (newid()) NOT NULL,
    [DniCliente]         VARCHAR (50)     NOT NULL,
    [VendedorIdOrigen]   UNIQUEIDENTIFIER NOT NULL,
    [CodigoSeguimiento] VARCHAR (50)     NOT NULL,
    [MontoPactado]       FLOAT (53)       NOT NULL,
    [ViajeId]            UNIQUEIDENTIFIER NULL,
    [Estado]             INT              NOT NULL,
    [FechaCreacion]      DATETIME         CONSTRAINT [DF_Presupuesto_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [FechaExpiracion]    DATETIME         NOT NULL,
    [FacturaId]          UNIQUEIDENTIFIER NULL,
    [VendedorIdCierre]   UNIQUEIDENTIFIER NULL,
    [Observaciones]      VARCHAR (500)    NULL,
    CONSTRAINT [PK_Presupuesto] PRIMARY KEY CLUSTERED ([PresupuestoID] ASC),
    CONSTRAINT [FK_Presupuesto_VendedorOrigen] FOREIGN KEY ([VendedorIdOrigen]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_Presupuesto_VendedorCierre] FOREIGN KEY ([VendedorIdCierre]) REFERENCES [dbo].[Vendedor] ([VendedorID]),
    CONSTRAINT [FK_Presupuesto_Viaje] FOREIGN KEY ([ViajeId]) REFERENCES [dbo].[Viaje] ([ViajeID]),
    CONSTRAINT [FK_Presupuesto_Factura] FOREIGN KEY ([FacturaId]) REFERENCES [dbo].[Factura] ([FacturaID])
);

GO

-- Índice para búsqueda por DNI
CREATE NONCLUSTERED INDEX [IX_Presupuesto_DniCliente]
ON [dbo].[Presupuesto] ([DniCliente])
INCLUDE ([PresupuestoID], [Estado], [FechaExpiracion], [CodigoSeguimiento]);

GO

-- Índice para búsqueda por código de seguimiento
CREATE UNIQUE NONCLUSTERED INDEX [IX_Presupuesto_CodigoSeguimiento]
ON [dbo].[Presupuesto] ([CodigoSeguimiento])
WHERE [CodigoSeguimiento] IS NOT NULL;

GO

-- Índice para búsqueda por estado y fecha de expiración (para limpieza de expirados)
CREATE NONCLUSTERED INDEX [IX_Presupuesto_Estado_FechaExpiracion]
ON [dbo].[Presupuesto] ([Estado], [FechaExpiracion])
INCLUDE ([PresupuestoID], [DniCliente]);

GO

-- Índice para búsqueda por vendedor origen
CREATE NONCLUSTERED INDEX [IX_Presupuesto_VendedorIdOrigen]
ON [dbo].[Presupuesto] ([VendedorIdOrigen])
INCLUDE ([PresupuestoID], [Estado], [FechaCreacion]);

GO

-- Índice para búsqueda por factura (cuando se cierra)
CREATE NONCLUSTERED INDEX [IX_Presupuesto_FacturaId]
ON [dbo].[Presupuesto] ([FacturaId])
WHERE [FacturaId] IS NOT NULL;

GO

