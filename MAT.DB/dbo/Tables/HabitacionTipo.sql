CREATE TABLE [dbo].[HabitacionTipo] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion]     VARCHAR (50) NOT NULL,
    [CapacidadNormal] INT          NULL,
    [mg_RoomTypeId]   INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

