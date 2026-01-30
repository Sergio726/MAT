CREATE PROCEDURE [dbo].[usp_MAT_Admin_AuditoriaFacturas](@dateFrom VARCHAR(10),
													     @dateTo VARCHAR(10)) 
AS 
 /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 05/26/2017
  -- Description:  get facturas by date range
  
  History
  05/27/2017	Garcia Sergio		Fix query
  06/01/2017	Garcia Sergio		fix data range
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT af.ID, 
			   af.Accion, 
			   af.Descripcion, 
			   (CONVERT(VARCHAR(50), af.Fecha,103) + ' ' + CONVERT(VARCHAR(10), af.Fecha,108)) AS Fecha,
			   Upper(p1.Apellido + ', ' + p1.Nombre) AS Cliente, 
			   Upper(v1.Apellido + ', ' + v1.Nombre) AS Vendedor 
		FROM   AuditFactura af 
			   INNER JOIN Persona p1 
					   ON af.PersonaID = p1.PersonaID 
			   INNER JOIN Persona v1 
					   ON af.VendedorID = v1.PersonaID 
		WHERE af.Fecha >= CONVERT(date, @dateFrom,103)    
			  AND af.Fecha < = CONVERT(date, @dateTo,103) 
		  
END 


