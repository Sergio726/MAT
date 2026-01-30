CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetByPersonaID](@PersonaID UniqueIdentifier)
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2017-09-25
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
		WHERE p.PersonaID = @PersonaID

END

