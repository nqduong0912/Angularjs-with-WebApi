using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class Modules_UMS_checkaccount : PageBase
    {
        protected string _accountname = string.Empty;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _accountname = Request["u"];

            bus_User objUser = new bus_User(_objUserContext, _dbName);
            DataSet ds = objUser.getList(" AND name='" + _accountname + "'", "name");
            if (base.isValidDataSet(ds))
            {
                Response.Write("exists");
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.Write("not_exists");
                Response.Flush();
                Response.End();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}