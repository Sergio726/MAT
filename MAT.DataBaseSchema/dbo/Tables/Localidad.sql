CREATE TABLE [dbo].[Localidad] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [idDepartamento] INT            NOT NULL,
    [Nombre]         NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED ([ID] ASC)
);

