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

public partial class LocalidadEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "LocalidadEdit.aspx?{0}", LocalidadDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "LocalidadEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Localidad.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
	protected void GridViewHotel1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("HotelId={0}", GridViewHotel1.SelectedDataKey.Values[0]);
		Response.Redirect("HotelEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPaquete2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteId={0}", GridViewPaquete2.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteEdit.aspx?" + urlParams, true);		
	}	
}


