CREATE PROCEDURE [dbo].[usp_MAT_Dashboard_Viajes_LugaresDisponibles]
AS 
/*
============================================= 
Author:    Sergio Garcia 
Create date: 21/12/2024
Description:  Consulta cuales son los viajes que estan a 40 dias o menos de realizarse y muestra los lugares disponibles

============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted;
    
    SELECT V.ViajeID,
	   v.descripcion,
       v.fechasalida,
       COUNT(p.viajeid) AS Disponibles
	  
    FROM dbo.pasaje p
    INNER JOIN dbo.viaje v ON p.viajeid = v.viajeid AND p.PasajeroID is null
    WHERE v.fechasalida BETWEEN GETDATE() AND (GETDATE() + 40)
    GROUP BY V.ViajeID, v.descripcion, v.fechasalida, p.pasajeroid;
    
END