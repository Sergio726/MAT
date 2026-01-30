CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetListFacturaIDByViajeByClienteID](@ClienteID uniqueidentifier)
AS
/*-----------------------------------------------------------
Author:    Garcia Sergio 
Create date: 2017-09-24
Description:  Get List FacturaIDs group by Viaje by ClienteID


-----------------------------------------------------------*/

	SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
BEGIN
	SELECT f.FacturaID, 
       ViajeSalida = v.FechaSalida, 
       PaqueteNombre =pa.Descripcion,
	   FacturaFecha = f.Fecha  
	FROM   dbo.Viaje v 
			INNER JOIN dbo.Pasaje p 
					ON v.ViajeID = p.ViajeID 
			INNER JOIN dbo.Factura f 
					ON p.FacturaID = f.FacturaID 
			INNER JOIN dbo.Paquete pa
					ON v.PaqueteID = pa.PaqueteID
	WHERE  f.ClienteID = @ClienteID 
	group by
			f.FacturaID, 
			v.FechaSalida, 
			v.Descripcion,
			pa.Descripcion,
			f.Fecha

END

