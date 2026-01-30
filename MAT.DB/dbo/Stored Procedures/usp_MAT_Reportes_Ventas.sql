CREATE PROCEDURE [dbo].[usp_MAT_Reportes_Ventas]
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
Description:  Reporte de ventas

2024/01/13 Ruben Tejerina Create reporte
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    SELECT 
        tViaje.ViajeId,
        tViaje.ViajeDescripcion,
        perVen.PersonaID AS VendedorId,
        perVen.FullName AS VendedorFullName,
        perCli.PersonaID AS ClienteId,
        perCli.FullName AS ClienteFullName,
        f.FacturaID AS FacturaId,
        f.Fecha AS FacturaFecha,
        ef.Descripcion AS FacturaEstado,
        tFacturaTotal.Total AS TotalFactura,
        tFacturaPagos.MontoPagado,
        tFacturaPagos.UltimaFechaPago,
        tFacturaTotal.Total - tFacturaPagos.MontoPagado AS Saldo,
        SUM(tFacturaTotal.Total) OVER(PARTITION BY tViaje.ViajeId) AS total_Viaje,
        SUM(tFacturaTotal.Total) OVER(PARTITION BY perCli.PersonaID) AS total_Cliente,
        SUM(tFacturaTotal.Total) OVER(PARTITION BY perVen.PersonaID) AS total_Vendedor,
        SUM(tFacturaTotal.Total) OVER() AS total_rows
    FROM dbo.Factura f
    INNER JOIN dbo.EstadoFactura ef ON ef.ID = f.Estado
    INNER JOIN (
        SELECT
            p.FacturaID,
            v.ViajeID AS ViajeId,
            v.Descripcion AS ViajeDescripcion
        FROM dbo.Pasaje p
        INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
        WHERE p.FacturaID IS NOT NULL
        GROUP BY p.FacturaID, v.ViajeID, v.Descripcion
    ) tViaje ON tViaje.FacturaID = f.FacturaID
    INNER JOIN dbo.Persona perVen ON perVen.PersonaID = f.VendedorID
    INNER JOIN dbo.Persona perCli ON perCli.PersonaID = f.ClienteID
    CROSS APPLY (
        SELECT SUM(df.precio * df.Cantidad) AS Total
        FROM DetalleFactura df
        WHERE df.FacturaID = f.FacturaId
    ) AS tFacturaTotal
    CROSS APPLY (
        SELECT 
            ISNULL(SUM(ISNULL(a_p.Monto, 0)), 0) AS MontoPagado,
            MAX(a_p.FechaPago) AS UltimaFechaPago
        FROM MovimientoCuenta a_mc
        LEFT JOIN Pago a_p ON a_p.PagoID = a_mc.PagoID
        WHERE a_mc.FacturaId = f.FacturaId
    ) AS tFacturaPagos
    WHERE 
        (@From IS NULL OR f.Fecha >= @From) AND
        (@To IS NULL OR f.Fecha <= @To) AND
        (@ViajeId IS NULL OR tViaje.ViajeId = @ViajeId) AND
        (@VendedorId IS NULL OR perVen.PersonaID = @VendedorId) AND
        (@ClienteId IS NULL OR perCli.PersonaID = @ClienteId)
    ORDER BY 
        tViaje.ViajeId, f.FacturaID;

END