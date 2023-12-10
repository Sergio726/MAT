
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Inserts a record into the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_Insert
(

	@FacturaId uniqueidentifier    OUTPUT,

	@NroFactura varchar (50)  ,

	@Monto float   ,

	@Fecha datetime   ,

	@Tipo int   ,

	@Estado int   ,

	@ClienteId uniqueidentifier   ,

	@VendedorId uniqueidentifier   ,

	@DescuentoAplicado float   
)
AS


				
				INSERT INTO [dbo].[Factura]
					(
					[FacturaID]
					,[NroFactura]
					,[Monto]
					,[Fecha]
					,[Tipo]
					,[Estado]
					,[ClienteID]
					,[VendedorID]
					,[DescuentoAplicado]
					)
				VALUES
					(
					@FacturaId
					,@NroFactura
					,@Monto
					,@Fecha
					,@Tipo
					,@Estado
					,@ClienteId
					,@VendedorId
					,@DescuentoAplicado
					)
				
									
							
			

