using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

public partial class AddMoreRoleGroup : PageBase
    {
        #region page variables
        protected string _userID = string.Empty;
        protected string _username = string.Empty;
        protected string roleID = string.Empty;
        protected string groupID = string.Empty;
        protected string _pgd = string.Empty;
        protected string _cn = string.Empty;
        protected string _action = string.Empty;
        protected string chinhanh = string.Empty;
        DataTable dt = new DataTable("MOREROLENAME");
    
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _userID = Request["id"];
            _username = Request["uname"];
            base.InitForm("Quyền kiêm nhiệm thêm", "userinfo.gif", string.Empty, VIEWTYPE.EDIT);
           
            dt.Columns.Add("RoleName", typeof(String));
            dt.Columns.Add("RoleID", typeof(String));
            dt.Columns.Add("GroupID", typeof(String));
            dt.Columns.Add("GroupName", typeof(String));
            roleID = Request["fk_roleid"];
            groupID = Request["fk_groupid"];
            chinhanh = Request.Form["ctl00$FormContent$cboChiNhanh"];
            string fk_parentgroupid = Request["fk_parentgroupid"];
            if ((groupID == "ALL") || string.IsNullOrEmpty(groupID))
                groupID = fk_parentgroupid;
            _action = Request["act"];
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "delete")
                {
                    DeleteMoreRole(_userID, Request["roleid"], Request["groupid"]);
                    loadMoreRole(_userID);
                }
                if (_action == "update")
                {
                    Update_MoreGroup(_userID,roleID,groupID);
                    loadMoreRole(_userID);
                    Response.Write("update");
                    Response.Flush();
                    Response.End();
                }
            }
            _btnDelete.Visible = false;
            _btnCloseWindow.Visible = true;
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {


            //if (!Page.IsPostBack)
            //{
                LoadChiNhanh();
                LoadRole();
                LoadUserDetail(_userID);
                loadMoreRole(_userID);
            //}
            _btnEdit.Attributes.Add("onclick", "{addmoreRole();return false;}");
            
        }

        
        #endregion

        #region form button control
        /// <summary>
        /// AddnewForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void AddnewForm(object sender, EventArgs e)
        {
            //base.AddnewForm(sender, e);
            Response.Redirect("NewUser.aspx");
        }
        ///// <summary>
        ///// ReSetPassword
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void ReSetPassword(object sender, EventArgs e)
        //{
        //    ReSetPassword(_userID);
        //}
        ///// <summary>
        ///// ReSetPassword
        ///// </summary>
        ///// <param name="UserID"></param>
        //protected void ReSetPassword(string UserID)
        //{
        //    bus_User objUser = new bus_User(_objUserContext, _dbName);
        //    string newpwd = UserContext.Encrypt(CORE.Helper.Constant.UMS.PASSWORD_DEFAULT, CORE.Helper.Constant.UMS.PASSWORD_SALT);
        //    objUser.AdminUpdateUser(UserID, string.Empty, string.Empty, newpwd, 0);
        //}
        ///// <summary>
        ///// DeleteForm
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected override void DeleteForm(object sender, EventArgs e)
        //{
        //    //base.DeleteForm(sender, e);
        //    if (string.IsNullOrEmpty(_userID))
        //        return;

        //    DeleteMoreRole(_userID,roleID,groupID);
        //    Response.Write("<script language='javascript'>");
        //    Response.Write("window.open('../MainView/MainViewer.aspx?docspace=user&toc=new','fraDetail');");
        //    Response.Write("</script>");

        //}
        ///// <summary>
        ///// EditForm
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected override void EditForm(object sender, EventArgs e)
        //{
        //    Update_UserInfo();
        //    Update_UserInfo_RoleGroup();
        //    LoadUserDetail(_userID);
        //}
        /// <summary>
        /// cboChiNhanh_SelectedIndexChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ChiNhanh = Request["ctl00$FormContent$cboChiNhanh"];
            LoadPGD(ChiNhanh);
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadChiNhanh
        /// </summary>
        protected void LoadChiNhanh()
        {
            string query = "PK_GROUPID,NAME,NAME+'.....'+DESCRIPTION AS FULLNAME";
            string condition = " AND Type Not In (" + GROUPTYPE.SYS_ADMIN + "," + GROUPTYPE.APP_ADMIN + ")";
            //condition += " AND NAME <> '" + ((Group)_objUserContext.Groups[0]).GroupName + "'";
            condition += " ORDER BY NAME";

            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);

            if (!base.isValidDataSet(ds))
                return;

            string CurGroupID = string.Empty;
            this.BindData2Combo(cboChiNhanh, ds, "FULLNAME", "PK_GROUPID", CurGroupID);

            DataSet vds = CommonFunc.getUserInfo(_userID, _objUserContext, _dbName);
            if (!base.isValidDataSet(vds)) return;
            _pgd = vds.Tables[0].Rows[0]["FK_GroupID"].ToString();
            bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            vds = objgrp.getList(" AND PK_GroupID='" + _pgd + "'", "FK_ParentGroupID");
            _cn = vds.Tables[0].Rows[0]["FK_ParentGroupID"].ToString();
            if (string.IsNullOrEmpty(_cn)) _cn = _pgd;
            if (!string.IsNullOrEmpty(_cn))
            {
                string CN = "";
                if (!Page.IsPostBack)
                     CN= _cn;
                else
                {
                    CN = chinhanh;
                }
                    
                this.cboChiNhanh.SelectedValue=CN;
                LoadPGD(CN);
            }
        }
        /// <summary>
        /// LoadPGD
        /// </summary>
        /// <param name="ChiNhanh"></param>
        protected void LoadPGD(string ChiNhanh)
        {
            cboPGD.Items.Clear();
            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(" AND FK_PARENTGROUPID = '" + ChiNhanh + "' ORDER BY NAME", "PK_GROUPID,NAME,NAME+'.....'+DESCRIPTION AS FULLNAME");
            if (!base.isValidDataSet(ds))
            {
                cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
                return;
            }
            string CurGroupID = string.Empty;
            this.BindData2Combo(cboPGD, ds, "FULLNAME", "PK_GROUPID", CurGroupID);
            if (!string.IsNullOrEmpty(_pgd))
            {
                if (!_pgd.Equals(ChiNhanh))
                    cboPGD.SelectedValue = _pgd;
            }
            cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        /// <summary>
        /// LoadRole
        /// </summary>
        protected void LoadRole()
        {
            string query = "PK_ROLEID,NAME";
            string condition = " AND PK_ROLEID NOT IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "') AND ISEXPIRED =0";
            condition += " ORDER BY NAME";
            bus_Role objRole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objRole.getList(condition, query);
            if (!base.isValidDataSet(ds))
                return;
            this.BindData2Combo(cboVaiTro, ds, "NAME", "PK_ROLEID", string.Empty);
        }
        /// <summary>
        /// LoadUserDetail
        /// </summary>
        /// <param name="userID"></param>
        protected void LoadUserDetail(string userID)
        {
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            DataSet ds = objUser.getByID(userID, string.Empty);

            if (!base.isValidDataSet(ds))
                return;

            DataRow R = ds.Tables[0].Rows[0];

            this.UserCode.Text = R["UserCode"].ToString();
            this.FULLNAME.Text = R["FULLNAME"].ToString();
            this.Email.Text = R["EMAIL"].ToString();
            this.MobilePhone.Text = R["MOBILEPHONE"].ToString();
            this.Description.Text = R["DESCRIPTION"].ToString();
            this.lblUserName.Text = R["NAME"].ToString();

            if ((Convert.ToInt32(R["ISEXPIRED"])) == 1)
                this.IsExpired.Checked = false;
            else
                this.IsExpired.Checked = true;

            roleID = R["FK_ROLEID"].ToString();
            groupID = R["FK_GROUPID"].ToString();

            bus_User_In_Role objusr = new bus_User_In_Role(_objUserContext, _dbName);
            DataSet vds = objusr.getList(" AND FK_UserID='" + _userID + "'", "FK_RoleID");
            if (!base.isValidDataSet(vds)) return;
            string roleid = vds.Tables[0].Rows[0]["FK_RoleID"].ToString();
            if (roleid.ToUpper().Equals(ROLES.SYS_ADMIN.ToUpper()))
            {
                //cboChiNhanh.Items.Insert(0, new ListItem("ALL", "ALL"));
                cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
            }

            if (!string.IsNullOrEmpty(groupID))
            {
                string parentgroupID = GetParentGroupID(groupID);
                if (!string.IsNullOrEmpty(parentgroupID))
                {
                    if (!Page.IsPostBack)
                    {
                        this.cboChiNhanh.SelectedValue = parentgroupID;
                    }
                    else
                        this.cboChiNhanh.SelectedValue = chinhanh;
                    this.cboPGD.SelectedValue = groupID;
                }
                else
                {

                    if (!Page.IsPostBack)
                    {
                        this.cboChiNhanh.SelectedValue = groupID;
                    }
                    else
                        this.cboChiNhanh.SelectedValue = chinhanh;
                }
            }
            if (roleID.Length == 36)
                this.cboVaiTro.SelectedValue = roleID;
        }

        /// <summary>
        /// loadMoreRole
        /// </summary>
        /// <param name="userID"></param>
        private void loadMoreRole(string _userID)
        {
            string Query = " FK_UserID,FK_RoleID,FK_GROUPID";
            string Condition = " AND FK_UserID = '" + _userID + "'";
            Condition += " AND ORDER_NUMBER = 1 ORDER BY FK_GROUPID ASC";

            bus_User_In_Role objuser = new bus_User_In_Role(_objUserContext, _dbName);
            DataSet ds = objuser.getList(Condition,Query);
            if (base.isValidDataSet(ds))
            {
                foreach (DataRow R in ds.Tables[0].Rows)
                {

                    string roleid = R["FK_RoleID"].ToString();
                    string groupid = R["FK_GROUPID"].ToString();
                    DataRow rownew = dt.NewRow();
                    rownew["RoleName"] = loadRoleName(roleid);
                    rownew["GroupName"] = loadrGroupName(groupid);
                    rownew["RoleID"] = roleid;
                    rownew["GroupID"] = groupid;
                    dt.Rows.Add(rownew);
                }
            }

            this.DataRole.DataSource = dt;
            this.DataRole.DataBind();
        }

        /// <summary>
        /// loadrGroupName
        /// </summary>
        /// <param name="userID"></param>
        private string loadrGroupName(string groupid)
        {
            bus_Group objgroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgroup.getByID(groupid, "NAME+'.....'+DESCRIPTION AS Name");
            objgroup = null;
            if (base.isValidDataSet(ds))
                return ds.Tables[0].Rows[0]["Name"].ToString();
            return string.Empty;
        }
        /// <summary>
        /// loadRoleName
        /// </summary>
        /// <param name="userID"></param>
        private string loadRoleName(string roleid)
        {
            bus_Role objrole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objrole.getByID(roleid, "Name");
            objrole = null;
            if (base.isValidDataSet(ds))
                return ds.Tables[0].Rows[0]["Name"].ToString();
            return string.Empty;
        }
        /// <summary>
        /// GetParentGroupID
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        protected string GetParentGroupID(string groupID)
        {
            bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getByID(groupID, "fk_parentgroupid");
            objgrp = null;
            if (!base.isValidDataSet(ds)) return string.Empty;
            return ds.Tables[0].Rows[0]["fk_parentgroupid"].ToString();
        }
        /// <summary>
        /// AddminUpdateUser
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="GroupID"></param>
        /// <param name="RoleID"></param>
        /// <param name="Password"></param>
        /// <param name="isExpired"></param>
        /// <author>dungnt</author>
        /// <createddate>2008</createddate>
        protected void AddminUpdateUser(string userID, string GroupID, string RoleID, string Password, byte isExpired)
        {
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            objUser.AdminUpdateUser(userID, GroupID, RoleID, Password, isExpired);
        }

        protected void delImg(object sender, GridViewRowEventArgs e)
        {
            DataControlRowType rowtype = e.Row.RowType;
            if (rowtype == DataControlRowType.DataRow)
            {
                string groupid = (string)this.DataRole.DataKeys[e.Row.RowIndex].Values["GroupID"].ToString();
                string roleid = (string)this.DataRole.DataKeys[e.Row.RowIndex].Values["RoleID"].ToString();
                Image img = (Image)e.Row.FindControl("imgDel") as Image;
                img.Attributes.Add("onclick", "{deleteMoreRole('" + _userID + "','" + groupid + "','" + roleid + "');return false;}");

            }
        }

        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userID"></param>
        protected void DeleteMoreRole(string userID, string roleid, string groupid)
        {
            
            bus_User_In_Role objUser = new bus_User_In_Role(_objUserContext, _dbName);
            string condition = " AND FK_UserID = '" + userID + "'";
            condition += " AND FK_RoleId = '" + roleid +"'";
            condition += " AND FK_GROUPID = '" + groupid + "'";
            objUser.delete(condition);
            objUser = null;
            
            //CommonFunc.AddAuditLog(string.Empty, 0, AUDIT.OPERATION_TYPE_DELETE, "Xóa", "Xóa người sử dụng " + _username, _objUserContext, _dbName);
            CommonFunc.AddAuditLog(0,0, "Xóa", "Xóa người sử dụng " + _username, _objUserContext);
            Response.Write("deleted");
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// Update_UserInfo
        /// </summary>
        protected void Update_UserInfo()
        {
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            DataSet ds;

            ds = objUser.getByID(_userID, string.Empty);

            if ((ds == null) || (ds.Tables[0].Rows.Count == 0)) return;

            DataRow R = ds.Tables[0].Rows[0];

            string UserCode = (string)Request["ucode"];
            string FullName = (string)Request["fullname"];
            string Email = (string)Request["email"];
            string MobilePhone = (string)Request["mphone"];
            string Description = (string)Request["desc"];

            string exp;
            exp = Request["exp"];

            if (string.IsNullOrEmpty(exp)) exp = "0";

            R["FullName"] = FullName;
            R["Email"] = Email;
            R["MobilePhone"] = MobilePhone;
            R["Description"] = Description;
            R["IsExpired"] = Convert.ToByte(exp);
            objUser.saveDataSet(ds);
        }
        /// <summary>
        /// Update_UserInfo_RoleGroup
        /// </summary>
        protected void Update_UserInfo_RoleGroup()
        {
            bus_User objuser = new bus_User(_objUserContext, _dbName);
            string fk_parentgroupid = Request["fk_parentgroupid"];
            string fk_groupid = Request["fk_groupid"];
            string fk_roleid = Request["fk_roleid"];
            if ((fk_groupid == "ALL") || (string.IsNullOrEmpty(fk_groupid)))
                fk_groupid = fk_parentgroupid;
            objuser.AdminUpdateUser(_userID, fk_groupid, fk_roleid, string.Empty, 0);
        }

    /// <summary>
    /// Update moreRoleGroup
    /// </summary>
        protected void Update_MoreGroup(string userid,string roleid,string group)
         {
            bus_User objuser = new bus_User(_objUserContext,_dbName);
            bus_User_In_Role objuserinrole = new bus_User_In_Role(_objUserContext, _dbName);
            string condition = " AND FK_UserID = '" + userid + "'";
            condition += " AND FK_RoleId = '" + roleid + "'";
            condition += " AND FK_GROUPID = '" + group + "'";
            DataSet ds = objuserinrole.getList(condition, "FK_UserID");
            
            if (base.isValidDataSet(ds))
            {
                Response.Write("error");
                Response.Flush();
                Response.End();
            }
            else
            {
                objuser.asignUserToMoreGroupRole(userid, groupID, roleID);
                Response.Write("updated");
                Response.Flush();
                Response.End();
            }
        }
        #endregion

        protected void cboPGD_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.End();
        }
    }
