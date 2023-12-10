
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Vendedor table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureVendedor_Find
(

	@SearchUsingOR bit   = null ,

	@VendedorId uniqueidentifier   = null ,

	@Descripcion varchar (100)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [VendedorID]
	, [Descripcion]
    FROM
	[dbo].[Vendedor]
    WHERE 
	 ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([Descripcion] = @Descripcion OR @Descripcion IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [VendedorID]
	, [Descripcion]
    FROM
	[dbo].[Vendedor]
    WHERE 
	 ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([Descripcion] = @Descripcion AND @Descripcion is not null)
	SELECT @@ROWCOUNT			
  END
				

