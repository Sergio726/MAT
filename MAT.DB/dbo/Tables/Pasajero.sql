CREATE TABLE [dbo].[Pasajero] (
    [PasajeroID]           UNIQUEIDENTIFIER NOT NULL,
    [Pasaporte]            VARCHAR (100)    NULL,
    [VencimientoPasaporte] DATE             NULL,
    [EmisionPasaporte]     DATE             NULL,
    [PaisOrigen]           VARCHAR (50)     NULL,
    CONSTRAINT [PK_Pasajero] PRIMARY KEY CLUSTERED ([PasajeroID] ASC),
    CONSTRAINT [FK_Pasajero_Persona] FOREIGN KEY ([PasajeroID]) REFERENCES [dbo].[Persona] ([PersonaID])
);

