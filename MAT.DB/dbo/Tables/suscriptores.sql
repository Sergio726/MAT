CREATE TABLE [dbo].[suscriptores] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Email]     VARCHAR (255) NULL,
    [Celular]   VARCHAR (255) NULL,
    [Estado]    BIT           DEFAULT ((1)) NOT NULL,
    [CreatedAt] DATETIME      NULL,
    [UpdatedAt] DATETIME      NULL,
    CONSTRAINT [PK_suscriptores] PRIMARY KEY CLUSTERED ([Id] ASC)
);

