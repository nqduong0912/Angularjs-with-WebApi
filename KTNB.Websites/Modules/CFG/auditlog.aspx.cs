using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.OPERATORS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_auditlog : PageBase
    {
        #region initiation page variables
        protected string _auditdate = string.Empty;
        protected byte _auditevent = 0;
        protected string _groupid = string.Empty;
        protected DataSet _dsaudit;
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
            _auditdate = Request["ctl00$FormContent$txtValueDate"];
            _groupid = Request["ctl00$FormContent$cboCompany"];
            if (!string.IsNullOrEmpty(Request["ctl00$FormContent$cboEvents"]))
                _auditevent = Convert.ToByte(Request["ctl00$FormContent$cboEvents"]);
            #endregion

            #region action handler
            #endregion

            base.InitForm("<font color='red'>Nhật ký hệ thống</font>", "userman.png", string.Empty, 0);

            #region client control event handler
            this.btnView.Text = "Nhật ký";
            btnClose.Attributes.Add("onclick", "{closeme();return false;}");
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
                loadBangSuKien();
                loadGroup();
            }
            if (!string.IsNullOrEmpty(_auditdate))
            {
                dataCtrl.CurrentPageIndex = 0;
                GetAuditInfo(_auditdate, _auditevent);
            }
            this.txtValueDate.Focus();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// loadBangSuKien
        /// </summary>
        protected void loadBangSuKien()
        {
            this.cboEvents.Items.Add(new ListItem("---Tất cả---", "0"));
            this.cboEvents.Items.Add(new ListItem("Login", AUDITLOG.LOGIN.ToString()));
            
            this.cboEvents.Items.Add(new ListItem("Logout", AUDITLOG.LOGOUT.ToString()));
            this.cboEvents.Items.Add(new ListItem("Create", AUDITLOG.CREATE.ToString()));
            this.cboEvents.Items.Add(new ListItem("Update/Edit", AUDITLOG.EDIT.ToString()));
            this.cboEvents.Items.Add(new ListItem("Delete", AUDITLOG.DELETE.ToString()));
            this.cboEvents.Items.Add(new ListItem("Distribution", AUDITLOG.DISTRIBUTION.ToString()));
            this.cboEvents.Items.Add(new ListItem("Append", AUDITLOG.APPEND.ToString()));
            this.cboEvents.Items.Add(new ListItem("Monitoring", AUDITLOG.MONITORING.ToString()));
            this.cboEvents.Items.Add(new ListItem("Read", AUDITLOG.READ.ToString()));
            this.cboEvents.Items.Add(new ListItem("Recall", AUDITLOG.RECALL.ToString()));
            this.cboEvents.Items.Add(new ListItem("Set permission", AUDITLOG.SET_PERMISSION.ToString()));
            this.cboEvents.Items.Add(new ListItem("Share permission", AUDITLOG.SHARE_PERMISSION.ToString()));

        }
        /// <summary>
        /// GetAuditInfo
        /// </summary>
        /// <param name="auditdate"></param>
        /// <param name="auditevent"></param>
        /// <returns></returns>
        protected void GetAuditInfo(string auditdate, byte auditevent)
        {
            string condition = " and Convert(char(10),operationdatetime,103)='" + auditdate + "'";
            condition += " and len(groupname)>0";

            if (auditevent > 0)
                condition += " and operationtype=" + auditevent;

            if (!string.IsNullOrEmpty(_groupid))
                condition += " and fk_groupid='" + _groupid + "'";

            condition += " order by operationdatetime desc";

            string query = "pk_auditid,fk_userid,fk_objectid,objecttype,operationtype";
            query += ",Convert(char(10),operationdatetime,103) + ' ' + Convert(char(10),operationdatetime,108) as operationdatetime";
            query += ",AdditionalData1,AdditionalData2,username, groupname+'('+description + ')' as groupfullname";

            string action = ",action=case";
            action += " when operationtype=" + AUDITLOG.LOGIN + " then 'Đăng nhập'";
            action += " when operationtype=" + AUDITLOG.LOGOUT + " then 'Đăng xuất'";

            action += " when operationtype=" + AUDITLOG.CREATE + " then 'Tạo mới'";
            action += " when operationtype=" + AUDITLOG.EDIT + " then 'Sửa đổi'";
            action += " when operationtype=" + AUDITLOG.DELETE + " then 'Xóa'";

            action += " else 'unknow' end";
            query += action;

            base.LoadAuditInfo(condition, query, dataCtrl, SqlDataSource1);
        }
        /// <summary>
        /// loadGroup
        /// </summary>
        protected void loadGroup()
        {
            cboCompany.Items.Clear();
            string condition = " Order By Name";
            string query = "PK_GroupID, Name + '.....' + Description As FullName";
            bus_Group objgrp = bus_Group.Instance(_objUserContext);  //bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            DataSet ds = objgrp.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;
            BindData2Combo(cboCompany, ds, "FullName", "PK_GroupID", "");
            cboCompany.Items.Insert(0, new ListItem("---Tất cả---", ""));
            cboCompany.SelectedIndex = 0;
        }
        #endregion

        #region page button processing
        /// <summary>
        /// dataCtrl_OnItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label lblOperationType = (Label)e.Item.FindControl("lblOperationType") as Label;
                byte OperationType = 0;
                if (lblOperationType != null)
                    OperationType = Convert.ToByte(lblOperationType.Text);

                //Image imgEvent = (Image)e.Item.FindControl("imgEvent") as Image;
                //string img = "";
                //if (OperationType == AUDITLOG.LOGIN)
                //    img = "~/images/audit/login.gif";
                //if (OperationType == AUDITLOG.LOGOUT)
                //    img = "~/images/audit/logout.gif";
                //if (OperationType == AUDITLOG.IMPORT_CUSTOMER_BALANCE)
                    //img = "~/images/audit/upload.gif";
                //if (OperationType == AUDITLOG.CREATE_CUS_RANK)
                    //img = "~/images/audit/phanquyen.gif";
                //if (OperationType == AuditOperationType.READ_DOWNLOAD)
                //    img = "~/images/audit/download.gif";
                //if (OperationType == AuditOperationType.READ_VIEW)
                //    img = "~/images/audit/read.gif";
                //if (OperationType == AuditOperationType.READ_SEARCH)
                //    img = "~/images/audit/search.gif";
                //if (OperationType == AuditOperationType.CREATE)
                //    img = "~/images/audit/new.gif";
                //if (OperationType == AuditOperationType.EDIT)
                //    img = "~/images/audit/edit.gif";
                //if (OperationType == AuditOperationType.DELETE)
                //    img = "~/images/audit/delete.gif";

                //if (OperationType == AuditOperationType.APPLICATION_UNSHARED_PERMISSION)
                //    img = "~/images/audit/thuquyen.gif";
                //if (OperationType == AuditOperationType.APPLICATION_CUSTOMIZE)
                //    img = "~/images/audit/tuychinh.gif";
                //if (OperationType == AuditOperationType.APPLICATION_UYQUYEN)
                //    img = "~/images/audit/uyquyen.gif";
                //if (OperationType == AuditOperationType.CONNECT_FILESERVER_ERROR)
                //    img = "~/images/audit/connect_err.png";
                //imgEvent.ImageUrl = img;
            }
        }
        #endregion
    }
}