CREATE PROCEDURE [dbo].[usp_MAT_ListaEspera_Delete_Soft]
(
    @ID INT,
    @UsuarioID INT
)
AS
/*
  Autor: Seba Garcia 19/01/2026
  NUEVO SP (v2) - Soft delete + normalización de respuesta para el botón "Quitar" de Lista de Espera.

  Objetivo:
    - NO borrar físico: mantener historial en dbo.ListaEspera.
    - Permitir "quitar" solo al usuario creador (UsuarioID).
    - Respuesta coherente para UI (1 solo resultset).

  Requiere:
    - dbo.ListaEspera: columnas IsDeleted(bit), DeletedAt(datetime), DeletedBy(int)
      (ver database/upgrade_ListaEspera_softdelete.sql)

  Contrato (1 solo resultset):
    - Estado: 'Done' | 'Error'
    - Mensaje: texto para mostrar al usuario
*/


BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    DECLARE @Estado  VARCHAR(10) = 'Error';
    DECLARE @Mensaje NVARCHAR(2048) = N'';

    IF EXISTS (
        SELECT 1
        FROM dbo.ListaEspera WITH (READUNCOMMITTED)
        WHERE Id = @ID
          AND UsuarioID = @UsuarioID
          AND ISNULL(IsDeleted, 0) = 0
    )
    BEGIN
        BEGIN TRY
            BEGIN TRAN;

            UPDATE dbo.ListaEspera
               SET IsDeleted = 1,
                   DeletedAt = GETDATE(),
                   DeletedBy = @UsuarioID
             WHERE Id = @ID
               AND UsuarioID = @UsuarioID
               AND ISNULL(IsDeleted, 0) = 0;

            COMMIT TRAN;

            SET @Estado = 'Done';
            SET @Mensaje = N'Registro quitado de la lista de espera.';
        END TRY
        BEGIN CATCH
            IF @@TRANCOUNT > 0 ROLLBACK TRAN;
            SET @Estado = 'Error';
            SET @Mensaje = N'Error al eliminar elemento: ' + ERROR_MESSAGE();
        END CATCH
    END
    ELSE
    BEGIN
        SET @Estado = 'Error';
        IF EXISTS (SELECT 1 FROM dbo.ListaEspera WITH (READUNCOMMITTED) WHERE Id = @ID AND ISNULL(IsDeleted, 0) = 1)
            SET @Mensaje = N'El registro ya fue quitado previamente.';
        ELSE
            SET @Mensaje = N'El registro solo puede ser eliminado por la persona que lo creó.';
    END

    SELECT @Estado AS Estado, @Mensaje AS Mensaje;
END