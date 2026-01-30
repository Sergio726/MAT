CREATE PROCEDURE [dbo].[usp_MAT_Persona_GetByDNI](@DNI varchar(50))
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2022-10-18
  -- Description:  get personnel data
  -- ============================================= 
    SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
  BEGIN 
	SELECT p.PersonaID,
			   p.Apellido, 
			   p.Nombre, 
			   p.NroDocumento, 
			   p.Telefono, 
			   p.Celular,
			   LocalidadNombre = l.Nombre, 
			   p.Nacionalidad, 
			   p.PaisResidencia 
		FROM   dbo.Persona p 
			   LEFT JOIN dbo.Localidad l 
					   ON p.LocalidadID = l.ID 
		WHERE p.NroDocumento = @DNI

END