CREATE TABLE [dbo].[PaqueteExcursion] (
    [PaqueteExcursionID] UNIQUEIDENTIFIER CONSTRAINT [DF_PaqueteExcursion_PaqueteExcursionID] DEFAULT (newid()) NOT NULL,
    [ExcursionID]        UNIQUEIDENTIFIER NOT NULL,
    [PaqueteID]          UNIQUEIDENTIFIER NOT NULL,
    [IsOpcional]         BIT              CONSTRAINT [DF_PaqueteExcursion_IsOpcional] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PaqueteExcursion] PRIMARY KEY CLUSTERED ([PaqueteExcursionID] ASC),
    CONSTRAINT [FK_PaqueteExcursion_Excursion] FOREIGN KEY ([ExcursionID]) REFERENCES [dbo].[Excursion] ([ExcursionID]),
    CONSTRAINT [FK_PaqueteExcursion_Paquete] FOREIGN KEY ([PaqueteID]) REFERENCES [dbo].[Paquete] ([PaqueteID])
);

