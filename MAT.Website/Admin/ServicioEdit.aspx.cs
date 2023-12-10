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

public partial class ServicioEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "ServicioEdit.aspx?{0}", ServicioDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "ServicioEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Servicio.aspx");
		FormUtil.SetDefaultMode(FormView1, "ServicioId");
	}
	protected void GridViewPaqueteServicio1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteServicioId={0}", GridViewPaqueteServicio1.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteServicioEdit.aspx?" + urlParams, true);		
	}	
}


