

CREATE PROCEDURE [dbo].[usp_MAT_Hotel_GetByViajeID] (@ViajeID uniqueidentifier)
AS
/*-- ============================================= 
  -- Author:    Garcia Sergio
  -- Create date: 2019-06-17
  -- Description:  Get by ViajeID
  History
 

  -- ============================================= 
  */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
    
		select Nombre = upper(h.Nombre),
			   Fecha = vh.Desde,
			   h.HotelID
		from dbo.ViajeHotel vh
		inner join dbo.Hotel h
			on vh.HotelID = h.HotelID
		where vh.ViajeID = @ViajeID
		group by h.HotelID, h.Nombre, vh.Desde


END