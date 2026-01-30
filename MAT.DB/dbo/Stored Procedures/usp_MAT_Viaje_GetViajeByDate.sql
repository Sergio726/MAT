CREATE PROCEDURE [dbo].[usp_MAT_Viaje_GetViajeByDate] 
    @Date varchar(10) = NULL,
    @Year int = NULL
AS 
/* ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 31/03/2025
-- Description: get Viaje by date or year
-- ============================================= */
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    SELECT v.Viajeid, 
           v.Paqueteid, 
           p.Descripcion AS PaqueteNombre,
           v.Origen, 
           CONVERT(VARCHAR(10), v.Fechasalida, 103) AS FechaSalida,
           CONVERT(VARCHAR(10), v.Horasalida, 103) AS HoraSalida,
           v.Paisorigen, 
           v.Paisdestino, 
           v.Paso, 
           v.Medio, 
           v.Busid, 
           CONVERT(VARCHAR(10), v.Fecharegreso, 103) AS FechaRegreso,
           v.Horaregreso, 
           v.Descripcion, 
           v.MonedaTipo,
           v.Preciosemicama, 
           v.Preciocama, 
           v.Preciopromocional, 
           CONVERT(VARCHAR(10), v.Fechapromocion, 103) AS FechaPromocion,
           v.Ndias, 
           v.Nnoches,
           ISNULL(v.TiempoConsentracion, 30) AS TiempoConsentracion,
           v.Observaciones,
           v.IsPublicWeb
    FROM dbo.Viaje v 
    LEFT JOIN dbo.Paquete p ON v.PaqueteID = p.PaqueteID
    WHERE (@Date IS NULL OR v.FechaSalida = CONVERT(DATETIME, @Date, 103))
          AND (@Year IS NULL OR YEAR(v.FechaSalida) = @Year)
    ORDER BY v.FechaSalida DESC
END