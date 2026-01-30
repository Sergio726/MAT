
CREATE PROCEDURE [dbo].[usp_MAT_Hotel_DropDown]
AS 
    SELECT HotelID, 
           Nombre = Upper(Ltrim(Nombre)) 
    FROM   dbo.Hotel 
    ORDER  BY Nombre 


