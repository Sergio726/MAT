CREATE TABLE [dbo].[foto_galeria] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [FotoId]    INT      NOT NULL,
    [GaleriaId] INT      NOT NULL,
    [CreatedAt] DATETIME NULL,
    [UpdatedAt] DATETIME NULL,
    CONSTRAINT [PK_fotos_galeria] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_fotos_galeria_FotoId] FOREIGN KEY ([FotoId]) REFERENCES [dbo].[fotos] ([Id]),
    CONSTRAINT [FK_fotos_galeria_GaleriaId] FOREIGN KEY ([GaleriaId]) REFERENCES [dbo].[galerias] ([Id])
);

