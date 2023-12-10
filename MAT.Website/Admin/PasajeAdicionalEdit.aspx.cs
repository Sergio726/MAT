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

public partial class PasajeAdicionalEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PasajeAdicionalEdit.aspx?{0}", PasajeAdicionalDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PasajeAdicionalEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "PasajeAdicional.aspx");
		FormUtil.SetDefaultMode(FormView1, "PasajeAdicionalId");
	}
}


