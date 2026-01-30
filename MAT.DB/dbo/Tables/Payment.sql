CREATE TABLE [dbo].[Payment] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [ReservaId]          UNIQUEIDENTIFIER NULL,
    [FacturaId]          UNIQUEIDENTIFIER NULL,
    [PreferenceId]       VARCHAR (100)    NULL,
    [InitPoint]          VARCHAR (500)    NULL,
    [ClienteId]          VARCHAR (100)    NULL,
    [CollectorId]        INT              NULL,
    [DateCreated]        DATETIME         NULL,
    [DateExpiration]     DATETIME         NULL,
    [ExpirationDateFrom] DATETIME         NULL,
    [ExpirationDateTo]   DATETIME         NULL,
    [ItemId]             UNIQUEIDENTIFIER NULL,
    [ItemTitle]          VARCHAR (100)    NULL,
    [ItemQuanity]        TINYINT          NULL,
    [ItemUnitPrice]      DECIMAL (12, 3)  NULL,
    [ItemDescription]    VARCHAR (500)    NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payment_Factura] FOREIGN KEY ([FacturaId]) REFERENCES [dbo].[Factura] ([FacturaID]),
    CONSTRAINT [FK_Payment_PedidoReserva] FOREIGN KEY ([ReservaId]) REFERENCES [dbo].[PedidoReserva] ([Id])
);

