-- NetTiers F12 — SPs de performance para LookupDataAccess
-- Ejecutar contra MAT_DEV.Intranet / MAT.Intranet según entorno

IF OBJECT_ID(N'dbo.usp_MAT_Viaje_GetSelectList', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_Viaje_GetSelectList AS SELECT 1');
GO

ALTER PROCEDURE [dbo].[usp_MAT_Viaje_GetSelectList]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: ViajeID + descripcion paquete + fecha salida para dropdowns (NetTiers F12 perf).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        v.ViajeID,
        v.FechaSalida,
        PaqueteDescripcion = ISNULL(p.Descripcion, N'Sin paquete')
    FROM dbo.Viaje v
    LEFT JOIN dbo.Paquete p ON p.PaqueteID = v.PaqueteID
    ORDER BY v.FechaSalida DESC;
END
GO

IF OBJECT_ID(N'dbo.usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje AS SELECT 1');
GO

ALTER PROCEDURE [dbo].[usp_MAT_ReservaHabitacion_CountByHabitacionAndViaje]
    @HabitacionID UNIQUEIDENTIFIER,
    @ViajeID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-05
 -- Description: Cuenta reservas por habitacion y viaje (NetTiers F12 perf).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT COUNT(1)
    FROM dbo.ReservaHabitacion
    WHERE HabitacionID = @HabitacionID
      AND ViajeID = @ViajeID;
END
GO
