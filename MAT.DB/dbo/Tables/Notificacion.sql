-- Avisos de la campanita (2026-11-06). Son para todos los usuarios: cada uno marca los suyos como leídos en
-- NotificacionLeida. Clave evita repetir el mismo aviso (por ejemplo, "viaje-ultimos:{ViajeID}" se crea una sola vez).
CREATE TABLE [dbo].[Notificacion] (
    [ID]      INT             IDENTITY (1, 1) NOT NULL,
    [Clave]   VARCHAR (150)   NOT NULL,
    -- ViajeProximo | UltimosLugares | ViajeCompleto | Cumpleanios
    [Tipo]    VARCHAR (30)    NOT NULL,
    [Titulo]  NVARCHAR (200)  NOT NULL,
    [Detalle] NVARCHAR (400)  NULL,
    [Url]     VARCHAR (300)   NULL,
    [Fecha]   DATETIME        NOT NULL,
    -- Después de esta fecha deja de mostrarse (por ejemplo, el día siguiente a la salida del viaje)
    [Vence]   DATETIME        NULL,
    CONSTRAINT [PK_Notificacion] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UQ_Notificacion_Clave] UNIQUE NONCLUSTERED ([Clave] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Notificacion_Fecha]
    ON [dbo].[Notificacion]([Fecha] DESC)
    INCLUDE([Vence]);
