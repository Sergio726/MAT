CREATE PROCEDURE [dbo].[usp_Factura_DetalleFacturaByFacturaID](@FacturaId uniqueidentifier)

AS

-- ============================================= 

-- Author:    Garcia Sergio 

-- Create date: 01/04/2017

-- Description:  MUESTRA EL DETALLE DE LA FACTURA

--

-- 04/18/2017	Garcia Sergio: add top 1
-- 05/30/2018

-- =============================================

SET nocount, xact_abort ON;

SET TRANSACTION isolation level READ uncommitted;

BEGIN 
	SELECT DISTINCT
	     P.pasajeid, 
         P.pasajeroid, 
         P.butacaid, 
         P.fechareserva, 
         P.fechacompra, 
         P.viajeid, 
         P.facturaid, 
         P.estadopasaje, 
         P.voucherid, 
         b.NroButaca as ButacaNro,
		 b.Piso as ButacaPiso,
		 b.Fila as ButacaFila,
		 b.Posicion as ButacaPosicion,
		 b.CodigoButaca as ButacaCodigoButaca,
		 v.PaqueteID,
		 isnull(per.Nombre,'') as PasajeroNombre, 
		 isnull(per.Apellido,'') as PasajeroApellido,
		 (CASE WHEN rh.HabitacionID IS NOT NULL THEN '11111111-1111-1111-1111-111111111111' ELSE NULL END) AS HabitacionID
  FROM   pasaje P 
  INNER JOIN dbo.Butaca b 
		on p.ButacaID = b.ButacaID
  INNER JOIN dbo.Viaje v
		on p.ViajeID = v.ViajeID
  LEFT JOIN dbo.Persona per
		on p.PasajeroID = per.PersonaID 
  LEFT JOIN dbo.ReservaHabitacion rh
		on rh.PasajeID = p.PasajeID
  WHERE  p.FacturaID = @FacturaId 
  GROUP BY
     P.pasajeid, 
         P.pasajeroid, 
         P.butacaid, 
         P.fechareserva, 
         P.fechacompra, 
         P.viajeid, 
         P.facturaid, 
         P.estadopasaje, 
         P.voucherid, 
         b.NroButaca,
		 b.Piso,
		 b.Fila,
		 b.Posicion,
		 b.CodigoButaca,
		 v.PaqueteID,
		 per.Nombre, 
		 per.Apellido,
		 HabitacionID
END
