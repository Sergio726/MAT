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

public partial class PasajeroEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PasajeroEdit.aspx?{0}", PasajeroDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PasajeroEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Pasajero.aspx");
		FormUtil.SetDefaultMode(FormView1, "PasajeroId");
	}
	protected void GridViewPasaje1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PasajeId={0}", GridViewPasaje1.SelectedDataKey.Values[0]);
		Response.Redirect("PasajeEdit.aspx?" + urlParams, true);		
	}	
}


