USE [MAT.Intranet]
GO
ALTER VIEW [dbo].[PersonaCliente]
AS
SELECT        dbo.Persona.PersonaID, dbo.Persona.Apellido, dbo.Persona.Nombre, dbo.Persona.TipoDocumento, dbo.Persona.NroDocumento, dbo.Persona.Pais, 
                         dbo.Persona.Telefono, dbo.Persona.Email, dbo.Persona.FechaNacimiento, dbo.Persona.Sexo, dbo.Cliente.ClienteID, dbo.Cliente.RazonSocial, dbo.Cliente.Cuit, 
                         dbo.Cliente.Moneda, dbo.Cliente.Empresa, dbo.Cliente.FormaPago, dbo.Cliente.CondicionIva, dbo.Cliente.VendedorID, dbo.Cliente.Fax, dbo.Cliente.Web, 
                         dbo.Cliente.Idioma, dbo.Cliente.Promotor,dbo.Cliente.Observacion ,dbo.Cliente.TipoID
FROM            dbo.Cliente INNER JOIN
                         dbo.Persona ON dbo.Cliente.ClienteID = dbo.Persona.PersonaID


GO

