using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Processes.ThucHienKiemToan.KhoiTaoJob
{
    public partial class KhoiTaoJob : PageBase
    {
        private string _id;
        private string _loaiDoiTuongKiemToanId;


        public string LoaiDoiTuongKiemToanId
        {
            get
            {
                if (_loaiDoiTuongKiemToanId == null)
                {
                    _loaiDoiTuongKiemToanId = Request["loaidoituongkiemtoanId"];
                }

                return _loaiDoiTuongKiemToanId;
            }
        }
        public string Id
        {
            get
            {
                if (_id == null)
                {
                    _id = Request["id"];
                }

                return _id;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AuthorizeUserCtx();

            InitForm("Đợt kiểm toán", string.Empty, string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
    }
}