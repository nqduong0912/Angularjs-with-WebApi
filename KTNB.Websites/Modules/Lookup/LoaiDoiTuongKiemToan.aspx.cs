using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal.Lib;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class LoaiDoiTuongKiemToan : PageBase
    {
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.LOAI_DOITUONG_KT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _documentid = Request["doc"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            base.InitForm("Loại đối tượng kiểm toán", string.Empty, _doctypeid, 0);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
    }
}