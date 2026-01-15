
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureServicio_Get_List

AS


				
				SELECT
					[ServicioID],
					[Descripcion],
					[Precio],
					[Moneda],
					[Iva],
					[Alicuota],
					[Validez],
					[VisibilidadTarifa],
					[ProveedorID],
					[TransporteID],
					[HotelID]
				FROM
					[dbo].[Servicio]
					
				SELECT @@ROWCOUNT
			

