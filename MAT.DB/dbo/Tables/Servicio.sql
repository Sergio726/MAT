CREATE TABLE [dbo].[Servicio] (
    [ServicioID]        UNIQUEIDENTIFIER CONSTRAINT [DF_Servicio_ServicioID] DEFAULT (newid()) NOT NULL,
    [Descripcion]       VARCHAR (250)    NOT NULL,
    [Precio]            FLOAT (53)       NULL,
    [Moneda]            VARCHAR (50)     NULL,
    [Iva]               VARCHAR (50)     NULL,
    [Alicuota]          FLOAT (53)       NULL,
    [Validez]           DATE             NULL,
    [VisibilidadTarifa] INT              NULL,
    [ProveedorID]       UNIQUEIDENTIFIER NULL,
    [TransporteID]      UNIQUEIDENTIFIER NULL,
    [HotelID]           UNIQUEIDENTIFIER NULL,
    [TipoServicio]      INT              NULL,
    CONSTRAINT [PK_Servicio] PRIMARY KEY CLUSTERED ([ServicioID] ASC),
    CONSTRAINT [FK_Servicio_Hotel] FOREIGN KEY ([HotelID]) REFERENCES [dbo].[Hotel] ([HotelID]),
    CONSTRAINT [FK_Servicio_Proveedor] FOREIGN KEY ([ProveedorID]) REFERENCES [dbo].[Proveedor] ([ProveedorID]),
    CONSTRAINT [FK_Servicio_Transporte] FOREIGN KEY ([TransporteID]) REFERENCES [dbo].[Transporte] ([TransporteID])
);

