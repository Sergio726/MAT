CREATE TABLE [dbo].[Departamento] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [idProvincia] INT            NOT NULL,
    [Nombre]      NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED ([ID] ASC)
);

