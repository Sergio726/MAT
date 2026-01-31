CREATE PROCEDURE [dbo].[usp_MAT_Nota_GetNotaByID](@NotaID UNIQUEIDENTIFIER)
AS
	/*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 09-30-2017   
  -- Description:  get NotaCredito 
    2018-03-05	add Vendedor, detalle  
	2026-01-31	add Cliente
  -- =============================================*/ 
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	select n.Dias,
		   n.Fecha,
		   n.MontoNota,
		   n.MontoRetencion,
		   n.MontoDevolucion,
		   n.NroNota,
		   n.PorcentajeRetencion,
		   Vendedor = p.FullName,
		   Cliente = pCliente.FullName,
		   n.Detalle
	from dbo.Nota n
	inner join dbo.Persona p
		on n.VendedorID = p.PersonaID
	inner join dbo.Persona pCliente
		on n.ClienteID = pCliente.PersonaID
	where n.NotaID = @NotaID

END