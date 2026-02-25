CREATE TABLE [dbo].[Proveedor] (
    [ProveedorID]  UNIQUEIDENTIFIER NOT NULL,
    [RazonSocial]  VARCHAR (50)     NULL,
    [Telefono]     VARCHAR (50)     NULL,
    [Fax]          VARCHAR (50)     NULL,
    [Web]          VARCHAR (50)     NULL,
    [Email]        VARCHAR (50)     NULL,
    [Idioma]       VARCHAR (50)     NULL,
    [CondicionIva] INT              NULL,
    [Cuit]         VARCHAR (50)     NULL,
    [FormaPago]    INT              NULL,
    [Domicilio]    VARCHAR (200)    NULL,
    [Estado]       INT              NOT NULL DEFAULT(1),
    [LocalidadID]  INT              NULL,
    CONSTRAINT [PK_Proveedor] PRIMARY KEY CLUSTERED ([ProveedorID] ASC)
);

