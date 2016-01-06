using System;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Share
{
    public partial class Share_VPB : System.Web.UI.MasterPage
    {
        public UserContext obj;
        Role role;
        protected string username;
        protected string usercode;
        protected string rolename;
        protected string groupname;
        protected string groupdesc;
        private string _appID;

        protected override void OnInit(EventArgs e)
        {
            if (Session["objUserContext"] == null)
            {
                Response.Redirect("~/SignIn.aspx");
            }

            base.OnInit(e);

            obj = (UserContext)Session["objUserContext"];
            obj.ProjectID = "F8CD07D8-F7F4-4D81-9D2F-FD22B5B2F7BE";
            Session["objUserContext"] = obj;
            try
            {
                role = (Role)obj.Roles[0];
            }
            catch (Exception ex)
            {
                FormHelper.FormWarning("Core Warning", ex.Message + ". " + ex.Source, "red");
                Response.End();
            }

            username = obj.UserName;
            usercode = obj.UserCode;
            rolename = ((CORE.CoreObjectContext.Role)obj.Roles[0]).RoleName;
            groupname = ((CORE.CoreObjectContext.Group)obj.Groups[0]).GroupName;
            groupdesc = ((CORE.CoreObjectContext.Group)obj.Groups[0]).GroupDescription;

            this.WhoIsLogin.Text = username;
            if (!string.IsNullOrEmpty(usercode))
            {
                Page.Title = ConfigurationManager.AppSettings["ProjectName"] + " | " + username + " | " + usercode + " | " + rolename + " | " + CORE.HELPERS.StringHelper.Right(groupname, 3) + " | " + groupdesc;
            }
            else
            {
                Page.Title = ConfigurationManager.AppSettings["ProjectName"] + " | " + username + " | " + rolename + " | " + CORE.HELPERS.StringHelper.Right(groupname, 3) + " | " + groupdesc;
            }

            MenuHelper objMenu = new MenuHelper();
            try
            {
                _appID = Request.QueryString["a"].ToString();
            }
            catch
            {
                _appID = string.Empty;
            }

            //Load menu
            XMenuHelper XobjMenu = new XMenuHelper();
            string _componentId = Request.QueryString["c"] == null ? string.Empty : Request.QueryString["c"].ToString();
            string _curApp = Request.QueryString["curApp"] == null ? _appID : Request.QueryString["curApp"].ToString();

            this.barmenu.Text = objMenu.LoadMenuBar(_curApp);
            loadRoleGroupPopUp();
            this.boxmenu.Text = XobjMenu.LoadMenuBox(_curApp, _appID, _componentId, "Red");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Load Role
        /// </summary>
        private void loadRoleGroupPopUp()
        {
            bus_User_In_Role objurg = bus_User_In_Role.Instance(obj);
            DataSet ds = objurg.getList_MultiRoleGroup(obj.UserID);
            objurg = null;
            rptRole.DataSource = ds.Tables[0].Rows;
            rptRole.DataBind();
            //foreach (DataRow R in ds.Tables[0].Rows)
            //{
            //    DevExpress.Web.ASPxMenu.MenuItem aItem = new DevExpress.Web.ASPxMenu.MenuItem();
            //    aItem.Text = R["RoleName"].ToString() + " (" + R["GroupName"].ToString() + ")";
            //    if (R["FK_RoleID"].ToString().ToUpper() == ((Role)obj.Roles[0]).RoleID.ToUpper() && R["FK_GroupID"].ToString().ToUpper() == ((Group)obj.Groups[0]).GroupID.ToUpper())
            //        aItem.Checked = true;
            //    else
            //        aItem.NavigateUrl = "javascript:switchToRole('" + R["FK_RoleID"].ToString() + "','" + R["FK_GroupID"].ToString() + "')";
            //    ASPxPopupMenu1.Items.Add(aItem);
            //}
        }

        protected void rptRole_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRow row = (DataRow)e.Item.DataItem;
                Literal lblRole = (Literal)e.Item.FindControl("lblRole");
                string text = row["RoleName"].ToString() + " (" + row["GroupName"].ToString() + ")";
                if (row["FK_RoleID"].ToString().ToUpper() == ((Role)obj.Roles[0]).RoleID.ToUpper() && row["FK_GroupID"].ToString().ToUpper() == ((Group)obj.Groups[0]).GroupID.ToUpper())
                {
                    lblRole.Text = "<a tabindex='-1' href='javascript:;' style='background: #007338'>" + text + "</a></li>";
                }
                else
                {
                    lblRole.Text = "<a tabindex='-1' href='javascript:;' onclick=\"javascript:switchToRole('" + row["FK_RoleID"].ToString() + "','" + row["FK_GroupID"].ToString() + "')\">" + text + "</a></li>";
                }
            }
        }

    }
}