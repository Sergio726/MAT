CREATE TABLE [dbo].[Pais] (
    [PaisID]       UNIQUEIDENTIFIER CONSTRAINT [DF_Pais_PaisID] DEFAULT (newid()) NOT NULL,
    [Descripcion]  VARCHAR (100)    NULL,
    [mg_CountryId] INT              NULL,
    -- Código ISO 3166-1 alfa-3 (ARG, BOL…). Es lo que se guarda en Persona.Nacionalidad y Persona.PaisResidencia (2026-11-04, F4).
    [Codigo]       CHAR (3)         NULL,
    CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED ([PaisID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Pais_Codigo]
    ON [dbo].[Pais]([Codigo] ASC) WHERE ([Codigo] IS NOT NULL);
