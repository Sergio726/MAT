CREATE TABLE [dbo].[fotos] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (255) NOT NULL,
    [Size]      VARCHAR (255) NOT NULL,
    [Ruta]      VARCHAR (255) NOT NULL,
    [Miniatura] VARCHAR (255) NOT NULL,
    [CreatedAt] DATETIME      NULL,
    [UpdatedAt] DATETIME      NULL,
    CONSTRAINT [PK_fotos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

