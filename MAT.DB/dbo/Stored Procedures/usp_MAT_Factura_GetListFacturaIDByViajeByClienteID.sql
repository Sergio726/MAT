CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetListFacturaIDByViajeByClienteID](@ClienteID uniqueidentifier)
AS
/*-----------------------------------------------------------
Author:    Garcia Sergio 
Create date: 2017-09-24
Description:  Lista de facturas agrupadas por viaje para el cliente (Historial de Pagos).
              Incluye facturas con pasaje (viaje activo) y facturas convertidas en Nota de crédito
              (estado 6), para poder ver los pagos que tuvo antes de la anulación.
 2017-09-24  Garcia Sergio: Create
 2026-02     Incluir facturas en estado Nota de crédito (6); viaje desde Nota.ViajeFecha/ViajeNombre
-----------------------------------------------------------*/
	SET nocount, xact_abort ON; 
	SET TRANSACTION isolation level READ uncommitted; 

BEGIN
	-- Facturas con pasaje vinculado (viaje activo)
	SELECT f.FacturaID, 
		ViajeSalida = v.FechaSalida, 
		PaqueteNombre = pa.Descripcion,
		FacturaFecha = f.Fecha  
	FROM dbo.Viaje v 
	INNER JOIN dbo.Pasaje p ON v.ViajeID = p.ViajeID AND p.FacturaID IS NOT NULL
	INNER JOIN dbo.Factura f ON p.FacturaID = f.FacturaID 
	INNER JOIN dbo.Paquete pa ON v.PaqueteID = pa.PaqueteID
	WHERE f.ClienteID = @ClienteID 
	GROUP BY f.FacturaID, v.FechaSalida, v.Descripcion, pa.Descripcion, f.Fecha

	UNION ALL

	-- Facturas convertidas en Nota de crédito (ya no tienen pasaje; viaje desde Nota)
	SELECT f.FacturaID,
		ViajeSalida = CAST(n.ViajeFecha AS DATETIME),
		PaqueteNombre = ISNULL(n.ViajeNombre, 'Nota de crédito'),
		FacturaFecha = f.Fecha
	FROM dbo.Factura f
	CROSS APPLY (SELECT TOP 1 mc.NotaID FROM dbo.MovimientoCuenta mc WHERE mc.FacturaID = f.FacturaID AND mc.NotaID IS NOT NULL) x
	INNER JOIN dbo.Nota n ON n.NotaID = x.NotaID
	WHERE f.ClienteID = @ClienteID AND f.Estado = 6

	ORDER BY ViajeSalida DESC, FacturaFecha DESC;
END

