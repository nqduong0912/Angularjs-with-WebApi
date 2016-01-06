using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_appcustom : PageBase
    {
        #region initiation page variables
        protected string _appicationid = string.Empty;
        protected string _componentid = string.Empty;
        protected string _componentname = string.Empty;
        protected string _roleselected = string.Empty;
        protected string _rolename = string.Empty;
        protected string _groupexclusive = string.Empty;
        protected string _groupinclude = string.Empty;
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
            _appicationid = Request["app"];
            _componentid = Request["id"];
            _componentname = Request["name"];
            _roleselected = Request["ctl00$FormContent$cboRole"];
            if (!string.IsNullOrEmpty(_roleselected))
                _rolename = CommonFunc.getRoleInfo(_roleselected, _objUserContext, _dbName).Tables[0].Rows[0]["name"].ToString();
            _groupexclusive = Request["ctl00$FormContent$cboGroupUnMapped"];
            _groupinclude = Request["ctl00$FormContent$cboGroupMapped"];

            #endregion

            #region action handler

            #endregion

            base.InitForm("Tùy chỉnh ứng dụng", "edit.gif", string.Empty, 0);

            #region client control event handler
            _btnCloseWindow.Visible = false;
            btnLoaiTru.Text = "Loại...";
            btnThemVao.Text = "Thêm...";
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
                this.lblComponentName.Text = _componentname;
                LoadRoles(_componentid);
            }
            if (!string.IsNullOrEmpty(_roleselected))
            {
                loadGroupUnmap(_roleselected);
                loadGroupMapped(_roleselected);
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadRoles
        /// </summary>
        /// <param name="componentid"></param>
        protected void LoadRoles(string componentid)
        {
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objcomp.getRoleOnComponent(componentid);
            if (!base.isValidDataSet(ds)) return;
            BindData2Combo(this.cboRole, ds, "Name", "FK_RoleID", "");
            this.cboRole.Items.Insert(0, new ListItem("", string.Empty));
            this.cboRole.SelectedIndex = 0;
        }
        /// <summary>
        /// loadGroupUnmap
        /// </summary>
        /// <param name="roleid"></param>
        protected void loadGroupUnmap(string roleid)
        {
            cboGroupUnMapped.Items.Clear();
            string condition = " AND PK_GroupID NOT IN (SELECT FK_GROUPID FROM T_COMPONENT_GROUP WHERE FK_ROLEID='" + roleid + "' AND FK_COMPONENTID='" + _componentid + "')";
            condition += " AND PK_GroupID NOT IN ('" + GROUPS.SYS_ADMIN + "','" + GROUPS.APP_ADMIN + "')";

            condition += " Order By Name";
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getList(condition, "PK_GroupID,Name+'.....'+Description as FullName");
            if (!base.isValidDataSet(ds)) return;
            BindData2Combo(cboGroupUnMapped, ds, "FullName", "PK_GroupID", "");
        }
        /// <summary>
        /// loadGroupMapped
        /// </summary>
        /// <param name="roleid"></param>
        protected void loadGroupMapped(string roleid)
        {
            cboGroupMapped.Items.Clear();
            string condition = " AND PK_GroupID IN (SELECT FK_GROUPID FROM T_COMPONENT_GROUP WHERE FK_ROLEID='" + roleid + "' AND FK_COMPONENTID='" + _componentid + "')";
            condition += " AND PK_GroupID NOT IN ('" + GROUPS.SYS_ADMIN + "','" + GROUPS.APP_ADMIN + "')";
            condition += " Order By Name";
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getList(condition, "PK_GroupID,Name+'.....'+Description as FullName");
            if (!base.isValidDataSet(ds)) return;
            BindData2Combo(cboGroupMapped, ds, "FullName", "PK_GroupID", "");
        }
        #endregion

        #region page button processing
        /// <summary>
        /// LoaiTruNhom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoaiTruNhom(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_groupexclusive)) return;
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            if (objcomp.mapComponentGroup(_componentid, _roleselected, _groupexclusive) == 0)
            {
                loadGroupUnmap(_roleselected);
                loadGroupMapped(_roleselected);
                string groupname = GetGroupName(_groupexclusive);
                string additionaldata1 = "Tùy chỉnh";
                string additionaldata2 = "Không cho phép vai trò " + _rolename + " tại chi nhánh " + groupname + " sử dụng ứng dụng " + _componentname;
                //CommonFunc.AddAuditLog(0, AUDITLOG.APPLICATION_CUSTOMIZE, additionaldata1, additionaldata2);
            }
            objcomp = null;
        }
        /// <summary>
        /// ThemNhom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ThemNhom(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_groupinclude)) return;
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            if (objcomp.deleteComponentGroup(_componentid, _roleselected, _groupinclude) == 0)
            {
                loadGroupUnmap(_roleselected);
                loadGroupMapped(_roleselected);
                string groupname = GetGroupName(_groupinclude);
                string additionaldata1 = "Tùy chỉnh";
                string additionaldata2 = "Cho phép vai trò " + _rolename + " tại chi nhánh " + groupname + " sử dụng ứng dụng " + _componentname;
                //CommonFunc.AddAuditLog(0, AUDITLOG.APPLICATION_CUSTOMIZE, additionaldata1, additionaldata2);
            }
            objcomp = null;
        }
        /// <summary>
        /// GetGroupName
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        protected string GetGroupName(string groupid)
        {
            bus_Group obj = bus_Group.Instance(_objUserContext);  //bus_Group obj = new bus_Group(_objUserContext, _dbName);
            DataSet ds = obj.getByID(groupid, "name");
            return ds.Tables[0].Rows[0]["name"].ToString();
        }
        #endregion
    }
}