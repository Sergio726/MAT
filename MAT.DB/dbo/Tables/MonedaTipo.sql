CREATE TABLE [dbo].[MonedaTipo] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (50) NULL,
    [Codigo]      NCHAR (10)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

