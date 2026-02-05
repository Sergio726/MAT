ALTER PROCEDURE [dbo].[usp_MAT_Voucher_GetVoucherByFacturaID](@FacturaID	uniqueidentifier)
AS 
 /* -- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 10/04/2017
  -- Description:  New room reservation
  History
  2017-06-18	Garcia Sergio  add TipoTransporte
  2017-08-21	Garcia Sergio add VouvherNrPrint
  2018-11-05	Garcia Sergio add PaqueteExcusionesOpcionales
  2019-04-17	Garcia Sergio add TiempoConsentracion
  2019-05-05	Garcia Sergio add Observaciones
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 


		SELECT pj.PasajeroID                             AS PasajePasajeroID,
			   pj.PasajeID                               AS PasajePasajeID,
			   pj.ViajeID                                AS ViajeID,
			   v.NroVoucher                              AS VoucherNroVoucher, 
			   v.NrPrint								 AS VouvherNrPrint,
			   CONVERT(VARCHAR(10), v.FechaEmision, 103) AS VoucherFechaEmision, 
			   b.NroButaca                               AS ButacaNroButaca, 
			   b.Tipo                                    AS ButacaTipo, 
			   p.Apellido                                AS PersonaApellido, 
			   p.Nombre                                  AS PersonaNombre, 
			   p.NroDocumento                            AS PersonaNroDocumento, 
			   lPer.Nombre                               AS PersonaLocalidad, 
			   p.Email                                   AS PersonaEmail, 
			   p.Telefono                                AS PersonaTelefono, 
			   p.Celular                                 AS PersonaCelular, 
			   p.Domicilio                               AS PersonaDomicilio, 
			   l.Nombre                                  AS DestinoNombre, 
			   vi.HoraSalida                             AS ViajeHoraSalida, 
			   vi.HoraRegreso                            AS ViajeHoraRegreso, 
			   isnull(vi.TiempoConsentracion, 30)		 AS TiempoConsentracion,
			   vi.Observaciones,
			   CONVERT(VARCHAR(10),vi.FechaSalida,103)   AS ViajeFechaSalida, 
			   CONVERT(VARCHAR(10),vi.FechaRegreso,103)  AS ViajeFechaRegreso, 
			   vi.Medio AS ViajeMedio,
			   Stuff((SELECT ', ' + s.Descripcion 
					  FROM   dbo.PaqueteServicio ps 
							 INNER JOIN dbo.Servicio s 
									 ON ps.ServicioID = s.ServicioID 
					  WHERE  ps.PaqueteID = vi.PaqueteID
					  FOR xml path('')), 1, 1, '')       PaqueteServicios, 
			   Stuff((SELECT ', ' + ex.Descripcion
					  FROM   dbo.PaqueteExcursion pe 
							 INNER JOIN dbo.Excursion ex 
									 ON pe.ExcursionID = ex.ExcursionID
					  WHERE  pe.PaqueteID = vi.PaqueteID
					  AND pe.IsOpcional = 0
					  FOR xml path('')), 1, 1, '')       PaqueteExcusiones,
				Stuff((SELECT ', ' + ex.Descripcion
					  FROM   dbo.PaqueteExcursion pe 
							 INNER JOIN dbo.Excursion ex 
									 ON pe.ExcursionID = ex.ExcursionID
					  WHERE  pe.PaqueteID = vi.PaqueteID
					  AND pe.IsOpcional = 1
					  FOR xml path('')), 1, 1, '')       PaqueteExcusionesOpcionales  
		FROM   dbo.Pasaje pj 
			   LEFT JOIN dbo.Voucher v 
					  ON pj.VoucherID = v.VoucherID
			   INNER JOIN dbo.Butaca b 
					   ON pj.ButacaID = b.ButacaID
			   INNER JOIN dbo.Persona p 
					   ON pj.PasajeroID = p.PersonaID
			   INNER JOIN dbo.Viaje vi 
					   ON pj.ViajeID = vi.ViajeID
			   INNER JOIN dbo.Paquete pa 
					   ON vi.PaqueteID = pa.PaqueteID
			   LEFT JOIN dbo.Localidad l 
					  ON pa.DestinoID = l.id 
			   LEFT JOIN dbo.Localidad lPer 
					  ON p.LocalidadID = lPer.id 
		WHERE  pj.facturaid = @FacturaID 
		
		
		BEGIN TRAN
			UPDATE v
			SET v.NrPrint = ISNULL(v.NrPrint,0) + 1
			FROM dbo.Pasaje pj 
			   LEFT JOIN dbo.Voucher v 
					  ON pj.VoucherID = v.VoucherID
			WHERE  pj.FacturaID = @FacturaID  

		COMMIT 

END