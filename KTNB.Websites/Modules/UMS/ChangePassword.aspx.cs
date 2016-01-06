using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

//using VPB_CRM.Helper.Constant;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_ChangePassword : PageBase
    {
        #region initiation page variables

        #endregion

        #region page init & load

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            base.InitForm("Thay đổi mật khẩu", "userman.png", string.Empty, 0);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.MatKhauCu.Focus();
            _btnCloseWindow.Visible = false;
        }

        #endregion

        #region page helper processing

        #endregion

        #region form button control

        protected void changePassword(object sender, EventArgs e)
        {
            string Query;
            string Condition="";
            string oldPass = this.MatKhauCu.Text;
            string newPass = this.MatKhauMoi.Text;

            Query = "PK_UserID,UserCode";
            //Condition = " AND PK_UserID='" + _objUserContext.UserID + "' AND Name='" + _objUserContext.UserName + "' AND Password='" + UserContext.Encrypt(oldPass, VPB_CRM.Helper.Constant.UMS.PASSWORD_SALT) + "'";
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            DataSet dsUser = objUser.getList(Condition, Query);

            if (!base.isValidDataSet(dsUser)) return;

            //newPass = UserContext.Encrypt(newPass, VPB_CRM.Helper.Constant.UMS.PASSWORD_SALT);

            string sql = "update T_USER set Password='" + newPass + "' where pk_userid='" + _objUserContext.UserID + "'";
            SqlConnection objcon = new SqlConnection(_objUserContext.ConnectionString);
            objcon.Open();

            if (objcon.State != ConnectionState.Open) return;

            SqlCommand objcom = new SqlCommand(sql, objcon);
            int n = objcom.ExecuteNonQuery();
            if (n == 1)
            {
                Session.Abandon();
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("window.open('../../SignIn.aspx','_top')");
                Response.Write("</script>");
            }

            return;
        }

        protected override void EditForm(object sender, EventArgs e)
        {
            //changePassword();
        }

        #endregion
    }
}