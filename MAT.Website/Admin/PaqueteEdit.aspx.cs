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

public partial class PaqueteEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PaqueteEdit.aspx?{0}", PaqueteDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PaqueteEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Paquete.aspx");
		FormUtil.SetDefaultMode(FormView1, "PaqueteId");
	}
	protected void GridViewPaquetePrecio1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaquetePrecioId={0}", GridViewPaquetePrecio1.SelectedDataKey.Values[0]);
		Response.Redirect("PaquetePrecioEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPaqueteAdicional2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteAdicionalId={0}", GridViewPaqueteAdicional2.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteAdicionalEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPaqueteExcursion3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteExcursionId={0}", GridViewPaqueteExcursion3.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteExcursionEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewViaje4_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ViajeId={0}", GridViewViaje4.SelectedDataKey.Values[0]);
		Response.Redirect("ViajeEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPaqueteServicio5_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteServicioId={0}", GridViewPaqueteServicio5.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteServicioEdit.aspx?" + urlParams, true);		
	}	
}


