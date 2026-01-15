
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Factura table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_GetByFacturaId
(

	@FacturaId uniqueidentifier   
)
AS


				SELECT
					[FacturaID],
					[NroFactura],
					[Monto],
					[Fecha],
					[Tipo],
					[Estado],
					[ClienteID],
					[VendedorID],
					[DescuentoAplicado]
				FROM
					[dbo].[Factura]
				WHERE
					[FacturaID] = @FacturaId
				SELECT @@ROWCOUNT
					
			

