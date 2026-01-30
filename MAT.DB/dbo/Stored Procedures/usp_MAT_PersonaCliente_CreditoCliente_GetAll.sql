CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_CreditoCliente_GetAll]
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-02-25
  -- Description:  Get all Nota Credito

  --
  --2018-02-25	Garcia Sergio: Create
  -- ============================================= 
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	select 
	   cred.ClienteID,
	   p.FullName, 
	   Monto = cred.Monto,
	   UltimaFecha = cred.UltimaFecha
	from dbo.Persona p
	join (
			select cc.ClienteID, sum(cc.Monto) as Monto, max(cc.Fecha) as UltimaFecha
			from dbo.CreditoCliente cc
			group by cc.ClienteID
		)  as cred(ClienteID,Monto,UltimaFecha)
	on p.PersonaID = cred.ClienteID

END