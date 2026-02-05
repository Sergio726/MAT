CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_Insert]
    @Nombre VARCHAR(200),
    @Descripcion VARCHAR(500) = NULL
AS
/*-- =============================================
-- Author:    Mati Terradas
-- Create date: 05-02-2026
-- Description:  Inserta una nueva parada de itinerario

-- =============================================*/
BEGIN
    SET NOCOUNT ON;

    DECLARE @ItinerarioID UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Itinerario] ([ItinerarioID], [Nombre], [Descripcion], [Activo])
    VALUES (@ItinerarioID, @Nombre, @Descripcion, 1);

    SELECT
        ItinerarioID = @ItinerarioID,
        Result = 'Done.'
END
