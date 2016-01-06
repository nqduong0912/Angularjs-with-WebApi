using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DotKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
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
            _documentid = Request["doc"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Các đợt kiểm toán", string.Empty, _doctypeid, 0);
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
        protected void Page_Load(object sender, EventArgs e)
        {
            GetList(_doctypeid);

        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,[Năm],[Loại đối tượng kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Phạm vi],[Mục tiêu]";
            string PropertyFields = "Năm,Loại đối tượng kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Phạm vi,Mục tiêu";
            string Condition = " Order By [Năm],[Loại đối tượng kiểm toán]";

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
                Label DotKiemToan = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label Persons = (Label)e.Item.FindControl("Persons") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgDoanKT = (Image)e.Item.FindControl("imgDoanKT") as Image;

                if (DotKiemToan != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + DotKiemToan.Text + "')}");
                    imgDoanKT.Attributes.Add("onclick", "{LapDoanKiemToan('" + DotKiemToan.Text + "')}");
                    Persons.Text = CommonFunc.getCountOnDocumentLink(" And FK_DocLinkID='" + DotKiemToan.Text + "'").ToString();
                }
                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                        Status.Text = "Active";
                }
            }
        }
        #endregion
    }
}