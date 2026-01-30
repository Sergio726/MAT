
CREATE PROCEDURE [dbo].[usp_MAT_Viajes_GetListHotel] @ViajeID UNIQUEIDENTIFIER
AS
	/*-- =============================================   
  -- Author:    Garcia Sergio   
  -- Create date: 11-06-2018   
  -- Description:  get hoteles by ViajeID
      
	 History:
	 2019-05-16		Garcia Sergio: select only hotel
  -- =============================================*/ 
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

	  select h.Nombre, h.HotelID
	  from dbo.Hotel h
	 
  END