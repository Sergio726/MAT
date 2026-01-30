CREATE TABLE [dbo].[Property] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               VARCHAR (50)   NOT NULL,
    [Description]        VARCHAR (2000) NULL,
    [Address]            VARCHAR (50)   NULL,
    [CP]                 VARCHAR (50)   NULL,
    [Phone]              VARCHAR (50)   NULL,
    [Email]              VARCHAR (50)   NULL,
    [IdPropertyType]     INT            NULL,
    [IdPropertyCategory] INT            NULL,
    [CheckIn]            VARCHAR (8)    NULL,
    [CheckOut]           VARCHAR (8)    NULL,
    [IdLocation]         INT            NULL,
    CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Property_Localidad] FOREIGN KEY ([IdLocation]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_Property_PropertyCategory] FOREIGN KEY ([IdPropertyCategory]) REFERENCES [dbo].[PropertyCategory] ([Id]),
    CONSTRAINT [FK_Property_PropertyType] FOREIGN KEY ([IdPropertyType]) REFERENCES [dbo].[PropertyType] ([Id])
);

