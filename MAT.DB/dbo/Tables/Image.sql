CREATE TABLE [dbo].[Image] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [URL]       VARCHAR (500) NOT NULL,
    [PublicId]  VARCHAR (500) NOT NULL,
    [FileName]  VARCHAR (500) NULL,
    [Size]      INT           NULL,
    [CreatedAt] DATETIME      CONSTRAINT [Image_CreatedAt_df] DEFAULT (getdate()) NOT NULL,
    [UpdatedAt] DATETIME      NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Image_PublicId]
    ON [dbo].[Image]([PublicId] ASC) WHERE ([PublicId] IS NOT NULL);

