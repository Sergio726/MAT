CREATE TABLE [dbo].[ScriptLog] (
    [OperationKey] UNIQUEIDENTIFIER NOT NULL,
    [OnDate]       DATETIME         CONSTRAINT [DF_ScriptLog_OnDate] DEFAULT (getdate()) NOT NULL,
    [ScripName]    VARCHAR (500)    NOT NULL,
    CONSTRAINT [PK_ScriptLog] PRIMARY KEY CLUSTERED ([OperationKey] ASC)
);

