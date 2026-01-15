
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquete_Insert
(

	@PaqueteId uniqueidentifier    OUTPUT,

	@Descripcion varchar (100)  ,

	@PrecioCama float   ,

	@Moneda int   ,

	@Iva varchar (50)  ,

	@Alicuota varchar (50)  ,

	@Temporada int   ,

	@Cotizacion float   ,

	@Codigo varchar (50)  ,

	@DestinoId int   ,

	@PrecioSemiCama float   
)
AS


				
				INSERT INTO [dbo].[Paquete]
					(
					[PaqueteID]
					,[Descripcion]
					,[PrecioCama]
					,[Moneda]
					,[Iva]
					,[Alicuota]
					,[Temporada]
					,[Cotizacion]
					,[Codigo]
					,[DestinoID]
					,[PrecioSemiCama]
					)
				VALUES
					(
					@PaqueteId
					,@Descripcion
					,@PrecioCama
					,@Moneda
					,@Iva
					,@Alicuota
					,@Temporada
					,@Cotizacion
					,@Codigo
					,@DestinoId
					,@PrecioSemiCama
					)
				
									
							
			

