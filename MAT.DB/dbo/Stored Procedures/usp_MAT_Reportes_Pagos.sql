CREATE PROCEDURE [dbo].[usp_MAT_Reportes_Pagos]
(
    @From NVARCHAR(10) = NULL,  -- 'dd-mm-aaaa'
    @To   NVARCHAR(10) = NULL,  -- 'dd-mm-aaaa'
    @VendedorId UNIQUEIDENTIFIER = NULL,
    @ClienteId UNIQUEIDENTIFIER = NULL,
    @TipoVentaId INT = NULL,
    @ViajeId UNIQUEIDENTIFIER = NULL
)
AS
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 2024/01/14
Description:  Reporte de pagos

2024/01/14 Ruben Tejerina Create reporte
2025/08/15 sebagarcia format parameters from date, add Viaje
============================================= 
*/
BEGIN 
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE 
    @Dfrom DATE = CASE 
        WHEN @From IS NULL THEN NULL
        WHEN LEN(@From) = 10 THEN CONVERT(DATE, @From, 105)
        ELSE NULL
    END,
    @Dto DATE = CASE 
        WHEN @To IS NULL THEN NULL
        WHEN LEN(@To) = 10 THEN CONVERT(DATE, @To, 105)
        ELSE NULL
    END;

    IF @ViajeId IS NOT NULL
    BEGIN
        ;WITH tTipoVenta AS (
            SELECT 
                f.FacturaID,
                CASE WHEN pay.Id IS NULL THEN 1 ELSE 2 END AS TipoVentaId,
                CASE WHEN pay.Id IS NULL THEN 'Pago en oficina' ELSE 'Pago online' END AS TipoVentaDescripcion
            FROM dbo.Factura f
            LEFT JOIN dbo.Payment pay ON pay.FacturaId = f.FacturaID
        )
        SELECT DISTINCT
            mc.FacturaID,    
            pa.FechaPago,
            pa.Monto,    
            f.MonedaTipo,
            pt.Id AS PagoTipoId,
            pt.Descripcion,
            tv.TipoVentaId,
            tv.TipoVentaDescripcion,
            COUNT(pt.Id) OVER (PARTITION BY tv.TipoVentaId, pt.Id) AS CantidadTipoPago,
            CAST(DENSE_RANK() OVER (PARTITION BY tv.TipoVentaId ORDER BY pt.Id) AS INT) AS RankingTipoPago,
            perVen.PersonaID AS VendedorId,
            perVen.FullName AS VendedorFullName,
            perCli.PersonaID AS ClienteId,
            perCli.FullName AS ClienteFullName,
            v.Descripcion + ' ' + CONVERT(VARCHAR(10), v.FechaSalida, 105) AS Viaje
        FROM MovimientoCuenta mc
        INNER JOIN Pago pa ON pa.PagoID = mc.PagoID
        INNER JOIN PagoTipo pt ON pt.Id = pa.TipoPago
        INNER JOIN dbo.Factura f ON f.FacturaID = mc.FacturaID
        INNER JOIN dbo.Persona perCli ON perCli.PersonaID = f.ClienteID
        INNER JOIN dbo.Persona perVen ON perVen.PersonaID = f.VendedorID
        INNER JOIN tTipoVenta tv ON tv.FacturaID = f.FacturaID
        INNER JOIN dbo.Pasaje pj ON pj.FacturaID = f.FacturaID
        INNER JOIN dbo.Viaje v ON v.ViajeID = pj.ViajeID
        WHERE v.ViajeID = @ViajeId
        ORDER BY pa.FechaPago;
    END
    ELSE
    BEGIN
        ;WITH tTipoVenta AS (
            SELECT 
                f.FacturaID,
                CASE WHEN pay.Id IS NULL THEN 1 ELSE 2 END AS TipoVentaId,
                CASE WHEN pay.Id IS NULL THEN 'Pago en oficina' ELSE 'Pago online' END AS TipoVentaDescripcion
            FROM dbo.Factura f
            LEFT JOIN dbo.Payment pay ON pay.FacturaId = f.FacturaID
        )
        SELECT DISTINCT
            mc.FacturaID,    
            pa.FechaPago,
            pa.Monto,    
            f.MonedaTipo,
            pt.Id AS PagoTipoId,
            pt.Descripcion,
            tv.TipoVentaId,
            tv.TipoVentaDescripcion,
            COUNT(pt.Id) OVER (PARTITION BY tv.TipoVentaId, pt.Id) AS CantidadTipoPago,
            CAST(DENSE_RANK() OVER (PARTITION BY tv.TipoVentaId ORDER BY pt.Id) AS INT) AS RankingTipoPago,
            perVen.PersonaID AS VendedorId,
            perVen.FullName AS VendedorFullName,
            perCli.PersonaID AS ClienteId,
            perCli.FullName AS ClienteFullName,
            v.Descripcion + ' ' + CONVERT(VARCHAR(10), v.FechaSalida, 105) AS Viaje
        FROM MovimientoCuenta mc
        INNER JOIN Pago pa ON pa.PagoID = mc.PagoID
        INNER JOIN PagoTipo pt ON pt.Id = pa.TipoPago
        INNER JOIN dbo.Factura f ON f.FacturaID = mc.FacturaID
        INNER JOIN dbo.Persona perCli ON perCli.PersonaID = f.ClienteID
        INNER JOIN dbo.Persona perVen ON perVen.PersonaID = f.VendedorID
        INNER JOIN tTipoVenta tv ON tv.FacturaID = f.FacturaID
        INNER JOIN dbo.Pasaje pj ON pj.FacturaID = f.FacturaID
        INNER JOIN dbo.Viaje v ON v.ViajeID = pj.ViajeID
        WHERE 
            f.Fecha BETWEEN @Dfrom AND DATEADD(DAY, 1, @Dto)
            AND (@VendedorId IS NULL OR perVen.PersonaID = @VendedorId)
            AND (@ClienteId IS NULL OR perCli.PersonaID = @ClienteId)
            AND (@TipoVentaId IS NULL OR tv.TipoVentaId = @TipoVentaId)
        ORDER BY pa.FechaPago;
    END
END