CREATE PROCEDURE [dbo].[usp_MAT_ErrorLog_Insert]
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
/*-----------------------------------------------------------
Author:     Sebastian Garcia
Create date: 2026-04-04
Description: Inserta una entrada en ErrorLog. Los errores de INSERT se propagan
            a la capa C# (DbErrorLogger) para fallback al Event Log sin romper el request.
-----------------------------------------------------------*/
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[ErrorLog]
        ([CorrelationId], [Tipo], [Mensaje], [StackTrace], [Url], [Usuario], [Importancia])
    VALUES
        (@CorrelationId, @Tipo, @Mensaje, @StackTrace, @Url, @Usuario, @Importancia);
END
