
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Nota table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureNota_Find
(

	@SearchUsingOR bit   = null ,

	@NotaId uniqueidentifier   = null ,

	@PorcentajeRetencion float   = null ,

	@MontoRetencion float   = null ,

	@Fecha date   = null ,

	@Dias int   = null ,

	@ClienteId uniqueidentifier   = null ,

	@VendedorId uniqueidentifier   = null ,

	@NroNota varchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [NotaID]
	, [PorcentajeRetencion]
	, [MontoRetencion]
	, [Fecha]
	, [Dias]
	, [ClienteID]
	, [VendedorID]
	, [NroNota]
    FROM
	[dbo].[Nota]
    WHERE 
	 ([NotaID] = @NotaId OR @NotaId IS NULL)
	AND ([PorcentajeRetencion] = @PorcentajeRetencion OR @PorcentajeRetencion IS NULL)
	AND ([MontoRetencion] = @MontoRetencion OR @MontoRetencion IS NULL)
	AND ([Fecha] = @Fecha OR @Fecha IS NULL)
	AND ([Dias] = @Dias OR @Dias IS NULL)
	AND ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([NroNota] = @NroNota OR @NroNota IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [NotaID]
	, [PorcentajeRetencion]
	, [MontoRetencion]
	, [Fecha]
	, [Dias]
	, [ClienteID]
	, [VendedorID]
	, [NroNota]
    FROM
	[dbo].[Nota]
    WHERE 
	 ([NotaID] = @NotaId AND @NotaId is not null)
	OR ([PorcentajeRetencion] = @PorcentajeRetencion AND @PorcentajeRetencion is not null)
	OR ([MontoRetencion] = @MontoRetencion AND @MontoRetencion is not null)
	OR ([Fecha] = @Fecha AND @Fecha is not null)
	OR ([Dias] = @Dias AND @Dias is not null)
	OR ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([NroNota] = @NroNota AND @NroNota is not null)
	SELECT @@ROWCOUNT			
  END