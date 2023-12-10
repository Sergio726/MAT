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

public partial class PasajeEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PasajeEdit.aspx?{0}", PasajeDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PasajeEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Pasaje.aspx");
		FormUtil.SetDefaultMode(FormView1, "PasajeId");
	}
	protected void GridViewPasajeAdicional1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PasajeAdicionalId={0}", GridViewPasajeAdicional1.SelectedDataKey.Values[0]);
		Response.Redirect("PasajeAdicionalEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewReservaHabitacion2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ReservaHabitacionId={0}", GridViewReservaHabitacion2.SelectedDataKey.Values[0]);
		Response.Redirect("ReservaHabitacionEdit.aspx?" + urlParams, true);		
	}	
}


