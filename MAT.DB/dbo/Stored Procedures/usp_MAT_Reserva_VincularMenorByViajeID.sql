CREATE PROCEDURE [dbo].[usp_MAT_Reserva_VincularMenorByViajeID] (@ViajeID varchar(max) = '',
														         @PasajeroID varchar(max) = '',
														         @MenorID varchar(max) = ''
														         )
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-09-30
  -- Description: Vincula uno o más menores (lista CSV en @MenorID, separador "," o ";") a un
  --              responsable (@PasajeroID) dentro de un viaje (@ViajeID), tomando el pasaje más
  --              reciente del responsable en ese viaje. Siempre devuelve un resultset (Id, ErrorMsg):
  --              1 = vinculó al menos uno, 0 = todos ya estaban vinculados, -1 = error / datos faltantes.
  -- Historial:
  --   2017-01-17  Garcia Sergio     Creación del SP.
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: @PasajeID sin inicializar quedaba NULL y el SP no
  --                                 devolvía resultset; se rechazaba el lote entero si un menor ya
  --                                 estaba vinculado; acepta ";" además de ","; descarta IDs inválidos.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (ISNULL(@ViajeID, '') = '' OR ISNULL(@PasajeroID, '') = '' OR ISNULL(@MenorID, '') = '')
    BEGIN
        SELECT -1 AS Id, 'Faltan datos para vincular menores (viaje, responsable o menores).' AS ErrorMsg;
        RETURN;
    END

    DECLARE @PasajeID uniqueidentifier = NULL;

    SELECT TOP 1 @PasajeID = p.PasajeID
    FROM dbo.Pasaje p
    WHERE p.PasajeroID = @PasajeroID
      AND p.ViajeID = @ViajeID
    ORDER BY p.FechaReserva DESC;

    IF (@PasajeID IS NULL)
    BEGIN
        SELECT -1 AS Id, 'El responsable seleccionado no tiene pasaje en este viaje; no se pueden vincular los menores.' AS ErrorMsg;
        RETURN;
    END

    BEGIN TRY
        DECLARE @tblMenorID TABLE (MenorID uniqueidentifier NOT NULL);

        INSERT INTO @tblMenorID (MenorID)
        SELECT DISTINCT TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item)))
        FROM dbo.Split(REPLACE(@MenorID, ';', ','), ',') s
        WHERE TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item))) IS NOT NULL;

        IF NOT EXISTS (SELECT 1 FROM @tblMenorID)
        BEGIN
            SELECT -1 AS Id, 'No se recibieron menores válidos para vincular.' AS ErrorMsg;
            RETURN;
        END

        DECLARE @Existentes int = 0,
                @Insertados int = 0;

        SELECT @Existentes = COUNT(*)
        FROM @tblMenorID t
        WHERE EXISTS (SELECT 1
                      FROM dbo.PasajeroMenor pm
                      WHERE pm.pasajeid = @PasajeID
                        AND pm.pasajeroid = @PasajeroID
                        AND pm.menorid = t.MenorID);

        BEGIN TRAN;

        INSERT INTO dbo.PasajeroMenor (pasajeid, pasajeroid, menorid)
        SELECT @PasajeID, @PasajeroID, t.MenorID
        FROM @tblMenorID t
        WHERE NOT EXISTS (SELECT 1
                          FROM dbo.PasajeroMenor pm
                          WHERE pm.pasajeid = @PasajeID
                            AND pm.pasajeroid = @PasajeroID
                            AND pm.menorid = t.MenorID);

        SET @Insertados = @@ROWCOUNT;

        COMMIT TRAN;

        IF (@Insertados > 0)
            SELECT 1 AS Id,
                   'Se vincularon ' + CAST(@Insertados AS varchar(10)) + ' menor(es) al responsable.'
                   + CASE WHEN @Existentes > 0 THEN ' ' + CAST(@Existentes AS varchar(10)) + ' ya estaba(n) vinculado(s).' ELSE '' END AS ErrorMsg;
        ELSE
            SELECT 0 AS Id, 'Los menores seleccionados ya estaban vinculados con este responsable.' AS ErrorMsg;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @errmsg nvarchar(2048) = '*** ' + COALESCE(QUOTENAME(ERROR_PROCEDURE()), '<dynamic SQL>')
                                         + ', ' + CAST(ERROR_LINE() AS nvarchar(10))
                                         + '. Errno ' + CAST(ERROR_NUMBER() AS nvarchar(10)) + ': ' + ERROR_MESSAGE();

        SELECT -1 AS Id, @errmsg AS ErrorMsg;
    END CATCH
END
