CREATE TYPE [dbo].[tvp_PrecioIDTableType] AS TABLE (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Cantidad] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

