
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProveedor_Insert]
(

	@ProveedorId uniqueidentifier   ,

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


				
				INSERT INTO [dbo].[Proveedor]
					(
					[ProveedorID]
					,[RazonSocial]
					,[Telefono]
					,[Fax]
					,[Web]
					,[Email]
					,[Idioma]
					,[CondicionIva]
					,[Cuit]
					,[FormaPago]
					,[LocalidadID]
					)
				VALUES
					(
					@ProveedorId
					,@RazonSocial
					,@Telefono
					,@Fax
					,@Web
					,@Email
					,@Idioma
					,@CondicionIva
					,@Cuit
					,@FormaPago
					,@LocalidadId
					)
				
									
							
			



