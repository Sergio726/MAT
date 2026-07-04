CREATE PROCEDURE [dbo].[usp_MAT_VPersona_GetEntities]
    @PersonaID UNIQUEIDENTIFIER = NULL,
    @Term VARCHAR (100) = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista vPersona (NetTiers F7 - reemplaza VPersonaService.GetAll).
 --              @PersonaID filtra por PersonaID; @Term busca por documento (con y sin puntos)/nombre/apellido.
 --              F7.1: columnas explícitas en vez de SELECT *.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PersonaID,
        Apellido,
        Nombre,
        TipoDocumento,
        NroDocumento,
        Telefono,
        Email,
        FechaNacimiento,
        LocalidadID,
        UserId,
        Domicilio,
        Ocupacion,
        Nacionalidad,
        PaisResidencia,
        Sexo
    FROM dbo.vPersona
    WHERE (@PersonaID IS NULL OR PersonaID = @PersonaID)
      AND (@Term IS NULL
           OR REPLACE(NroDocumento, '.', '') LIKE '%' + REPLACE(@Term, '.', '') + '%'
           OR NroDocumento LIKE '%' + @Term + '%'
           OR Nombre LIKE '%' + @Term + '%'
           OR Apellido LIKE '%' + @Term + '%');
END
