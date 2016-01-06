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
    public partial class ReViewDotKiemToan_BLD : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOAN_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

        private const string const_ctkt = "ctkt";
        private const string const_rvkt = "rvkt";
        private const string const_bld = "bld";

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
            _action = Request["act"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin các đoàn kiểm toán", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = false;
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
            GetList();
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>

        private void GetList()
        {
            ObjectDataSource1.SelectParameters["Status"].DefaultValue = "5";
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
                Label DotKiemToanID = (Label)e.Item.FindControl("DotKiemToanID") as Label;
                Label TruongDoan = (Label)e.Item.FindControl("TruongDoan") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgReView = (Image)e.Item.FindControl("imgReView") as Image;
                string truongdoan = e.Item.Cells[4].Text;
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;
                if (PK_DocumentID != null && DotKiemToanID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocumentSeeDotKT('" + PK_DocumentID.Text + "','" + TruongDoan.Text + "','" + DotKiemToanID.Text + "')}");
                    imgReView.Attributes.Add("onclick", "{LoadDocumentReViewDotKT_BLD('" + PK_DocumentID.Text +"','"+DotKiemToanID.Text + "')}");
                }

                if (lblTrangThai != null)
                    CommonFunc.SetTrangThaiDotKT(lblTrangThai, DotKiemToanID.Text);

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