CREATE TABLE [dbo].[Department] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    [IdProvince] INT          NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Department_Province] FOREIGN KEY ([IdProvince]) REFERENCES [dbo].[Province] ([Id])
);

