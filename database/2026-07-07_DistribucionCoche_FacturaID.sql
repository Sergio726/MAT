-- DistribucionCoche: FacturaID en SP para acciones al clic en butaca ocupada.

IF OBJECT_ID(N'dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID AS BEGIN SET NOCOUNT ON; END');
GO

ALTER PROCEDURE [dbo].[usp_MAT_Reserva_DistribucionCoche_GetByViajeID](@ViajeID varchar(36))
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-06-16
  -- Updated:   2026-07-07 — FacturaID para acciones en mapa
  -- Description: Distribución de coche por viaje: butacas, pasajeros,
  --              PasajeID, FacturaID y EstadoPasaje.
  ============================================= */
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	SELECT b.NroButaca    AS ButacaNro, 
		   b.Posicion     AS ButacaPosicion, 
		   b.CodigoButaca AS ButacaCodigo, 
		   p.PasajeID     AS PasajeID,
		   p.FacturaID    AS FacturaID,
		   p.EstadoPasaje AS EstadoPasaje,
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
GO
