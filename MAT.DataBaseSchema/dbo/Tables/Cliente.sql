CREATE TABLE [dbo].[Cliente] (
    [ClienteID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Cliente_ClienteID] DEFAULT (newid()) NOT NULL,
    [RazonSocial]  VARCHAR (50)     NULL,
    [Cuit]         VARCHAR (50)     NULL,
    [Moneda]       VARCHAR (50)     NULL,
    [Empresa]      VARCHAR (50)     NULL,
    [Ocupacion]    VARCHAR (50)     NULL,
    [FormaPago]    INT              NULL,
    [CondicionIva] INT              NULL,
    [VendedorID]   UNIQUEIDENTIFIER NULL,
    [Fax]          VARCHAR (50)     NULL,
    [Web]          VARCHAR (50)     NULL,
    [Idioma]       VARCHAR (50)     NULL,
    [Promotor]     VARCHAR (50)     NULL,
    [Observacion]  VARCHAR (250)    NULL,
    [TipoID]       INT              NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([ClienteID] ASC)
);



