CREATE TABLE [dbo].[PaqueteAdicional] (
    [PaqueteAdicionalID] UNIQUEIDENTIFIER CONSTRAINT [DF_PaqueteAdicional_PaqueteAdicionalID] DEFAULT (newid()) NOT NULL,
    [PaqueteID]          UNIQUEIDENTIFIER NULL,
    [AdicionalID]        UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_PaqueteAdicional] PRIMARY KEY CLUSTERED ([PaqueteAdicionalID] ASC),
    CONSTRAINT [FK_PaqueteAdicional_Adicional] FOREIGN KEY ([AdicionalID]) REFERENCES [dbo].[Adicional] ([AdicionalID]),
    CONSTRAINT [FK_PaqueteAdicional_Paquete] FOREIGN KEY ([PaqueteID]) REFERENCES [dbo].[Paquete] ([PaqueteID])
);

