create PROCEDURE [dbo].[usp_MAT_Hotel_GetById] @HotelID UNIQUEIDENTIFIER
AS
/*-- ============================================= 
  -- Author:    Garcia Sergio
  -- Create date: 2019-05-15
  -- Description:  Get by HotelID
  History
 

  -- ============================================= 
  */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
    SELECT h.HotelID, 
           h.Nombre,
		   h.Direccion,
		   h.CP,
		   h.Localidad,
		   h.Telefono,
		   h.Email,
		   h.Contacto,
		   h.CantidadHabitaciones,
		   h.Categoria,
		   h.CheckIn,
		   h.CheckOut,
		   h.GoogleMapHtml 
    FROM   dbo.Hotel h
	WHERE h.HotelID = @HotelID


END