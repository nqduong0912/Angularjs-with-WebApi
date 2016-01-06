using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.WFS;
using vpb.app.business.ktnb.Definition.UMS;
using System.Collections.Generic;
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.MainView
{
    public partial class NhomChucNang_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected byte _viewtype = 0;
        protected string _docspaceid = string.Empty;
        protected string _name = string.Empty;
        protected string _des = string.Empty;
        protected string _valueactive = string.Empty;
        protected static UserContext objContext = null;
        protected static string dbName = String.Empty;
        protected static string _roleID;
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
            _roleID = Request["doc"];
            _action = Request["act"];
            _name = Request["name"];
            _des = Request["des"];
            _valueactive = Request["valueactive"];
            #endregion
            objContext = (UserContext)Session["objUserContext"];
            dbName = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();
            if (string.IsNullOrEmpty(_roleID))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(createRole(_name, _des).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(editRole(_name, _des).ToString());
                if (_action == "deleteRole")
                    FeedBackClient(deleteRole(_roleID).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới Nhóm chức năng";
            if (!string.IsNullOrEmpty(_roleID))
                caption = "Thông tin Nhóm chức năng";
            base.InitForm(caption, string.Empty, string.Empty, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "',''); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _roleID + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{preparedeletedoc('" + _roleID + "'); return false;}");
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
                createComponentGrid();
                if (!String.IsNullOrEmpty(_roleID))
                {
                    DataSet ds = CommonFunc.getRoleInfo(_roleID, objContext, dbName);
                    this.txtTenNhom.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                    this.txtDienGiai.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                }
            }
        }

        private int createRole(String roleName, String description)
        {
            bus_Role objRole = bus_Role.Instance(_objUserContext);
            DataSet ds = objRole.getByName(roleName,"PK_ROLEID");
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds = objRole.getEmpty(string.Empty);
                DataRow R = ds.Tables[0].NewRow();
                Guid guid = Guid.NewGuid();
                _roleID = guid.ToString();
                R["PK_ROLEID"] = guid;
                R["NAME"] = roleName;
                R["DESCRIPTION"] = description;
                R["ISEXPIRED"] = 0;
                R["ACTIVATIONDATETIME"] = DateTime.Now;
                R["ORDERINDEX"] = 1;
                ds.Tables[0].Rows.Add(R);
                if (objRole.addnewDataSet(ds) == 0)
                {
                    bus_application_role al = new bus_application_role();
                    al.FK_APPLICATIONID = "0C2A2496-B6D9-492E-9FB2-A29537F7D32F"; //id của Application Kế Hoạch Năm
                    al.FK_ROLEID = _roleID;
                    ManagerFactory.applicationrole_manager.Insert(al);
                    return 0;
                }
                else return 1;
            }
            return ds.Tables[0].Rows.Count;
        }

        private int editRole(String roleName, String description)
        {
            bus_Role objRole = bus_Role.Instance(_objUserContext);
            DataSet ds = objRole.getByName(roleName, "PK_ROLEID,NAME,DESCRIPTION, ORDERINDEX");

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds = CommonFunc.getRoleInfo(_roleID, objContext, dbName);
                DataRow dr = ds.Tables[0].Rows[0];
                dr["NAME"] = roleName;
                dr["DESCRIPTION"] = description;
                dr["ORDERINDEX"] = 1;
                return objRole.saveDataSet(ds);
            }
            else if (ds.Tables[0].Rows[0]["PK_ROLEID"].ToString().Equals(_roleID))
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["NAME"] = roleName;
                dr["DESCRIPTION"] = description;
                dr["ORDERINDEX"] = 1;
                return objRole.saveDataSet(ds);
            }
            return ds.Tables[0].Rows.Count;
        }

        private int deleteRole(String id)
        {
            bus_Role objRole = bus_Role.Instance(_objUserContext);
            return objRole.deleteByID(id);
        }

        [WebMethod]
        public static string SetRoleForComponent(List<string> componentIDs)
        {
            for (int i = 0; i < componentIDs.Count; i++)
            {
                CommonFunc.AddRoleToComponent(componentIDs[i], _roleID.ToString(), objContext, dbName);
            }
            return "1";
        }

        [WebMethod]
        public static string ClearRoleForComponent(List<string> componentIDs)
        {
            for (int i = 0; i < componentIDs.Count; i++)
            {
                CommonFunc.deleteRoleFromComponent(componentIDs[i], _roleID.ToString(), objContext, dbName);
            }
            return "1";
        }

        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_COMPONENTID = (Label)e.Item.FindControl("PK_COMPONENTID") as Label;
                DataRowView drv = e.Item.DataItem as DataRowView;


                CheckBox chk = (CheckBox)e.Item.FindControl("chkItem") as CheckBox;

                if (!string.IsNullOrEmpty(_roleID))
                {
                    if (CommonFunc.IsRoleAssignedOnComponent(PK_COMPONENTID.Text, _roleID, objContext, dbName)) chk.Checked = true;
                }
                if (chk != null)
                {
                    chk.Attributes.Add("componentID", PK_COMPONENTID.Text);
                }
            }
        }
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PK_COMPONENTID");
            dt.Columns.Add("NAME");
            dt.AcceptChanges();
            return dt;
        }

        private DataTable AddRow(DataTable dt, String id, string name)
        {
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1]["PK_COMPONENTID"] = id;
            dt.Rows[dt.Rows.Count - 1]["NAME"] = name;
            dt.AcceptChanges();
            return dt;
        }

        private void createComponentGrid()
        {
            DataTable dt = CreateDataTable();
            dt = AddRow(dt, "AB6C2FDC-79FC-4AB7-B6FB-63BD5A358BED", "Chấm điểm rủi ro ĐTKT");
            dt = AddRow(dt, "8F321C60-D86C-4D21-ABE9-4EB03C254523", "Cập nhật điểm sửa đổi và thời gian kiểm toán gần nhất");
            dt = AddRow(dt, "8C19A1AA-B3A0-4F31-AAA0-298D7859701B", "Lập kế hoạch năm");
            dt = AddRow(dt, "75B77AB7-1862-4519-A70B-ACCECA743EDD", "Xem các ĐTKT đã chọn");
            dataCtrl.DataSource = dt;
            dataCtrl.DataBind();
        }
        #endregion
    }
}