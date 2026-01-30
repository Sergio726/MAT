CREATE TABLE [dbo].[PropertyCategory] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)   NOT NULL,
    [Description] VARCHAR (2000) NULL,
    CONSTRAINT [PK_PropertyCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

