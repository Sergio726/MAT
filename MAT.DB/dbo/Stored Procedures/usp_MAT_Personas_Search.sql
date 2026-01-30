CREATE PROCEDURE [dbo].[usp_MAT_Personas_Search]
(
	@search varchar(100) = ''
)
AS
 /*-- ============================================= 
  -- Author: Ruben Tejerina 
  -- Create date: 18/10/2024
  -- Description: Search person by nombre, apellido, email, nroDocumento and NroDocumentoCalc
	 This SP is called by API BACKEND

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
	from Persona p
	where
		p.Nombre like '%'+ @search +'%'
		or p.Apellido like '%'+ @search +'%'
		or p.Email like '%'+ @search +'%'
		or p.NroDocumento like '%'+ @search +'%'
		or p.NroDocumentoCalc like '%'+ @search +'%'
		or @search = ''
END