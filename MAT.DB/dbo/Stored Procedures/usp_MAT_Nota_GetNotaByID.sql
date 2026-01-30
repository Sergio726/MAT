CREATE PROCEDURE [dbo].[usp_MAT_Nota_GetNotaByID](@NotaID UNIQUEIDENTIFIER)
AS
	/*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 09-30-2017   
  -- Description:  get NotaCredito 
    2018-03-05	add Vendedor, detalle     
  -- =============================================*/ 
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	select n.Dias,
		   n.Fecha,
		   n.MontoNota,
		   n.MontoRetencion,
		   n.NroNota,
		   n.PorcentajeRetencion,
		   Vendedor = p.FullName,
		   n.Detalle
	from dbo.Nota n
	inner join dbo.Persona p
		on n.VendedorID = p.PersonaID
	where n.NotaID = @NotaID

END