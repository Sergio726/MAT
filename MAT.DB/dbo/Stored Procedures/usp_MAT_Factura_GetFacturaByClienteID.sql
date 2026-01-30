CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetFacturaByClienteID]( @ClienteID uniqueidentifier )
AS
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 09/04/2017
-- Description:  trae una lista de factura por ClienteID
-- 2017-09-17	Garcia Sergio	actulizacion del campo monto de factura
-- 2017-09-29	Garcia Sergio	join EstadoFactura
-- =============================================
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;
BEGIN 

	SELECT f.[facturaid], 
		   f.[nrofactura], 
		   [monto] = (select sum(df.Precio) from dbo.DetalleFactura df where df.FacturaId = f.facturaid),
		   f.[fecha], 
		   isnull(ef.descripcion,'') AS [EstadoFactura], 
		   f.[estado], 
		   f.[clienteid], 
		   p.nombre       AS [PersonaNombre], 
		   p.apellido     AS [PersonaApellido], 
		   PA.descripcion AS [PaqueteDescripcion]
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
	WHERE  f.ClienteID = @ClienteID 
	GROUP  BY f.[facturaid], 
			  f.[nrofactura], 
			  f.[monto], 
			  f.[fecha], 
			  f.[estado], 
			  f.[clienteid], 
			  p.Nombre, 
			  p.Apellido, 
			  Pa.Descripcion, 
			  ef.Descripcion 
	ORDER BY f.Fecha desc

END

