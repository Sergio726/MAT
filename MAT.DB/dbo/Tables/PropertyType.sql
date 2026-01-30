CREATE TABLE [dbo].[PropertyType] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)   NOT NULL,
    [Description] VARCHAR (2000) NULL,
    CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

