using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    /// <summary>
    /// Invoke template: Process.aspx?action=...&component=...&role=...&companies=
    /// </summary>
    public partial class Modules_CFG_Process : PageBase
    {
        #region initiation page variables

        protected const byte ASSIGN_TO_IBPS = 1;
        protected const byte REMOVE_FFROM_IBPS = 2;
        protected const byte SET_PERMISSION_ON_COMPONENT = 11;
        protected const byte GET_PERMISSION_ON_COMPONENT = 12;
        protected const byte CHECK_ROLE_ASSIGNED_ON_COMPONENT = 13;
        protected const byte DISABLE_ROLE_ON_COMPONENT = 14;
        protected const byte ENABLE_ROLE_ON_COMPONENT = 15;

        protected byte _action = 0;
        protected string _component = string.Empty;
        protected string _roleassignedname = string.Empty;
        protected string _role = string.Empty;
        protected string _companies = string.Empty;
        protected byte _permission = 0;
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

            _action = Convert.ToByte(Request["action"]);
            _component = Request["component"];
            _roleassignedname = Request["roleassignedname"];
            _role = Request["role"];
            _companies = Request["companies"];
            _permission = Convert.ToByte(Request["permission"]);

            if (!string.IsNullOrEmpty(_companies))
                if (StringHelper.Right(_companies, 1).Equals(","))
                    _companies = StringHelper.Left(_companies, _companies.Length - 1);

            if (_action == ASSIGN_TO_IBPS)
            {
                AppendGroupToComponentRole(_companies, _component, _role);
            }
            else if (_action == REMOVE_FFROM_IBPS)
            {
                RemoveGroupFromComponentRole(_companies, _component, _role);
            }
            else if (_action == SET_PERMISSION_ON_COMPONENT)
            {
                SetPermissionOnObject(_component, _role, _permission);
            }
            else if (_action == GET_PERMISSION_ON_COMPONENT)
            {
                getPermissionOnObject(_component, _role);
            }
            else if (_action == CHECK_ROLE_ASSIGNED_ON_COMPONENT)
            {
                CheckRoleAssignedOnComponent(_component, _role);
            }
            else if (_action == DISABLE_ROLE_ON_COMPONENT)
            {
                DisableRoleOnComponent(_component, _role);
            }
            else if (_action == ENABLE_ROLE_ON_COMPONENT)
            {
                EnableRoleOnComponent(_component, _role);
            }

        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// AppendGroupToComponentRole
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void AppendGroupToComponentRole(string companies, string component, string role)
        {
            bus_Component objComp = bus_Component.Instance(_objUserContext);  //bus_Component objComp = new bus_Component(_objUserContext, _dbName);
            int err = objComp.appendGroup(component, role, companies);
            objComp = null;
            base.FeedBackClient(err.ToString());
        }
        /// <summary>
        /// RemoveGroupFromComponentRole
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void RemoveGroupFromComponentRole(string companies, string component, string role)
        {
            string groupassigned = GetGroupAssignedOnComponentRole(component, role);
            if (string.IsNullOrEmpty(groupassigned)) base.FeedBackClient("ok");
            string[] a = companies.Split(new char[1] { ',' });
            foreach (string s in a)
            {
                groupassigned = groupassigned.Replace(s, "");
            }
            a = groupassigned.Split(new char[1] { ',' });
            groupassigned = "";
            foreach (string s in a)
            {
                if (!string.IsNullOrEmpty(s)) groupassigned += "," + s;
            }
            //update (after removed)
            bus_Component objComp = bus_Component.Instance(_objUserContext);  //bus_Component objComp = new bus_Component(_objUserContext, _dbName);
            int err = objComp.updateGroup(component, role, groupassigned);
            objComp = null;
            base.FeedBackClient(err.ToString());
        }
        /// <summary>
        /// GetGroupAssignedOnComponentRole
        /// </summary>
        /// <param name="component"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        protected string GetGroupAssignedOnComponentRole(string component, string role)
        {
            bus_Component objComp = bus_Component.Instance(_objUserContext);  //bus_Component objComp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objComp.getListComponentRole(" AND FK_RoleID='" + role + "' AND FK_ComponentID='" + component + "'", "FK_GroupID");
            if (!base.isValidDataSet(ds)) return string.Empty;
            string groups = ds.Tables[0].Rows[0]["FK_GroupID"].ToString();
            return groups;
        }
        /// <summary>
        /// SetPermissionOnObject
        /// </summary>
        /// <param name="componentid"></param>
        /// <param name="roleid"></param>
        /// <param name="permission"></param>
        protected void SetPermissionOnObject(string componentid, string roleid, byte permission)
        {
            System.Threading.Thread.Sleep(200);
            bus_Permission objpermission = bus_Permission.Instance(_objUserContext);  //bus_Permission objpermission = new bus_Permission(_objUserContext, _dbName);
            string additionaldata1 = "Phân quyền";
            string additionaldata2 = "Phân quyền ứng dụng " + " cho vai trò " + _roleassignedname;
            if (objpermission.updatePermissionOnObject(roleid, TYPE_OF_APPLICANT.ROLE, componentid, TYPE_OF_OBJECT.COMPONENT, permission, DBNull.Value.ToString(), _objUserContext.UserName, "01/01/3000", 0) == 0)
            {
                base.FeedBackClient("success");
            }
        }
        /// <summary>
        /// getPermissionOnObject
        /// </summary>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void getPermissionOnObject(string component, string role)
        {
            System.Threading.Thread.Sleep(200);
            bus_Permission objpermission = bus_Permission.Instance(_objUserContext);  //bus_Permission objpermission = new bus_Permission(_objUserContext, _dbName);
            int permission;
            try
            {
                permission = objpermission.getPermissionOnObject(component, role);
            }
            catch (Exception ex)
            {
                permission = 0;
            }

            base.FeedBackClient(permission.ToString());
        }
        /// <summary>
        /// CheckRoleAssignedOnComponent
        /// </summary>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void CheckRoleAssignedOnComponent(string component, string role)
        {
            string mess = "unassigned";
            if (CommonFunc.IsRoleAssignedOnComponent(component, role, _objUserContext, _dbName))
                mess = "assigned";
            base.FeedBackClient(mess);
        }
        /// <summary>
        /// DisableRoleOnComponent
        /// </summary>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void DisableRoleOnComponent(string component, string role)
        {
            if (CommonFunc.deleteRoleFromComponent(component, role, _objUserContext, _dbName) == 0)
                base.FeedBackClient("disabled");
            else
                base.FeedBackClient("error");
        }
        /// <summary>
        /// EnableRoleOnComponent
        /// </summary>
        /// <param name="component"></param>
        /// <param name="role"></param>
        protected void EnableRoleOnComponent(string component, string role)
        {
            if (CommonFunc.AddRoleToComponent(component, role, _objUserContext, _dbName) == 0)
                base.FeedBackClient("enabled");
            else
                base.FeedBackClient("error");
        }
        #endregion

        #region page button processing

        #endregion
    }
}