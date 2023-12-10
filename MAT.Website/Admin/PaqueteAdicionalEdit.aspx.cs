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

public partial class PaqueteAdicionalEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PaqueteAdicionalEdit.aspx?{0}", PaqueteAdicionalDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PaqueteAdicionalEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "PaqueteAdicional.aspx");
		FormUtil.SetDefaultMode(FormView1, "PaqueteAdicionalId");
	}
}


