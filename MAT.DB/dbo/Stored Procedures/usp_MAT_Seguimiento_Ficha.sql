CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_Ficha]
(
    @ClienteID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Ficha de seguimiento de un cliente.
  --              RS1: datos + métricas (fn_MAT_Seguimiento_Clientes).
  --              RS2: viajes (sin Disponible ni Anulado), más reciente primero.
  --              RS3: historial de contactos.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    SELECT s.*
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    WHERE s.ClienteID = @ClienteID;

    SELECT v.FechaSalida,
           v.FechaRegreso,
           v.Descripcion AS Viaje,
           e.Descripcion AS EstadoPasaje
    FROM dbo.Pasaje p
    INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
    INNER JOIN dbo.EstadoPasaje e ON e.ID = p.EstadoPasaje
    WHERE p.PasajeroID = @ClienteID
      AND p.EstadoPasaje NOT IN (1, 7)
    ORDER BY v.FechaSalida DESC;

    SELECT cc.Fecha,
           cc.Resultado,
           cc.Observacion,
           COALESCE(pv.FullName, cc.Usuario) AS Quien
    FROM dbo.ClienteContacto cc
    LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
    WHERE cc.ClienteID = @ClienteID
    ORDER BY cc.Fecha DESC;
END
