CREATE TABLE [dbo].[Hotel] (
    [HotelID]              UNIQUEIDENTIFIER CONSTRAINT [DF_Hotel_HotelID] DEFAULT (newid()) NOT NULL,
    [Nombre]               VARCHAR (50)     NOT NULL,
    [Direccion]            VARCHAR (50)     NULL,
    [CP]                   VARCHAR (50)     NULL,
    [Telefono]             VARCHAR (50)     NULL,
    [Email]                VARCHAR (50)     NULL,
    [Contacto]             VARCHAR (50)     NULL,
    [CantidadHabitaciones] INT              NULL,
    [Categoria]            INT              NULL,
    [CheckIn]              VARCHAR (8)      NULL,
    [CheckOut]             VARCHAR (8)      NULL,
    [GoogleMapHtml]        VARCHAR (200)    NULL,
    [Localidad]            VARCHAR (50)     NULL,
    CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED ([HotelID] ASC)
);

