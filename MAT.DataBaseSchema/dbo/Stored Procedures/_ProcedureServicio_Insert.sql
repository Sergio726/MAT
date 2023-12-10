
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Servicio table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureServicio_Insert
(

	@ServicioId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Servicio]
					(
					[ServicioID]
					,[Descripcion]
					,[Precio]
					,[Moneda]
					,[Iva]
					,[Alicuota]
					,[Validez]
					,[VisibilidadTarifa]
					,[ProveedorID]
					,[TransporteID]
					,[HotelID]
					)
				VALUES
					(
					@ServicioId
					,@Descripcion
					,@Precio
					,@Moneda
					,@Iva
					,@Alicuota
					,@Validez
					,@VisibilidadTarifa
					,@ProveedorId
					,@TransporteId
					,@HotelId
					)
				
									
							
			

