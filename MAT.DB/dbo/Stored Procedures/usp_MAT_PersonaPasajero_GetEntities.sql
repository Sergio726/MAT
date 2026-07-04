CREATE PROCEDURE [dbo].[usp_MAT_PersonaPasajero_GetEntities]
    @PersonaID UNIQUEIDENTIFIER = NULL,
    @Term VARCHAR (100) = NULL,
    @Top INT = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaPasajero (NetTiers F7 - reemplaza PersonaPasajeroService.GetAll).
 --              @PersonaID filtra por PersonaID; @Term busca por documento/nombre/apellido.
 --              F7.1: @Top acota la cantidad de filas (NULL = todas) y columnas explícitas.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT TOP (COALESCE(@Top, 2147483647))
        PersonaID,
        Apellido,
        Nombre,
        NroDocumento,
        Telefono,
        Domicilio,
        Email,
        FechaNacimiento,
        Sexo,
        PasajeroID,
        Pasaporte,
        VencimientoPasaporte,
        EmisionPasaporte,
        PaisOrigen,
        LocalidadID,
        TipoDocumento
    FROM dbo.PersonaPasajero
    WHERE (@PersonaID IS NULL OR PersonaID = @PersonaID)
      AND (@Term IS NULL
           OR NroDocumento LIKE '%' + @Term + '%'
           OR Nombre LIKE '%' + @Term + '%'
           OR Apellido LIKE '%' + @Term + '%')
    ORDER BY Apellido, Nombre;
END
