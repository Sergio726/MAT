CREATE PROCEDURE [dbo].[usp_MAT_Servicio_GetById]
    @ServicioID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-30
 -- Description: Obtiene servicio por ID (migración NetTiers F3)
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        ServicioID,
        Descripcion,
        Precio,
        Moneda,
        Iva,
        Alicuota,
        Validez,
        VisibilidadTarifa,
        ProveedorID,
        TransporteID,
        HotelID,
        TipoServicio
    FROM dbo.Servicio
    WHERE ServicioID = @ServicioID;
END
