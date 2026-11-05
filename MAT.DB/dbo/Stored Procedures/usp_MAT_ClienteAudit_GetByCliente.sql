CREATE PROCEDURE [dbo].[usp_MAT_ClienteAudit_GetByCliente]
(
    @ClienteID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Historial de cambios del cliente, más reciente primero (pestaña de la ficha, solo administradores).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT a.Fecha, a.Usuario, a.Accion, a.Detalle
    FROM dbo.ClienteAudit a
    WHERE a.ClienteID = @ClienteID
    ORDER BY a.Fecha DESC, a.ID DESC;
END
