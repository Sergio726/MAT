CREATE TABLE [dbo].[ObservacionViaje] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [ViajeID]          UNIQUEIDENTIFIER NOT NULL,
    [VendedorID]       INT              NOT NULL,
    [Fecha]            DATETIME         CONSTRAINT [DF_ObservacionViaje_Fecha] DEFAULT (getdate()) NOT NULL,
    [PasajerosID]      VARCHAR (MAX)    NULL,
    [Detalle]          VARCHAR (5000)   NULL,
    [CategoriaId]      INT              NULL,
    [Update]           DATETIME         NULL,
    [UpdateVendedorID] INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ObservacionViaje_ObservacionViajeCategoria] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[ObservacionViajeCategoria] ([Id]),
    CONSTRAINT [FK_ObservacionViaje_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ObservacionViaje_ViajeID]
    ON [dbo].[ObservacionViaje]([ViajeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ObservacionViaje_VendedorID]
    ON [dbo].[ObservacionViaje]([VendedorID] ASC);

