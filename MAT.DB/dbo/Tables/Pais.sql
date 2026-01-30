CREATE TABLE [dbo].[Pais] (
    [PaisID]       UNIQUEIDENTIFIER CONSTRAINT [DF_Pais_PaisID] DEFAULT (newid()) NOT NULL,
    [Descripcion]  VARCHAR (100)    NULL,
    [mg_CountryId] INT              NULL,
    CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED ([PaisID] ASC)
);

