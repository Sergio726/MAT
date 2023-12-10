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

public partial class PrecioHabitacionEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "PrecioHabitacionEdit.aspx?{0}", PrecioHabitacionDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "PrecioHabitacionEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "PrecioHabitacion.aspx");
		FormUtil.SetDefaultMode(FormView1, "PrecioHabitacionId");
	}
}


