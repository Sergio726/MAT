-- NetTiers F2 — SPs catálogo geográfico
-- Publicar en SQL Server antes de usar MVC migrado en cada entorno.
-- Fuente de verdad: MAT.DB (archivos individuales en dbo/Stored Procedures/)

-- Nuevos:
-- usp_MAT_Pais_GetAll.sql
-- usp_MAT_Provincia_GetById.sql
-- usp_MAT_Departamento_GetByProvinciaId.sql
-- usp_MAT_Localidad_GetById.sql
-- usp_MAT_Localidad_GetAll.sql
-- usp_MAT_Localidad_Insert.sql
-- usp_MAT_VLocalidad_Search.sql (min 3 caracteres; actualizado post-auditoría F2)

-- Reutilizados (ya en BD):
-- usp_Provincia_GetAllByPaisID.sql
-- usp_Localidad_GetByIdDepartamento.sql
-- usp_GetInfoByLocalidadId.sql
-- usp_GetAllProvincia.sql
