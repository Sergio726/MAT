CREATE PROCEDURE usp_MAT_Reserva_GetPasajeByViajeID( @ViajeID uniqueidentifier)
AS 
/*-- ============================================= 
-- Author:    Garcia Sergio 
-- Create date: 28/03/2017 
-- Description:  trae los pasajes segun viajeID 
   2018-04-29	 Garcia Sergio: add MonedaTipo
   2021-08-10    Garcia Sergio: add TransporteTipo
   2024-10-18    Ruben Tejerina SP is called by API BACKEND
   2024-11-15	 Ruben Tejerina Get ButacaTipo

-- =============================================*/
SET nocount, xact_abort ON;
SET TRANSACTION isolation level READ uncommitted;

BEGIN 
  DECLARE @Today DATE = GETDATE()
  SELECT P.pasajeid, 
         P.pasajeroid, 
         P.butacaid, 
         P.fechareserva, 
         P.fechacompra, 
         P.viajeid, 
         P.facturaid, 
         P.estadopasaje, 
         P.voucherid, 
         --P.precioid,
		 b.NroButaca as ButacaNro,
		 b.Piso as ButacaPiso,
		 b.Fila as ButacaFila,
		 b.Posicion as ButacaPosicion,
		 b.CodigoButaca as ButacaCodigoButaca,
		 b.Tipo as ButacaTipo,
		 t.TransporteID,
		 t.NroCoche as TransporteNroCoche,
		 v.PaqueteID,
		 per.Nombre as PasajeroNombre, 
		 per.Apellido as PasajeroApellido,
		 MonedaTipo = pq.Moneda,
		 TransporteTipo = t.Tipo,
		 v.FechaPromocion,
		 v.PrecioCama,
		 v.PrecioSemicama,
		 PrecioCalculado = case when @Today <= v.FechaPromocion 
			then v.PrecioPromocional
			else
				case b.Piso 
					when 2 then v.PrecioCama
					when 1 then v.PrecioSemicama
				end
			end 
  FROM   pasaje P 
  INNER JOIN dbo.Butaca b 
		on p.ButacaID = b.ButacaID
  INNER JOIN dbo.Transporte t
		on b.TransporteID = t.TransporteID
  INNER JOIN dbo.Viaje v
		on p.ViajeID = v.ViajeID
  cross apply (select top 1 Moneda from dbo.Paquete where PaqueteID = v.PaqueteID) pq
  LEFT JOIN dbo.Persona per
		on p.PasajeroID = per.PersonaID
  WHERE  p.viajeid = @ViajeID
    
END
