CREATE TABLE [dbo].[AuditFactura] (
    [ID]          INT              IDENTITY (1, 1) NOT NULL,
    [FacturaID]   UNIQUEIDENTIFIER NULL,
    [PersonaID]   UNIQUEIDENTIFIER NULL,
    [VendedorID]  UNIQUEIDENTIFIER NOT NULL,
    [Accion]      VARCHAR (200)    NULL,
    [Descripcion] VARCHAR (200)    NULL,
    [Fecha]       DATETIME         DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

