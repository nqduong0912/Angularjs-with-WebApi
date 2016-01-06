using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_Parameters : PageBase
    {
        #region initiation page variables
        protected string _id = string.Empty;
        protected string _name = string.Empty;
        protected string _fullname = string.Empty;
        protected string _action = string.Empty;
        protected string _ungdung = string.Empty;
        protected string _kieuthamso = string.Empty;
        protected string _docspace = string.Empty;
        protected string _caption = string.Empty;
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

            _id = Request["id"];
            _name = Request["name"];
            _m_groupname = Request["groupname"];
            _fullname = Request["fullname"];
            _action = Request["a"];
            _ungdung = Request["ungdung"];
            _kieuthamso = Request["kieuthamso"];
            _docspace = Request["docspace"];

            #region Processing backend early
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action.ToUpper().Equals("UPDATE"))
                    updateparameter(_id, _name, _fullname, _m_groupname, _ungdung, _kieuthamso);
                else if (_action.ToUpper().Equals("NEW"))
                {
                    if (!string.IsNullOrEmpty(_id))
                        createparameter(_id, _name, _fullname, _m_groupname, _ungdung, _kieuthamso);
                }
                else if (_action.ToUpper().Equals("DEL"))
                    delparameter(_id);
                else if (_action.ToLower().Equals("verifyname"))
                    verifyname(_name);
            }
            #endregion

            if (!string.IsNullOrEmpty(_id))
                _caption = _id;
            else
            {
                if (_docspace.ToLower() == "appcfg")
                    _caption = "Thêm mới tham số ứng dụng";
                else
                    _caption = "Thêm mới tham số hệ thống";
            }
            base.InitForm(_caption, "check.gif", string.Empty, 0);

            _btnCloseWindow.Visible = false;
            _btnSave.Visible = true;
            _btnSave.Attributes.Add("onclick", "{saveform(); return false;}");
            _btnDelete.Attributes.Add("onclick", "{delform(); return false;}");
            cboGroupName.Attributes.Add("onchange", "{switchgroup(this.value); return false;}");
            if (string.IsNullOrEmpty(_id))
                txtName.Attributes.Add("onblur", "{verifyname(this); return false;}");
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGroupParameter();
            loadApplication();
            loadParaType();

            if (!string.IsNullOrEmpty(_id))
            {
                _btnDelete.Visible = true;
                this.txtName.Text = _id;
                Match thematch = Regex.Match(_id.ToUpper(), "PASS");
                if (thematch.Success)
                    this.txtValue.TextMode = TextBoxMode.Password;
                LoadParameter(_id);
                _btnAddNew.Visible = true;
                this.txtValue.Focus();
            }
            else
            {
                this.txtName.ReadOnly = false;
                this.txtName.Focus();

            }

        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadParameter
        /// </summary>
        /// <param name="name"></param>
        protected void LoadParameter(string name)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataRow r = objParam.getByID(name, string.Empty).Tables[0].Rows[0];
            this.txtValue.Text = r["Value"].ToString();
            this.txtFullName.Text = r["FullName"].ToString();
            this.txtGroupName.Text = r["GroupName"].ToString();
            string fk_applicationid = "";
            if (!string.IsNullOrEmpty(("" + r["fk_applicationid"])))
            {
                fk_applicationid = r["fk_applicationid"].ToString();
                if (fk_applicationid.Length == 36)
                    try
                    {
                        if (!string.IsNullOrEmpty(fk_applicationid)) cboApplication.SelectedValue = fk_applicationid;
                    }
                    catch (Exception ex)
                    {
                    }
            }

            string paratype = r["parameter_type"].ToString(); if (!string.IsNullOrEmpty(paratype)) cboParamType.SelectedValue = paratype;


        }
        /// <summary>
        /// updateparameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fullname"></param>
        protected void updateparameter(string id, string name, string fullname, string groupname, string ungdung, string kieuthamso)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            if (objParam.UpdateParameter(id, fullname, name, groupname, ungdung, kieuthamso) == 0)
            {
                Response.Write("Tham số đã được cập nhật.");
                Response.Flush();
                Response.End();
            }
        }
        /// <summary>
        /// delparameter
        /// </summary>
        /// <param name="id"></param>
        protected void delparameter(string id)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            if (objParam.deleteByID(id) == 0)
            {
                Response.Write("Tham số đã bị xóa.");
                Response.Flush();
                Response.End();
            }
        }
        /// <summary>
        /// verifyname
        /// </summary>
        /// <param name="name"></param>
        protected void verifyname(string name)
        {
            string feedback = string.Empty;
            string condition = " and name='" + name + "'";
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataSet ds = objParam.getList(condition, "name");
            objParam = null;
            if (base.isValidDataSet(ds))
                feedback = "duplicated";
            else
                feedback = "not duplicated";
            Response.Write(feedback);
            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// createparameter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="fullname"></param>
        /// <param name="groupname"></param>
        protected void createparameter(string id, string name, string fullname, string groupname, string ungdung, string kieuthamso)
        {
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            if (objParam.CreateParameter(id, fullname, name, groupname, ungdung, kieuthamso) == 0)
            {
                Response.Write("Tham số mới đã được tạo.");
                Response.Flush();
                Response.End();
            }
        }
        /// <summary>
        /// loadGroupParameter
        /// </summary>
        protected void loadGroupParameter()
        {
            string query = "distinct GroupName";
            string condition = " AND Len(GroupName)>0";

            if (_docspace.ToLower() == "appcfg")
                condition += " AND Parameter_Type <> 'SYSTEM'";
            else
                condition += " AND Parameter_Type = 'SYSTEM'";

            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(_objUserContext);  //bus_Project_Parameter objParam = new bus_Project_Parameter(_objUserContext, _dbName);
            DataSet ds = objParam.getList(condition, query);

            if (!base.isValidDataSet(ds))
            {
                objParam = null;
                return;
            }
            foreach (DataRow R in ds.Tables[0].Rows)
                this.cboGroupName.Items.Add(new ListItem(R["GroupName"].ToString(), R["GroupName"].ToString()));
            this.cboGroupName.Items.Insert(0, new ListItem(".....?.....", ""));
            if (string.IsNullOrEmpty(_m_groupname))
                this.cboGroupName.SelectedIndex = 0;
            else
                this.cboGroupName.SelectedValue = _m_groupname;
            objParam = null;
        }
        /// <summary>
        /// loadApplication
        /// </summary>
        protected void loadApplication()
        {
            string query = "PK_ComponentID,Name";
            string condition = ""; // " AND FK_ApplicationID IN (" + VPB_CRM.Helper.APPLICATION.ALL.IDs + ")";
            condition += " Order by Name";
            bus_Component objcomp = bus_Component.Instance(_objUserContext);  //bus_Component objcomp = new bus_Component(_objUserContext, _dbName);
            DataSet ds = objcomp.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;
            BindData2Combo(this.cboApplication, ds, "Name", "PK_ComponentID", "");
            cboApplication.Items.Insert(0, new ListItem("", string.Empty));
            cboApplication.SelectedIndex = 0;
        }

        protected void loadParaType()
        {

            cboParamType.Items.Insert(0, new ListItem("", string.Empty));

            if (_docspace.ToLower() == "appcfg")
            {
                cboParamType.Items.Insert(1, new ListItem("UPLOAD", "UPLOAD"));
                cboParamType.Items.Insert(2, new ListItem("DOWNLOAD", "DOWNLOAD"));
            }
            else if (_docspace.ToLower() == "syscfg")
                cboParamType.Items.Insert(1, new ListItem("SYSTEM", "SYSTEM"));

            cboParamType.SelectedIndex = 0;
        }
        #endregion

        #region page button processing
        protected override void AddnewForm(object sender, EventArgs e)
        {
            //base.AddnewForm(sender, e);
            string url = "Parameters.aspx?a=new&docspace=" + _docspace;
            Response.Redirect(url);
        }
        #endregion
    }
}