CREATE PROCEDURE [dbo].[usp_MAT_Paquete_GetById]
    @PaqueteID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Paquete completo por ID (NetTiers F4 - reemplaza PaqueteService.GetByPaqueteId)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PaqueteID,
        Descripcion,
        PrecioCama,
        Moneda,
        Iva,
        Alicuota,
        Temporada,
        Cotizacion,
        Codigo,
        DestinoID,
        PrecioSemiCama,
        Foto,
        ServiciosParticulares,
        FechaCreacion,
        LastUpdate
    FROM dbo.Paquete
    WHERE PaqueteID = @PaqueteID;
END
