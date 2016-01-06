using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_newapp : PageBase
    {
        #region initiation page variables
        protected string _appication = string.Empty;
        protected string _appicationname = string.Empty;
        protected string _action = string.Empty;
        protected string _compname = string.Empty;
        protected string _note = string.Empty;
        protected string _roles = string.Empty;
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
            _appication = Request["ctl00$FormContent$cboApplication"];
            _action = "" + Request["act"];
            #endregion

            #region action handler
            //create new a component
            if (_action.ToUpper().Equals("NEW"))
            {
                _appication = Request["app"];
                _appicationname = Request["appname"];
                _compname = Request["comp"];
                _note = Request["nte"];
                _roles = Request["roles"];
                CreateNewComponent(_appication, _compname, _note, _roles);
            }
            //verify componentname
            if (_action.ToLower().Equals("verifyname"))
            {
                _compname = Request["comp"];
                VerifyComponentName(_compname);
            }
            #endregion

            base.InitForm("Thêm ứng dụng", "minifolder.gif", string.Empty, 0);

            #region client control event handler
            _btnCloseWindow.Visible = false;
            _btnSave.Visible = true;
            _btnSave.Attributes.Add("onclick", "{createcomponent();return false;}");
            txtComponentName.Attributes.Add("onblur", "{verifycomponentname(this); return false;}");
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
                LoadAppGroup();
            if (!string.IsNullOrEmpty(_appication))
                LoadRoleByApp(_appication);
            this.txtComponentName.Focus();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadAppGroup
        /// </summary>
        protected void LoadAppGroup()
        {
            string condition = ""; // " AND PK_ApplicationID IN (" + VPB_CRM.Helper.APPLICATION.ALL.IDs + ")";
            condition += " Order By Name";
            bus_Application objapp = bus_Application.Instance(_objUserContext);  //bus_Application objapp = new bus_Application(_objUserContext, _dbName);
            DataSet ds = objapp.getList(condition, "PK_ApplicationID,Name");
            if (!isValidDataSet(ds)) return;
            base.BindData2Combo(cboApplication, ds, "Name", "PK_ApplicationID", "");
            cboApplication.Items.Insert(0, new ListItem("", ""));
            cboApplication.SelectedIndex = 0;
        }
        /// <summary>
        /// CreateNewComponent
        /// </summary>
        /// <param name="appication"></param>
        /// <param name="compname"></param>
        /// <param name="note"></param>
        /// <param name="roles"></param>
        protected void CreateNewComponent(string appication, string compname, string note, string roles)
        {
            string pk_componentid = Guid.NewGuid().ToString();
            string url = "Modules/Admin/UserManager.aspx?docspace=fm&a=" + pk_componentid + "&c=" + pk_componentid;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                string sp = "dbo.CreateComponent";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sp;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlParameter par;

                par = new SqlParameter("@PK_ComponentID", pk_componentid);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@Name", compname);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@Description", note);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@FK_ApplicationID", appication);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@URL", url);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@ROLES", roles);
                cmd.Parameters.Add(par);

                par = new SqlParameter("@ICON", "component.gif");
                cmd.Parameters.Add(par);

                try
                {
                    cmd.ExecuteNonQuery();
                    CommonFunc.AddAuditLog(0, AUDITLOG.CREATE, "Thêm mới", "Thêm mới ứng dụng " + _appicationname + "/" + compname,_objUserContext);
                    Response.Write("0");
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {
                    Response.Write("-1");
                    Response.Flush();
                    Response.End();
                }

            }
        }
        /// <summary>
        /// componentname
        /// </summary>
        /// <param name="componentname"></param>
        protected void VerifyComponentName(string componentname)
        {
            string feedstring = string.Empty;
            string query = "name";
            string condition = " and name='" + componentname + "'";
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objcomp.getList(condition, query);
            objcomp = null;
            if (base.isValidDataSet(ds)) feedstring = "duplicated";
            Response.Write(feedstring);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region page button processing
        protected void LoadRoleByApp(string appication)
        {
            bus_Application objapp = bus_Application.Instance(_objUserContext);  //bus_Application objapp = new bus_Application(_objUserContext, _dbName);
            DataSet ds = objapp.getRoleOnApplication(appication);
            objapp = null;
            if (!base.isValidDataSet(ds)) return;
            grvRoles.DataSource = ds;
            grvRoles.DataBind();
        }
        #endregion
    }
}