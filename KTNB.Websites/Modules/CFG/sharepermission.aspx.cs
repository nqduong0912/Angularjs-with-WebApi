using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_sharepermission : PageBase
    {
        #region initiation page variables
        protected string _componentid = string.Empty;
        protected string _componentname = string.Empty;
        protected string _applicationid = string.Empty;
        protected string _action = string.Empty;
        protected string _groupassignment = string.Empty;
        protected string _roleassignment = string.Empty;
        protected string _userid = string.Empty;
        protected byte _permission = 0;
        protected string _finisheddate = string.Empty;
        protected byte _permissionstatus = 0;
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

            #region get data submit
            _action = Request["act"];

            _componentid = Request["id"];

            _componentname = CommonFunc.GetComponentName(_componentid, _objUserContext, _dbName);

            _applicationid = Request["app"];
            if (!string.IsNullOrEmpty(_applicationid)) _applicationid = _applicationid.ToUpper();

            _groupassignment = Request["ctl00$FormContent$cboCompany"];

            _roleassignment = Request["ctl00$FormContent$cboRole"];

            _finisheddate = Request["fdate"];

            if (!string.IsNullOrEmpty(Request["sta"]))
                _permissionstatus = Convert.ToByte(Request["sta"]);
            if (_permissionstatus == 0)
                _permissionstatus = CORE.WFS.Const.ACTIVITY_INSTANCE_STATUS_FINISHED;

            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if ((_action == "setpermission") || (_action == "changepermission"))
                {
                    _userid = Request["user"];
                    if (!string.IsNullOrEmpty(Request["permission"]))
                        _permission = Convert.ToByte(Request["permission"]);

                    int errorcode = -1;
                    if (_action == "setpermission")
                    {
                        errorcode = CommonFunc.SetPermissionOnObject(_userid, TYPE_OF_APPLICANT.USER, _componentid, TYPE_OF_OBJECT.COMPONENT, _permission, DBNull.Value.ToString(), _objUserContext.UserName, _finisheddate, _permissionstatus, _objUserContext, _dbName);
                        if (errorcode == 0)
                        {
                            //add message notify
                            CommonFunc.AddMessNotify(_userid, "Bạn có công việc mới: " + _componentname, _componentid, _objUserContext, _dbName);
                        }
                    }
                    else if (_action == "changepermission")
                    {
                        errorcode = CommonFunc.changePermissionOnObject(_userid, TYPE_OF_APPLICANT.USER, _componentid, TYPE_OF_OBJECT.COMPONENT, _permission, _objUserContext, _dbName);
                        if (errorcode == 0)
                            if (_permission == 0)
                            {
                                //delete message notify
                                CommonFunc.DelMessNotify(_userid, _componentid, _objUserContext, _dbName);
                            }
                    }
                    string username_assigned = CommonFunc.getUserInfo(_userid, _objUserContext, _dbName).Tables[0].Rows[0]["name"].ToString();
                    string additionaldata1;
                    string additionaldata2;
                    if (_permission > 0)
                    {
                        additionaldata1 = "Ủy quyền";
                        additionaldata2 = "Ủy quyền thực hiện ứng dụng " + _componentname + " cho " + username_assigned;
                    }
                    else
                    {
                        additionaldata1 = "Thu hồi ủy quyền";
                        additionaldata2 = "Thu hồi Ủy quyền thực hiện ứng dụng " + _componentname + " của " + username_assigned;
                    }
                    //CommonFunc.AddAuditLog(0, AUDITLOG.APPLICATION_UYQUYEN, additionaldata1, additionaldata2);

                    Response.Write(errorcode.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            #endregion

            base.InitForm("Ủy quyền", "001_57.gif", string.Empty, 0);

            #region client control event handler
            _btnSharePermission.Visible = true;
            _btnSharePermission.Attributes.Add("onclick", "{uyquyen(); return false;}");
            this.lblComponentName.Text = _componentname.ToUpper();
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadChiNhanh();
                LoadRole();
                this.txtFinishedDate.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            if ((!string.IsNullOrEmpty(_roleassignment)) && (!string.IsNullOrEmpty(_groupassignment)))
                LoadUserInRoleInGroup(_roleassignment, _groupassignment);
            LockControls();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadChiNhanh
        /// </summary>
        protected void LoadChiNhanh()
        {
            string query = "PK_GROUPID,NAME,NAME+'.....'+DESCRIPTION AS FULLNAME";
            string condition = " AND PK_GROUPID NOT IN ('" + GROUPS.SYS_ADMIN + "','" + GROUPS.APP_ADMIN + "')";
            condition += " ORDER BY NAME";
            bus_Group objGroup = bus_Group.Instance(_objUserContext);  //bus_Group objGroup = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objGroup.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;
            base.BindData2Combo(this.cboCompany, ds, "FullName", "pk_groupid", "");
        }
        /// <summary>
        /// LoadRole
        /// </summary>
        protected void LoadRole()
        {
            string query = "PK_ROLEID,NAME";
            string condition = " AND ISEXPIRED=0 AND PK_ROLEID NOT IN ('" + ROLES.SYS_ADMIN + "','" + ROLES.APP_ADMIN + "')";
            condition += " ORDER BY NAME";

            bus_Role objRole = bus_Role.Instance(_objUserContext);  //bus_Role objRole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objRole.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;
            base.BindData2Combo(cboRole, ds, "NAME", "PK_ROLEID", string.Empty);
            this.cboRole.Items.Insert(0, new ListItem("", ""));
            this.cboRole.SelectedIndex = 0;
        }
        /// <summary>
        /// LoadUserInRoleInGroup
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="groupid"></param>
        protected void LoadUserInRoleInGroup(string roleid, string groupid)
        {
            this.cboUser.Items.Clear();
            string condition = " and fk_roleid='" + roleid + "' and fk_groupid='" + groupid + "'";
            string query = "fk_userid,username+'.....'+fullname as fullname";
            condition += " and fk_userid not in(select fk_appliedonid from t_permission where fk_appliedonobjectid='" + _componentid + "')";
            condition += " order by rolename";
            bus_User objuser = bus_User.Instance(_objUserContext);  //bus_User objuser = new bus_User(_objUserContext, _dbName);
            DataSet ds = objuser.GetUserNGroupByRole(condition, query);
            objuser = null;
            if (!base.isValidDataSet(ds)) return;
            base.BindData2Combo(cboUser, ds, "fullname", "fk_userid", "");
        }
        /// <summary>
        /// LockControls
        /// </summary>
        protected void LockControls()
        {
            //if (_applicationid.ToUpper() == VPB_CRM.Helper.APPLICATION.T24REPORT.ID)
            //    this.chkUpload.Enabled = false;
        }
        #endregion

        #region page button processing

        #endregion
    }
}