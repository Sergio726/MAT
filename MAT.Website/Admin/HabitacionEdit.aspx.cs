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

public partial class HabitacionEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "HabitacionEdit.aspx?{0}", HabitacionDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "HabitacionEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Habitacion.aspx");
		FormUtil.SetDefaultMode(FormView1, "HabitacionId");
	}
	protected void GridViewReservaHabitacion1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ReservaHabitacionId={0}", GridViewReservaHabitacion1.SelectedDataKey.Values[0]);
		Response.Redirect("ReservaHabitacionEdit.aspx?" + urlParams, true);		
	}	
}


