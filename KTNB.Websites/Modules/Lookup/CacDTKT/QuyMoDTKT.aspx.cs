using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.Biz;
using KTNB.Extended.Dal;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.CacDTKT
{
    public partial class QuyMoDTKT : PageBase
    {
        protected string _documentid = string.Empty;
        protected string _doctypeid = DOCTYPE.QuyMoDTKT;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _documentid = Request["doc"];

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Quy mô đối tượng kiểm toán", string.Empty, _doctypeid, 0);

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