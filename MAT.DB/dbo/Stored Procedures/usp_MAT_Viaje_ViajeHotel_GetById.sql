
CREATE PROCEDURE [dbo].[usp_MAT_Viaje_ViajeHotel_GetById] (@ViajeHotelID UNIQUEIDENTIFIER) 
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 2019-05-17 
  -- Description:  get viajehotel by Id

  History:
	
  -- ===========================================*/
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

      SELECT vh.ViajeHotelID,
             vh.ViajeID,
             vh.HotelID,
			 vh.Comentario,
			 Desde =		isnull(convert(varchar(10), vh.Desde, 101),''),
			 HoraIngreso =	isnull(vh.HoraIngreso,''),
			 Hasta =		isnull(convert(varchar(10),vh.Hasta,101),''),
			 HoraSalida =	isnull(vh.HoraSalida, ''),
			 ViajeNombre = v.Descripcion,
			 HotelNombre = h.Nombre
	  FROM dbo.ViajeHotel vh
	  INNER JOIN dbo.Viaje v
		on vh.ViajeID = v.ViajeID
	  INNER JOIN dbo.Hotel h
		on vh.HotelID = h.HotelID
	  WHERE vh.ViajeHotelID = @ViajeHotelID
	  
  END