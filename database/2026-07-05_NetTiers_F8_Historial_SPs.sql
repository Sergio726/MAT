-- NetTiers F8 — SPs de Historial (reemplaza HistorialService)
-- Publicar en SQL Server antes de usar MVC migrado en cada entorno.
-- Fuente de verdad: MAT.DB (archivos individuales en dbo/Stored Procedures/)

-- Ver archivos:
-- usp_MAT_Historial_GetAll.sql
-- usp_MAT_Historial_GetByHistorialId.sql

-- Nota: el submódulo Planilla (Planilla, PlanillaServicioItem, PlanillaHabitacionItem) no requirió
-- SPs nuevos: se declaró retiro total (cero referencias activas desde MAT.MVC). Ver
-- DOCUMENTACION/NETTIERS_MIGRACION_FASES.md y PROGRESS.md para el detalle de la decisión.
