CREATE TABLE [dbo].[Departamento] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [idProvincia]     INT            NOT NULL,
    [Nombre]          NVARCHAR (250) NOT NULL,
    [mg_DepartmentId] INT            NULL,
    CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Departamento_Provincia] FOREIGN KEY ([idProvincia]) REFERENCES [dbo].[Provincia] ([ID])
);

