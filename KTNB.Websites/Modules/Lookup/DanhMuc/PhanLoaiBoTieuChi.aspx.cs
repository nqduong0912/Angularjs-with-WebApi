using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Text;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc
{
    public partial class PhanLoaiBoTieuChi : PageBase
    {

        #region initiation page variables
        protected string _action = string.Empty;
        protected string _year = string.Empty;
        protected string _doctypeid = DOCTYPE.QUANLY_PHANLOAI_BOTIEUCHI;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected int countActive = 0;
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
            if (!string.IsNullOrEmpty(Request["doc"]))
                _documentid = Request["doc"];
            if(!string.IsNullOrEmpty(Request["y"]))
                _year = Request["y"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Quản lý phân loại bộ tiêu chí", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            loadYears();
            
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_year != null && _year.Length > 0)
                {
                    GetList(_doctypeid, _year);
                    drpYears.SelectedValue = _year;
                }
                else 
                    GetList(_doctypeid, MiscUtils.CurrentYear);
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID, string nam)
        {
            string DocFields = "PK_DocumentID,Status,[Năm],[Loại bộ tiêu chí]";
            string PropertyFields = "Năm,Loại bộ tiêu chí";
            string Condition = string.Format(" and [Năm] = '{0}' Order By [Năm],[Loại bộ tiêu chí]", nam);

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }
        #endregion

        #region page button processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;

                if (PK_DocumentID != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");

                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                    {
                        Status.Text = "Active";
                        imgEdit.Visible = false;
                        countActive++;

                    }
                }
            }
        }
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            _year = drpYears.SelectedValue;
            GetList(_doctypeid, _year);
        }
        #endregion
        private void loadYears()
        {
            List<String> listYears = MiscUtils.GetAllYear();
            drpYears.DataSource = listYears;
            drpYears.DataBind();
            drpYears.Items.Insert(0, new ListItem("", ""));
            drpYears.Items.FindByValue("").Attributes.Add("Disabled", "Disabled");
        }
     
    }
}
