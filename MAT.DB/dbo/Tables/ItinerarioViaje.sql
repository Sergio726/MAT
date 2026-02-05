CREATE TABLE [dbo].[ItinerarioViaje] (
    [ItinerarioViajeID]  UNIQUEIDENTIFIER CONSTRAINT [DF_ItinerarioViaje_ItinerarioViajeID] DEFAULT (newid()) NOT NULL,
    [ViajeID]            UNIQUEIDENTIFIER NOT NULL,
    [ItinerarioID]       UNIQUEIDENTIFIER NOT NULL,
    [Orden]              INT              NOT NULL,
    [HoraAprox]          VARCHAR(10)      NULL,
    [DuracionMin]        INT              NULL,
    [Observacion]        VARCHAR(200)     NULL,
    CONSTRAINT [PK_ItinerarioViaje] PRIMARY KEY CLUSTERED ([ItinerarioViajeID] ASC),
    CONSTRAINT [FK_ItinerarioViaje_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID]),
    CONSTRAINT [FK_ItinerarioViaje_Itinerario] FOREIGN KEY ([ItinerarioID]) REFERENCES [dbo].[Itinerario] ([ItinerarioID])
);
