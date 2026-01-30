CREATE PROCEDURE [dbo].[usp_MAT_Reportes_Pagos]
(
    @From DATE = NULL,
    @To DATE = NULL,
    @VendedorId UNIQUEIDENTIFIER = NULL,
    @ClienteId UNIQUEIDENTIFIER = NULL,
    @TipoVentaId INT = NULL
)
AS
/*
============================================= 
Author:    Ruben Tejerina 
Create date: 2024/01/14
Description:  Reporte de pagos

2024/01/14 Ruben Tejerina Create reporte
============================================= 
*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

    ;WITH tTipoVenta AS (
        SELECT 
            f.FacturaID,
            CASE 
                WHEN pay.Id IS NULL THEN 1
                ELSE 2
            END AS TipoVentaId,
            CASE 
                WHEN pay.Id IS NULL THEN 'Pago en oficina'
                ELSE 'Pago online'
            END AS TipoVentaDescripcion
        FROM dbo.Factura f
        LEFT JOIN dbo.Payment pay ON pay.FacturaId = f.FacturaID
    )
    SELECT 
        mc.FacturaID,    
        pa.FechaPago,
        pa.Monto,    
        pt.Id AS PagoTipoId,
        pt.Descripcion,
        tv.TipoVentaId,
        tv.TipoVentaDescripcion,
        COUNT(pt.Id) OVER (PARTITION BY tv.TipoVentaId, pt.Id) AS CantidadTipoPago,
        CAST(DENSE_RANK() OVER (PARTITION BY tv.TipoVentaId ORDER BY pt.Id) AS INT) AS RankingTipoPago,
        perVen.PersonaID AS VendedorId,
        perVen.FullName AS VendedorFullName,
        perCli.PersonaID AS ClienteId,
        perCli.FullName AS ClienteFullName
    FROM MovimientoCuenta mc
    INNER JOIN Pago pa ON pa.PagoID = mc.PagoID
    INNER JOIN PagoTipo pt ON pt.Id = pa.TipoPago
    INNER JOIN dbo.Factura f ON f.FacturaID = mc.FacturaID
    INNER JOIN dbo.Persona perCli ON perCli.PersonaID = f.ClienteID
    INNER JOIN dbo.Persona perVen ON perVen.PersonaID = f.VendedorID
    INNER JOIN tTipoVenta tv ON tv.FacturaID = f.FacturaID
    WHERE 
        (@From IS NULL OR pa.FechaPago >= @From)
        AND (@To IS NULL OR pa.FechaPago <= @To)
        AND (@VendedorId IS NULL OR perVen.PersonaID = @VendedorId)
        AND (@ClienteId IS NULL OR perCli.PersonaID = @ClienteId)
        AND (@TipoVentaId IS NULL OR tv.TipoVentaId = @TipoVentaId)
    ORDER BY mc.FacturaID;
END;