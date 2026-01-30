CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetReservasVencidas]  (@ViajeID VARCHAR(36))
AS 
  /*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 19-05-2017
  -- Description: SHOW reservas vencidas 
	 History:

	 05-31-2017		Garcia Sergio change clienteID by pasajeroID
	 11-14-2017		Gracia Sergio update estadofactura ,  pre-reserva
  -- ============================================= 
  */
  BEGIN 
      SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 

		SELECT f.FacturaID, 
			   f.ClienteID, 
			   Upper(per.Apellido) + ', ' + Upper(per.Nombre)                    AS FullName, 
			   CONVERT(VARCHAR(10), f.Fecha, 103)                                AS FechaPreReserva, 
			   CONVERT(VARCHAR(10), Dateadd(dd, f.DiasPreReserva, f.Fecha), 103) AS VencimientoPreReserva,
			   b.NroButaca
		FROM   dbo.Factura f 
			   INNER JOIN dbo.Pasaje p 
					   ON f.FacturaID = p.FacturaID 
			   INNER JOIN dbo.Persona per 
					   ON p.PasajeroID = per.PersonaID 
			   INNER JOIN dbo.Butaca b
					   ON p.ButacaID = b.ButacaID
		WHERE  p.ViajeID = @ViajeID
			   AND f.Estado = 2 --Pre-Reserva
			   AND Dateadd(dd, f.DiasPreReserva, f.Fecha) < GETDATE()
		ORDER BY b.NroButaca ASC
  END 
  

