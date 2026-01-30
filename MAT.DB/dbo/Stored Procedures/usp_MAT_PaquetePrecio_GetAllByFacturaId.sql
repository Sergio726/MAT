
CREATE PROCEDURE [dbo].[usp_MAT_PaquetePrecio_GetAllByFacturaId] (@FacturaID uniqueidentifier)
AS
/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2018-05-30
  -- Description:  get all prices by PrecioId
  
  ================================================
-- */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted;
	   
	     declare @PaqueteId uniqueidentifier

		select top 1 @PaqueteId = pq.PaqueteID
		from dbo.Pasaje p
		inner join dbo.Viaje v
			on p.ViajeID = v.ViajeID
		inner join dbo.Paquete pq
			on v.PaqueteID = pq.PaqueteID
		where p.FacturaID = @FacturaID

		select p.PrecioID,
			   p.Monto,
			   p.Descripcion
		from PaquetePrecio pp
		inner join Precio p
			on pp.PrecioID = p.PrecioID
		where PaqueteID = @PaqueteId 
END

