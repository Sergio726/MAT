
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Servicio table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureServicio_GetByServicioId]
(

	@ServicioId uniqueidentifier   
)
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
				WHERE
					[ServicioID] = @ServicioId
				SELECT @@ROWCOUNT
					
			



