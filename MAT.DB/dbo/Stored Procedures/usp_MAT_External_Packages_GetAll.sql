CREATE PROCEDURE dbo.usp_MAT_External_Packages_GetAll(@Date1 DATETIME = '',
													 @Date2 DATETIME = '',
						 							 @Price1 FLOAT = -1, --ALL PRICE
													 @Price2 FLOAT = -1 --ALL PRICE
													 )
						
AS
  /*-- ============================================= 
  -- Author:    Garcia Sergio
  -- Create date: 14/04/2017
  -- Description:  Get all packages by date and price
  History

  2017-03-06	Garcia Sergio	add Excusiones
  2017-06-11	Garcia Sergio	add filter by ModePublicity
  2018-03-10	Garcia Sergio	add MonedaTipoId
  2018-11-30	Garcia Sergio	add PaqueteExcusionesOpcionales
  2020-06-2020	Garcia Sergio	add HotelesID and PaqueteModePublicity
  2025-03-29	Garcia Sergio   remove field ModePublicity and PublicWeb
  -- ============================================= 
  */
BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


	SELECT p.paqueteid, 
		   UPPER(ISNULL(p.Descripcion,''))     AS PaqueteNombre, 
		   Stuff((SELECT ', ' + UPPER(s.descripcion) 
				  FROM   dbo.Servicio s 
						 INNER JOIN dbo.PaqueteServicio ps 
								 ON s.ServicioID = ps.servicioid 
				  WHERE  ps.paqueteid = p.paqueteid 
						 AND s.descripcion NOT LIKE '%COCHE%' 
				  FOR xml path('')), 1, 1, '') AS PaqueteServicios,
		   Stuff((SELECT ', ' + UPPER(e.Descripcion) 
					FROM PaqueteExcursion pe
					INNER JOIN Excursion e
						ON pe.ExcursionID = e.ExcursionID
						AND pe.IsOpcional = 0
					WHERE pe.PaqueteID = p.PaqueteID 
					FOR xml path('')), 1, 1, '') AS PaqueteExcusionesIncluidas, 
		   Stuff((SELECT ', ' + UPPER(e.Descripcion) 
					FROM PaqueteExcursion pe
					INNER JOIN Excursion e
						ON pe.ExcursionID = e.ExcursionID
						AND pe.IsOpcional = 1
					WHERE pe.PaqueteID = p.PaqueteID 
					FOR xml path('')), 1, 1, '') AS PaqueteExcusionesOpcionales,
		   UPPER(l.nombre)                     AS PaqueteDestino, 
		   ISNULL(v.fechasalida,'')                       AS ViajeFechaSalida, 
		   ISNULL(v.horasalida ,'')                       AS ViajeHoraSalida, 
		   ISNULL(v.fecharegreso ,'')                   AS ViajeFechaRegreso, 
		   ISNULL(v.horaregreso,'')                       AS ViajeHoraRegreso, 
		   UPPER(ISNULL(v.medio,''))           AS ViajeMedio, 
		   UPPER(ISNULL(v.descripcion,''))     AS ViajeDescripcion, 
		   ISNULL(v.preciocama ,0)             AS ViajePrecioCama, 
		   ISNULL(v.preciosemicama,0)          AS ViajePrecioSemiCama, 
		   ISNULL(v.PrecioPromocional,0)	   AS ViajePrecioPromocional,
		   v.FechaPromocion	                   AS ViajeVencimientoPromocion,
		   ISNULL(v.nDias ,'')				   AS ViajeNroDias,
		   ISNULL(v.nNoches ,'')			   AS ViajeNroNoches,
		   Stuff((SELECT ', ' + UPPER(ISNULL(h.Nombre,''))
				  FROM   dbo.hotel h 
						 INNER JOIN dbo.ViajeHotel vh 
								 ON h.HotelID = vh.HotelID 
				  WHERE  vh.viajeid = v.viajeid 
				  FOR xml path('')), 1, 1, '') AS PaqueteHoteles,
			Stuff((SELECT ', ' + UPPER(ISNULL(h.HotelID,''))
				  FROM   dbo.hotel h 
						 INNER JOIN dbo.ViajeHotel vh 
								 ON h.HotelID = vh.HotelID 
				  WHERE  vh.viajeid = v.viajeid 
				  FOR xml path('')), 1, 1, '') AS PaqueteHotelesId,
		   p.LastUpdate AS PaqueteLastUpdate,
		   MonedaTipoId = p.Moneda
	FROM   dbo.Paquete p 
		   LEFT JOIN dbo.Localidad l 
				   ON p.DestinoID = l.ID 
		   LEFT JOIN dbo.Viaje v 
				   ON p.PaqueteID = v.paqueteid
	WHERE (  YEAR(v.FechaSalida) >= YEAR(GETDATE())
			  AND
			  (((v.FechaSalida  BETWEEN @Date1 AND @Date2) AND @Date2 != '')
				OR
				(@Date2 = '')
			  )
			  AND
			  ((v.PrecioSemiCama >= @Price1 AND v.PrecioSemiCama <= @Price2)
				OR
				(@Price1 = -1 AND @Price2 = -1)
			  )
			  
		  )
		  
		  
	GROUP  BY p.PaqueteID, 
			  p.Descripcion, 
			  v.PrecioCama, 
			  v.PrecioSemicama,
			  v.PrecioPromocional, 
			  v.FechaPromocion,
			  v.Descripcion,
			  l.Nombre, 
			  v.FechaSalida, 
			  v.HoraSalida, 
			  v.FechaRegreso, 
			  v.HoraRegreso, 
			  v.Medio, 
			  v.Descripcion, 
			  v.ViajeID,
			  v.nDias,
			  v.nNoches,
			  p.LastUpdate,
			  p.Moneda

	ORDER BY v.fechasalida
END
