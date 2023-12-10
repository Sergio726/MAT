
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Paquete table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePaquete_Update
(

	@PaqueteId uniqueidentifier   ,

	@OriginalPaqueteId uniqueidentifier   ,

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


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Paquete]
				SET
					[PaqueteID] = @PaqueteId
					,[Descripcion] = @Descripcion
					,[PrecioCama] = @PrecioCama
					,[Moneda] = @Moneda
					,[Iva] = @Iva
					,[Alicuota] = @Alicuota
					,[Temporada] = @Temporada
					,[Cotizacion] = @Cotizacion
					,[Codigo] = @Codigo
					,[DestinoID] = @DestinoId
					,[PrecioSemiCama] = @PrecioSemiCama
				WHERE
[PaqueteID] = @OriginalPaqueteId 
				
			

