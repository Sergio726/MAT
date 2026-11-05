CREATE PROCEDURE [dbo].[usp_MAT_PersonaCliente_Ficha]
(
    @PersonaID UNIQUEIDENTIFIER
)
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Datos de la ficha del cliente (módulo Clientes F3) en una sola consulta, con los nombres
  --              ya resueltos (localidad, departamento, provincia, vendedor), el estado de la cuenta corriente
  --              y cuántas facturas tiene como titular. Reemplaza a Persona.GetById + Cliente.GetById + una
  --              consulta por cada nombre que hacía la vista.
  -- 2026-11-04 (F4): nombre del país de nacionalidad y de residencia (Pais.Codigo).
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT p.PersonaID, p.Apellido, p.Nombre, p.TipoDocumento, p.NroDocumento, p.FechaNacimiento, p.Sexo,
           p.Nacionalidad, p.PaisResidencia, p.Ocupacion,
           pn.Descripcion AS NacionalidadNombre, pr2.Descripcion AS PaisResidenciaNombre,
           p.Domicilio, p.Telefono, p.Celular, p.Email,
           l.Nombre AS LocalidadNombre,
           d.Nombre AS DepartamentoNombre,
           COALESCE(pr.Nombre, prl.Nombre) AS ProvinciaNombre,
           c.RazonSocial, c.Cuit, c.CondicionIva, c.FormaPago, c.Empresa, c.Fax, c.Web, c.Idioma, c.Promotor,
           c.Observacion, c.TipoID, c.FechaAlta,
           LTRIM(RTRIM(ISNULL(v.Apellido, '') + ', ' + ISNULL(v.Nombre, ''))) AS VendedorNombre,
           cu.Estado AS CuentaHabilitada,
           (SELECT COUNT(*) FROM dbo.Factura f WHERE f.ClienteID = c.ClienteID) AS CantidadFacturas
    FROM dbo.Cliente c
    INNER JOIN dbo.Persona p ON p.PersonaID = c.ClienteID
    LEFT JOIN dbo.Localidad l ON l.ID = p.LocalidadID
    LEFT JOIN dbo.Departamento d ON d.ID = l.idDepartamento
    LEFT JOIN dbo.Provincia prl ON prl.ID = d.idProvincia
    LEFT JOIN dbo.Provincia pr ON pr.ID = p.Provincia
    LEFT JOIN dbo.Persona v ON v.PersonaID = c.VendedorID
    -- Nacionalidad y país de residencia se guardan como código ISO (F4)
    LEFT JOIN dbo.Pais pn ON pn.Codigo = p.Nacionalidad
    LEFT JOIN dbo.Pais pr2 ON pr2.Codigo = p.PaisResidencia
    OUTER APPLY (SELECT TOP 1 cu.Estado FROM dbo.Cuenta cu WHERE cu.ClienteID = c.ClienteID) cu
    WHERE c.ClienteID = @PersonaID;
END
