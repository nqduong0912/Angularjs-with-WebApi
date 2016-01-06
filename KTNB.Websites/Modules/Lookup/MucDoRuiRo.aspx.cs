using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class MucDoRuiRo : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.MUCDO_RUIRO;
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
            base.InitForm("Mức độ rủi ro", string.Empty, _doctypeid, 0);
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
            string DocFields = "PK_DocumentID,Status,[Diễn giải],[Tên Mức độ rủi ro],[Điểm rủi ro (Chặn trên)],[Điểm rủi ro (Chặn dưới)],[Tần suất kiểm toán]";
            string PropertyFields = "Diễn giải,Tên Mức độ rủi ro,Điểm rủi ro (Chặn trên),Điểm rủi ro (Chặn dưới),Tần suất kiểm toán";
            string Condition = string.Empty;

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
                Label DiemRuiRoChanTren = (Label)e.Item.FindControl("DiemRuiRoChanTren") as Label;
                Label DiemRuiRoChanDuoi = (Label)e.Item.FindControl("DiemRuiRoChanDuoi") as Label;
                Label TanSuatKiemToan = (Label)e.Item.FindControl("TanSuatKiemToan") as Label;
                DataRowView drv = e.Item.DataItem as DataRowView;
                DiemRuiRoChanTren.Text = Math.Round(Convert.ToDecimal(drv.Row["Điểm rủi ro (Chặn trên)"]), 0).ToString();
                DiemRuiRoChanDuoi.Text = Math.Round(Convert.ToDecimal(drv.Row["Điểm rủi ro (Chặn dưới)"]), 0).ToString();
                TanSuatKiemToan.Text = Math.Round(Convert.ToDecimal(drv.Row["Tần suất kiểm toán"]), 0).ToString();
                    
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;

                if (PK_DocumentID != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");

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