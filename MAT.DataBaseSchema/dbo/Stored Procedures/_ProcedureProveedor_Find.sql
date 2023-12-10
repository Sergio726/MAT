
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the Proveedor table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureProveedor_Find
(

	@SearchUsingOR bit   = null ,

	@ProveedorId uniqueidentifier   = null ,

	@RazonSocial varchar (50)  = null ,

	@Telefono varchar (50)  = null ,

	@Fax varchar (50)  = null ,

	@Web varchar (50)  = null ,

	@Email varchar (50)  = null ,

	@Idioma varchar (50)  = null ,

	@CondicionIva int   = null ,

	@Cuit varchar (50)  = null ,

	@FormaPago int   = null ,

	@LocalidadId int   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [ProveedorID]
	, [RazonSocial]
	, [Telefono]
	, [Fax]
	, [Web]
	, [Email]
	, [Idioma]
	, [CondicionIva]
	, [Cuit]
	, [FormaPago]
	, [LocalidadID]
    FROM
	[dbo].[Proveedor]
    WHERE 
	 ([ProveedorID] = @ProveedorId OR @ProveedorId IS NULL)
	AND ([RazonSocial] = @RazonSocial OR @RazonSocial IS NULL)
	AND ([Telefono] = @Telefono OR @Telefono IS NULL)
	AND ([Fax] = @Fax OR @Fax IS NULL)
	AND ([Web] = @Web OR @Web IS NULL)
	AND ([Email] = @Email OR @Email IS NULL)
	AND ([Idioma] = @Idioma OR @Idioma IS NULL)
	AND ([CondicionIva] = @CondicionIva OR @CondicionIva IS NULL)
	AND ([Cuit] = @Cuit OR @Cuit IS NULL)
	AND ([FormaPago] = @FormaPago OR @FormaPago IS NULL)
	AND ([LocalidadID] = @LocalidadId OR @LocalidadId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [ProveedorID]
	, [RazonSocial]
	, [Telefono]
	, [Fax]
	, [Web]
	, [Email]
	, [Idioma]
	, [CondicionIva]
	, [Cuit]
	, [FormaPago]
	, [LocalidadID]
    FROM
	[dbo].[Proveedor]
    WHERE 
	 ([ProveedorID] = @ProveedorId AND @ProveedorId is not null)
	OR ([RazonSocial] = @RazonSocial AND @RazonSocial is not null)
	OR ([Telefono] = @Telefono AND @Telefono is not null)
	OR ([Fax] = @Fax AND @Fax is not null)
	OR ([Web] = @Web AND @Web is not null)
	OR ([Email] = @Email AND @Email is not null)
	OR ([Idioma] = @Idioma AND @Idioma is not null)
	OR ([CondicionIva] = @CondicionIva AND @CondicionIva is not null)
	OR ([Cuit] = @Cuit AND @Cuit is not null)
	OR ([FormaPago] = @FormaPago AND @FormaPago is not null)
	OR ([LocalidadID] = @LocalidadId AND @LocalidadId is not null)
	SELECT @@ROWCOUNT			
  END
				

