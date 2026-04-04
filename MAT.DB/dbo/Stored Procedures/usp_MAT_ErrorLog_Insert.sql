/*
  Migración: usp_MAT_ErrorLog_Insert — propagar errores de INSERT a la aplicación.
  Paridad con MAT.DB\dbo\Stored Procedures\usp_MAT_ErrorLog_Insert.sql
  Ejecutar contra la base configurada en MAT.Data.ConnectionString (p. ej. MAT.Intranet).
*/
SET NOCOUNT ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_ErrorLog_Insert]
(
    @CorrelationId  VARCHAR(50),
    @Tipo           VARCHAR(200),
    @Mensaje        NVARCHAR(MAX),
    @StackTrace     NVARCHAR(MAX),
    @Url            NVARCHAR(1000),
    @Usuario        NVARCHAR(200),
    @Importancia    INT = 1
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-04-04
  -- Description: Inserta una entrada en ErrorLog. Los errores de INSERT se propagan
  --              al cliente para que DbErrorLogger active fallback (Event Log) sin
  --              romper el request; la capa C# envuelve la llamada en try/catch.
  ============================================= */
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ErrorLog]
        ([CorrelationId], [Tipo], [Mensaje], [StackTrace], [Url], [Usuario], [Importancia])
    VALUES
        (@CorrelationId, @Tipo, @Mensaje, @StackTrace, @Url, @Usuario, @Importancia);
END
GO

-- Prueba manual (opcional): descomentar y ejecutar; debe aparecer 1 fila nueva en ErrorLog.
/*
EXEC [dbo].[usp_MAT_ErrorLog_Insert]
    @CorrelationId = 'test-correlation-id',
    @Tipo = 'System.Test',
    @Mensaje = N'Prueba de inserción',
    @StackTrace = N'',
    @Url = N'/',
    @Usuario = N'test',
    @Importancia = 1;
*/
