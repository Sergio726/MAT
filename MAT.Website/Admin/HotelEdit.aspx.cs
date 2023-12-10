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

public partial class HotelEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "HotelEdit.aspx?{0}", HotelDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "HotelEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Hotel.aspx");
		FormUtil.SetDefaultMode(FormView1, "HotelId");
	}
	protected void GridViewServicio1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ServicioId={0}", GridViewServicio1.SelectedDataKey.Values[0]);
		Response.Redirect("ServicioEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewHabitacion2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("HabitacionId={0}", GridViewHabitacion2.SelectedDataKey.Values[0]);
		Response.Redirect("HabitacionEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewViajeHotel3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("ViajeHotelId={0}", GridViewViajeHotel3.SelectedDataKey.Values[0]);
		Response.Redirect("ViajeHotelEdit.aspx?" + urlParams, true);		
	}	
}


