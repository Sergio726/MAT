CREATE TABLE [dbo].[Hotel] (
    [HotelID]              UNIQUEIDENTIFIER CONSTRAINT [DF_Hotel_HotelID] DEFAULT (newid()) NOT NULL,
    [Nombre]               VARCHAR (50)     NULL,
    [Direccion]            VARCHAR (50)     NULL,
    [CP]                   VARCHAR (50)     NULL,
    [Telefono]             VARCHAR (50)     NULL,
    [Email]                VARCHAR (50)     NULL,
    [Contacto]             VARCHAR (50)     NULL,
    [CantidadHabitaciones] INT              NULL,
    [Categoria]            INT              NULL,
    [Child1]               VARCHAR (50)     NULL,
    [Child2]               VARCHAR (50)     NULL,
    [ChildHabitacion]      INT              NULL,
    [CheckIn]              VARCHAR (8)      NULL,
    [CheckOut]             VARCHAR (8)      NULL,
    [GoogleMapHtml]        VARCHAR (200)    NULL,
    [LocalidadID]          INT              NULL,
    CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED ([HotelID] ASC),
    CONSTRAINT [FK_Hotel_Localidad] FOREIGN KEY ([LocalidadID]) REFERENCES [dbo].[Localidad] ([ID])
);







