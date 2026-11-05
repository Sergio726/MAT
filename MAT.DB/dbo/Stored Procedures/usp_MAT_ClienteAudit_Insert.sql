CREATE PROCEDURE [dbo].[usp_MAT_ClienteAudit_Insert]
(
    @ClienteID UNIQUEIDENTIFIER,
    @Usuario   VARCHAR(100)  = NULL,
    @Accion    VARCHAR(20),
    @Detalle   NVARCHAR(MAX) = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Registra un evento en el historial del cliente (alta o modificación; la baja y la
  --              reactivación las registra usp_MAT_Cliente_CambiarEstado). Hora argentina.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    INSERT INTO dbo.ClienteAudit (ClienteID, Fecha, Usuario, Accion, Detalle)
    VALUES (@ClienteID, CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME),
            @Usuario, @Accion, NULLIF(LTRIM(RTRIM(@Detalle)), N''));
END
