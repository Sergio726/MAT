-- DistribucionCoche Fase 5a: todas las butacas del transporte (LEFT JOIN Pasaje)
-- + EsMenor, EsTutor, TutorPasajeID, TutorNombre, TutorButacaNro, CapacidadTotal

IF OBJECT_ID(N'dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[usp_MAT_Reserva_DistribucionCoche_GetByViajeID](@ViajeID varchar(36))
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-06-16
  -- Updated:   2026-07-07 — FacturaID para acciones en mapa
  -- Updated:   2026-07-07 — Todas las butacas + menores/tutores + CapacidadTotal
  -- Description: Distribución de coche por viaje: todas las butacas del transporte,
  --              con datos de pasaje cuando existan. Segundo resultset: metadata viaje.
  ============================================= */
BEGIN
    SET nocount, xact_abort ON;
    SET TRANSACTION isolation level READ uncommitted;

    DECLARE @TransporteID uniqueidentifier;

    SELECT @TransporteID = v.BusID
    FROM dbo.Viaje v
    WHERE v.ViajeID = @ViajeID;

    SELECT b.NroButaca    AS ButacaNro,
           b.Posicion     AS ButacaPosicion,
           b.CodigoButaca AS ButacaCodigo,
           p.PasajeID     AS PasajeID,
           p.FacturaID    AS FacturaID,
           p.EstadoPasaje AS EstadoPasaje,
           p.PasajeroID   AS PasajeroID,
           per.Apellido   AS PasajeroApellido,
           per.Nombre     AS PasajeroNombre,
           EsMenor = CAST(CASE WHEN pm_minor.id IS NOT NULL THEN 1 ELSE 0 END AS bit),
           EsTutor = CAST(CASE WHEN pm_tutor.id IS NOT NULL THEN 1 ELSE 0 END AS bit),
           TutorPasajeID = pm_minor.pasajeid,
           TutorPasajeroID = pm_minor.pasajeroid,
           TutorNombre = CASE
               WHEN pm_minor.id IS NOT NULL THEN
                   LTRIM(RTRIM(ISNULL(per_tutor.Nombre, '') + ' ' + ISNULL(per_tutor.Apellido, '')))
               ELSE NULL
           END,
           TutorButacaNro = bt_tutor.NroButaca
    FROM dbo.Butaca b
    LEFT JOIN dbo.Pasaje p
        ON p.ButacaID = b.ButacaID
       AND p.ViajeID = @ViajeID
    LEFT JOIN dbo.Persona per
        ON p.PasajeroID = per.PersonaID
    LEFT JOIN dbo.PasajeroMenor pm_minor
        ON pm_minor.menorid = p.PasajeroID
       AND EXISTS (
           SELECT 1
           FROM dbo.Pasaje pj_chk
           WHERE pj_chk.PasajeID = pm_minor.pasajeid
             AND pj_chk.ViajeID = @ViajeID
       )
    LEFT JOIN dbo.Persona per_tutor
        ON pm_minor.pasajeroid = per_tutor.PersonaID
    LEFT JOIN dbo.Pasaje pj_tutor
        ON pm_minor.pasajeid = pj_tutor.PasajeID
    LEFT JOIN dbo.Butaca bt_tutor
        ON pj_tutor.ButacaID = bt_tutor.ButacaID
    LEFT JOIN dbo.PasajeroMenor pm_tutor
        ON pm_tutor.pasajeid = p.PasajeID
       AND pm_tutor.pasajeroid = p.PasajeroID
    WHERE b.TransporteID = @TransporteID
    ORDER BY b.NroButaca;

    SELECT NroCoche = t.NroCoche,
           TransporteTipo = t.Tipo,
           CapacidadTotal = (
               SELECT COUNT(*)
               FROM dbo.Butaca bx
               WHERE bx.TransporteID = t.TransporteID
           )
    FROM dbo.Viaje v
    INNER JOIN dbo.Transporte t
        ON v.BusID = t.TransporteID
    WHERE v.ViajeID = @ViajeID;
END
GO
