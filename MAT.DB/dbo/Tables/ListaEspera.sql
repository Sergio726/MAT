CREATE TABLE [dbo].[ListaEspera] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [ViajeID]          UNIQUEIDENTIFIER NOT NULL,
    [ClienteID]        UNIQUEIDENTIFIER NULL,
    [UsuarioID]        INT              NOT NULL,
    [Fecha]            DATETIME         DEFAULT (getdate()) NOT NULL,
    [Observacion]      VARCHAR (300)    NULL,
    [PasajeroTemporal] VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ListaEspera_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_ListaEspera_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID])
);

