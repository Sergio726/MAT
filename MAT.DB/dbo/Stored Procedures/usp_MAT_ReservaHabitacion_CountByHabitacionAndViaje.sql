CREATE PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje]
    @HabitacionID UNIQUEIDENTIFIER,
    @ViajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: Cuenta reservas por habitacion y viaje (NetTiers F12 perf - LookupDataAccess).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT COUNT(1)
    FROM dbo.ReservaHabitacion
    WHERE HabitacionID = @HabitacionID
      AND ViajeID = @ViajeID;
END
