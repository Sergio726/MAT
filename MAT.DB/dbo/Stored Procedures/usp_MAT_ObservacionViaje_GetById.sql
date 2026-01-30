
CREATE PROCEDURE [dbo].[usp_MAT_ObservacionViaje_GetById] @Id int
AS
/*- =============================================
-- Author:		Garcia Sergio
-- Create date: 2019-10-11
-- Description:	get observations by Id
-- =============================================*/
BEGIN
	SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	
	select ov.Id,
		   Vendedor = ven.Apellido + ' ' + ven.Nombre,
		   ov.Fecha,
		   Pasajeros = x.Pasajeros,
		   ov.Detalle,
		   c.Categoria,
		   CategoriaId = c.Id,
		   [Update] = nullif(ov.[Update],''),
		   UpdateVendedor = venUp.Apellido + ' ' + venUp.Nombre
	from dbo.ObservacionViaje ov
	inner join dbo.Persona ven
		on ov.VendedorID = ven.UserId
	inner join dbo.ObservacionViajeCategoria c
		on ov.CategoriaId = c.Id
	left join dbo.Persona venUp
		on ov.UpdateVendedorID = venUp.UserId
	outer apply (
					SELECT Pasajeros = STUFF(
						(SELECT  p.Apellido + ' ' + p.Nombre + '#' + CONVERT(varchar(100), p.PersonaID) + ','
						 FROM   dbo.Persona p 
						 WHERE  p.PersonaID IN (ov.PasajerosID) 
						 FOR xml path ('')
						), 1, 1, '') 		  
				) x
	where ov.Id = @Id 

END