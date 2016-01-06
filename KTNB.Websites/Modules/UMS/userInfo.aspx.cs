using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using System.Text;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_userInfo : PageBase
    {
        #region page variables
        protected string _userID = string.Empty;
        protected string _username = string.Empty;
        protected string roleID = string.Empty;
        protected string groupID = string.Empty;
        protected string parent_groupID = string.Empty;
        protected string _pgd = string.Empty;
        protected string _cn_submitted = string.Empty;
        protected string _action = string.Empty;
        protected string _docspace = string.Empty;
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.AuthorizeUserCtx();
            base.OnInit(e);

            #region get data submit
            _action = Request["act"];
            _userID = Request["id"];
            _username = Request["uname"];
            _docspace = Request["docspace"];
            _cn_submitted = Request["ctl00$FormContent$cboChiNhanh"];
            if (!string.IsNullOrEmpty(_userID))
            {
                groupID = getGroupOfUser(_userID);
                parent_groupID = CommonFunc.getParentGroupID(groupID, _objUserContext, _dbName);
            }
            #endregion

            #region init form
            base.InitForm("Người sử dụng", "userinfo.gif", string.Empty, VIEWTYPE.EDIT);
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "delete")
                    DeleteUser(_userID);
                else if (_action == "update")
                {
                    int n = Update_UserInfo();
                    if (n == 100)
                    {
                        Update_UserInfo_RoleGroup();
                        Response.Write("Trùng mã DAO. Thao tác không thành công.");
                        Response.Flush();
                        Response.End();
                    }
                    else if (n == -1)
                    {
                        Update_UserInfo_RoleGroup();
                        Response.Write("Có lỗi xảy ra trong quá trình cập nhật. Thao tác không thành công.");
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        Update_UserInfo_RoleGroup();
                        Response.Write("updated");
                        Response.Flush();
                        Response.End();
                    }
                }
                else if (_action == "getpgd")
                {
                    FeedBackClient(getlistPGD(_cn_submitted, _userID));
                }
                else if (_action == "kiemnhiem")
                {
                    string cnkn = Request["cnkn"];
                    string pgdkn = Request["pgdkn"];
                    string vtkn = Request["vtkn"];
                    if (pgdkn.Length == 36)
                        FeedBackClient(kiemnhiem(_userID, pgdkn, vtkn));
                    else
                        FeedBackClient(kiemnhiem(_userID, cnkn, vtkn));
                }
                else if (_action == "bokiemnhiem")
                {
                    string pgdkn = Request["pgdkn"];
                    if (pgdkn.Length == 36)
                        FeedBackClient(huykiemnhiem(_userID, pgdkn));
                }
            }
            #endregion

            #region event controller
            //if (ctxIsRole(ROLES.LANHDAO_TTD))
            //{
            //    _btnAddNew.Visible = false;
            //    _btnDelete.Visible = false;
            //    _btnEdit.Visible = false;
            //    btnResetPassword.Visible = false;
            //    _btnCloseWindow.Visible = true;
            //}
            else
            {
                btnResetPassword.Click += new EventHandler(ReSetPassword);
                btnResetPassword.Visible = false;
                _btnAddNew.Visible = true;
                _btnCloseWindow.Visible = true;
            }
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadChiNhanh();

            if (string.IsNullOrEmpty(_cn_submitted))
            {
                if (!string.IsNullOrEmpty(parent_groupID))
                    LoadPGD(parent_groupID);
                else
                    LoadPGD(groupID);
            }
            else
                LoadPGD(_cn_submitted);

            LoadRole();
            LoadUserDetail(_userID);
            getRoleGroupDelegated(_userID);
            LockControls();

            _btnAddNew.Attributes.Add("onclick", "{window.location='NewUser.aspx?docspace=" + _docspace + "';return false;}");
            _btnDelete.Attributes.Add("onclick", "{deleteuser('" + _userID + "','" + this.lblUserName.Text + "');return false;}");
            _btnEdit.Attributes.Add("onclick", "{updateuser();return false;}");

            //if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
            //    this.litKiemNhiem.Visible = false;
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
        /// <summary>
        /// ReSetPassword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReSetPassword(object sender, EventArgs e)
        {
            ReSetPassword(_userID);
        }
        /// <summary>
        /// ReSetPassword
        /// </summary>
        /// <param name="UserID"></param>
        protected void ReSetPassword(string UserID)
        {
            //bus_User objUser = new bus_User(_objUserContext, _dbName);
            //string newpwd = UserContext.Encrypt(PRIVACY.PASSWORD_DEFAULT, PRIVACY.);
            //objUser.AdminUpdateUser(UserID, string.Empty, string.Empty, newpwd, 0);
        }
        /// <summary>
        /// DeleteForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DeleteForm(object sender, EventArgs e)
        {
            //base.DeleteForm(sender, e);
            if (string.IsNullOrEmpty(_userID))
                return;

            DeleteUser(_userID);
            Response.Write("<script language='javascript'>");
            Response.Write("window.open('../Admin/UserManager.aspx?docspace=user&toc=new','fraDetail');");
            Response.Write("</script>");

        }
        /// <summary>
        /// EditForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void EditForm(object sender, EventArgs e)
        {
            Update_UserInfo();
            Update_UserInfo_RoleGroup();
            LoadUserDetail(_userID);
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadChiNhanh
        /// </summary>
        protected void LoadChiNhanh()
        {
            string condition = string.Empty;
            string query = "PK_GROUPID,NAME,NAME+'.....' + DESCRIPTION AS FULLNAME";
            if (_docspace.ToLower() == "sysuser")
                condition = " AND Type in (" + GROUPTYPE.SYS_ADMIN + "," + GROUPTYPE.APP_ADMIN + ")";
            else
            {
                condition = " And Type In (" + GROUPTYPE.CHI_NHANH + ")";
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    condition += " And PK_GroupID='" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
            }
            condition += " ORDER BY DESCRIPTION";

            bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);

            if (!base.isValidDataSet(ds))
                return;
            this.BindData2Combo(cboChiNhanh, ds, "FULLNAME", "PK_GROUPID", "");

            if (!string.IsNullOrEmpty(_cn_submitted))
                this.cboChiNhanh.SelectedValue = _cn_submitted;
            else
                if (!string.IsNullOrEmpty(parent_groupID))
                    this.cboChiNhanh.SelectedValue = parent_groupID;
                else
                    this.cboChiNhanh.SelectedValue = groupID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChiNhanh"></param>
        /// <returns></returns>
        protected string getlistPGD(string ChiNhanh, string userid)
        {
            string condition = " AND FK_PARENTGROUPID = '" + ChiNhanh + "'";
            condition += " and PK_GROUPID Not In (Select FK_GroupID From T_User_In_Group Where FK_UserID='" + userid + "' And IsGroupLeader=0)";
            condition += " ORDER BY DESCRIPTION";
            string query = "PK_GROUPID,NAME+'.....'+DESCRIPTION AS FULLNAME";
            bus_Group objGroup = bus_Group.Instance(_objUserContext);
            DataSet ds = objGroup.getList(condition, query);
            StringBuilder options = new StringBuilder();
            options.Append("<option value='' selected='true'></option>");
            if (isValidDataSet(ds))
            {
                foreach (DataRow R in ds.Tables[0].Rows)
                {
                    string option = "@FM<option value='" + R["PK_GROUPID"].ToString() + "'>" + R["FULLNAME"].ToString() + "</option>";
                    options.Append(option);
                }
            }
            return options.ToString();
        }
        /// <summary>
        /// LoadPGD
        /// </summary>
        /// <param name="ChiNhanh"></param>
        protected void LoadPGD(string ChiNhanh)
        {
            cboPGD.Items.Clear();
            string condition = string.Empty;

            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                condition = " AND FK_PARENTGROUPID = '" + vpb.app.business.ktnb.Definition.UMS.GROUPS.GROUP_OF_KTNB + "'";
            else
                condition = " AND FK_PARENTGROUPID = '" + ChiNhanh + "'";

            condition += " ORDER BY DESCRIPTION";

            string query = "PK_GROUPID,NAME+'.....'+DESCRIPTION AS FULLNAME";
            bus_Group objGroup = bus_Group.Instance(_objUserContext);
            DataSet ds = objGroup.getList(condition, query);
            if (!base.isValidDataSet(ds))
            {
                cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
                return;
            }
            this.BindData2Combo(cboPGD, ds, "FULLNAME", "PK_GROUPID", "");
            if (!string.IsNullOrEmpty(_pgd))
            {
                if (_pgd.ToUpper() != ChiNhanh.ToUpper())
                    cboPGD.SelectedValue = _pgd;
            }
            if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                cboPGD.SelectedValue = _m_groupid;
            else
            {
                cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));
                if (!string.IsNullOrEmpty(groupID))
                    this.cboPGD.SelectedValue = groupID;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected void getRoleGroupDelegated(string UserID)
        {
            cboDonViKN.Items.Clear();
            bus_User obj = bus_User.Instance(_objUserContext);
            DataSet ds = obj.getRoleGroupDelegated(UserID);
            obj = null;
            if (isValidDataSet(ds))
                foreach (DataRow R in ds.Tables[0].Rows)
                {
                    string id = R["fk_groupid"].ToString();
                    string name = R["groupname"].ToString() + " - " + R["rolename"].ToString();
                    this.cboDonViKN.Items.Add(new ListItem(name, id));
                }
        }
        /// <summary>
        /// LoadRole
        /// </summary>
        protected void LoadRole()
        {
            string query = "PK_ROLEID,NAME";
            string condition = " AND ISEXPIRED=0";

            if (_docspace == "appuser")
            {
                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    condition += " AND PK_ROLEID IN ('" + vpb.app.business.ktnb.Definition.UMS.ROLES.THANHVIEN_KIEMTOAN + "','" + vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_DONVI_KIEMTOAN + "')";
                else
                    condition += " AND PK_ROLEID NOT IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "')";
            }
            else if (_docspace.ToLower() == "sysuser")
                if (_m_roleID.ToUpper() == ROLES.SYS_ADMIN)
                    condition += " AND PK_ROLEID IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "')";
                else
                    condition += " AND PK_ROLEID IN ('" + ROLES.APP_ADMIN + "')";

            condition += " ORDER BY NAME";

            bus_Role objRole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objRole.getList(condition, query);
            if (!base.isValidDataSet(ds))
                return;

            this.BindData2Combo(cboVaiTro, ds, "NAME", "PK_ROLEID", string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string getGroupOfUser(string userID)
        {
            bus_User_In_Group objug = bus_User_In_Group.Instance(_objUserContext);
            string query = "pk_groupid";
            string condition = " and pk_userid='" + userID + "'";
            condition += " and isgroupleader=1";
            DataSet ds = objug.getList(condition, query);
            return ds.Tables[0].Rows[0]["pk_groupid"].ToString();
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
            string user_groupid = R["FK_GroupID"].ToString();

            bus_User_In_Role objusr = new bus_User_In_Role(_objUserContext, _dbName);
            DataSet vds = objusr.getList(" AND FK_UserID='" + _userID + "'", "FK_RoleID");
            if (!base.isValidDataSet(vds)) return;
            string roleid = vds.Tables[0].Rows[0]["FK_RoleID"].ToString();

            if (roleid.ToUpper().Equals(ROLES.SYS_ADMIN.ToUpper()))
                cboPGD.Items.Insert(0, new ListItem("ALL", "ALL"));

            if (roleID.Length == 36)
                this.cboVaiTro.SelectedValue = roleID;

            if (user_groupid.Length == 36)
                this.cboPGD.SelectedValue = user_groupid;
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
        /// <summary>
        /// DeleteUser
        /// </summary>
        /// <param name="userID"></param>
        protected void DeleteUser(string userID)
        {
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            objUser.deleteByID(userID);
            objUser = null;
            //CommonFunc.AddAuditLog(VPB_CRM.Definitions.DMS.TYPE_OF_OBJECT.USER, VPB_CRM.Definitions.OPERATORS.AUDITLOG.DELETE, "Xóa", "Xóa người sử dụng" + _username, _objUserContext);
            Response.Write("deleted");
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// Update_UserInfo
        /// </summary>
        protected int Update_UserInfo()
        {
            bus_User objUser = new bus_User(_objUserContext, _dbName);
            DataSet ds;

            ds = objUser.getByID(_userID, string.Empty);

            if ((ds == null) || (ds.Tables[0].Rows.Count == 0)) return -1;

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
            R["UserCode"] = UserCode;
            return objUser.saveDataSet(ds);
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
        /// LockControls
        /// </summary>
        protected void LockControls()
        {
            if (_docspace.ToLower() == "sysuser")
                this.cboPGD.Enabled = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="groupid"></param>
        /// <param name="roleid"></param>
        /// <returns></returns>
        protected string kiemnhiem(string userID, string groupid, string roleid)
        {
            bus_User obj = bus_User.Instance(_objUserContext);
            return obj.asignUserToMoreGroupRole(userID, groupid, roleid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        protected string huykiemnhiem(string userID, string groupid)
        {
            bus_User obj = bus_User.Instance(_objUserContext);
            obj.removeUserFromGroup(userID, groupid);
            return "";
        }
        #endregion

    }
}