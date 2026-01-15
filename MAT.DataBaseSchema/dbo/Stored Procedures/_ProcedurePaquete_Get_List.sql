
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquete_Get_List

AS


				
				SELECT
					[PaqueteID],
					[Descripcion],
					[PrecioCama],
					[Moneda],
					[Iva],
					[Alicuota],
					[Temporada],
					[Cotizacion],
					[Codigo],
					[DestinoID],
					[PrecioSemiCama]
				FROM
					[dbo].[Paquete]
					
				SELECT @@ROWCOUNT
			

