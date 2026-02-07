CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetFacturaByClienteID]( @ClienteID uniqueidentifier )
AS
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 09/04/2017
-- Description:  Lista de facturas por ClienteID (para PersonaCliente/Facturas).
-- 2017-09-17  Garcia Sergio: actualización del campo monto de factura
-- 2017-09-29  Garcia Sergio: join EstadoFactura
-- 2026-02     Agregado NotaID (OUTER APPLY MovimientoCuenta) para facturas en estado Nota de crédito; permite enlazar a popup de la nota.
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;
BEGIN 

	SELECT f.[facturaid], 
		   f.[nrofactura], 
		   [monto] = (SELECT SUM(df.Precio) FROM dbo.DetalleFactura df WHERE df.FacturaId = f.facturaid),
		   f.[fecha], 
		   ISNULL(ef.descripcion,'') AS [EstadoFactura], 
		   f.[estado], 
		   f.[clienteid], 
		   p.nombre       AS [PersonaNombre], 
		   p.apellido     AS [PersonaApellido], 
		   PA.descripcion AS [PaqueteDescripcion],
		   n.NotaID       AS [NotaID]
	FROM   [dbo].Factura f 
		   INNER JOIN dbo.Persona p 
			   ON f.ClienteID = p.PersonaID 
		   LEFT JOIN dbo.Pasaje pj 
			   ON f.FacturaID = pj.FacturaID 
		   LEFT JOIN dbo.Viaje v 
			   ON pj.ViajeID = v.viajeid 
		   LEFT JOIN dbo.Paquete pa 
			   ON v.PaqueteID = pa.PaqueteID 
		   INNER JOIN dbo.EstadoFactura ef 
			   ON f.Estado = ef.ID 
		   OUTER APPLY (SELECT TOP 1 mc.NotaID FROM dbo.MovimientoCuenta mc WHERE mc.FacturaID = f.FacturaID AND mc.NotaID IS NOT NULL) n
	WHERE  f.ClienteID = @ClienteID 
	GROUP BY f.[facturaid], f.[nrofactura], f.[monto], f.[fecha], f.[estado], f.[clienteid], 
			 p.Nombre, p.Apellido, Pa.Descripcion, ef.Descripcion, n.NotaID
	ORDER BY f.Fecha DESC

END

