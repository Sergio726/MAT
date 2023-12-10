
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Factura table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureFactura_GetByClienteId
(

	@ClienteId uniqueidentifier   
)
AS


				SET ANSI_NULLS OFF
				
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
					[ClienteID] = @ClienteId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON