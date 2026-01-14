
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Updates a record in the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_Update
(

	@FacturaId uniqueidentifier   ,

	@OriginalFacturaId uniqueidentifier   ,

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


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Factura]
				SET
					[FacturaID] = @FacturaId
					,[NroFactura] = @NroFactura
					,[Monto] = @Monto
					,[Fecha] = @Fecha
					,[Tipo] = @Tipo
					,[Estado] = @Estado
					,[ClienteID] = @ClienteId
					,[VendedorID] = @VendedorId
					,[DescuentoAplicado] = @DescuentoAplicado
				WHERE
[FacturaID] = @OriginalFacturaId 
				
			

