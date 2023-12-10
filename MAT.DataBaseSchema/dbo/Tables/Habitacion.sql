CREATE TABLE [dbo].[Habitacion] (
    [HabitacionID]  UNIQUEIDENTIFIER CONSTRAINT [DF_Habitacion_HabitacionID] DEFAULT (newid()) NOT NULL,
    [NroHabitacion] VARCHAR (50)     NULL,
    [Tipo]          INT              NULL,
    [HotelID]       UNIQUEIDENTIFIER NULL,
    [Estado]        INT              NULL,
    [Capacidad]     INT              NULL,
    [Ocupacion]     INT              NULL,
    CONSTRAINT [PK_Habitacion] PRIMARY KEY CLUSTERED ([HabitacionID] ASC),
    CONSTRAINT [FK_Habitacion_Hotel] FOREIGN KEY ([HotelID]) REFERENCES [dbo].[Hotel] ([HotelID])
);



