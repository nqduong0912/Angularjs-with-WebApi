using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_apppermission : PageBase
    {
        #region initiation page variables
        protected string _componentid = string.Empty;
        protected string _componentname = string.Empty;
        protected string _applicationid = string.Empty;
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
            _componentid = Request["id"];
            _componentname = Request["name"];
            _applicationid = Request["app"];
            if (!string.IsNullOrEmpty(_applicationid)) _applicationid = _applicationid.ToUpper();
            #endregion

            #region action handler

            #endregion

            base.InitForm(_componentname.ToUpper(), "minifolder.gif", string.Empty, 0);

            #region client control event handler
            _btnCloseWindow.Visible = false;
            this.lstRole.Attributes.Add("onchange", "{getpermission('" + _componentid + "');return false;}");
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
                LoadRole();
                LoadRoleWasPermissed(_componentid);
            }
            this.lstRole.Focus();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadRole
        /// </summary>
        protected void LoadRole()
        {
            string condition = " and PK_RoleID IN (SELECT FK_ROLEID FROM T_APPLICATION_ROLE WHERE FK_APPLICATIONID='" + _applicationid + "')";
            condition += " Order By Name";
            string query = " PK_RoleID,Name";
            bus_Role objrole = bus_Role.Instance(_objUserContext); 
            DataSet ds = objrole.getList(condition, query);
            objrole = null;
            if (!base.isValidDataSet(ds)) return;
            base.BindData2ListBox(lstRole, ds, "Name", "PK_RoleID", "");
        }
        /// <summary>
        /// LoadRoleWasPermissed
        /// </summary>
        /// <param name="?"></param>
        protected void LoadRoleWasPermissed(string componentid)
        {
            string condition;
            condition = " AND FK_AppliedOnObjectID='" + componentid + "' and TypeOfApplicant='" + TYPE_OF_APPLICANT.ROLE + "'";
            bus_Permission objper = bus_Permission.Instance(_objUserContext);  
            DataSet ds = objper.getList(condition, "FK_AppliedOnID As RoleID,GrantedRight, '' As RoleName, '' As sRight");
            objper = null;
            if (base.isValidDataSet(ds))
            {
                foreach (DataRow R in ds.Tables[0].Rows)
                {
                    string id = R["RoleID"].ToString();
                    byte right = Convert.ToByte(R["GrantedRight"]);
                    string r = "";
                    if ((right & TYPE_OF_PERMISSION.CREATE) == TYPE_OF_PERMISSION.CREATE)
                        r += "<font color='blue'>Up</font> ";
                    if ((right & TYPE_OF_PERMISSION.READ) == TYPE_OF_PERMISSION.READ)
                        r += "<font color='green'>Down</font> ";
                    if ((right & TYPE_OF_PERMISSION.DELETE) == TYPE_OF_PERMISSION.DELETE)
                        r += "<font color='red'>Del</font> ";
                    
                    R["sRight"] = r;
                    R["RoleName"] = GetRoleName(id);
                }
                this.grvRole.DataSource = ds;
                this.grvRole.DataBind();
            }
        }
        /// <summary>
        /// GetRoleName
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetRoleName(string id)
        {
            bus_Role objrole = bus_Role.Instance(_objUserContext);  //bus_Role objrole = new bus_Role(_objUserContext, _dbName);
            DataSet ds = objrole.getByID(id, "name");
            objrole = null;
            if (base.isValidDataSet(ds))
                return ds.Tables[0].Rows[0]["name"].ToString();
            return string.Empty;
        }
        #endregion

        #region page button processing

        #endregion
    }
}