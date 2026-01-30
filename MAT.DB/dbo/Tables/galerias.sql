CREATE TABLE [dbo].[galerias] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]    VARCHAR (255) NOT NULL,
    [Ruta]      VARCHAR (255) NOT NULL,
    [Estado]    TINYINT       NOT NULL,
    [CreatedAt] DATETIME      NULL,
    [UpdatedAt] DATETIME      NULL,
    CONSTRAINT [PK_galerias] PRIMARY KEY CLUSTERED ([Id] ASC)
);

