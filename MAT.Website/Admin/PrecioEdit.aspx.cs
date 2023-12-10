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

public partial class PrecioEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PrecioEdit.aspx?{0}", PrecioDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PrecioEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Precio.aspx");
		FormUtil.SetDefaultMode(FormView1, "PrecioId");
	}
	protected void GridViewPaquetePrecio1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaquetePrecioId={0}", GridViewPaquetePrecio1.SelectedDataKey.Values[0]);
		Response.Redirect("PaquetePrecioEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPasaje2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PasajeId={0}", GridViewPasaje2.SelectedDataKey.Values[0]);
		Response.Redirect("PasajeEdit.aspx?" + urlParams, true);		
	}	
}


