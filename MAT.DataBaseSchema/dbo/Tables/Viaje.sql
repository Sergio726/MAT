CREATE TABLE [dbo].[Viaje] (
    [ViajeID]      UNIQUEIDENTIFIER CONSTRAINT [DF_Viaje_ViajeID] DEFAULT (newid()) NOT NULL,
    [PaqueteID]    UNIQUEIDENTIFIER NULL,
    [Origen]       VARCHAR (50)     NULL,
    [FechaSalida]  DATE             NULL,
    [HoraSalida]   VARCHAR (50)     NULL,
    [PaisOrigen]   VARCHAR (50)     NULL,
    [PaisDestino]  VARCHAR (50)     NULL,
    [Paso]         VARCHAR (50)     NULL,
    [Medio]        VARCHAR (50)     NULL,
    [BusID]        UNIQUEIDENTIFIER NULL,
    [FechaRegreso] DATE             NULL,
    [HoraRegreso]  VARCHAR (50)     NULL,
    [Descripcion]  VARCHAR (200)    NULL,
    CONSTRAINT [PK_Viaje] PRIMARY KEY CLUSTERED ([ViajeID] ASC),
    CONSTRAINT [FK_Viaje_Paquete] FOREIGN KEY ([PaqueteID]) REFERENCES [dbo].[Paquete] ([PaqueteID]),
    CONSTRAINT [FK_Viaje_Transporte] FOREIGN KEY ([BusID]) REFERENCES [dbo].[Transporte] ([TransporteID])
);







