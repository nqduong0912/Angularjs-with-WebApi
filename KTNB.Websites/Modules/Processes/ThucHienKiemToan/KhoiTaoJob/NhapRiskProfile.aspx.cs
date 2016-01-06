using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob
{
    public partial class NhapRiskProfile : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AuthorizeUserCtx();

            InitForm("Nhập Risk profile", string.Empty, string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
    }
}