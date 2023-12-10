
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Updates a record in the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCliente_Update
(

	@ClienteId uniqueidentifier   ,

	@OriginalClienteId uniqueidentifier   ,

	@RazonSocial varchar (50)  ,

	@Cuit varchar (50)  ,

	@Moneda varchar (50)  ,

	@Empresa varchar (50)  ,

	@Ocupacion varchar (50)  ,

	@FormaPago int   ,

	@CondicionIva int   ,

	@VendedorId uniqueidentifier   ,

	@Fax varchar (50)  ,

	@Web varchar (50)  ,

	@Idioma varchar (50)  ,

	@Promotor varchar (50)  ,

	@Observacion varchar (250)  ,

	@TipoId int   
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Cliente]
				SET
					[ClienteID] = @ClienteId
					,[RazonSocial] = @RazonSocial
					,[Cuit] = @Cuit
					,[Moneda] = @Moneda
					,[Empresa] = @Empresa
					,[Ocupacion] = @Ocupacion
					,[FormaPago] = @FormaPago
					,[CondicionIva] = @CondicionIva
					,[VendedorID] = @VendedorId
					,[Fax] = @Fax
					,[Web] = @Web
					,[Idioma] = @Idioma
					,[Promotor] = @Promotor
					,[Observacion] = @Observacion
					,[TipoID] = @TipoId
				WHERE
[ClienteID] = @OriginalClienteId 
				
			

