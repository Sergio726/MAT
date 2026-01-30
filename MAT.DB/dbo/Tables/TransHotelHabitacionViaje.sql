CREATE TABLE [dbo].[TransHotelHabitacionViaje] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [HotelID]      UNIQUEIDENTIFIER NOT NULL,
    [HabitacionID] UNIQUEIDENTIFIER NOT NULL,
    [ViajeID]      UNIQUEIDENTIFIER NULL,
    [Fecha]        DATE             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransHotelHabitacionViaje_Habitacion] FOREIGN KEY ([HabitacionID]) REFERENCES [dbo].[Habitacion] ([HabitacionID]),
    CONSTRAINT [FK_TransHotelHabitacionViaje_Hotel] FOREIGN KEY ([HotelID]) REFERENCES [dbo].[Hotel] ([HotelID]),
    CONSTRAINT [FK_TransHotelHabitacionViaje_Viaje] FOREIGN KEY ([ViajeID]) REFERENCES [dbo].[Viaje] ([ViajeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TransHotelHabitacionViaje_HotelID]
    ON [dbo].[TransHotelHabitacionViaje]([HotelID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TransHotelHabitacionViaje_HabitacionID]
    ON [dbo].[TransHotelHabitacionViaje]([HabitacionID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TransHotelHabitacionViaje_Viaje]
    ON [dbo].[TransHotelHabitacionViaje]([ViajeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TransHotelHabitacionViaje_Fecha]
    ON [dbo].[TransHotelHabitacionViaje]([Fecha] ASC);

