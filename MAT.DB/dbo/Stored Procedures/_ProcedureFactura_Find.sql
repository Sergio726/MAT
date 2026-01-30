
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Factura table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureFactura_Find]
(

	@SearchUsingOR bit   = null ,

	@FacturaId uniqueidentifier   = null ,

	@NroFactura varchar (50)  = null ,

	@Monto float   = null ,

	@Fecha datetime   = null ,

	@Tipo int   = null ,

	@Estado int   = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@DescuentoAplicado float   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [FacturaID]
	, [NroFactura]
	, [Monto]
	, [Fecha]
	, [Tipo]
	, [Estado]
	, [ClienteID]
	, [VendedorID]
	FROM
	[dbo].[Factura]
    WHERE 
	 ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([NroFactura] = @NroFactura OR @NroFactura IS NULL)
	AND ([Monto] = @Monto OR @Monto IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Tipo] = @Tipo OR @Tipo IS NULL)
	AND ([Estado] = @Estado OR @Estado IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	
						
  END
  ELSE
  BEGIN
    SELECT
	  [FacturaID]
	, [NroFactura]
	, [Monto]
	, [Fecha]
	, [Tipo]
	, [Estado]
	, [ClienteID]
	, [VendedorID]
	FROM
	[dbo].[Factura]
    WHERE 
	 ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([NroFactura] = @NroFactura AND @NroFactura is not null)
	OR ([Monto] = @Monto AND @Monto is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Tipo] = @Tipo AND @Tipo is not null)
	OR ([Estado] = @Estado AND @Estado is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	SELECT @@ROWCOUNT			
  END

