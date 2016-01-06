using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.CacDTKT
{
    public partial class QuyMoDTKT_Copy : PageBase
    {
        public string _idboquymo = string.Empty;
        public  dm_boquymo oldBoQuyMo = new dm_boquymo();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.SetFormCaption("Copy Bộ Quy Mô");
            _idboquymo = Request["id"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
    }
}