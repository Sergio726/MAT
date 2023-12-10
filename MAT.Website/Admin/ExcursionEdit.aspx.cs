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

public partial class ExcursionEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "ExcursionEdit.aspx?{0}", ExcursionDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "ExcursionEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Excursion.aspx");
		FormUtil.SetDefaultMode(FormView1, "ExcursionId");
	}
	protected void GridViewPaqueteExcursion1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PaqueteExcursionId={0}", GridViewPaqueteExcursion1.SelectedDataKey.Values[0]);
		Response.Redirect("PaqueteExcursionEdit.aspx?" + urlParams, true);		
	}	
}


