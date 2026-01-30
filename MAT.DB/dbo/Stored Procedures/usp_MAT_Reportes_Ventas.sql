CREATE PROCEDURE [dbo].[usp_MAT_Reportes_Ventas]
(	
    @From NVARCHAR(10) = NULL,  -- 'dd-mm-aaaa'
    @To   NVARCHAR(10) = NULL,  -- 'dd-mm-aaaa'
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
2025/08/14 sebagarcia   add convert date in filter to date
2025/01/08 sebagarcia   fix date format conversion
2025/08/20 sebagarcia   remove parameters whitout used 
2025/08/29 sebagarcia   set filter by date
2025/12/23 sebagarcia   add FechaSalida formatted as dd/mm/yyyy
2025/12/24 sebagarcia   add CantidadButacas to result set
============================================= 
*/
BEGIN 
    SET NOCOUNT ON; 
    SET XACT_ABORT ON; 
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED; 

    -- Convertir fechas del formato dd-mm-yyyy a date
    DECLARE 
    @Dfrom DATE = CASE 
        WHEN @From IS NULL THEN NULL
        WHEN LEN(@From) = 10 THEN
            CONVERT(DATE, @From, 105)
        ELSE
            NULL
    END,
    @Dto DATE = CASE 
        WHEN @To IS NULL THEN NULL
        WHEN LEN(@To) = 10 THEN
            CONVERT(DATE, @To, 105)
        ELSE
            NULL
    END

    PRINT 'Fechas recibidas: @From=' + ISNULL(@From, 'NULL') + ', @To=' + ISNULL(@To, 'NULL')
    PRINT 'Fechas convertidas: @Dfrom=' + ISNULL(CONVERT(VARCHAR, @Dfrom, 105), 'NULL') + ', @Dto=' + ISNULL(CONVERT(VARCHAR, @Dto, 105), 'NULL')

    IF @ViajeId IS NOT NULL
    BEGIN
        SELECT 
            tViaje.ViajeId,
            tViaje.ViajeDescripcion,
            CONVERT(VARCHAR(10), tViaje.FechaSalida, 103) AS FechaSalida,
			tViaje.CantidadButacas,
            perVen.PersonaID AS VendedorId,
            perVen.FullName AS VendedorFullName,
            perCli.PersonaID AS ClienteId,
            perCli.FullName AS ClienteFullName,
            f.FacturaID AS FacturaId,
            f.Fecha AS FacturaFecha,
            ef.Descripcion AS FacturaEstado,
            f.MonedaTipo AS MonedaTipo,
			tFacturaTotal.Total AS TotalFactura,
            tFacturaPagos.MontoPagado,
            tFacturaTotal.Total - tFacturaPagos.MontoPagado AS Saldo
        FROM dbo.Factura f
        INNER JOIN dbo.EstadoFactura ef ON ef.ID = f.Estado
        INNER JOIN (
            SELECT
                p.FacturaID,
				CantidadButacas = count(p.FacturaID),
                v.ViajeID AS ViajeId,
                v.Descripcion AS ViajeDescripcion,
                v.FechaSalida
            FROM dbo.Pasaje p
            INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
            WHERE p.FacturaID IS NOT NULL
            GROUP BY p.FacturaID, v.ViajeID, v.Descripcion, v.FechaSalida
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
                ISNULL(SUM(ISNULL(a_p.Monto, 0)), 0) AS MontoPagado
            FROM MovimientoCuenta a_mc
            LEFT JOIN Pago a_p ON a_p.PagoID = a_mc.PagoID
            WHERE a_mc.FacturaId = f.FacturaId
        ) AS tFacturaPagos
        WHERE tViaje.ViajeId = @ViajeId
        ORDER BY tViaje.ViajeId, f.FacturaID;
    END
    ELSE
    BEGIN
        SELECT 
            tViaje.ViajeId,
            tViaje.ViajeDescripcion,
            CONVERT(VARCHAR(10), tViaje.FechaSalida, 103) AS FechaSalida, -- dd/mm/yyyy
			tViaje.CantidadButacas,
            perVen.PersonaID AS VendedorId,
            perVen.FullName AS VendedorFullName,
            perCli.PersonaID AS ClienteId,
            perCli.FullName AS ClienteFullName,
            f.FacturaID AS FacturaId,
            f.Fecha AS FacturaFecha,
            ef.Descripcion AS FacturaEstado,
            f.MonedaTipo AS MonedaTipo,
            tFacturaTotal.Total AS TotalFactura,
            tFacturaPagos.MontoPagado,
            tFacturaTotal.Total - tFacturaPagos.MontoPagado AS Saldo
        FROM dbo.Factura f
        INNER JOIN dbo.EstadoFactura ef ON ef.ID = f.Estado
        INNER JOIN (
            SELECT
                p.FacturaID,
                v.ViajeID AS ViajeId,
				CantidadButacas = count(p.FacturaID),
                v.Descripcion AS ViajeDescripcion,
                v.FechaSalida
            FROM dbo.Pasaje p
            INNER JOIN dbo.Viaje v ON v.ViajeID = p.ViajeID
            WHERE p.FacturaID IS NOT NULL
            GROUP BY p.FacturaID, v.ViajeID, v.Descripcion, v.FechaSalida
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
                ISNULL(SUM(ISNULL(a_p.Monto, 0)), 0) AS MontoPagado
            FROM MovimientoCuenta a_mc
            LEFT JOIN Pago a_p ON a_p.PagoID = a_mc.PagoID
            WHERE a_mc.FacturaId = f.FacturaId
        ) AS tFacturaPagos
        WHERE 
            f.Fecha BETWEEN @Dfrom AND DATEADD(DAY, 1, @Dto)
            AND (@VendedorId IS NULL OR perVen.PersonaID = @VendedorId)
            AND (@ClienteId IS NULL OR perCli.PersonaID = @ClienteId)
        ORDER BY tViaje.ViajeId, f.FacturaID;
    END
END