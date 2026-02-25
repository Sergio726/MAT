CREATE PROCEDURE [dbo].[usp_MAT_Proveedor_GetAll_Fiscal]
(
    @Estado INT = NULL
)
AS
/*
----------------------------------------------------------------------------------------------------
-- Purpose: Retrieves all Proveedor records with optional Estado filter
-- Description: Lista proveedores para el modulo fiscal con filtro opcional por estado
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
    WHERE (@Estado IS NULL OR [Estado] = @Estado)
    ORDER BY [RazonSocial];
END
