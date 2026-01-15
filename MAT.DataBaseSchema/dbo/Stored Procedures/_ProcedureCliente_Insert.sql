
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Inserts a record into the Cliente table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureCliente_Insert
(

	@ClienteId uniqueidentifier    OUTPUT,

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


				
				INSERT INTO [dbo].[Cliente]
					(
					[ClienteID]
					,[RazonSocial]
					,[Cuit]
					,[Moneda]
					,[Empresa]
					,[Ocupacion]
					,[FormaPago]
					,[CondicionIva]
					,[VendedorID]
					,[Fax]
					,[Web]
					,[Idioma]
					,[Promotor]
					,[Observacion]
					,[TipoID]
					)
				VALUES
					(
					@ClienteId
					,@RazonSocial
					,@Cuit
					,@Moneda
					,@Empresa
					,@Ocupacion
					,@FormaPago
					,@CondicionIva
					,@VendedorId
					,@Fax
					,@Web
					,@Idioma
					,@Promotor
					,@Observacion
					,@TipoId
					)
				
									
							
			

