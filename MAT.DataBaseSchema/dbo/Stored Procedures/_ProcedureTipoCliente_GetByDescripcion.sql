
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the TipoCliente table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedureTipoCliente_GetByDescripcion
(

	@Descripcion varchar (50)  
)
AS


				SELECT
					[TipoID],
					[Descripcion]
				FROM
					[dbo].[TipoCliente]
				WHERE
					[Descripcion] = @Descripcion
				SELECT @@ROWCOUNT
					
			

