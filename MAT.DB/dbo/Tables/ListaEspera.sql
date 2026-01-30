CREATE TABLE [dbo].[ListaEspera] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [ViajeID]          UNIQUEIDENTIFIER NOT NULL,
    [ClienteID]        UNIQUEIDENTIFIER NULL,
    [UsuarioID]        INT              NOT NULL,
    [Fecha]            DATETIME         DEFAULT (getdate()) NOT NULL,
    [Observacion]      VARCHAR (300)    NULL,
    [PasajeroTemporal] VARCHAR (100)    NULL,
    [IsDeleted]        BIT              CONSTRAINT [DF_ListaEspera_IsDeleted] DEFAULT ((0)) NOT NULL,
    [DeletedAt]        DATETIME         NULL,
    [DeletedBy]        INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ListaEspera_Cliente] FOREIGN KEY ([ClienteID]) REFERENCES [dbo].[Cliente] ([ClienteID]),
    CONSTRAINT [FK_ListaEspera_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ListaEspera_ViajeID_IsDeleted]
    ON [dbo].[ListaEspera]([ViajeID] ASC, [IsDeleted] ASC)
    INCLUDE([Fecha], [UsuarioID], [ClienteID], [Observacion], [PasajeroTemporal]);


GO
CREATE NONCLUSTERED INDEX [IX_ListaEspera_Id_Usuario_IsDeleted]
    ON [dbo].[ListaEspera]([Id] ASC, [UsuarioID] ASC, [IsDeleted] ASC);

