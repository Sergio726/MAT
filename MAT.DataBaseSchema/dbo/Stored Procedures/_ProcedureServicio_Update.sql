
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureServicio_Update
(

	@ServicioId uniqueidentifier   ,

	@OriginalServicioId uniqueidentifier   ,

	@Descripcion varchar (100)  ,

	@Precio float   ,

	@Moneda varchar (50)  ,

	@Iva varchar (50)  ,

	@Alicuota float   ,

	@Validez date   ,

	@VisibilidadTarifa int   ,

	@ProveedorId uniqueidentifier   ,

	@TransporteId uniqueidentifier   ,

	@HotelId uniqueidentifier   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Servicio]
				SET
					[ServicioID] = @ServicioId
					,[Descripcion] = @Descripcion
					,[Precio] = @Precio
					,[Moneda] = @Moneda
					,[Iva] = @Iva
					,[Alicuota] = @Alicuota
					,[Validez] = @Validez
					,[VisibilidadTarifa] = @VisibilidadTarifa
					,[ProveedorID] = @ProveedorId
					,[TransporteID] = @TransporteId
					,[HotelID] = @HotelId
				WHERE
[ServicioID] = @OriginalServicioId 
				
			

