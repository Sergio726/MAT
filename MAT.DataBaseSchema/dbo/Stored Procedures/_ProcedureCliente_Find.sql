
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Finds records in the Cliente table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCliente_Find
(

	@SearchUsingOR bit   = null ,

	@ClienteId uniqueidentifier   = null ,

	@RazonSocial varchar (50)  = null ,

	@Cuit varchar (50)  = null ,

	@Moneda varchar (50)  = null ,

	@Empresa varchar (50)  = null ,

	@Ocupacion varchar (50)  = null ,

	@FormaPago int   = null ,

	@CondicionIva int   = null ,

	@VendedorId uniqueidentifier   = null ,

	@Fax varchar (50)  = null ,

	@Web varchar (50)  = null ,

	@Idioma varchar (50)  = null ,

	@Promotor varchar (50)  = null ,

	@Observacion varchar (250)  = null ,

	@TipoId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ClienteID]
	, [RazonSocial]
	, [Cuit]
	, [Moneda]
	, [Empresa]
	, [Ocupacion]
	, [FormaPago]
	, [CondicionIva]
	, [VendedorID]
	, [Fax]
	, [Web]
	, [Idioma]
	, [Promotor]
	, [Observacion]
	, [TipoID]
    FROM
	[dbo].[Cliente]
    WHERE 
	 ([ClienteID] = @ClienteId OR @ClienteId IS NULL)
	AND ([RazonSocial] = @RazonSocial OR @RazonSocial IS NULL)
	AND ([Cuit] = @Cuit OR @Cuit IS NULL)
	AND ([Moneda] = @Moneda OR @Moneda IS NULL)
	AND ([Empresa] = @Empresa OR @Empresa IS NULL)
	AND ([Ocupacion] = @Ocupacion OR @Ocupacion IS NULL)
	AND ([FormaPago] = @FormaPago OR @FormaPago IS NULL)
	AND ([CondicionIva] = @CondicionIva OR @CondicionIva IS NULL)
	AND ([VendedorID] = @VendedorId OR @VendedorId IS NULL)
	AND ([Fax] = @Fax OR @Fax IS NULL)
	AND ([Web] = @Web OR @Web IS NULL)
	AND ([Idioma] = @Idioma OR @Idioma IS NULL)
	AND ([Promotor] = @Promotor OR @Promotor IS NULL)
	AND ([Observacion] = @Observacion OR @Observacion IS NULL)
	AND ([TipoID] = @TipoId OR @TipoId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ClienteID]
	, [RazonSocial]
	, [Cuit]
	, [Moneda]
	, [Empresa]
	, [Ocupacion]
	, [FormaPago]
	, [CondicionIva]
	, [VendedorID]
	, [Fax]
	, [Web]
	, [Idioma]
	, [Promotor]
	, [Observacion]
	, [TipoID]
    FROM
	[dbo].[Cliente]
    WHERE 
	 ([ClienteID] = @ClienteId AND @ClienteId is not null)
	OR ([RazonSocial] = @RazonSocial AND @RazonSocial is not null)
	OR ([Cuit] = @Cuit AND @Cuit is not null)
	OR ([Moneda] = @Moneda AND @Moneda is not null)
	OR ([Empresa] = @Empresa AND @Empresa is not null)
	OR ([Ocupacion] = @Ocupacion AND @Ocupacion is not null)
	OR ([FormaPago] = @FormaPago AND @FormaPago is not null)
	OR ([CondicionIva] = @CondicionIva AND @CondicionIva is not null)
	OR ([VendedorID] = @VendedorId AND @VendedorId is not null)
	OR ([Fax] = @Fax AND @Fax is not null)
	OR ([Web] = @Web AND @Web is not null)
	OR ([Idioma] = @Idioma AND @Idioma is not null)
	OR ([Promotor] = @Promotor AND @Promotor is not null)
	OR ([Observacion] = @Observacion AND @Observacion is not null)
	OR ([TipoID] = @TipoId AND @TipoId is not null)
	SELECT @@ROWCOUNT			
  END
				

