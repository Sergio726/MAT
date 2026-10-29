CREATE PROCEDURE [dbo].[usp_MAT_Seguimiento_Cumpleanios]
(
    @Dias INT = 0
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-10-29
  -- Description: Clientes que cumplen años entre hoy y hoy + @Dias (0 = solo hoy), sin los
  --              "No volver a contactar" ni fechas de nacimiento imposibles. Informa si ya se los
  --              saludó en los últimos 30 días y quién (aviso sin bloqueo entre vendedores).
  --              29/02: en años no bisiestos se saluda el 28/02 (DATEADD).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);
    IF (@Dias IS NULL OR @Dias < 0) SET @Dias = 0;
    IF (@Dias > 60) SET @Dias = 60;

    SELECT s.*,
           pc.ProximoCumple,
           DATEDIFF(DAY, @Hoy, pc.ProximoCumple) AS DiasFaltan,
           DATEDIFF(YEAR, s.FechaNacimiento, pc.ProximoCumple) AS EdadQueCumple,
           sal.Fecha AS UltimoSaludoFecha,
           sal.Quien AS UltimoSaludoPor
    FROM dbo.fn_MAT_Seguimiento_Clientes(@Hoy) s
    CROSS APPLY (
        SELECT CASE
                   WHEN DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento), s.FechaNacimiento) >= @Hoy
                       THEN DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento), s.FechaNacimiento)
                   ELSE DATEADD(YEAR, YEAR(@Hoy) - YEAR(s.FechaNacimiento) + 1, s.FechaNacimiento)
               END AS ProximoCumple
    ) pc
    OUTER APPLY (
        SELECT TOP (1) cc.Fecha, COALESCE(pv.FullName, cc.Usuario) AS Quien
        FROM dbo.ClienteContacto cc
        LEFT JOIN dbo.Persona pv ON pv.PersonaID = cc.VendedorID
        WHERE cc.ClienteID = s.ClienteID
          AND cc.Resultado = 1
          AND cc.Fecha >= DATEADD(DAY, -30, @Hoy)
        ORDER BY cc.Fecha DESC
    ) sal
    WHERE s.FechaNacimiento IS NOT NULL
      AND YEAR(s.FechaNacimiento) >= 1910
      AND s.FechaNacimiento <= @Hoy
      AND s.NoContactar = 0
      AND DATEDIFF(DAY, @Hoy, pc.ProximoCumple) <= @Dias
    ORDER BY DiasFaltan, s.Apellido, s.Nombre;
END
