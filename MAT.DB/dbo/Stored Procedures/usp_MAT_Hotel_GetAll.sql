
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_GetAll]
AS
/*-- ============================================= 
  -- Author:    Garcia Sergio
  -- Create date: 2017-08-08
  -- Description:  Get all hotel
  History
  2019-05-15: Garcia Sergio, Localidad

  -- ============================================= 
  */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
    SELECT h.HotelID, 
           Nombre = Upper(Ltrim(h.Nombre)),
		   h.Direccion,
		   h.CP,
		   h.Localidad,
		   h.Telefono,
		   h.Email,
		   h.Contacto 
    FROM   dbo.Hotel h
	ORDER  BY Nombre 


END