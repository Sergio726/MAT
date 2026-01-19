/*
  Índices recomendados para acelerar:
    dbo.usp_MAT_Factura_GetDetallePopupByFacturaID (@FacturaId UNIQUEIDENTIFIER)

  Objetivo:
    - Minimizar scans y key lookups en tablas “hijas” filtradas por FacturaID/PasajeID.
    - Optimizar JOINs y columnas leídas por la UI (incluidas en INCLUDE).

  Nota:
    - Este script NO asume edición Enterprise (no usa ONLINE=ON).
    - Usa IF NOT EXISTS para no duplicar índices.
    - Revisar nombres/PK existentes antes de ejecutar en producción.
*/

/* ============================================================
   1) DetalleFactura: usado 2 veces
      - SUM(df.Precio) por FacturaID
      - listado de items por FacturaID (Id, Fecha, Detalle, Cantidad, Precio)
   ============================================================ */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_DetalleFactura_FacturaID_Include_Items'
      AND object_id = OBJECT_ID('dbo.DetalleFactura')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_DetalleFactura_FacturaID_Include_Items
    ON dbo.DetalleFactura (FacturaID)
    INCLUDE (Precio, Fecha, Detalle, Cantidad);
END
GO

/* ============================================================
   2) Pasaje: usado 3 veces
      - RS2: TOP 1 Pasaje por FacturaID (para buscar Viaje/Paquete/Moneda)
      - RS3: detalle pasajes por FacturaID (ButacaID, ViajeID, PasajeroID, PasajeID)
      - RS4: menores por FacturaID (join PasajeroMenor por PasajeID)
   ============================================================ */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Pasaje_FacturaID_Include_Core'
      AND object_id = OBJECT_ID('dbo.Pasaje')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Pasaje_FacturaID_Include_Core
    ON dbo.Pasaje (FacturaID)
    INCLUDE (PasajeID, ViajeID, PasajeroID, ButacaID);
END
GO

/* ============================================================
   3) ReservaHabitacion: left join por PasajeID
      - Solo se necesita saber si existe HabitacionID para ese PasajeID
   ============================================================ */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ReservaHabitacion_PasajeID_Include_Habitacion'
      AND object_id = OBJECT_ID('dbo.ReservaHabitacion')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_ReservaHabitacion_PasajeID_Include_Habitacion
    ON dbo.ReservaHabitacion (PasajeID)
    INCLUDE (HabitacionID);
END
GO

/* ============================================================
   4) PasajeroMenor: join por PasajeID y lectura de tutor/menor
      - Se lee pm.Id y se usan pm.PasajeroID / pm.MenorID para join a Persona
   ============================================================ */
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_PasajeroMenor_PasajeID_Include_Personas'
      AND object_id = OBJECT_ID('dbo.PasajeroMenor')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_PasajeroMenor_PasajeID_Include_Personas
    ON dbo.PasajeroMenor (PasajeID)
    INCLUDE (PasajeroID, MenorID);
END
GO

/* ============================================================
   Opcionales (depende de PK/cluster existentes y tamaño):

   - Butaca: se lee NroButaca por ButacaID.
     Si ButacaID NO es clustered (o hay muchos lookups), puede convenir:

     CREATE NONCLUSTERED INDEX IX_Butaca_ButacaID_Include_Nro
     ON dbo.Butaca (ButacaID) INCLUDE (NroButaca);

   - Viaje: se lee PaqueteID por ViajeID (para luego join a Paquete).
     Si ViajeID NO es clustered, o se ven lookups:

     CREATE NONCLUSTERED INDEX IX_Viaje_ViajeID_Include_Paquete
     ON dbo.Viaje (ViajeID) INCLUDE (PaqueteID);
   ============================================================ */

