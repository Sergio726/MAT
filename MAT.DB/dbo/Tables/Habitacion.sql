CREATE TABLE [dbo].[Habitacion] (
    [HabitacionID]  UNIQUEIDENTIFIER CONSTRAINT [DF_Habitacion_HabitacionID] DEFAULT (newid()) NOT NULL,
    [NroHabitacion] INT              NULL,
    [Tipo]          INT              NOT NULL,
    [HotelID]       UNIQUEIDENTIFIER NULL,
    [Estado]        INT              NOT NULL,
    [Capacidad]     INT              NOT NULL,
    [Ocupacion]     INT              NOT NULL,
    [Nombre]        VARCHAR (100)    NULL,
    [Precio]        DECIMAL (12, 2)  CONSTRAINT [DF_Habitacion_Precio] DEFAULT ((0)) NOT NULL,
    [Descripcion]   VARCHAR (500)    NULL,
    CONSTRAINT [PK_Habitacion] PRIMARY KEY CLUSTERED ([HabitacionID] ASC),
    CONSTRAINT [FK_Habitacion_Hotel] FOREIGN KEY ([HotelID]) REFERENCES [dbo].[Hotel] ([HotelID])
);

