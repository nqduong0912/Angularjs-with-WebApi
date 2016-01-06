using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Text;
using System.Data;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.KeHoachNam
{
    public partial class LapKeHoachNam : PageBase
    {
        protected string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            base.InitForm("Lập kế hoạch năm", string.Empty, _doctypeid, 0);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}
