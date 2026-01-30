CREATE TABLE [dbo].[CuentaCorriente] (
    [CuentaCorrienteID] UNIQUEIDENTIFIER NOT NULL,
    [Fecha]             DATETIME         NULL,
    [Monto]             FLOAT (53)       NULL,
    [ClienteID]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_CuentaCorriente_1] PRIMARY KEY CLUSTERED ([CuentaCorrienteID] ASC),
    CONSTRAINT [FK_CuentaCorriente_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID])
);

