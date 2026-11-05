CREATE PROCEDURE [dbo].[usp_MAT_Catalogo_Geografia]
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-11-04
  -- Description: Catálogos del formulario de cliente (módulo Clientes F4).
  --              RS1: países (PaisID, Codigo, Nombre, TieneProvincias), Argentina primero.
  --              RS2: provincias con su país, para filtrar País → Provincia en la pantalla.
  --              RS3: ocupaciones activas.
  ============================================= */
BEGIN
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SELECT pa.PaisID, pa.Codigo, pa.Descripcion AS Nombre,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Provincia p WHERE p.IdPais = pa.PaisID) THEN 1 ELSE 0 END AS BIT) AS TieneProvincias
    FROM dbo.Pais pa
    ORDER BY CASE WHEN pa.Codigo = 'ARG' THEN 0 ELSE 1 END, pa.Descripcion;

    SELECT p.ID, p.Nombre, p.IdPais
    FROM dbo.Provincia p
    ORDER BY p.Nombre;

    SELECT o.Nombre
    FROM dbo.Ocupacion o
    WHERE o.Activo = 1
    ORDER BY o.Orden, o.Nombre;
END
