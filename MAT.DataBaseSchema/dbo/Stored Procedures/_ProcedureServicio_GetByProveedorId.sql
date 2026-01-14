
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Servicio table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureServicio_GetByProveedorId
(

	@ProveedorId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[ProveedorID] = @ProveedorId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

