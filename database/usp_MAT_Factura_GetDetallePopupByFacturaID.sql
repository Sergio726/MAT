/*
    Consolidado optimizado para Popin DetalleFactura
    Unifica:
      - dbo.usp_MAT_Factura_GetFacturaClienteByFacturaID
      - dbo.usp_Factura_DetalleFacturaByFacturaID
      - dbo.usp_MAT_PersonaCliente_GetPasajeroMenorByFactura
      - dbo.usp_MAT_Factura_GetDetalleByFacturaID

    Notas de optimización:
      - @FacturaId ahora es UNIQUEIDENTIFIER en todo el flujo (evita conversiones implícitas y scans).
      - Se elimina DISTINCT + GROUP BY redundantes en detalle pasajes.
      - Se devuelven múltiples resultsets en un solo roundtrip.
*/

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Factura_GetDetallePopupByFacturaID]
(
    @FacturaId UNIQUEIDENTIFIER
)
AS
SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    DECLARE @SaldoFactura MONEY = 0;
    SELECT @SaldoFactura = dbo.fn_MAT_SaldoFactura(@FacturaId);

    /* Resultset 1: Cabecera factura */
    SELECT
        f.[FacturaID],
        [Monto] = ISNULL(SUM(df.Precio), 0),
        f.[Fecha],
        f.[Estado],
        f.[ClienteID],
        f.Observaciones,
        ISNULL(p.Nombre, '')  AS Nombre,
        ISNULL(p.Apellido,'') AS Apellido,
        ISNULL(v.Nombre, '')  AS VendedorNombre,
        ISNULL(v.Apellido,'') AS VendedorApellido,
        [Saldo] = @SaldoFactura
    FROM [dbo].[Factura] f
    INNER JOIN dbo.Persona p ON f.ClienteID = p.PersonaID
    INNER JOIN dbo.Persona v ON f.VendedorID = v.PersonaID
    LEFT  JOIN dbo.DetalleFactura df ON f.FacturaID = df.FacturaID
    WHERE f.[FacturaID] = @FacturaId
    GROUP BY
        f.[FacturaID],
        f.[Fecha],
        f.[Estado],
        f.[ClienteID],
        f.Observaciones,
        p.Nombre,
        p.Apellido,
        v.Nombre,
        v.Apellido;

    /* Resultset 2: Paquete/Moneda (mantiene semántica del SP original) */
    SELECT TOP (1)
        ISNULL(pa.Descripcion,'') AS PaqueteDescripcion,
        MonedaTipo          = pa.Moneda
    FROM dbo.Pasaje pj
    INNER JOIN dbo.Viaje v  ON pj.ViajeID = v.ViajeID
    INNER JOIN dbo.Paquete pa ON v.PaqueteID = pa.PaqueteID
    WHERE pj.FacturaID = @FacturaId;

    /* Resultset 3: Detalle pasajes (columnas mínimas para UI). Una fila por pasaje (evitar duplicados por múltiples ReservaHabitacion). */
    SELECT
        p.PasajeID,
        p.PasajeroID,
        p.ButacaID,
        p.ViajeID,
        b.NroButaca      AS ButacaNro,
        ISNULL(per.Nombre,'')   AS PasajeroNombre,
        ISNULL(per.Apellido,'') AS PasajeroApellido,
        CASE WHEN rh.PasajeID IS NOT NULL THEN CONVERT(uniqueidentifier, '11111111-1111-1111-1111-111111111111') ELSE NULL END AS HabitacionID
    FROM dbo.Pasaje p
    INNER JOIN dbo.Butaca b ON p.ButacaID = b.ButacaID
    INNER JOIN dbo.Viaje  v ON p.ViajeID  = v.ViajeID
    LEFT  JOIN dbo.Persona per ON p.PasajeroID = per.PersonaID
    LEFT  JOIN (SELECT PasajeID FROM dbo.ReservaHabitacion GROUP BY PasajeID) rh ON rh.PasajeID = p.PasajeID
    WHERE p.FacturaID = @FacturaId;

    /* Resultset 4: Menores (equivalente a usp_MAT_PersonaCliente_GetPasajeroMenorByFactura) */
    SELECT
        pm.Id,
        p.PasajeID         AS PasajeID,
        tutor.Apellido     AS ApellidoMayor,
        tutor.Nombre       AS NombreMayor,
        tutor.NroDocumento AS DocMayor,
        menor.Apellido     AS ApellidoMenor,
        menor.Nombre       AS NomreMenor,
        menor.NroDocumento AS DocMenor
    FROM dbo.Pasaje p
    INNER JOIN dbo.PasajeroMenor pm ON pm.PasajeID = p.PasajeID
    INNER JOIN dbo.Persona tutor ON pm.PasajeroID = tutor.PersonaID
    INNER JOIN dbo.Persona menor ON pm.MenorID = menor.PersonaID
    WHERE p.FacturaID = @FacturaId;

    /* Resultset 5: Items DetalleFactura (equivalente a usp_MAT_Factura_GetDetalleByFacturaID) */
    SELECT
        df.Id,
        df.Fecha,
        df.Detalle,
        df.Cantidad,
        df.Precio
    FROM dbo.DetalleFactura df
    WHERE df.FacturaID = @FacturaId;
END

