
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Select records from the Proveedor table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProveedor_GetByProveedorId]
(

	@ProveedorId uniqueidentifier   
)
AS


				SELECT
					[ProveedorID],
					[RazonSocial],
					[Telefono],
					[Fax],
					[Web],
					[Email],
					[Idioma],
					[CondicionIva],
					[Cuit],
					[FormaPago],
					[LocalidadID]
				FROM
					[dbo].[Proveedor]
				WHERE
					[ProveedorID] = @ProveedorId
				SELECT @@ROWCOUNT
					
			



