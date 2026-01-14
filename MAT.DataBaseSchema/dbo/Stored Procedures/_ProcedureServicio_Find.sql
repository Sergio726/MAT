
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Servicio table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureServicio_Find
(

	@SearchUsingOR bit   = null ,

	@ServicioId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null ,

	@Precio float   = null ,

	@Moneda varchar (50)  = null ,

	@Iva varchar (50)  = null ,

	@Alicuota float   = null ,

	@Validez date   = null ,

	@VisibilidadTarifa int   = null ,

	@ProveedorId uniqueidentifier   = null ,

	@TransporteId uniqueidentifier   = null ,

	@HotelId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ServicioID]
	, [Descripcion]
	, [Precio]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Validez]
	, [VisibilidadTarifa]
	, [ProveedorID]
	, [TransporteID]
	, [HotelID]
    FROM
	[dbo].[Servicio]
    WHERE 
	 ([ServicioID] = @ServicioId OR @ServicioId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
	AND ([Precio] = @Precio OR @Precio IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Iva] = @Iva OR @Iva IS NULL)
	AND ([Alicuota] = @Alicuota OR @Alicuota IS NULL)
	AND ([Validez] = @Validez OR @Validez IS NULL)
	AND ([VisibilidadTarifa] = @VisibilidadTarifa OR @VisibilidadTarifa IS NULL)
	AND ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
	AND ([TransporteID] = @TransporteId OR @TransporteId IS NULL)
	AND ([HotelID] = @HotelId OR @HotelId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ServicioID]
	, [Descripcion]
	, [Precio]
	, [Moneda]
	, [Iva]
	, [Alicuota]
	, [Validez]
	, [VisibilidadTarifa]
	, [ProveedorID]
	, [TransporteID]
	, [HotelID]
    FROM
	[dbo].[Servicio]
    WHERE 
	 ([ServicioID] = @ServicioId AND @ServicioId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	OR ([Precio] = @Precio AND @Precio is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Iva] = @Iva AND @Iva is not null)
	OR ([Alicuota] = @Alicuota AND @Alicuota is not null)
	OR ([Validez] = @Validez AND @Validez is not null)
	OR ([VisibilidadTarifa] = @VisibilidadTarifa AND @VisibilidadTarifa is not null)
	OR ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	OR ([TransporteID] = @TransporteId AND @TransporteId is not null)
	OR ([HotelID] = @HotelId AND @HotelId is not null)
	SELECT @@ROWCOUNT			
  END
				

