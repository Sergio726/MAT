
CREATE PROCEDURE [dbo].[usp_MAT_ListaEspera_SearchCliente]
(@param   VARCHAR(50)      = NULL, 
 @ViajeID UNIQUEIDENTIFIER
)
AS

/*-- ============================================= 
  -- Author:    Garcia Sergio 
  -- Create date: 23/03/2023
  -- Description:  search person by Name or DNI
23/03/2023
Garcia Sergio: create
  -- ============================================= */

     SET NOCOUNT, XACT_ABORT ON;
     SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    BEGIN
        DECLARE @countParam INT= 1;
        DECLARE @tmp TABLE(Value VARCHAR(100));
        SET @param = REPLACE(@param, '.', '');
        INSERT INTO @tmp
               SELECT s.Item
               FROM dbo.Split(@param, ' ') s;
        SELECT @countParam = COUNT(*)
        FROM @tmp;
        SELECT DISTINCT 
               p.PersonaID, 
               p.Apellido, 
               p.Nombre, 
               p.NroDocumento
        FROM dbo.Persona p
             INNER JOIN @tmp t ON p.FullName LIKE '%' + t.Value + '%'
                                  OR p.NroDocumentoCalc LIKE '' + t.Value + '%'
        WHERE dbo.fn_MAT_Pasaje_TieneConflictoFechaSalida(p.PersonaID, @ViajeID, NULL) = 0
        GROUP BY p.PersonaID, 
                 p.Apellido, 
                 p.Nombre, 
                 p.NroDocumento
        HAVING(COUNT(p.PersonaID) > 1
               AND @countParam > 1)
              OR (COUNT(p.PersonaID) > 0
                  AND @countParam = 1);
    END