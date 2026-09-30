CREATE PROCEDURE [dbo].[usp_MAT_Reserva_GetMenoresDisponibles] (@ViajeID varchar(max) = ''
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
