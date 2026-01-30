CREATE TABLE [dbo].[GalleryImage] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [IdImage]   INT NOT NULL,
    [IdGallery] INT NOT NULL,
    CONSTRAINT [PK_GalleryImage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GalleryImage_Gallery] FOREIGN KEY ([IdGallery]) REFERENCES [dbo].[Gallery] ([Id]),
    CONSTRAINT [FK_GalleryImage_Image] FOREIGN KEY ([IdImage]) REFERENCES [dbo].[Image] ([Id])
);

