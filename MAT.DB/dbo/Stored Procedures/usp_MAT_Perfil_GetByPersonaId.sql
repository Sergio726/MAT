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
