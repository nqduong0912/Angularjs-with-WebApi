using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Biz;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob
{
    public partial class DotKiemToan : PageBase
    {
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            base.InitForm("Đợt kiểm toán", string.Empty, string.Empty, 0);
        }
    }
}