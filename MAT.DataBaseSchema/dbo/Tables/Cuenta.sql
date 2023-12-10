CREATE TABLE [dbo].[Cuenta] (
    [CuentaID]  UNIQUEIDENTIFIER CONSTRAINT [DF_CuentaCorriente_CuentaCorrienteID] DEFAULT (newid()) NOT NULL,
    [ClienteID] UNIQUEIDENTIFIER NULL,
    [Estado]    BIT              NULL,
    CONSTRAINT [PK_CuentaCorriente] PRIMARY KEY CLUSTERED ([CuentaID] ASC),
    CONSTRAINT [FK_Cuenta_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID])
);

