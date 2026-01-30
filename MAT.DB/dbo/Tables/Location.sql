CREATE TABLE [dbo].[Location] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [IdDepartment] INT          NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Location_Department] FOREIGN KEY ([IdDepartment]) REFERENCES [dbo].[Department] ([Id])
);

