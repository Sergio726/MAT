
/*
----------------------------------------------------------------------------------------------------

-- Created By: Reproisa (www.reproisa.com)
-- Purpose: Finds records in the MovimientoCuenta table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE [dbo].[_ProcedureMovimientoCuenta_Find]
(

	@SearchUsingOR bit   = null ,

	@MovimientoId uniqueidentifier   = null ,

	@PagoId uniqueidentifier   = null ,

	@FacturaId uniqueidentifier   = null ,

	@FechaRegistro datetime   = null ,

	@CuentaId uniqueidentifier   = null ,

	@NotaId uniqueidentifier   = null ,

	@CuentaCorrienteId uniqueidentifier   = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [MovimientoID]
	, [PagoID]
	, [FacturaID]
	, [FechaRegistro]
	, [CuentaID]
	, [NotaID]
	, [CuentaCorrienteID]
    FROM
	[dbo].[MovimientoCuenta]
    WHERE 
	 ([MovimientoID] = @MovimientoId OR @MovimientoId IS NULL)
	AND ([PagoID] = @PagoId OR @PagoId IS NULL)
	AND ([FacturaID] = @FacturaId OR @FacturaId IS NULL)
	AND ([FechaRegistro] = @FechaRegistro OR @FechaRegistro IS NULL)
	AND ([CuentaID] = @CuentaId OR @CuentaId IS NULL)
	AND ([NotaID] = @NotaId OR @NotaId IS NULL)
	AND ([CuentaCorrienteID] = @CuentaCorrienteId OR @CuentaCorrienteId IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [MovimientoID]
	, [PagoID]
	, [FacturaID]
	, [FechaRegistro]
	, [CuentaID]
	, [NotaID]
	, [CuentaCorrienteID]
    FROM
	[dbo].[MovimientoCuenta]
    WHERE 
	 ([MovimientoID] = @MovimientoId AND @MovimientoId is not null)
	OR ([PagoID] = @PagoId AND @PagoId is not null)
	OR ([FacturaID] = @FacturaId AND @FacturaId is not null)
	OR ([FechaRegistro] = @FechaRegistro AND @FechaRegistro is not null)
	OR ([CuentaID] = @CuentaId AND @CuentaId is not null)
	OR ([NotaID] = @NotaId AND @NotaId is not null)
	OR ([CuentaCorrienteID] = @CuentaCorrienteId AND @CuentaCorrienteId is not null)
	SELECT @@ROWCOUNT			
  END
				



