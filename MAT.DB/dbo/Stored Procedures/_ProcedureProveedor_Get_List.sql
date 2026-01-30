
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Gets all records from the Proveedor table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureProveedor_Get_List]

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
					
				SELECT @@ROWCOUNT
			



