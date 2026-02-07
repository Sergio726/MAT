CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_CreditoCliente_GetAll]
AS 
  -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-02-25
  -- Description:  Saldo de crédito por cliente (lista para NotaCreditoList).
  --
  -- 2018-02-25  Garcia Sergio: Create
  -- 2026-02-26     Agregado DNI (p.NroDocumento) para mostrar en listado y búsqueda por documento. ORDER BY p.FullName.
  -- ============================================= 
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	SELECT 
	   cred.ClienteID,
	   p.FullName, 
	   DNI = p.NroDocumento,
	   Monto = cred.Monto,
	   UltimaFecha = cred.UltimaFecha
	FROM dbo.Persona p
	INNER JOIN (
		SELECT cc.ClienteID, SUM(cc.Monto) AS Monto, MAX(cc.Fecha) AS UltimaFecha
		FROM dbo.CreditoCliente cc
		GROUP BY cc.ClienteID
	) AS cred (ClienteID, Monto, UltimaFecha)
		ON p.PersonaID = cred.ClienteID
	ORDER BY p.FullName;

END