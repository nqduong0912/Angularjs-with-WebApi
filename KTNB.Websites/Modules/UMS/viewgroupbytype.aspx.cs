using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class Modules_UMS_viewgroupbytype : PageBase
    {
        #region initiation page variables
        //protected byte _grouptype = 0;
        protected string _groupid;
        protected string _removegroupid;
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
            if (!string.IsNullOrEmpty(Request["type"]))
                _m_grouptype = Convert.ToByte(Request["type"]);
            _groupid = Request["groupid"];
            _removegroupid = Request["removegroup"];
            if (!string.IsNullOrEmpty(_removegroupid))
                removecompany(_removegroupid, _groupid);
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Danh sách chi nhánh hỗ trợ", "userman.png", string.Empty, 0);
            this.lblCompany.Text = "XXX";
            #endregion

            #region client control event handler
            _btnCloseWindow.Visible = true;
            btnSave.Attributes.Add("onclick", "{return addcompany();}");
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
                this.lblCompany.Text = CommonFunc.getCompanyInfo(_groupid, _objUserContext, _dbName);

                LoadChiNhanh();
            }
            LoadDanhSachTTHTro(_groupid);
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// LoadChiNhanh
        /// </summary>
        protected void LoadChiNhanh()
        {
            bus_Group objgrp = new bus_Group(_objUserContext, _dbName);
            string query = "PK_GroupID,Name + '.....' + Description As FullName";
            string condition = " AND Type In (" + GROUPTYPE.CHI_NHANH + ")";
            
            condition += " AND Name <>'VN0010001'";
            condition += " AND PK_GroupID Not In (Select FK_DocLinkID From T_DocLink Where LinkType=" + TYPE_OF_LINK.GROUP + ")";  

            condition += " ORDER By Name";

            DataSet ds = objgrp.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;
            base.BindData2Combo(this.cboCompany, ds, "FullName", "PK_GroupID", "");
        }
        /// <summary>
        /// LoadDanhSachTTHTro
        /// </summary>
        /// <param name="groupid"></param>
        protected void LoadDanhSachTTHTro(string groupid)
        {
            //fk_documentid=groupid;

            bus_Group objgrp = new bus_Group(_objUserContext);
            string query = "PK_GroupID,Name,Description";
            string condition = " AND PK_GroupID In (Select FK_DocLinkID From T_DocLink Where FK_DocumentID='" + groupid + "' And LinkType=" + TYPE_OF_LINK.GROUP + ")";
            DataSet ds = objgrp.getList(condition, query);
            if (!base.isValidDataSet(ds)) return;

            SqlDataSource1.ConnectionString = _objUserContext.ConnectionString;
            SqlDataSource1.SelectCommand = "SELECT " + query + " FROM T_Group WHERE 1=1 " + condition;
            dataCtrl.DataBind();
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
                //btnRemove
                //RowID
                Label RowID = (Label)e.Item.FindControl("RowID") as Label;
                HyperLink Remove = (HyperLink)e.Item.FindControl("btnRemove") as HyperLink;

                Remove.Attributes.Add("onclick", "{return removeCompany('" + RowID.Text + "');}");

            }
        }
        /// <summary>
        /// addCompany2TTHTro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void addCompany2TTHTro(object sender, EventArgs e)
        {
            string newcompany = Request["ctl00$FormContent$cboCompany"];
            CommonFunc.AddDocLink(_groupid, newcompany, TYPE_OF_LINK.GROUP, _objUserContext);
            LoadChiNhanh();
            LoadDanhSachTTHTro(_groupid);
        }
        /// <summary>
        /// removecompany
        /// </summary>
        /// <param name="removegroupid"></param>
        /// <param name="parentgroupid"></param>
        protected void removecompany(string removegroupid, string parentgroupid)
        {
            CommonFunc.RemoveDocLink(parentgroupid, removegroupid, _objUserContext);
        }
        #endregion
    }
}