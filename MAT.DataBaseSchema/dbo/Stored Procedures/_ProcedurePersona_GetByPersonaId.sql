
/*
----------------------------------------------------------------------------------------------------

-- Purpose: Select records from the Persona table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo._ProcedurePersona_GetByPersonaId
(

	@PersonaId uniqueidentifier   
)
AS


				SELECT
					[PersonaID],
					[Apellido],
					[Nombre],
					[TipoDocumento],
					[NroDocumento],
					[Telefono],
					[Email],
					[FechaNacimiento],
					[LocalidadID],
					[UserId],
					[Domicilio],
					[Sexo],
					[Ocupacion],
					[Nacionalidad],
					[PaisResidencia]
				FROM
					[dbo].[Persona]
				WHERE
					[PersonaID] = @PersonaId
				SELECT @@ROWCOUNT
					
			

