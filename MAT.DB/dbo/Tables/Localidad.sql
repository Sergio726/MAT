CREATE TABLE [dbo].[Localidad] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [idDepartamento] INT            NOT NULL,
    [Nombre]         NVARCHAR (250) NOT NULL,
    [mg_LocationId]  INT            NULL,
    CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Localidad_Departamento] FOREIGN KEY ([idDepartamento]) REFERENCES [dbo].[Departamento] ([ID])
);

