using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

public partial class _Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        base.AuthorizeUserCtx();
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
    }
}
