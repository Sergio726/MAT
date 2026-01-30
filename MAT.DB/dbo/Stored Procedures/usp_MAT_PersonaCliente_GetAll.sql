
CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetAll]
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/11/2017
  -- Description:  Remove registration ReservaHabitacion
  01-04-2018	Sergio Garcia: add IsTituarFactura
  -- ============================================= */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT p.PersonaID,
			   p.Apellido, 
			   p.Nombre, 
			   p.NroDocumento, 
			   p.Telefono, 
			   p.Celular,
			   LocalidadNombre = l.Nombre, 
			   p.Nacionalidad, 
			   p.PaisResidencia,
			   IsTituarFactura =  (select top 1 iif(count(*) > 0,1,0) from dbo.Factura f where f.ClienteID = p.PersonaID)
		FROM   dbo.Persona p 
			   LEFT JOIN dbo.Localidad l 
					   ON p.LocalidadID = l.ID
			   
					    

END
