using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_roleinfo : PageBase
    {
        DataSet dsRoleInfo = null;
        bus_Role objRole = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _objUserContext = (UserContext)Session["objUserContext"];
            _m_roleID = Request.QueryString["id"];
            base.InitForm("Kích hoạt vai trò", "userman.png", string.Empty, VIEWTYPE.EDIT); 
            objRole = new bus_Role(_objUserContext, _dbName);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _btnDelete.Visible = false;
            LoadRoleDetail(_m_roleID);
        }

        protected void LoadRoleDetail(string RoleID)
        {

            dsRoleInfo = objRole.getByID(RoleID, string.Empty);

            if (!base.isValidDataSet(dsRoleInfo))
                return;

            DataRow R = dsRoleInfo.Tables[0].Rows[0];

            this.FULLNAME.Text = R["NAME"].ToString();
            this.DESCRIPTION.Text = R["DESCRIPTION"].ToString();

            if ((Convert.ToInt32(R["ISEXPIRED"])) == 1)
                this.IsExpired.Checked = false;
            else
                this.IsExpired.Checked = true;

        }

        protected override void EditForm(object sender, EventArgs e)
        {
            //base.EditForm(sender, e);
            dsRoleInfo.Tables[0].Rows[0]["NAME"] = Request.Form["_ctl0:FormContent:FULLNAME"];
            dsRoleInfo.Tables[0].Rows[0]["DESCRIPTION"] = Request.Form["_ctl0:FormContent:DESCRIPTION"];
            byte isExpired = 1;
            if (!string.IsNullOrEmpty(Request.Form["_ctl0:FormContent:IsExpired"]))
                isExpired = 0;
            dsRoleInfo.Tables[0].Rows[0]["ISEXPIRED"] = isExpired;
            dsRoleInfo.Tables[0].Rows[0]["OrderIndex"] = DBNull.Value;

            if (objRole.saveDataSet(dsRoleInfo) == 0)
            {
                Response.Write("<script language='javascript'>");
                Response.Write("window.open('../Admin/UserManager.aspx?docspace=role&toc=new','fraDetail');");
                Response.Write("</script>");
            }
        }
    }
}