CREATE TABLE [dbo].[Debito] (
    [DebitoID]    UNIQUEIDENTIFIER NOT NULL,
    [Fecha]       DATE             NULL,
    [ClienteID]   UNIQUEIDENTIFIER NULL,
    [VendedorID]  UNIQUEIDENTIFIER NULL,
    [MontoDebito] FLOAT (53)       NULL,
    CONSTRAINT [PK_Debito] PRIMARY KEY CLUSTERED ([DebitoID] ASC)
);

