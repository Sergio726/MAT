CREATE TABLE [dbo].[PasajeAdicional] (
    [PasajeAdicionalID] UNIQUEIDENTIFIER CONSTRAINT [DF_PasajeAdicional_PasajeAdicionalID] DEFAULT (newid()) NOT NULL,
    [PasajeID]          UNIQUEIDENTIFIER NULL,
    [AdicionalID]       UNIQUEIDENTIFIER NULL,
    [PasajeroId]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PasajeAdicional] PRIMARY KEY CLUSTERED ([PasajeAdicionalID] ASC),
    CONSTRAINT [FK_PasajeAdicional_Adicional] FOREIGN KEY ([AdicionalID]) REFERENCES [dbo].[Adicional] ([AdicionalID]),
    CONSTRAINT [FK_PasajeAdicional_Pasaje] FOREIGN KEY ([PasajeID]) REFERENCES [dbo].[Pasaje] ([PasajeID])
);

