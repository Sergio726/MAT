CREATE TABLE [dbo].[Provincia] (
    [ID]     INT            IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_Provincia] PRIMARY KEY CLUSTERED ([ID] ASC)
);

