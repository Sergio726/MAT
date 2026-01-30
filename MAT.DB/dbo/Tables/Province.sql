CREATE TABLE [dbo].[Province] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [IdCountry] INT          NOT NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Province_Country] FOREIGN KEY ([IdCountry]) REFERENCES [dbo].[Country] ([Id])
);

