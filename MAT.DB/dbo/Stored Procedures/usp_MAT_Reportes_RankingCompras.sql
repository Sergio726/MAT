CREATE PROCEDURE [dbo].[usp_MAT_Reportes_RankingCompras]
(	
	@To DATE = NULL, 
    @From DATE = NULL,
    @ViajeId UNIQUEIDENTIFIER = NULL,
    @VendedorId UNIQUEIDENTIFIER = NULL,
    @ClienteId UNIQUEIDENTIFIER = NULL
)
AS 
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 2024/01/13
Description:  Reporte de ranking de compras de cliente y viajes

2024/01/13 Ruben Tejerina Create reporte
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    WITH cte AS (
        SELECT 
            f.FacturaID,
            f.Fecha,
            f.ClienteID,
		    perCli.FullName,
		    tCompras.ViajeID  ,
            v.Descripcion AS ViajeDescripcion,
            v.FechaSalida AS ViajeFechaSalida,
            tCompras.CantidadPasajesXFactura,
            COUNT(f.FacturaID) OVER (PARTITION BY f.ClienteID) AS CantViajesCompradosXCliente,
            SUM(tCompras.CantidadPasajesXFactura) OVER (PARTITION BY f.ClienteID) AS CantPasajesCompradosXCliente,
		    COUNT(tCompras.ViajeID) OVER (PARTITION BY tCompras.ViajeID) AS CantClientesEligieronViaje
        FROM dbo.Factura f
        INNER JOIN (
            SELECT 
                p.FacturaID,
                p.ViajeID,
                COUNT(*) AS CantidadPasajesXFactura
            FROM dbo.Pasaje p
            WHERE
                p.PasajeroID IS NOT NULL
            GROUP BY p.FacturaID, p.ViajeID
        ) tCompras ON tCompras.FacturaID = f.FacturaID
        INNER JOIN dbo.Viaje v ON v.ViajeID = tCompras.ViajeID
	    INNER JOIN dbo.Persona perCli ON perCli.PersonaID = f.ClienteID
        WHERE
            (@From IS NULL OR f.Fecha >= @From) AND
            (@To IS NULL OR f.Fecha <= @To) AND
            (@ViajeId IS NULL OR tCompras.ViajeId = @ViajeId) AND
            --(@VendedorId IS NULL OR perVen.PersonaID = @VendedorId) AND
            (@ClienteId IS NULL OR f.ClienteID = @ClienteId)
    )

    SELECT 
        t.FacturaID
        ,t.Fecha
        ,t.ClienteID
        ,t.FullName
        ,t.ViajeID
        ,t.ViajeDescripcion
        ,t.ViajeFechaSalida
        ,t.CantidadPasajesXFactura
        ,t.CantViajesCompradosXCliente
        ,t.CantPasajesCompradosXCliente
        ,t.CantClientesEligieronViaje
        ,CAST( DENSE_RANK() OVER (ORDER BY t.CantViajesCompradosXCliente DESC) AS INT) AS RankingClientesCompradoresViajes
	    ,CAST( DENSE_RANK() OVER (ORDER BY t.CantClientesEligieronViaje DESC) AS INT) AS RankingViajes
    FROM cte as t   
    ORDER BY 
        RankingClientesCompradoresViajes , ClienteID
	    --RankingViajes,ViajeID
END