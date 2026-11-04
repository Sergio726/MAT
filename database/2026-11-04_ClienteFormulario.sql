-- =============================================================================
-- 2026-11-04 — Módulo Clientes F1: formulario único de alta y edición
-- Author: Sebastian Garcia
-- usp_MAT_PersonaCliente_Guardar (alta/edición transaccional: Persona + Cliente + Cuenta + Pasajero)
-- usp_MAT_Persona_BuscarPorDocumento (documento repetido sin puntos ni ceros a la izquierda)
-- Paridad con MAT.DB (generado desde esos archivos). Idempotente: CREATE OR ALTER.
-- No modifica datos ni objetos existentes. Requiere SQL Server 2016+ (THROW, AT TIME ZONE).
-- =============================================================================
SET NOCOUNT ON;
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_PersonaCliente_Guardar]
(
    @PersonaID                 UNIQUEIDENTIFIER = NULL,
    @Apellido                  VARCHAR (100),
    @Nombre                    VARCHAR (100),
    @TipoDocumento             INT,
    @NroDocumento              VARCHAR (50),
    @Sexo                      INT              = NULL,
    @FechaNacimiento           DATE,
    @Nacionalidad              VARCHAR (50),
    @PaisResidencia            VARCHAR (50),
    @Telefono                  VARCHAR (50)     = NULL,
    @Celular                   VARCHAR (50)     = NULL,
    @Email                     VARCHAR (50)     = NULL,
    @Domicilio                 VARCHAR (100),
    @Provincia                 INT,
    @LocalidadID               INT,
    @Ocupacion                 VARCHAR (50)     = NULL,
    @RazonSocial               VARCHAR (50)     = NULL,
    @Cuit                      VARCHAR (50)     = NULL,
    @Empresa                   VARCHAR (50)     = NULL,
    @CondicionIva              INT              = NULL,
    @FormaPago                 INT              = NULL,
    @TipoID                    INT,
    @Fax                       VARCHAR (50)     = NULL,
    @Web                       VARCHAR (50)     = NULL,
    @Idioma                    VARCHAR (50)     = NULL,
    @Promotor                  VARCHAR (50)     = NULL,
    @Observacion               VARCHAR (250)    = NULL,
    @CuentaCorrienteHabilitada BIT              = 1,
    @VendedorID                UNIQUEIDENTIFIER = NULL
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Alta (@PersonaID NULL) o edición de un cliente en una sola transacción:
  --              Persona + Cliente + Cuenta (estado de cuenta corriente) + Pasajero.
  --              Reemplaza usp_MAT_PersonaCliente_Create y las dos actualizaciones sueltas de la
  --              edición (Persona y Cliente sin transacción). En la edición no se tocan VendedorID,
  --              Moneda ni FechaAlta. Devuelve PersonaID.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;

    DECLARE @EsAlta BIT = CASE WHEN @PersonaID IS NULL THEN 1 ELSE 0 END;
    -- Hora argentina: el servidor está en UTC.
    DECLARE @Ahora DATETIME = CAST(SYSDATETIMEOFFSET() AT TIME ZONE 'Argentina Standard Time' AS DATETIME);

    IF (@EsAlta = 0 AND NOT EXISTS (SELECT 1 FROM dbo.Cliente WHERE ClienteID = @PersonaID))
    BEGIN
        RAISERROR ('No existe el cliente que se quiere modificar.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        IF (@EsAlta = 1)
        BEGIN
            SET @PersonaID = NEWID();

            INSERT INTO dbo.Persona (PersonaID, Apellido, Nombre, TipoDocumento, NroDocumento, Sexo, FechaNacimiento,
                                     Nacionalidad, PaisResidencia, Telefono, Celular, Email, Domicilio, Provincia,
                                     LocalidadID, Ocupacion)
            VALUES (@PersonaID, @Apellido, @Nombre, @TipoDocumento, @NroDocumento, @Sexo, @FechaNacimiento,
                    @Nacionalidad, @PaisResidencia, @Telefono, @Celular, @Email, @Domicilio, @Provincia,
                    @LocalidadID, @Ocupacion);

            INSERT INTO dbo.Cliente (ClienteID, RazonSocial, Cuit, Empresa, Ocupacion, FormaPago, CondicionIva,
                                     VendedorID, Fax, Web, Idioma, Promotor, Observacion, TipoID, FechaAlta)
            VALUES (@PersonaID, @RazonSocial, @Cuit, @Empresa, @Ocupacion, @FormaPago, @CondicionIva,
                    @VendedorID, @Fax, @Web, @Idioma, @Promotor, @Observacion, @TipoID, @Ahora);
        END
        ELSE
        BEGIN
            UPDATE dbo.Persona
            SET Apellido = @Apellido, Nombre = @Nombre, TipoDocumento = @TipoDocumento, NroDocumento = @NroDocumento,
                Sexo = @Sexo, FechaNacimiento = @FechaNacimiento, Nacionalidad = @Nacionalidad,
                PaisResidencia = @PaisResidencia, Telefono = @Telefono, Celular = @Celular, Email = @Email,
                Domicilio = @Domicilio, Provincia = @Provincia, LocalidadID = @LocalidadID, Ocupacion = @Ocupacion
            WHERE PersonaID = @PersonaID;

            UPDATE dbo.Cliente
            SET RazonSocial = @RazonSocial, Cuit = @Cuit, Empresa = @Empresa, Ocupacion = @Ocupacion,
                FormaPago = @FormaPago, CondicionIva = @CondicionIva, Fax = @Fax, Web = @Web, Idioma = @Idioma,
                Promotor = @Promotor, Observacion = @Observacion, TipoID = @TipoID
            WHERE ClienteID = @PersonaID;
        END

        -- Cuenta corriente: una por cliente; se crea si falta.
        IF EXISTS (SELECT 1 FROM dbo.Cuenta WHERE ClienteID = @PersonaID)
            UPDATE dbo.Cuenta SET Estado = @CuentaCorrienteHabilitada WHERE ClienteID = @PersonaID;
        ELSE
            INSERT INTO dbo.Cuenta (CuentaID, ClienteID, Estado) VALUES (NEWID(), @PersonaID, @CuentaCorrienteHabilitada);

        -- Todo cliente puede viajar: registro de pasajero.
        IF NOT EXISTS (SELECT 1 FROM dbo.Pasajero WHERE PasajeroID = @PersonaID)
            INSERT INTO dbo.Pasajero (PasajeroID) VALUES (@PersonaID);

        COMMIT TRANSACTION;

        SELECT @PersonaID AS PersonaID;
    END TRY
    BEGIN CATCH
        IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Persona_BuscarPorDocumento]
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
GO
