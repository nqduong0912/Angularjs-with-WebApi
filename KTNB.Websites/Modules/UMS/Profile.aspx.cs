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

using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_Profile : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _objUserContext = (UserContext)Session["objUserContext"];
            base.InitForm("Quản lý cá nhân", "userman.png", string.Empty, 0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.changePwd.Attributes.Add("onclick", "{do_changepwd();return false;}");
            this.changeInfo.Attributes.Add("onclick", "{do_changeinfo();return false;}");
        }
    }
}