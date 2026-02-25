CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_GetById_Fiscal]
(
    @ProveedorID UNIQUEIDENTIFIER
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Retrieves a single Proveedor record by its ID
-- Description: Obtiene un proveedor por su identificador para el modulo fiscal
----------------------------------------------------------------------------------------------------
*/
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ProveedorID],
        [RazonSocial],
        [Telefono],
        [Fax],
        [Web],
        [Email],
        [Idioma],
        [CondicionIva],
        [Cuit],
        [FormaPago],
        [LocalidadID],
        [Domicilio],
        [Estado]
    FROM [dbo].[Proveedor]
    WHERE [ProveedorID] = @ProveedorID;
END
