CREATE TABLE [dbo].[Pasaje] (
    [PasajeID]     UNIQUEIDENTIFIER CONSTRAINT [DF_Pasaje_PasajeID] DEFAULT (newid()) NOT NULL,
    [PasajeroID]   UNIQUEIDENTIFIER NULL,
    [ButacaID]     UNIQUEIDENTIFIER NULL,
    [FechaReserva] DATE             NULL,
    [FechaCompra]  DATE             NULL,
    [ViajeID]      UNIQUEIDENTIFIER NULL,
    [FacturaID]    UNIQUEIDENTIFIER NULL,
    [EstadoPasaje] INT              NOT NULL,
    [VoucherID]    UNIQUEIDENTIFIER NULL,
    [PrecioID]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Pasaje] PRIMARY KEY CLUSTERED ([PasajeID] ASC),
    CONSTRAINT [FK_Pasaje_Butaca] FOREIGN KEY ([ButacaID]) REFERENCES [dbo].[Butaca] ([ButacaID]),
    CONSTRAINT [FK_Pasaje_Factura] FOREIGN KEY ([FacturaID]) REFERENCES [dbo].[Factura] ([FacturaID]),
    CONSTRAINT [FK_Pasaje_Pasajero] FOREIGN KEY ([PasajeroID]) REFERENCES [dbo].[Pasajero] ([PasajeroID]),
    CONSTRAINT [FK_Pasaje_Precio] FOREIGN KEY ([PrecioID]) REFERENCES [dbo].[Precio] ([PrecioID]),
    CONSTRAINT [FK_Pasaje_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID]),
    CONSTRAINT [FK_Pasaje_Voucher] FOREIGN KEY ([VoucherID]) REFERENCES [dbo].[Voucher] ([VoucherID]),
    CONSTRAINT [FK_PasajeEstadoPasaje] FOREIGN KEY ([EstadoPasaje]) REFERENCES [dbo].[EstadoPasaje] ([ID])
);




GO
CREATE NONCLUSTERED INDEX [IX_Pasaje__ViajeID]
    ON [dbo].[Pasaje]([ViajeID] ASC)
    INCLUDE([EstadoPasaje], [FacturaID]);


GO
CREATE NONCLUSTERED INDEX [IX_Pasaje_FacturaID_PasajeroIDViajeID]
    ON [dbo].[Pasaje]([FacturaID] ASC)
    INCLUDE([PasajeroID], [ViajeID]);


GO
CREATE NONCLUSTERED INDEX [IX_Pasaje_FacturaID_Include_Core]
    ON [dbo].[Pasaje]([FacturaID] ASC)
    INCLUDE([PasajeID], [ViajeID], [PasajeroID], [ButacaID]);

