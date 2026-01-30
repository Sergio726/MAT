
CREATE PROCEDURE [dbo].[usp_MAT_MonedaTipo_GetAll]
AS
/*===========================================================
	Author:    Garcia Sergio 
	Create date: 2018/03/07
	Description:  get all tipe monedatipo
 ===========================================================*/
begin
SET nocount, xact_abort ON; 
      SET TRANSACTION isolation level READ uncommitted; 
	  select mt.Id,
		     mt.Descripcion,
		     mt.Codigo
	  from dbo.MonedaTipo mt
end

