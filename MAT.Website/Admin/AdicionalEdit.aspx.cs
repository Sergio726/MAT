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

public partial class AdicionalEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "AdicionalEdit.aspx?{0}", AdicionalDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "AdicionalEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Adicional.aspx");
		FormUtil.SetDefaultMode(FormView1, "AdicionalId");
	}
	protected void GridViewPasajeAdicional1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PasajeAdicionalId={0}", GridViewPasajeAdicional1.SelectedDataKey.Values[0]);
		Response.Redirect("PasajeAdicionalEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPaqueteAdicional2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteAdicionalId={0}", GridViewPaqueteAdicional2.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteAdicionalEdit.aspx?" + urlParams, true);		
	}	
}


