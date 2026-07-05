CREATE PROCEDURE [dbo].[usp_MAT_HistorialPagos_GetByRango]
(
    @From NVARCHAR(10) = NULL,  -- 'dd-mm-aaaa' | 'aaaa-mm-dd' | 'aaaammdd'
    @To   NVARCHAR(10) = NULL,
    @VendedorId UNIQUEIDENTIFIER = NULL,
    @TipoVentaId INT = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-07-05
  -- Description: Historial de pagos por rango de FECHA DE PAGO para /Home/HistorialPagos.
  --              Base: dbo.Pago (1 fila por pago, sin dbo.Cuenta legacy).
  --              Enriquecimiento opcional: Factura/MovimientoCuenta, CuentaCorriente, Viaje.
  --              Admin: @VendedorId NULL = todos los pagos del rango.
  --              Validacion: COUNT(*) debe coincidir con
  --                SELECT COUNT(*) FROM dbo.Pago pa
  --                WHERE pa.FechaPago >= @Dfrom AND pa.FechaPago < DATEADD(DAY,1,@Dto)
  --                  AND (@VendedorId IS NULL OR pa.VendedorId = @VendedorId)
  --              (antes de filtrar @TipoVentaId).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Dfrom DATE = CASE
            WHEN @From IS NULL THEN NULL
            WHEN LEN(@From) = 10 AND SUBSTRING(@From, 3, 1) = '-' THEN CONVERT(DATE, @From, 105)   -- dd-mm-yyyy
            WHEN LEN(@From) = 10 AND SUBSTRING(@From, 5, 1) = '-' THEN CONVERT(DATE, @From, 23)    -- yyyy-mm-dd
            WHEN LEN(@From) = 8  THEN CONVERT(DATE, @From, 112)                                     -- yyyymmdd
            ELSE NULL
        END,
        @Dto DATE = CASE
            WHEN @To IS NULL THEN NULL
            WHEN LEN(@To) = 10 AND SUBSTRING(@To, 3, 1) = '-' THEN CONVERT(DATE, @To, 105)
            WHEN LEN(@To) = 10 AND SUBSTRING(@To, 5, 1) = '-' THEN CONVERT(DATE, @To, 23)
            WHEN LEN(@To) = 8  THEN CONVERT(DATE, @To, 112)
            ELSE NULL
        END;

    IF @Dfrom IS NULL OR @Dto IS NULL
        RETURN;

    ;WITH PagosRango AS (
        SELECT
            pa.PagoID,
            pa.FechaPago,
            pa.Monto,
            pa.TipoPago,
            pa.VendedorId,
            pa.ClienteID,
            pa.CuentaCorrienteID
        FROM dbo.Pago pa
        WHERE pa.FechaPago >= @Dfrom
          AND pa.FechaPago < DATEADD(DAY, 1, @Dto)
          AND (@VendedorId IS NULL OR pa.VendedorId = @VendedorId)
    )
    SELECT
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
        pa.VendedorId AS VendedorId,
        perVen.FullName AS VendedorFullName,
        COALESCE(pa.ClienteID, f.ClienteID, cc.ClienteID) AS ClienteId,
        perCli.FullName AS ClienteFullName,
        vj.Viaje
    FROM PagosRango pa
    LEFT JOIN dbo.PagoTipo pt ON pt.Id = pa.TipoPago
    LEFT JOIN dbo.Persona perVen ON perVen.PersonaID = pa.VendedorId
    LEFT JOIN dbo.CuentaCorriente cc ON cc.CuentaCorrienteID = pa.CuentaCorrienteID
    LEFT JOIN dbo.MovimientoCuenta mc ON mc.PagoID = pa.PagoID
    LEFT JOIN dbo.Factura f ON f.FacturaID = mc.FacturaID
    LEFT JOIN dbo.Persona perCli ON perCli.PersonaID = COALESCE(pa.ClienteID, f.ClienteID, cc.ClienteID)
    CROSS APPLY (
        SELECT
            CASE
                WHEN f.FacturaID IS NOT NULL
                     AND EXISTS (SELECT 1 FROM dbo.Payment pay WHERE pay.FacturaId = f.FacturaID)
                THEN 2 ELSE 1
            END AS TipoVentaId,
            CASE
                WHEN f.FacturaID IS NOT NULL
                     AND EXISTS (SELECT 1 FROM dbo.Payment pay WHERE pay.FacturaId = f.FacturaID)
                THEN N'Pago online' ELSE N'Pago en oficina'
            END AS TipoVentaDescripcion
    ) tv
    OUTER APPLY (
        SELECT TOP 1 v.Descripcion + N' ' + CONVERT(VARCHAR(10), v.FechaSalida, 105) AS Viaje
        FROM dbo.Pasaje pj
        INNER JOIN dbo.Viaje v ON v.ViajeID = pj.ViajeID
        WHERE pj.FacturaID = f.FacturaID
        ORDER BY v.FechaSalida
    ) vj
    WHERE (@TipoVentaId IS NULL OR tv.TipoVentaId = @TipoVentaId)
    ORDER BY pa.FechaPago;
END
