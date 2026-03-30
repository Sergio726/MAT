/* Despliegue: alinear con MAT.DB\dbo\Stored Procedures\usp_MAT_Admin_AuditoriaFacturas.sql */
CREATE PROCEDURE [dbo].[usp_MAT_Admin_AuditoriaFacturas] (
    @dateFrom VARCHAR(10),
    @dateTo   VARCHAR(10)
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-03-30
  -- Description: Auditoría de facturas por rango de fechas (incluye todo el día "Hasta").
  --              LEFT JOIN a Persona para no perder filas con PersonaID NULL o cliente
  --              ya inexistente; fechas interpretadas como dd/mm/yyyy (estilo 103).
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @dFrom DATE = TRY_CONVERT(DATE, @dateFrom, 103);
    DECLARE @dTo   DATE = TRY_CONVERT(DATE, @dateTo, 103);

    IF @dFrom IS NULL OR @dTo IS NULL
        RETURN;

    SELECT af.ID,
           af.Accion,
           af.Descripcion,
           (CONVERT(VARCHAR(50), af.Fecha, 103) + ' ' + CONVERT(VARCHAR(10), af.Fecha, 108)) AS Fecha,
           CASE
               WHEN p1.PersonaID IS NULL THEN '(Sin cliente)'
               ELSE UPPER(LTRIM(RTRIM(ISNULL(p1.Apellido, ''))) + ', ' + LTRIM(RTRIM(ISNULL(p1.Nombre, ''))))
           END AS Cliente,
           CASE
               WHEN v1.PersonaID IS NULL THEN '(Sin vendedor)'
               ELSE UPPER(LTRIM(RTRIM(ISNULL(v1.Apellido, ''))) + ', ' + LTRIM(RTRIM(ISNULL(v1.Nombre, ''))))
           END AS Vendedor
    FROM dbo.AuditFactura af
    LEFT JOIN dbo.Persona p1 ON af.PersonaID = p1.PersonaID
    LEFT JOIN dbo.Persona v1 ON af.VendedorID = v1.PersonaID
    WHERE af.Fecha >= @dFrom
      AND af.Fecha < DATEADD(DAY, 1, @dTo);

END
