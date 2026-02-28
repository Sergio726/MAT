CREATE PROCEDURE [dbo].[usp_MAT_ErrorLog_GetRecent]
(
    @Top            INT             = 100,
    @CorrelationId  VARCHAR(50)     = NULL,
    @FechaDesde     DATETIME        = NULL,
    @Importancia    INT             = NULL
)
AS
/*-----------------------------------------------------------
Author:     Sistema
Create date: 2026-02-27
Description: Devuelve los ultimos N errores registrados, con filtros opcionales.
-----------------------------------------------------------*/
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@Top)
        [Id],
        [FechaHora],
        [CorrelationId],
        [Tipo],
        [Mensaje],
        [StackTrace],
        [Url],
        [Usuario],
        [Importancia]
    FROM [dbo].[ErrorLog]
    WHERE
        (@CorrelationId IS NULL OR [CorrelationId] = @CorrelationId)
        AND (@FechaDesde    IS NULL OR [FechaHora]     >= @FechaDesde)
        AND (@Importancia   IS NULL OR [Importancia]   >= @Importancia)
    ORDER BY [Id] DESC;
END
