CREATE TABLE [dbo].[ViajeAudit] (
    [Id]             INT              IDENTITY (1, 1) NOT NULL,
    [ViajeId]        UNIQUEIDENTIFIER NOT NULL,
    [ViajeNombre]    VARCHAR (100)    NOT NULL,
    [CreateOn]       DATETIME         NULL,
    [CreateUserId]   UNIQUEIDENTIFIER NULL,
    [CreateUser]     VARCHAR (50)     NULL,
    [DeleteOn]       DATETIME         NULL,
    [DeleteByUserId] UNIQUEIDENTIFIER NULL,
    [DeleteUser]     VARCHAR (50)     NULL,
    [DeleteDetalle]  VARCHAR (500)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

