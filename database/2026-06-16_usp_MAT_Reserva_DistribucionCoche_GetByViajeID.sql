-- Migración: DistribucionCoche Fase 2 — PasajeID y EstadoPasaje
-- Paridad con MAT.DB/dbo/Stored Procedures/usp_MAT_Reserva_DistribucionCoche_GetByViajeID.sql
-- Ejecutar en el entorno destino antes de desplegar la app con Fase 2.

IF OBJECT_ID(N'dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Reserva_DistribucionCoche_GetByViajeID;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Reserva_DistribucionCoche_GetByViajeID](@ViajeID varchar(36))
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-06-16
  -- Description: Distribución de coche por viaje: butacas, pasajeros,
  --              PasajeID y EstadoPasaje para colores alineados con Reserva/Index.
  --              Segundo resultset: NroCoche y TransporteTipo.
  ============================================= */
BEGIN 
    SET nocount, xact_abort ON; 
    SET TRANSACTION isolation level READ uncommitted; 

	SELECT b.NroButaca    AS ButacaNro, 
		   b.Posicion     AS ButacaPosicion, 
		   b.CodigoButaca AS ButacaCodigo, 
		   p.PasajeID     AS PasajeID,
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
