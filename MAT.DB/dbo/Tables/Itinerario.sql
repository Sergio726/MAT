CREATE TABLE [dbo].[Itinerario] (
    [ItinerarioID]   UNIQUEIDENTIFIER CONSTRAINT [DF_Itinerario_ItinerarioID] DEFAULT (newid()) NOT NULL,
    [Nombre]         VARCHAR(200)     NOT NULL,
    [Descripcion]    VARCHAR(500)     NULL,
    [Activo]         BIT              CONSTRAINT [DF_Itinerario_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Itinerario] PRIMARY KEY CLUSTERED ([ItinerarioID] ASC)
);
