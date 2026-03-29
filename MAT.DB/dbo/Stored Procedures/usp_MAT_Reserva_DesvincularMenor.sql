
CREATE PROCEDURE [dbo].[usp_MAT_Reserva_DesvincularMenor]
    @PasajeroMenorID INT,
    @ViajeID         VARCHAR(MAX) = ''
AS
/*
    Fecha: 28/03/2026
    Autor: Seba Garcia
    Detalle: Elimina una vinculación PasajeroMenor solo si el pasaje pertenece al ViajeID indicado.
*/
SET NOCOUNT,
    XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN
    DECLARE @vid UNIQUEIDENTIFIER = TRY_CAST(NULLIF(LTRIM(RTRIM(@ViajeID)), '') AS UNIQUEIDENTIFIER);

    IF @vid IS NULL
        OR @PasajeroMenorID IS NULL
        OR @PasajeroMenorID <= 0
    BEGIN
        SELECT 0         AS Id,
               N'Parámetros inválidos.' AS ErrorMsg;
        RETURN;
    END;

    BEGIN TRY
        BEGIN TRAN;

        DELETE pm
        FROM dbo.PasajeroMenor pm
        INNER JOIN dbo.Pasaje p ON pm.pasajeid = p.PasajeID
        WHERE pm.id = @PasajeroMenorID
              AND p.ViajeID = @vid;

        IF @@ROWCOUNT > 0
        BEGIN
            COMMIT TRAN;
            SELECT 1 AS Id,
                   N'Se desvinculó el menor correctamente.' AS ErrorMsg;
        END
        ELSE
        BEGIN
            ROLLBACK TRAN;
            SELECT 0 AS Id,
                   N'No se encontró la vinculación para este viaje.' AS ErrorMsg;
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        DECLARE @errmsg NVARCHAR(2048) = ERROR_MESSAGE();
        SELECT -1       AS Id,
               @errmsg AS ErrorMsg;
    END CATCH
END
