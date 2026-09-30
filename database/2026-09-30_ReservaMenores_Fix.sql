-- =============================================================================
-- BUG P1 — Reserva con menores (2026-09-30, Sebastian Garcia)
-- Publicar en cada entorno (Dev / Staging / Prod) junto con el deploy de MAT.MVC.
-- Paridad: MAT.DB\dbo\Stored Procedures\<mismo nombre>.sql (fuente de verdad SSDT).
--
-- Qué corrige:
--   1. usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher: acepta "," y ";" en @AdicionalesIDs
--      (la UI envía ";"; con 2+ adicionales fallaba el cast a uniqueidentifier) y CATCH corregido.
--   2. usp_MAT_Reserva_DetalleFactura: acepta ";" y ","; descarta tokens no Guid; CATCH corregido.
--   3. usp_MAT_Reserva_VincularMenorByViajeID: @PasajeID inicializado (antes NULL => sin resultset),
--      inserta por menor con NOT EXISTS (antes rechazaba el lote entero), acepta ";" y ",".
--   4. usp_MAT_Reserva_GetMenoresDisponibles: join a Cliente (FK PasajeroMenor.menorid -> Cliente).
-- Idempotente: CREATE OR ALTER. Requiere SQL Server 2016 SP1+ (CREATE OR ALTER, TRY_CONVERT).
-- =============================================================================
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- -----------------------------------------------------------------------------
-- usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher
-- -----------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher](@PasajeID uniqueidentifier,
															    @FacturaID uniqueidentifier,
															    @PasajeroID uniqueidentifier,
															    @AdicionalesIDs varchar(max),
															    @EstadoFactura INT,
															    @VendedorID uniqueidentifier = null)

AS
/*-- =============================================
  -- Author:    Garcia Sergio
  -- Create date: 09-05-2017
  -- Description: Asigna pasajero/factura/estado al pasaje, registra sus adicionales
  --              (@AdicionalesIDs: lista de Guid separados por "," o ";") y emite voucher si está pagado.
  -- Historial:
  --   2018-05-30  Garcia Sergio     Quita PrecioID.
  --   2026-06-19  Sebastian Garcia  Valida conflicto de fecha de salida (fn_MAT_Pasaje_TieneConflictoFechaSalida).
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: acepta ";" además de "," como separador (la UI envía ";"),
  --                                 descarta tokens que no sean Guid y corrige el CATCH
  --                                 (Error_message() + ERROR_LINE() fallaba por conversión nvarchar/int).
  ============================================= */
 BEGIN
      SET nocount, xact_abort ON;
      SET TRANSACTION isolation level READ uncommitted;

	  BEGIN TRY
			BEGIN TRAN

			DECLARE @ViajeID UNIQUEIDENTIFIER;

			SELECT @ViajeID = p.ViajeID
			FROM dbo.Pasaje p
			WHERE p.PasajeID = @PasajeID;

			IF dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(@PasajeroID, @ViajeID, @PasajeID) = 1
			BEGIN
				RAISERROR('El pasajero ya está registrado en otro viaje con la misma fecha de salida.', 16, 1);
				RETURN;
			END

			/*update pasaje*/
			UPDATE Pasaje
			SET   FacturaID = @FacturaID,
				  PasajeroID = @PasajeroID,
				  EstadoPasaje = @EstadoFactura
			WHERE PasajeID = @PasajeID


			/*Insert Adicionales (separador "," o ";"; se ignoran tokens no Guid)*/
			IF (ISNULL(@AdicionalesIDs, '') != '')
			BEGIN
				INSERT INTO dbo.PasajeAdicional
				(pasajeadicionalid,
				 pasajeid,
				 adicionalid
				)
			SELECT Newid()   AS PasajeAdicionalID,
				   @PasajeID AS PasajeID,
				   TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item))) AS AdicionalID
			FROM   dbo.Split(REPLACE(@AdicionalesIDs, ';', ','), ',') s
			WHERE  TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item))) IS NOT NULL
			END


			/*4	Pagado, inserta un nuevo voucher*/
			IF ( @EstadoFactura = 4 )
			  BEGIN
				  DECLARE @VoucherID UNIQUEIDENTIFIER

				  SET @VoucherID = Newid()

				  INSERT INTO dbo.Voucher
							  (VoucherID,
							   FechaEmision,
							   VendedorID)
				  VALUES      (@VoucherID,
							   Getdate(),
							   @VendedorID)

				  UPDATE dbo.Pasaje
				  SET    voucherid = @VoucherID
				  WHERE  pasajeid = @PasajeID
			  END

			COMMIT TRAN;
	  END TRY

	  BEGIN CATCH
	  	IF @@TRANCOUNT > 0
			ROLLBACK TRAN

			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			SELECT  @errmsg = ERROR_MESSAGE() + ' (línea ' + CAST(ERROR_LINE() AS nvarchar(10)) + ')',
					@errState = ERROR_STATE()
			RAISERROR (@errmsg, 16, @errState);
	  END CATCH

  END
GO

-- -----------------------------------------------------------------------------
-- usp_MAT_Reserva_DetalleFactura
-- -----------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_DetalleFactura](@FacturaID varchar(36),
												@AdicionalesIDs varchar(max),
												@PrecioID varchar(36))
as
/*-- =============================================
  -- Author:    Garcia Sergio
  -- Create date: 09-04-2017
  -- Description: Carga DetalleFactura: renglón del paquete (precio) y un renglón por cada
  --              adicional de @AdicionalesIDs (lista de Guid separados por ";" o ","; los
  --              repetidos generan renglones repetidos, p. ej. un seguro de menor por menor).
  -- Historial:
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: acepta "," además de ";", descarta tokens no Guid
  --                                 y corrige el CATCH (Error_message() + ERROR_LINE() fallaba por conversión).
  ============================================= */
begin
 SET nocount, xact_abort ON;
      SET TRANSACTION isolation level READ uncommitted;

	begin try

		declare @Detalle varchar(1000),
				@Precio money

		--inert pasaje
		select @Precio =  p.Monto,
			   @Detalle = 'PAQUETE ' + upper(p.Descripcion)
		from dbo.Precio p
		where PrecioID = @PrecioID

		begin tran
			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad)
			values (@FacturaID,@Detalle,@Precio,1)

		--insert adicional (separador ";" o ","; se ignoran tokens no Guid)
		if (ISNULL(@AdicionalesIDs, '') != '')
		begin

			insert into dbo.DetalleFactura (FacturaID,Detalle,Precio,Cantidad,AdicionalID)
			select @FacturaID,a.Descripcion, a.Monto,1,a.AdicionalID
			from dbo.Split(REPLACE(@AdicionalesIDs, ',', ';'), ';') sp
			inner join dbo.Adicional a
				on a.AdicionalID = TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(sp.Item)))

		end

		commit
	end try

	begin catch
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN

			DECLARE @errmsg   AS NVARCHAR (2048),
					@errState int
			SELECT  @errmsg = ERROR_MESSAGE() + ' (línea ' + CAST(ERROR_LINE() AS nvarchar(10)) + ')',
					@errState = ERROR_STATE()
			RAISERROR (@errmsg, 16, @errState);

	end catch

end
GO

-- -----------------------------------------------------------------------------
-- usp_MAT_Reserva_VincularMenorByViajeID
-- -----------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_VincularMenorByViajeID] (@ViajeID varchar(max) = '',
														         @PasajeroID varchar(max) = '',
														         @MenorID varchar(max) = ''
														         )
AS
/*-- =============================================
  -- Author:    Sebastian Garcia
  -- Create date: 2026-09-30
  -- Description: Vincula uno o más menores (lista CSV en @MenorID, separador "," o ";") a un
  --              responsable (@PasajeroID) dentro de un viaje (@ViajeID), tomando el pasaje más
  --              reciente del responsable en ese viaje. Siempre devuelve un resultset (Id, ErrorMsg):
  --              1 = vinculó al menos uno, 0 = todos ya estaban vinculados, -1 = error / datos faltantes.
  -- Historial:
  --   2017-01-17  Garcia Sergio     Creación del SP.
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: @PasajeID sin inicializar quedaba NULL y el SP no
  --                                 devolvía resultset; se rechazaba el lote entero si un menor ya
  --                                 estaba vinculado; acepta ";" además de ","; descarta IDs inválidos.
  ============================================= */
BEGIN
    SET NOCOUNT, XACT_ABORT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    IF (ISNULL(@ViajeID, '') = '' OR ISNULL(@PasajeroID, '') = '' OR ISNULL(@MenorID, '') = '')
    BEGIN
        SELECT -1 AS Id, 'Faltan datos para vincular menores (viaje, responsable o menores).' AS ErrorMsg;
        RETURN;
    END

    DECLARE @PasajeID uniqueidentifier = NULL;

    SELECT TOP 1 @PasajeID = p.PasajeID
    FROM dbo.Pasaje p
    WHERE p.PasajeroID = @PasajeroID
      AND p.ViajeID = @ViajeID
    ORDER BY p.FechaReserva DESC;

    IF (@PasajeID IS NULL)
    BEGIN
        SELECT -1 AS Id, 'El responsable seleccionado no tiene pasaje en este viaje; no se pueden vincular los menores.' AS ErrorMsg;
        RETURN;
    END

    BEGIN TRY
        DECLARE @tblMenorID TABLE (MenorID uniqueidentifier NOT NULL);

        INSERT INTO @tblMenorID (MenorID)
        SELECT DISTINCT TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item)))
        FROM dbo.Split(REPLACE(@MenorID, ';', ','), ',') s
        WHERE TRY_CONVERT(uniqueidentifier, LTRIM(RTRIM(s.Item))) IS NOT NULL;

        IF NOT EXISTS (SELECT 1 FROM @tblMenorID)
        BEGIN
            SELECT -1 AS Id, 'No se recibieron menores válidos para vincular.' AS ErrorMsg;
            RETURN;
        END

        DECLARE @Existentes int = 0,
                @Insertados int = 0;

        SELECT @Existentes = COUNT(*)
        FROM @tblMenorID t
        WHERE EXISTS (SELECT 1
                      FROM dbo.PasajeroMenor pm
                      WHERE pm.pasajeid = @PasajeID
                        AND pm.pasajeroid = @PasajeroID
                        AND pm.menorid = t.MenorID);

        BEGIN TRAN;

        INSERT INTO dbo.PasajeroMenor (pasajeid, pasajeroid, menorid)
        SELECT @PasajeID, @PasajeroID, t.MenorID
        FROM @tblMenorID t
        WHERE NOT EXISTS (SELECT 1
                          FROM dbo.PasajeroMenor pm
                          WHERE pm.pasajeid = @PasajeID
                            AND pm.pasajeroid = @PasajeroID
                            AND pm.menorid = t.MenorID);

        SET @Insertados = @@ROWCOUNT;

        COMMIT TRAN;

        IF (@Insertados > 0)
            SELECT 1 AS Id,
                   'Se vincularon ' + CAST(@Insertados AS varchar(10)) + ' menor(es) al responsable.'
                   + CASE WHEN @Existentes > 0 THEN ' ' + CAST(@Existentes AS varchar(10)) + ' ya estaba(n) vinculado(s).' ELSE '' END AS ErrorMsg;
        ELSE
            SELECT 0 AS Id, 'Los menores seleccionados ya estaban vinculados con este responsable.' AS ErrorMsg;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @errmsg nvarchar(2048) = '*** ' + COALESCE(QUOTENAME(ERROR_PROCEDURE()), '<dynamic SQL>')
                                         + ', ' + CAST(ERROR_LINE() AS nvarchar(10))
                                         + '. Errno ' + CAST(ERROR_NUMBER() AS nvarchar(10)) + ': ' + ERROR_MESSAGE();

        SELECT -1 AS Id, @errmsg AS ErrorMsg;
    END CATCH
END
GO

-- -----------------------------------------------------------------------------
-- usp_MAT_Reserva_GetMenoresDisponibles
-- -----------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE [dbo].[usp_MAT_Reserva_GetMenoresDisponibles] (@ViajeID varchar(max) = ''
																 )
AS
/*-- =============================================
  -- Author:    Garcia Sergio
  -- Create date: 08-01-2017
  -- Description: Lista de menores (menos de 5 años) disponibles para vincular en un viaje:
  --              personas con FechaNacimiento cargada, con fila en Cliente (requisito de la FK
  --              PasajeroMenor.menorid -> Cliente) y que aún no estén vinculadas en ese viaje.
  -- Historial:
  --   2026-09-30  Sebastian Garcia  BUG P1 menores: join a dbo.Cliente (una Persona sin Cliente rompía
  --                                 fk_cliente_menor al vincular) y FechaNacimiento IS NOT NULL explícito.
  ============================================= */
BEGIN
	SET NOCOUNT,
    XACT_ABORT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	if (@ViajeID != '')
	begin

		select pm.menorid
		into #tblMenoresEnViaje
		from dbo.PasajeroMenor pm
		inner join Pasaje pj on pm.pasajeid = pj.PasajeID
		where pj.ViajeID = @ViajeID

		select
			   p.PersonaID,
			   p.Apellido,
			   p.Nombre,
			   p.NroDocumento
		from dbo.Persona p
		inner join dbo.Cliente c on c.ClienteID = p.PersonaID
		left join #tblMenoresEnViaje mv on p.PersonaID = mv.menorid
		where p.FechaNacimiento is not null
		and (cast((datediff(dd, p.FechaNacimiento , GETDATE()) + 1) / 365.25 as int))  < 5 --personas menores de 5 años
		and mv.menorid is null
		order by p.Apellido, p.Nombre


	end
END
GO

-- Verificación posterior (opcional):
-- SELECT name, modify_date FROM sys.procedures
-- WHERE name IN ('usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher','usp_MAT_Reserva_DetalleFactura',
--                'usp_MAT_Reserva_VincularMenorByViajeID','usp_MAT_Reserva_GetMenoresDisponibles');
