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
	   Monto = cred.Monto
	from dbo.Persona p
	join (
			select cc.ClienteID, sum(cc.Monto) 
			from dbo.CreditoCliente cc
			group by cc.ClienteID
		)  as cred(ClienteID,Monto)
	on p.PersonaID = cred.ClienteID

END