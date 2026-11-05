CREATE PROCEDURE [dbo].[usp_MAT_Cliente_CambiarEstado]
(
    @ClienteID UNIQUEIDENTIFIER,
    @Activo    BIT,
    @Motivo    VARCHAR(250) = NULL,
    @Usuario   VARCHAR(100) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Baja (@Activo = 0, con motivo) o reactivación (@Activo = 1) de un cliente, con su registro en
  --              ClienteAudit, en una transacción. No borra nada: facturas, pagos y viajes siguen accesibles.
  --              Devuelve 1 si cambió, 0 si ya estaba en ese estado o el cliente no existe.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);
    SET @Motivo = NULLIF(LTRIM(RTRIM(@Motivo)), '');

    BEGIN TRANSACTION;

    UPDATE dbo.Cliente
    SET Activo = @Activo,
        FechaBaja = CASE WHEN @Activo = 0 THEN @Ahora END,
        MotivoBaja = CASE WHEN @Activo = 0 THEN @Motivo END,
        BajaUsuario = CASE WHEN @Activo = 0 THEN @Usuario END
    WHERE ClienteID = @ClienteID AND Activo <> @Activo;

    DECLARE @Cambio INT = @@ROWCOUNT;
    IF (@Cambio = 1)
        INSERT INTO dbo.ClienteAudit (ClienteID, Fecha, Usuario, Accion, Detalle)
        VALUES (@ClienteID, @Ahora, @Usuario, CASE WHEN @Activo = 0 THEN 'Baja' ELSE 'Reactivacion' END,
                CASE WHEN @Activo = 0 THEN N'Motivo: ' + ISNULL(@Motivo, N'(sin motivo)') END);

    COMMIT TRANSACTION;

    SELECT @Cambio AS Cambio;
END
