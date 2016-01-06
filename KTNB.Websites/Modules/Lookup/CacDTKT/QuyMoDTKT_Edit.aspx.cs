using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.CacDTKT
{
    public partial class QuyMoDTKT_Edit : PageBase
    {
        public string _id = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _id = Request["id"];

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.SetFormCaption("Thông tin bộ quy mô");

            #endregion

            #region client control event handler

            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }
    }
}