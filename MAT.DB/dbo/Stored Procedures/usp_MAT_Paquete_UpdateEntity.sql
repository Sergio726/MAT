CREATE PROCEDURE [dbo].[usp_MAT_Paquete_UpdateEntity]
    @PaqueteID UNIQUEIDENTIFIER,
    @Descripcion VARCHAR (100),
    @PrecioCama FLOAT (53) = NULL,
    @Moneda INT = NULL,
    @Iva VARCHAR (50) = NULL,
    @Alicuota VARCHAR (50) = NULL,
    @Temporada INT = NULL,
    @Cotizacion FLOAT (53) = NULL,
    @Codigo VARCHAR (50) = NULL,
    @DestinoID INT,
    @PrecioSemiCama FLOAT (53) = NULL,
    @Foto VARCHAR (200) = NULL,
    @ServiciosParticulares VARCHAR (MAX) = NULL,
    @FechaCreacion DATETIME = NULL,
    @LastUpdate DATETIME
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Update de la entidad Paquete (NetTiers F4 - reemplaza PaqueteService.Update).
 --              Actualiza solo las columnas conocidas por la entidad; no toca ImageId/GalleryId.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    UPDATE dbo.Paquete
    SET Descripcion = @Descripcion,
        PrecioCama = @PrecioCama,
        Moneda = @Moneda,
        Iva = @Iva,
        Alicuota = @Alicuota,
        Temporada = @Temporada,
        Cotizacion = @Cotizacion,
        Codigo = @Codigo,
        DestinoID = @DestinoID,
        PrecioSemiCama = @PrecioSemiCama,
        Foto = @Foto,
        ServiciosParticulares = @ServiciosParticulares,
        FechaCreacion = @FechaCreacion,
        LastUpdate = @LastUpdate
    WHERE PaqueteID = @PaqueteID;
END
