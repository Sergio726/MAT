CREATE TABLE [dbo].[Persona] (
    [PersonaID]        UNIQUEIDENTIFIER NOT NULL,
    [Apellido]         VARCHAR (100)    NULL,
    [Nombre]           VARCHAR (100)    NULL,
    [TipoDocumento]    INT              NULL,
    [NroDocumento]     VARCHAR (50)     NULL,
    [Celular]          VARCHAR (50)     NULL,
    [Telefono]         VARCHAR (50)     NULL,
    [Email]            VARCHAR (50)     NULL,
    [FechaNacimiento]  DATE             NULL,
    [LocalidadID]      INT              NULL,
    [UserId]           INT              NULL,
    [Domicilio]        VARCHAR (100)    NULL,
    [Sexo]             INT              NULL,
    [Ocupacion]        VARCHAR (50)     NULL,
    [Nacionalidad]     VARCHAR (50)     NULL,
    [PaisResidencia]   VARCHAR (50)     NULL,
    [Provincia]        INT              NULL,
    [NroDocumentoCalc] AS               (replace([NroDocumento],'.','')),
    [FullName]         AS               (([Persona].[Nombre]+' ')+[Persona].[Apellido]),
    CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED ([PersonaID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Persona__PersonaID]
    ON [dbo].[Persona]([PersonaID] ASC)
    INCLUDE([Apellido], [Nombre]);

