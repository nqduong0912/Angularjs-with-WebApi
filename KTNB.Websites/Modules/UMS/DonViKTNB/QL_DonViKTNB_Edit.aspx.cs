using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.UMS.DonViKTNB
{
    public partial class QL_DonViKTNB_Edit : PageBase
    {
        public string IdDonVi = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            IdDonVi = Request["id"];
            base.SetFormCaption("Thông tin Phòng Ban");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                
            }
        }
    }
}