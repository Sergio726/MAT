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

public partial class DepartamentoEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "DepartamentoEdit.aspx?{0}", DepartamentoDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "DepartamentoEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Departamento.aspx");
		FormUtil.SetDefaultMode(FormView1, "Id");
	}
}


