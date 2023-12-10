#region Imports...
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MAT.Web.UI;
#endregion

public partial class TransporteEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "TransporteEdit.aspx?{0}", TransporteDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "TransporteEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Transporte.aspx");
		FormUtil.SetDefaultMode(FormView1, "TransporteId");
	}
	protected void GridViewViaje1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ViajeId={0}", GridViewViaje1.SelectedDataKey.Values[0]);
		Response.Redirect("ViajeEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewServicio2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ServicioId={0}", GridViewServicio2.SelectedDataKey.Values[0]);
		Response.Redirect("ServicioEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewButaca3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ButacaId={0}", GridViewButaca3.SelectedDataKey.Values[0]);
		Response.Redirect("ButacaEdit.aspx?" + urlParams, true);		
	}	
}


