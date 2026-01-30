
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProveedor_Update]
(

	@ProveedorId uniqueidentifier   ,

	@OriginalProveedorId uniqueidentifier   ,

	@RazonSocial varchar (50)  ,

	@Telefono varchar (50)  ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Email varchar (50)  ,

	@Idioma varchar (50)  ,

	@CondicionIva int   ,

	@Cuit varchar (50)  ,

	@FormaPago int   ,

	@LocalidadId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Proveedor]
				SET
					[ProveedorID] = @ProveedorId
					,[RazonSocial] = @RazonSocial
					,[Telefono] = @Telefono
					,[Fax] = @Fax
					,[Web] = @Web
					,[Email] = @Email
					,[Idioma] = @Idioma
					,[CondicionIva] = @CondicionIva
					,[Cuit] = @Cuit
					,[FormaPago] = @FormaPago
					,[LocalidadID] = @LocalidadId
				WHERE
[ProveedorID] = @OriginalProveedorId 
				
			



