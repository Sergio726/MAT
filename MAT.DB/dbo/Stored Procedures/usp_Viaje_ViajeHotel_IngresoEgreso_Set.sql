
/*=============================================================
 Created By: Garcia Sergio
 Comments: actualiza el ingreso y egreso a los hoteles
 
 History
 -------------------------------------------------------------- 
 2019-05-16		Garcia Sergio: add @Comentario
 2025-04-22     Garcia Sergio: set date from TransHotelHabitacionViaje
 ==============================================================*/
CREATE PROCEDURE [dbo].[usp_Viaje_ViajeHotel_IngresoEgreso_Set] (@ViajeHotelID varchar(max) = '',
																 @Desde varchar(10) = '',
																 @Hasta varchar(10) = '',
																 @HoraIngreso varchar(10) = '' ,
																 @HoraSalida varchar(10) = '',
																 @Comentario varchar(50) = ''
																 )
AS
SET NOCOUNT, XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

BEGIN
    BEGIN TRY
        BEGIN TRAN;

        IF( @ViajeHotelId IS NOT NULL )
        BEGIN
            UPDATE dbo.ViajeHotel
            SET    Desde = CASE WHEN @Desde = '' THEN Desde ELSE  @Desde END,
                    Hasta = CASE WHEN @Hasta = '' THEN Hasta ELSE @Hasta END,
                    HoraIngreso = CASE WHEN @HoraIngreso = '' THEN HoraIngreso ELSE @HoraIngreso END,
                    HoraSalida = CASE WHEN @HoraSalida = '' THEN HoraSalida ELSE @HoraSalida END,
					Comentario = CASE WHEN @Comentario = '' THEN Comentario ELSE @Comentario END
            WHERE  ViajeHotelID = CONVERT(UNIQUEIDENTIFIER, @ViajeHotelID)

            UPDATE th
            SET th.Fecha = CONVERT(datetime,@Desde,103)
            FROM dbo.TransHotelHabitacionViaje th
            INNER JOIN dbo.ViajeHotel vh on th.ViajeID = vh.ViajeID
            WHERE vh.ViajeHotelID  = CONVERT(UNIQUEIDENTIFIER, @ViajeHotelID)

            UPDATE rh
	        SET rh.Desde = CONVERT(datetime,@Desde,103),
		        rh.Hasta = CONVERT(datetime,@Hasta,103),
		        rh.HoraIngreso = @HoraIngreso,
		        rh.HoraSalida = @HoraSalida
            FROM   dbo.ReservaHabitacion rh 
	        inner join dbo.ViajeHotel vh
		    on rh.ViajeID = vh.ViajeID
            WHERE  vh.ViajeHotelID  = CONVERT(UNIQUEIDENTIFIER, @ViajeHotelID)

            SELECT 1                                          AS Id,
                    'Se Actualizaron los datos correctamente.' AS ErrorMsg
        END
        ELSE
        BEGIN
            SELECT 0                                                                  AS Id,
                    'No se ha seleccionado ningún hotel. Por favor intente nuevamente.' AS ErrorMsg
        END

        COMMIT TRAN;
    END TRY

    BEGIN CATCH
        IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

        DECLARE @errmsg   AS NVARCHAR (2048),
                @severity AS TINYINT,
                @state    AS TINYINT,
                @errno    AS INT,
                @proc     AS SYSNAME,
                @lineno   AS INT;

        SELECT @errmsg = Error_message(),
                @severity = Error_severity(),
                @state = Error_state(),
                @errno = Error_number(),
                @proc = Error_procedure(),
                @lineno = Error_line();

        IF @errno IS NULL
        RETURN;

        IF @errmsg NOT LIKE '***%'
        BEGIN
            SET @errmsg = '*** '
                            + COALESCE (Quotename(@proc), '<dynamic SQL>')
                            + ', ' + Ltrim(Str(@lineno)) + '. Errno '
                            + Ltrim(Str(@errno)) + ': ' + @errmsg;
        END

        SELECT -1      AS Id,
                @errmsg AS ErrorMsg
    END CATCH
END