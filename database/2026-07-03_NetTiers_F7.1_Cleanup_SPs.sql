-- NetTiers F7.1 — limpieza y optimización: publicar SP nuevo/modificados en cada entorno.
-- Fuente de verdad: MAT.DB/dbo/Stored Procedures/
-- Contenido:
--   1) usp_MAT_Perfil_GetByPersonaId (NUEVO): perfil en 1 roundtrip (vPersona + flags EXISTS).
--   2) usp_MAT_PersonaPasajero_GetEntities (MODIFICADO): TOP acotado + columnas explícitas.
--   3) usp_MAT_PersonaCliente_GetEntities / _PersonaProveedor_GetEntities / _VPersona_GetEntities
--      (MODIFICADOS): SELECT * -> columnas explícitas.

-- =========================================================================
-- 1) NUEVO: usp_MAT_Perfil_GetByPersonaId
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_Perfil_GetByPersonaId', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_Perfil_GetByPersonaId;
GO

CREATE PROCEDURE [dbo].[usp_MAT_Perfil_GetByPersonaId]
    @PersonaID UNIQUEIDENTIFIER
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Perfil de una persona en un solo roundtrip (NetTiers F7.1).
 --              Devuelve las columnas de vPersona + flags de existencia
 --              (EsCliente/EsPasajero/EsVendedor/EsProveedor). Reemplaza los
 --              5 roundtrips de PerfilModel (vPersona + 4 GetById != null).
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        v.PersonaID,
        v.Apellido,
        v.Nombre,
        v.TipoDocumento,
        v.NroDocumento,
        v.Telefono,
        v.Email,
        v.FechaNacimiento,
        v.LocalidadID,
        v.UserId,
        v.Domicilio,
        v.Ocupacion,
        v.Nacionalidad,
        v.PaisResidencia,
        v.Sexo,
        EsCliente   = CASE WHEN EXISTS (SELECT 1 FROM dbo.Cliente   c WHERE c.ClienteID   = v.PersonaID) THEN 1 ELSE 0 END,
        EsPasajero  = CASE WHEN EXISTS (SELECT 1 FROM dbo.Pasajero  p WHERE p.PasajeroID  = v.PersonaID) THEN 1 ELSE 0 END,
        EsVendedor  = CASE WHEN EXISTS (SELECT 1 FROM dbo.Vendedor  ve WHERE ve.VendedorID = v.PersonaID) THEN 1 ELSE 0 END,
        EsProveedor = CASE WHEN EXISTS (SELECT 1 FROM dbo.Proveedor pr WHERE pr.ProveedorID = v.PersonaID) THEN 1 ELSE 0 END
    FROM dbo.vPersona v
    WHERE v.PersonaID = @PersonaID;
END
GO

-- =========================================================================
-- 2) MODIFICADO: usp_MAT_PersonaPasajero_GetEntities (TOP acotado + columnas)
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_PersonaPasajero_GetEntities', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_PersonaPasajero_GetEntities;
GO

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
 --              F7.1: @Top acota la cantidad de filas (NULL = todas) para evitar cargar toda la vista.
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
GO

-- =========================================================================
-- 3a) MODIFICADO: usp_MAT_PersonaCliente_GetEntities (SELECT * -> columnas)
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_PersonaCliente_GetEntities', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_PersonaCliente_GetEntities;
GO

CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_GetEntities]
    @PersonaID UNIQUEIDENTIFIER = NULL
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaCliente (NetTiers F7 - reemplaza PersonaClienteService.GetAll).
 --              @PersonaID NULL devuelve todas; en caso contrario filtra por ClienteID (= PersonaID, 1:1).
 --              F7.1: columnas explícitas en vez de SELECT *.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PersonaID,
        Apellido,
        Nombre,
        NroDocumento,
        Telefono,
        Email,
        FechaNacimiento,
        Domicilio,
        Sexo,
        LocalidadID,
        ClienteID,
        RazonSocial,
        Cuit,
        Moneda,
        Empresa,
        Ocupacion,
        FormaPago,
        CondicionIva,
        VendedorID,
        Fax,
        Web,
        Idioma,
        Promotor,
        Observacion,
        TipoID,
        TipoDocumento,
        Celular,
        Nacionalidad,
        PaisResidencia,
        Provincia
    FROM dbo.PersonaCliente
    WHERE (@PersonaID IS NULL OR ClienteID = @PersonaID);
END
GO

-- =========================================================================
-- 3b) MODIFICADO: usp_MAT_PersonaProveedor_GetEntities (SELECT * -> columnas)
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_PersonaProveedor_GetEntities', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_PersonaProveedor_GetEntities;
GO

CREATE PROCEDURE [dbo].[usp_MAT_PersonaProveedor_GetEntities]
AS
/*-- =============================================
 -- Author: Sebastian Garcia
 -- Create date: 2026-07-03
 -- Description: Filas de la vista PersonaProveedor (NetTiers F7 - reemplaza PersonaProveedorService.GetAll).
 --              F7.1: columnas explícitas en vez de SELECT *.
 ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT
        PersonaID,
        Apellido,
        Nombre,
        NroDocumento,
        LocalidadID,
        Telefono,
        Email,
        FechaNacimiento,
        Sexo,
        Domicilio,
        ProveedorID,
        RazonSocial,
        ProveedorLocalidadID,
        ProveedorTelefono,
        Fax,
        Web,
        ProveedorEmail,
        Idioma,
        CondicionIva,
        Cuit,
        FormaPago,
        TipoDocumento
    FROM dbo.PersonaProveedor;
END
GO

-- =========================================================================
-- 3c) MODIFICADO: usp_MAT_VPersona_GetEntities (SELECT * -> columnas)
-- =========================================================================
IF OBJECT_ID(N'dbo.usp_MAT_VPersona_GetEntities', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_MAT_VPersona_GetEntities;
GO

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
GO
