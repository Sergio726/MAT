CREATE PROCEDURE [dbo].[usp_MAT_Factura_GetPasajerosByFacturaID] @FacturaId varchar(36) = null
AS
-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 2017/01/16
-- Description:  get pasajeros from factura
--

-- 18/04/2017	Garcia Sergio: add top 1

-- =============================================

SET nocount, xact_abort ON;

SET TRANSACTION isolation level READ uncommitted;
BEGIN
	
;with cte as (
	SELECT DISTINCT
			 per.FullName 
	  FROM   pasaje P 
	  inner JOIN dbo.Persona per
		 on p.PasajeroID = per.PersonaID 
	WHERE  p.FacturaID = @FacturaId 

	union

	select per.FullName
	from dbo.PasajeroMenor pm
	inner join dbo.Pasaje p
		on pm.pasajeid = p.PasajeID
	 inner JOIN dbo.Persona per
		 on p.PasajeroID = per.PersonaID 
	WHERE  p.FacturaID = @FacturaId 
  
  )

  select PasajeroFullName = FullName
  from cte

END