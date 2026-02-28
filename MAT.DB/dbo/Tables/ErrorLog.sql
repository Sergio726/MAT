CREATE TABLE [dbo].[ErrorLog] (
    [Id]            INT IDENTITY(1,1)   NOT NULL,
    [FechaHora]     DATETIME            NOT NULL CONSTRAINT [DF_ErrorLog_FechaHora] DEFAULT (GETDATE()),
    [CorrelationId] VARCHAR(50)         NULL,
    [Tipo]          VARCHAR(200)        NULL,
    [Mensaje]       NVARCHAR(MAX)       NULL,
    [StackTrace]    NVARCHAR(MAX)       NULL,
    [Url]           NVARCHAR(1000)      NULL,
    [Usuario]       NVARCHAR(200)       NULL,
    [Importancia]   INT                 NOT NULL CONSTRAINT [DF_ErrorLog_Importancia] DEFAULT (1),
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([Id] ASC)
)
