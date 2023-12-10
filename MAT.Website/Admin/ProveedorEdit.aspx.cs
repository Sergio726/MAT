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

public partial class ProveedorEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "ProveedorEdit.aspx?{0}", ProveedorDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "ProveedorEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Proveedor.aspx");
		FormUtil.SetDefaultMode(FormView1, "ProveedorId");
	}
	protected void GridViewServicio1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ServicioId={0}", GridViewServicio1.SelectedDataKey.Values[0]);
		Response.Redirect("ServicioEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewExcursion2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ExcursionId={0}", GridViewExcursion2.SelectedDataKey.Values[0]);
		Response.Redirect("ExcursionEdit.aspx?" + urlParams, true);		
	}	
}


