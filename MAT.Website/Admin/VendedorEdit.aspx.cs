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

public partial class VendedorEdit : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{		
		FormUtil.RedirectAfterInsertUpdate(FormView1, "VendedorEdit.aspx?{0}", VendedorDataSource);
		FormUtil.RedirectAfterAddNew(FormView1, "VendedorEdit.aspx");
		FormUtil.RedirectAfterCancel(FormView1, "Vendedor.aspx");
		FormUtil.SetDefaultMode(FormView1, "VendedorId");
	}
	protected void GridViewFactura1_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("FacturaId={0}", GridViewFactura1.SelectedDataKey.Values[0]);
		Response.Redirect("FacturaEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewNota2_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("NotaId={0}", GridViewNota2.SelectedDataKey.Values[0]);
		Response.Redirect("NotaEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewPago3_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("PagoId={0}", GridViewPago3.SelectedDataKey.Values[0]);
		Response.Redirect("PagoEdit.aspx?" + urlParams, true);		
	}	
	protected void GridViewVoucher4_SelectedIndexChanged(object sender, EventArgs e)
	{
		string urlParams = string.Format("VoucherId={0}", GridViewVoucher4.SelectedDataKey.Values[0]);
		Response.Redirect("VoucherEdit.aspx?" + urlParams, true);		
	}	
}


