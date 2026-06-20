CREATE FUNCTION [dbo].[fn_MAT_Pasaje_TieneConflictoFechaSalida]
(
    @PasajeroID UNIQUEIDENTIFIER,
    @ViajeID UNIQUEIDENTIFIER,
    @ExcluirPasajeID UNIQUEIDENTIFIER = NULL
)
RETURNS BIT
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-06-19
 -- Description: Devuelve 1 si el pasajero ya tiene un pasaje activo
 --   en cualquier viaje con la misma FechaSalida que @ViajeID.
 --   @ExcluirPasajeID permite excluir el pasaje que se está editando.
 ============================================= */
BEGIN
    IF @PasajeroID IS NULL OR @ViajeID IS NULL
        RETURN 0;

    DECLARE @FechaSalida DATE;

    SELECT @FechaSalida = v.FechaSalida
    FROM dbo.Viaje v
    WHERE v.ViajeID = @ViajeID;

    IF @FechaSalida IS NULL
        RETURN 0;

    IF EXISTS (
        SELECT 1
        FROM dbo.Pasaje pa
        INNER JOIN dbo.Viaje v ON v.ViajeID = pa.ViajeID
        WHERE pa.PasajeroID = @PasajeroID
          AND ISNULL(pa.EstadoPasaje, 0) NOT IN (1, 7)
          AND v.FechaSalida = @FechaSalida
          AND (@ExcluirPasajeID IS NULL OR pa.PasajeID <> @ExcluirPasajeID)
    )
        RETURN 1;

    RETURN 0;
END
