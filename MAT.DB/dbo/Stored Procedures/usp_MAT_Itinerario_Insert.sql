CREATE PROCEDURE [dbo].[usp_MAT_Itinerario_Insert]
    @Nombre VARCHAR(200),
    @Descripcion VARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ItinerarioID UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[Itinerario] ([ItinerarioID], [Nombre], [Descripcion], [Activo])
    VALUES (@ItinerarioID, @Nombre, @Descripcion, 1);

    SELECT
        ItinerarioID = @ItinerarioID,
        Result = 'Done.'
END
