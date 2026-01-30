CREATE TABLE [dbo].[galeria_paquete] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [PaqueteId] INT      NOT NULL,
    [GaleriaId] INT      NOT NULL,
    [CreatedAt] DATETIME NULL,
    [UpdatedAt] DATETIME NULL,
    CONSTRAINT [PK_galeria_paquete] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_galeria_paquete_GaleriaId] FOREIGN KEY ([GaleriaId]) REFERENCES [dbo].[galerias] ([Id]),
    CONSTRAINT [FK_galeria_paquete_PaqueteId] FOREIGN KEY ([PaqueteId]) REFERENCES [dbo].[paquetesPublic] ([Id])
);

