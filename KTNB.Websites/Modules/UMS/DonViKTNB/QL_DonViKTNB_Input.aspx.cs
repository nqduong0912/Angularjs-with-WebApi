using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.UMS.DonViKTNB
{
    public partial class QL_DonViKTNB_Input : PageBase
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.SetFormCaption("Thêm mới Phòng Ban");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
            }
        }
    }
}