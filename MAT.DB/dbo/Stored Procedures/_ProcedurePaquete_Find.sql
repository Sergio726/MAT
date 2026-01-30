
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Paquete table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedurePaquete_Find]
(

	@SearchUsingOR bit   = null ,

	@PaqueteId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null ,

	@PrecioCama float   = null ,

	@Moneda int   = null ,

	@Iva varchar (50)  = null ,

	@Alicuota varchar (50)  = null ,

	@Temporada int   = null ,

	@Cotizacion float   = null ,

	@Codigo varchar (50)  = null ,

	@DestinoId int   = null ,

	@PrecioSemiCama float   = null ,

	@Foto varchar (200)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [PaqueteID]
	, [Descripcion]
	, [PrecioCama]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Temporada]
	, [Cotizacion]
	, [Codigo]
	, [DestinoID]
	, [PrecioSemiCama]
	, [Foto]
    FROM
	[dbo].[Paquete]
    WHERE 
	 ([PaqueteID] = @PaqueteId OR @PaqueteId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([PrecioCama] = @PrecioCama OR @PrecioCama IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Iva] = @Iva OR @Iva IS NULL)
	AND ([Alicuota] = @Alicuota OR @Alicuota IS NULL)
	AND ([Temporada] = @Temporada OR @Temporada IS NULL)
	AND ([Cotizacion] = @Cotizacion OR @Cotizacion IS NULL)
	AND ([Codigo] = @Codigo OR @Codigo IS NULL)
	AND ([DestinoID] = @DestinoId OR @DestinoId IS NULL)
	AND ([PrecioSemiCama] = @PrecioSemiCama OR @PrecioSemiCama IS NULL)
	AND ([Foto] = @Foto OR @Foto IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [PaqueteID]
	, [Descripcion]
	, [PrecioCama]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Temporada]
	, [Cotizacion]
	, [Codigo]
	, [DestinoID]
	, [PrecioSemiCama]
	, [Foto]
    FROM
	[dbo].[Paquete]
    WHERE 
	 ([PaqueteID] = @PaqueteId AND @PaqueteId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([PrecioCama] = @PrecioCama AND @PrecioCama is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Iva] = @Iva AND @Iva is not null)
	OR ([Alicuota] = @Alicuota AND @Alicuota is not null)
	OR ([Temporada] = @Temporada AND @Temporada is not null)
	OR ([Cotizacion] = @Cotizacion AND @Cotizacion is not null)
	OR ([Codigo] = @Codigo AND @Codigo is not null)
	OR ([DestinoID] = @DestinoId AND @DestinoId is not null)
	OR ([PrecioSemiCama] = @PrecioSemiCama AND @PrecioSemiCama is not null)
	OR ([Foto] = @Foto AND @Foto is not null)
	SELECT @@ROWCOUNT			
  END
				



