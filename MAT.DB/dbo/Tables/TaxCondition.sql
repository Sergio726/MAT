CREATE TABLE [dbo].[TaxCondition] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TaxCondition] PRIMARY KEY CLUSTERED ([Id] ASC)
);

