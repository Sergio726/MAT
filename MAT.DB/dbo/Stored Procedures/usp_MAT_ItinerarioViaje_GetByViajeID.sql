CREATE PROCEDURE [dbo].[usp_MAT_ItinerarioViaje_GetByViajeID]
    @ViajeID VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        iv.[ItinerarioViajeID],
        iv.[ViajeID],
        iv.[ItinerarioID],
        iv.[Orden],
        iv.[HoraAprox],
        iv.[DuracionMin],
        iv.[Observacion],
        i.[Nombre],
        i.[Descripcion] AS ItinerarioDescripcion
    FROM [dbo].[ItinerarioViaje] iv
    INNER JOIN [dbo].[Itinerario] i ON iv.[ItinerarioID] = i.[ItinerarioID]
    WHERE iv.[ViajeID] = CONVERT(UNIQUEIDENTIFIER, @ViajeID)
    ORDER BY iv.[Orden];
END
