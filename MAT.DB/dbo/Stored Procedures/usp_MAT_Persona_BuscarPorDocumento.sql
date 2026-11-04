CREATE PROCEDURE [dbo].[usp_MAT_Persona_BuscarPorDocumento]
(
    @Documento        VARCHAR (50),
    @ExcluirPersonaID UNIQUEIDENTIFIER = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Persona con el mismo documento, comparando solo el número: sin puntos, guiones,
  --              espacios ni ceros a la izquierda ("5787839" = "05.787.839"). @ExcluirPersonaID evita
  --              que en la edición el propio cliente cuente como repetido. Prioriza clientes.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Numero VARCHAR (50) = REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(ISNULL(@Documento, ''))), '.', ''), '-', ''), ' ', '');
    SET @Numero = SUBSTRING(@Numero, PATINDEX('%[^0]%', @Numero + 'x'), 50);

    IF (@Numero = '')
        RETURN;

    SELECT TOP (1)
           p.PersonaID,
           p.Apellido,
           p.Nombre,
           p.NroDocumento,
           CAST(CASE WHEN c.ClienteID IS NULL THEN 0 ELSE 1 END AS BIT) AS EsCliente
    FROM dbo.Persona p
    LEFT JOIN dbo.Cliente c ON c.ClienteID = p.PersonaID
    CROSS APPLY (SELECT REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(ISNULL(p.NroDocumento, ''))), '.', ''), '-', ''), ' ', '') AS Limpio) d
    WHERE SUBSTRING(d.Limpio, PATINDEX('%[^0]%', d.Limpio + 'x'), 50) = @Numero
      AND (@ExcluirPersonaID IS NULL OR p.PersonaID <> @ExcluirPersonaID)
    ORDER BY CASE WHEN c.ClienteID IS NULL THEN 1 ELSE 0 END, p.Apellido;
END
