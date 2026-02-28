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
Author:     Sistema
Create date: 2026-02-27
Description: Inserta una entrada en el log de errores de la aplicacion.
             Usa TRY/CATCH para que un fallo aqui nunca rompa el request.
-----------------------------------------------------------*/
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO [dbo].[ErrorLog]
            ([CorrelationId], [Tipo], [Mensaje], [StackTrace], [Url], [Usuario], [Importancia])
        VALUES
            (@CorrelationId, @Tipo, @Mensaje, @StackTrace, @Url, @Usuario, @Importancia);
    END TRY
    BEGIN CATCH
        -- Silencioso: el logging nunca debe interrumpir el flujo de la aplicacion
    END CATCH
END
