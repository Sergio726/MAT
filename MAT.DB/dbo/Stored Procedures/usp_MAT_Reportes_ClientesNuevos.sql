CREATE PROCEDURE [dbo].[usp_MAT_Reportes_ClientesNuevos]
(
    @FechaInicio  DATETIME         = NULL,
    @FechaFin     DATETIME         = NULL,
    @VendedorId   UNIQUEIDENTIFIER = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-04-04
  -- Description: Retorna clientes dados de alta en un rango de fechas.
  --              Filtra por FechaAlta del cliente.
  --              Si @VendedorId es NULL trae todos (rol Administrador).
  --              Incluye la primera factura del cliente como dato informativo.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    ;WITH PrimeraFactura AS (
        SELECT
            f.ClienteID,
            MIN(f.Fecha) AS PrimeraFecha
        FROM dbo.Factura f
        WHERE f.Fecha IS NOT NULL
        GROUP BY f.ClienteID
    )
    SELECT
        ISNULL(p.Nombre, '') + ' ' + ISNULL(p.Apellido, '') AS ClienteNombre,
        ISNULL(p.NroDocumento, '')                           AS NroDocumento,
        ISNULL(p.Celular, '')                                AS Celular,
        ISNULL(p.Email, '')                                  AS Email,
        c.FechaAlta,
        pf.PrimeraFecha,
        ISNULL(perVen.Nombre, '') + ' ' + ISNULL(perVen.Apellido, '') AS VendedorNombre
    FROM dbo.Cliente c
    INNER JOIN dbo.Persona p     ON c.ClienteID  = p.PersonaID
    LEFT  JOIN PrimeraFactura pf ON pf.ClienteID = c.ClienteID
    LEFT  JOIN dbo.Persona perVen ON c.VendedorID = perVen.PersonaID
    WHERE c.FechaAlta >= @FechaInicio
      AND c.FechaAlta <  DATEADD(DAY, 1, @FechaFin)
      --AND (@VendedorId IS NULL OR c.VendedorID = @VendedorId)
    ORDER BY c.FechaAlta DESC;
END
