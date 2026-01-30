
CREATE PROCEDURE [dbo].[usp_MAT_Reserva_DistribucionCoche_GetByViajeID](@ViajeID varchar(36))
AS 
  /* ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 20/04/2017
  -- Description:  Get all Passages

  --
  --20/04/2017	Garcia Sergio: Create
    12/09/2019	Garcia Sergio: add NroCoche
	11/08/2021	Garcia Sergio: add TransporteTipo
  -- ============================================*/
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	SELECT b.NroButaca    AS ButacaNro, 
		   b.Posicion     AS ButacaPosicion, 
		   b.CodigoButaca AS ButacaCodigo, 
		   p.PasajeroID   AS PasajeroID, 
		   per.Apellido   AS PasajeroApellido, 
		   per.Nombre     AS PasajeroNombre 
	FROM   dbo.Pasaje p 
		   INNER JOIN dbo.Butaca b 
				   ON p.ButacaID = b.ButacaID 
		   LEFT JOIN dbo.Persona per 
				  ON p.PasajeroID = per.PersonaID 
	WHERE  p.ViajeID = @ViajeID
	ORDER  BY NroButaca

	select NroCoche = t.NroCoche,
		   TransporteTipo = t.Tipo
	from dbo.Viaje v
	inner join dbo.Transporte t
		on v.BusID = t.TransporteID
	where v.ViajeID = @ViajeID

END