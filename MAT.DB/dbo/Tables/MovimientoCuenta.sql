CREATE TABLE [dbo].[MovimientoCuenta] (
    [MovimientoID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Table_1_CuentaCorriente] DEFAULT (newid()) NOT NULL,
    [PagoID]            UNIQUEIDENTIFIER NULL,
    [FacturaID]         UNIQUEIDENTIFIER NOT NULL,
    [FechaRegistro]     DATETIME         NULL,
    [CuentaID]          UNIQUEIDENTIFIER NOT NULL,
    [NotaID]            UNIQUEIDENTIFIER NULL,
    [CuentaCorrienteID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_MovimientoCuenta] PRIMARY KEY CLUSTERED ([MovimientoID] ASC),
    CONSTRAINT [FK_MovimientoCuenta_Cuenta] FOREIGN KEY ([CuentaID]) REFERENCES [dbo].[Cuenta] ([CuentaID]),
    CONSTRAINT [FK_MovimientoCuenta_CuentaCorriente] FOREIGN KEY ([CuentaCorrienteID]) REFERENCES [dbo].[CuentaCorriente] ([CuentaCorrienteID]),
    CONSTRAINT [FK_MovimientoCuenta_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID]),
    CONSTRAINT [FK_MovimientoCuenta_Nota] FOREIGN KEY ([NotaID]) REFERENCES [dbo].[Nota] ([NotaID]),
    CONSTRAINT [FK_MovimientoCuenta_Pago] FOREIGN KEY ([PagoID]) REFERENCES [dbo].[Pago] ([PagoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_MovimientoCuenta__PagoID]
    ON [dbo].[MovimientoCuenta]([PagoID] ASC)
    INCLUDE([CuentaID]);


GO
CREATE NONCLUSTERED INDEX [IX_MovimientoCuenta__FacturaID]
    ON [dbo].[MovimientoCuenta]([FacturaID] ASC);

