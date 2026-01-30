CREATE TABLE [dbo].[TransPlantillaDistribucionHabitacion] (
    [Id]                                INT             IDENTITY (1, 1) NOT NULL,
    [PlantillaDistribucionHabitacionID] INT             NOT NULL,
    [NroHabitacion]                     INT             NULL,
    [TipoHabitacion]                    INT             NOT NULL,
    [Capacidad]                         INT             NOT NULL,
    [Nombre]                            VARCHAR (100)   NULL,
    [Precio]                            DECIMAL (12, 2) CONSTRAINT [DF_TransPlantillaDistribucionHabitacion_Precio] DEFAULT ((0)) NOT NULL,
    [Descripcion]                       VARCHAR (500)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransPlantillaDistribucionHabitacion_PlantillaDistribucionHabitacion] FOREIGN KEY ([PlantillaDistribucionHabitacionID]) REFERENCES [dbo].[PlantillaDistribucionHabitacion] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TransPlantillaDistribucionHabitacion_PlantillaDistribucionHabitacionID]
    ON [dbo].[TransPlantillaDistribucionHabitacion]([PlantillaDistribucionHabitacionID] ASC);

