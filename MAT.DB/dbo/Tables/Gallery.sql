CREATE TABLE [dbo].[Gallery] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [CreatedAt] DATETIME     CONSTRAINT [Gallery_CreatedAt_df] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt] DATETIME     NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED ([Id] ASC)
);

