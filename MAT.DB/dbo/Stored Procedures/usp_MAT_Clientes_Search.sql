CREATE PROCEDURE dbo.usp_MAT_Clientes_Search
(
	@search varchar(100) = ''
)
AS
 /*-- ============================================= 
  -- Author: Ruben Tejerina 
  -- Create date: 08/10/2024
  -- Description: Search client by nombre, apellido, email, nroDocumento and NroDocumentoCalc
	 This SP is called by API BACKEND

	 18/10/2024 Ruben Tejerina Add IsMenor
	 
  -- ============================================= */
BEGIN
	SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted;	

	declare @Today date = getdate()
	select 
		p.PersonaID,
		p.Nombre,
		p.Apellido,
		p.NroDocumento,
		p.Email,
		datediff(year,p.FechaNacimiento,@Today) as Edad		
	from Cliente c
	inner join Persona p on p.PersonaID = c.ClienteID
	where
		p.Nombre like '%'+ @search +'%'
		or p.Apellido like '%'+ @search +'%'
		or p.Email like '%'+ @search +'%'
		or p.NroDocumento like '%'+ @search +'%'
		or p.NroDocumentoCalc like '%'+ @search +'%'
		or @search = ''
END