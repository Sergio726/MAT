-- =============================================
-- Upgrade: Trazabilidad Nota de Crédito (NotaCreditoAplicacion)
-- Ejecutar en la base de datos si las tablas/SP ya existen y no se despliegan desde SSDT.
-- 2026-02
-- =============================================

-- 1) Tabla NotaCreditoAplicacion (solo si no existe)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'NotaCreditoAplicacion')
BEGIN
    CREATE TABLE [dbo].[NotaCreditoAplicacion] (
        [Id]              INT              IDENTITY (1, 1) NOT NULL,
        [NotaCreditoID]   UNIQUEIDENTIFIER NOT NULL,
        [PagoID]          UNIQUEIDENTIFIER NOT NULL,
        [FacturaID]       UNIQUEIDENTIFIER NOT NULL,
        [MontoAplicado]   MONEY            NOT NULL,
        [Fecha]           DATETIME         CONSTRAINT [DF_NotaCreditoAplicacion_Fecha] DEFAULT (getdate()) NOT NULL,
        CONSTRAINT [PK_NotaCreditoAplicacion] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_NotaCreditoAplicacion_Nota] FOREIGN KEY ([NotaCreditoID]) REFERENCES [dbo].[Nota] ([NotaID]),
        CONSTRAINT [FK_NotaCreditoAplicacion_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID]),
        CONSTRAINT [FK_NotaCreditoAplicacion_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID])
    );
    CREATE NONCLUSTERED INDEX [IX_NotaCreditoAplicacion_NotaCreditoID] ON [dbo].[NotaCreditoAplicacion]([NotaCreditoID] ASC);
    CREATE NONCLUSTERED INDEX [IX_NotaCreditoAplicacion_PagoID] ON [dbo].[NotaCreditoAplicacion]([PagoID] ASC);
END
GO

-- 2) Columnas PagoID y FacturaID en CreditoCliente (solo si no existen)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.CreditoCliente') AND name = 'PagoID')
    ALTER TABLE [dbo].[CreditoCliente] ADD [PagoID] UNIQUEIDENTIFIER NULL;
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('dbo.CreditoCliente') AND name = 'FacturaID')
    ALTER TABLE [dbo].[CreditoCliente] ADD [FacturaID] UNIQUEIDENTIFIER NULL;
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CreditoCliente_Pago')
    ALTER TABLE [dbo].[CreditoCliente] ADD CONSTRAINT [FK_CreditoCliente_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID]);
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_CreditoCliente_Factura')
    ALTER TABLE [dbo].[CreditoCliente] ADD CONSTRAINT [FK_CreditoCliente_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID]);
GO
