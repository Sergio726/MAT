CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetDetalleViaje]  (@ViajeID UNIQUEIDENTIFIER)
AS 
   /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 19-05-2017
  -- Description: SHOW reservas vencidas 
	 History:

	 05-31-2017		Garcia Sergio change clienteID by pasajeroID
	 11-14-2017		Garcia Sergio update estadofactura ,  pre-reserva
	 04-17-2019		Garcia Sergio add TiempoConsentracion
	 05-04-2019		Garcia Sergio add Observaciones
	 10-18-2024		Ruben Tejerina SP is called by API BACKEND
	 10-18-2024		Ruben Tejerina add PaqueteId and ViajeId
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
	  		
		  select 
		  v.Descripcion,
		  Destino = l.Nombre,
		  v.FechaSalida,
		  v.FechaRegreso,
		  v.HoraSalida,
		  v.HoraRegreso,
		  TiempoConsentracion = ISNULL(v.TiempoConsentracion,30),
		  t.NroCoche,
		  PaqueteServicios = Stuff((SELECT ', ' + UPPER(s.descripcion) 
					  FROM   dbo.Servicio s 
							 INNER JOIN dbo.PaqueteServicio ps 
									 ON s.ServicioID = ps.servicioid 
					  WHERE  ps.paqueteid = p.paqueteid 
							 AND s.descripcion NOT LIKE '%COCHE%' 
					  FOR xml path('')), 1, 1, ''),
		  PaqueteExcusionesIncluidas = Stuff((SELECT ', ' + UPPER(e.Descripcion) 
						FROM PaqueteExcursion pe
						INNER JOIN Excursion e
							ON pe.ExcursionID = e.ExcursionID
							AND pe.IsOpcional = 0
						WHERE pe.PaqueteID = p.PaqueteID 
						FOR xml path('')), 1, 1, ''), 
		  PaqueteExcusionesOpcionales = Stuff((SELECT ', ' + UPPER(e.Descripcion) 
						FROM PaqueteExcursion pe
						INNER JOIN Excursion e
							ON pe.ExcursionID = e.ExcursionID
							AND pe.IsOpcional = 1
						WHERE pe.PaqueteID = p.PaqueteID 
						FOR xml path('')), 1, 1, ''),
			v.Observaciones,
			v.PaqueteID,
			v.ViajeID
			from dbo.Viaje v
			inner join dbo.Paquete p
				on v.PaqueteID = p.PaqueteID
			left join dbo.Localidad l
				on p.DestinoID = l.ID
			left join dbo.Transporte t
				on v.BusID = t.TransporteID
			where ViajeID = @ViajeID

  END
