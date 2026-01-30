
CREATE PROCEDURE [dbo].[usp_MAT_Viaje_ViajeHotel_GetByViajeID] (@ViajeID varchar(max)) 
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 07/12/2016 
  -- Description:  Trae la lista de hoteles vinculados a un viaje 

  History:
	2019-04-18	Garcia Sergio	add ViajeNombre
	2019-05-16  Garcia Sergio   add vh.Comentario
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
             h.Nombre,
			 ViajeNombre = v.Descripcion
	  FROM dbo.ViajeHotel vh
	  INNER JOIN Hotel h ON h.HotelID = vh.HotelID
	  INNER JOIN DBO.Viaje v on vh.ViajeID = v.ViajeID
	  WHERE vh.ViajeID = CONVERT(uniqueidentifier, @ViajeID)
  END