CREATE TABLE [dbo].[temporal] (
    [PagoID]        UNIQUEIDENTIFIER NOT NULL,
    [FechaPago]     DATETIME         NOT NULL,
    [Monto]         FLOAT (53)       NOT NULL,
    [TipoPago]      INT              NOT NULL,
    [VendedorId]    UNIQUEIDENTIFIER NOT NULL,
    [NroRecibo]     VARCHAR (50)     NOT NULL,
    [TransaccionID] VARCHAR (50)     NULL,
    [cliente]       VARCHAR (23)     NOT NULL
);

