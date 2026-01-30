CREATE TABLE [dbo].[Provincia] (
    [ID]            INT              IDENTITY (1, 1) NOT NULL,
    [Nombre]        NVARCHAR (250)   NOT NULL,
    [IdPais]        UNIQUEIDENTIFIER NOT NULL,
    [mg_ProvinceId] INT              NULL,
    CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Provincia_Pais] FOREIGN KEY ([IdPais]) REFERENCES [dbo].[Pais] ([PaisID])
);

