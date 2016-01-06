using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using VPB_CRM.Helper;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.ChucDanh
{
    public partial class QuymoLoaiDTKT : PageBase
    {
        protected string _action = string.Empty;
        public string _doctypeid = DOCTYPE.QuymoLoaiDTKT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonFunc.GetT_Type_Doc_Property("96C6D12F-F4BD-4598-AB18-873AE2362572", "Tên loại đối tượng kiểm toán", drl_LDTKT);
            CommonFunc.GetT_Type_Doc_Property("86C9EA7F-DF46-4D40-8714-17FBBA84A773", "Tên quy mô", drl_QM);
            CommonFunc.GetYear2Dropdownlist(drl_Year);
            GetList(_doctypeid);
        }
        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();
            _documentid = Request["doc"];
            _action = Request["act"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            #region get data submit
            
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "xoaLoaiDTKT")
                    FeedBackClient(xoaQMloaiDTKT(_documentid));
            }
            #endregion

            #region init form
            base.InitForm("Quy mô quản lý Loại đối tượng kiểm toán", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,Status,[Năm],[Loại đối tượng Kiểm toán],[Quy mô],[Nguồn lực]";
            string PropertyFields = "Năm,Loại đối tượng Kiểm toán,Quy mô,Nguồn lực";
            string Condition = string.Empty;
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }

        private void GetList(string DocumentTypeID, string Condition)
        {

            string DocFields = "PK_DocumentID,Status,[Năm],[Loại đối tượng Kiểm toán],[Quy mô],[Nguồn lực]";
            string PropertyFields = "Năm,Loại đối tượng Kiểm toán,Quy mô,Nguồn lực";
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
                Image imgDel = (Image)e.Item.FindControl("imgDel") as Image;
                if (PK_DocumentID != null && imgDel!= null)
                    imgDel.Attributes.Add("onclick", "deleteLoaiDTKT('" + PK_DocumentID.Text + "')");
                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                        Status.Text = "Active";
                }
            }
        }
        protected string xoaQMloaiDTKT(string documentID)
        {
            CommonFunc.deleteDocument(documentID);
            return string.Empty;
        }
        #endregion
    }
}