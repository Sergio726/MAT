CREATE TYPE [dbo].[tvp_AdicionalIDTableType] AS TABLE (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Cantidad] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));

