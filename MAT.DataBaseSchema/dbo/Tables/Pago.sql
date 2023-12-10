CREATE TABLE [dbo].[Pago] (
    [PagoID]            UNIQUEIDENTIFIER CONSTRAINT [DF_Pago_PagoID] DEFAULT (newid()) NOT NULL,
    [FechaPago]         DATETIME         NULL,
    [Monto]             FLOAT (53)       NULL,
    [TipoPago]          INT              NULL,
    [TransaccionID]     VARCHAR (100)    NULL,
    [ClienteId]         UNIQUEIDENTIFIER NULL,
    [VendedorId]        UNIQUEIDENTIFIER NULL,
    [NroRecibo]         VARCHAR (50)     NULL,
    [EstadoRendicion]   INT              NULL,
    [CuentaCorrienteID] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Pago] PRIMARY KEY CLUSTERED ([PagoID] ASC),
    CONSTRAINT [FK_Pago_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_Pago_CuentaCorriente] FOREIGN KEY ([CuentaCorrienteID]) REFERENCES [dbo].[CuentaCorriente] ([CuentaCorrienteID]),
    CONSTRAINT [FK_Pago_Vendedor] FOREIGN KEY ([VendedorId]) REFERENCES [dbo].[Vendedor] ([VendedorID])
);







