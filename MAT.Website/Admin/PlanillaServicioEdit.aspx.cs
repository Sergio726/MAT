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

public partial class PlanillaServicioEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //FormUtil.RedirectAfterInsertUpdate(FormView1, "PlanillaServicioEdit.aspx?{0}", PlanillaServicioDataSource);
        FormUtil.RedirectAfterAddNew(FormView1, "PlanillaServicioEdit.aspx");
        FormUtil.RedirectAfterCancel(FormView1, "PlanillaServicio.aspx");
        FormUtil.SetDefaultMode(FormView1, "PlanillaServicioId");
    }
    protected void GridViewPlanillaServicioItem1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string urlParams = string.Format("PlanillaServicioItemId={0}", GridViewPlanillaServicioItem1.SelectedDataKey.Values[0]);
        Response.Redirect("PlanillaServicioItemEdit.aspx?" + urlParams, true);
    }
}


