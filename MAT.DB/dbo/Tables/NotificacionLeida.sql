-- Qué avisos ya vio cada usuario (2026-11-06). Se guarda en la base para que lo leído no reaparezca en otra PC.
CREATE TABLE [dbo].[NotificacionLeida] (
    [NotificacionID] INT           NOT NULL,
    [Usuario]        VARCHAR (100) NOT NULL,
    [Fecha]          DATETIME      NOT NULL,
    CONSTRAINT [PK_NotificacionLeida] PRIMARY KEY CLUSTERED ([NotificacionID] ASC, [Usuario] ASC),
    CONSTRAINT [FK_NotificacionLeida_Notificacion] FOREIGN KEY ([NotificacionID]) REFERENCES [dbo].[Notificacion] ([ID]) ON DELETE CASCADE
);
