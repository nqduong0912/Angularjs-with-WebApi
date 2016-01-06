using C1.Web.C1WebGrid;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.KeHoachNam
{
    /// <summary>
    /// UC-KHN4: Xem danh sách các ĐTKT đã chọn
    /// </summary>
    public partial class XemLaiDoiTuong : PageBase
    {
        private const string _doctypeid = DOCTYPE.QUANLY_BOTIEUCHI_KEHOACHNAM;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.InitForm("Danh sách đối tượng kiểm toán được chọn cho kế hoạch năm", string.Empty, _doctypeid, 0);
        }
    }
}