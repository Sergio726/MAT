/*
  NUEVO SP (v2) - Soft delete: SELECT de Lista de Espera por Viaje (solo activos)
  - No modifica el SP legacy existente.
  - Filtra IsDeleted = 0 para no mostrar quitados
  - Mantiene output esperado por DataTables
*/

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ListaEspera_SelectByViajeId_Active]
(
    @ViajeID UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        LE.Id,
        LE.ViajeID,
        LE.ClienteID,
        Cliente =
            UPPER(ISNULL(LE.PasajeroTemporal,'')) +
            UPPER(ISNULL(PE.Apellido,'')) + ' ' + UPPER(ISNULL(PE.Nombre,'')) +
            ' (' + ISNULL(PE.NroDocumento,'') + ')',
        Vendedor = UPPER(USR.UserName),
        Fecha = CONVERT(VARCHAR(10), LE.Fecha, 103),
        LE.Observacion
    FROM dbo.ListaEspera LE
    LEFT JOIN dbo.Persona PE ON LE.ClienteID = PE.PersonaID
    LEFT JOIN [MAT.Session].[dbo].[UserProfile] USR ON LE.UsuarioID = USR.UserId
    WHERE LE.ViajeID = @ViajeID
      AND ISNULL(LE.IsDeleted, 0) = 0;
END

