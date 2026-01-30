CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_Search] @param varchar(50) = null
AS
 /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 01/14/2018
  -- Description:  search person by Name or DNI
	
	 2018-02-01	Garcia Sergio: improve search
  -- ============================================= */
SET nocount, xact_abort ON; 
SET TRANSACTION isolation level READ uncommitted; 

BEGIN
	declare @countParam int = 1
	declare @tmp table(Value varchar(100))
	set @param = REPLACE(@param,'.','')

	insert into @tmp
	select s.Item from dbo.Split(@param, ' ') s
	select @countParam = count(*) from @tmp

	SELECT distinct
	p.PersonaID,
	p.Apellido, 
	p.Nombre, 
	p.NroDocumento, 
	p.Telefono, 
	p.Celular,
	LocalidadNombre = l.Nombre, 
	p.Nacionalidad, 
	p.PaisResidencia
	FROM   dbo.Persona p 
	inner join @tmp t
		on p.FullName like '%' + t.Value +'%'
		or p.NroDocumentoCalc like '' + t.Value + '%'
	LEFT JOIN dbo.Localidad l 
			ON p.LocalidadID = l.ID

	group by 
	p.PersonaID,
	p.Apellido, 
	p.Nombre, 
	p.NroDocumento, 
	p.Telefono, 
	p.Celular,
	l.Nombre, 
	p.Nacionalidad, 
	p.PaisResidencia
	having (count(p.PersonaID) > 1 and @countParam > 1)
			or
			(count(p.PersonaID) > 0 and @countParam = 1)
				

END
