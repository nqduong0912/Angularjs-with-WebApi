using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Text;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.KeHoachNam
{
    /// <summary>
    /// KHN-MH 5: Duyệt kế hoạch năm
    /// </summary>
    public partial class DuyetKeHoachNam : PageBase
    {
        private const string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.InitForm("Duyệt kế hoạch năm", string.Empty, _doctypeid, 0);
        }
    }
}
