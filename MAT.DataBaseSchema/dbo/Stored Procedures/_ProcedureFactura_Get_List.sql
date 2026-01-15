
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Gets all records from the Factura table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_Get_List

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
					
				SELECT @@ROWCOUNT
			

