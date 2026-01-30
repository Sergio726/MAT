
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquete_Insert]
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

	@PrecioSemiCama float   ,

	@Foto varchar (200)  
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
					,[Foto]
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
					,@Foto
					)
				
									
							
			



