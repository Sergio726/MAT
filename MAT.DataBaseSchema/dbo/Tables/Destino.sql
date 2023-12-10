CREATE TABLE [dbo].[Destino] (
    [DestinoID]   UNIQUEIDENTIFIER CONSTRAINT [DF_Destino_DestinoID] DEFAULT (newid()) NOT NULL,
    [LocalidadID] INT              NULL,
    CONSTRAINT [PK_Destino] PRIMARY KEY CLUSTERED ([DestinoID] ASC)
);

