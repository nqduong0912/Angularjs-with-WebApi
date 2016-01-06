using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class BLD_Duyet : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOAN_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _hanhdong = string.Empty;
        private const string const_ctkt = "ctkt";
        private const string const_rvkt = "rvkt";
        private const string const_hsrr = "hsrr";
        private const string const_bbkt = "bbkt";

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
            _hanhdong = Request["hanhdong"];
            _dotkt = Request["dotkt"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "PheDuyetTuChoiHSRR")
                    FeedBackClient(PheDuyetTuChoiHSRR(_dotkt, _hanhdong));
            }
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
        }
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
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
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action.Equals("hsrr"))
                    ObjectDataSource1.SelectParameters["Status"].DefaultValue = "4";
                if (_action.Equals(const_bbkt))
                    ObjectDataSource1.SelectParameters["Status"].DefaultValue = "6";
                dataCtrl.DataBind();
            }
        }
        private string PheDuyetTuChoiHSRR(string DotKT, string HanhDong)
        {
            //thangma
            int _status_dotkt = CommonFunc.GetDocStatus(DotKT);
            if (HanhDong.Equals("pheduyet"))
            {
                //chuyển trạng thái đợt lên: Lập chương trình kiểm toán
                //CommonFunc.UpdateDocStatus(DotKT, 15);
                TrangThaiDotKiemToan.SetStatus(DotKT, 15);
                return "Phê duyệt thành công!";
            }
            else if (HanhDong.Equals("tuchoi"))
            {
                //chuyển trạng thái đợt lên: Phân tích chi tiết (chuyển về)
                CommonFunc.UpdateDocStatus(DotKT, 14);
                return "Từ chối thành công!";
            }
            //end
            return string.Empty;
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
                Image imgDoanKT = (Image)e.Item.FindControl("imgDoanKT") as Image;
                string truongdoan = e.Item.Cells[4].Text;
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;
                if (PK_DocumentID != null && DotKiemToanID != null)
                {
                    if (!String.IsNullOrEmpty(_action))
                    {
                        if (_action == const_ctkt)//lap chuongtrinhkiemtoan
                        {
                            imgEdit.Attributes.Add("onclick", "{LoadDocumentChuongTrinhKiemToan('" + PK_DocumentID.Text + "','" + TruongDoan.Text + "','" + DotKiemToanID.Text + "')}");
                            imgEdit.ToolTip = "Lập chương trình kiểm toán";

                            imgDoanKT.ImageUrl = "~/Images/viewdetail.gif";
                            imgDoanKT.ToolTip = "Chi tiết đợt kiểm toán";
                            imgDoanKT.Attributes.Add("onclick", "{LoadDocumentSeeDotKT('" + DotKiemToanID.Text + "')}");
                        }
                        if (_action == const_rvkt)//review-ket thuc dotkt
                        {
                            imgEdit.Attributes.Add("onclick", "{LoadDocumentReViewDotKT('" + PK_DocumentID.Text + "','" + DotKiemToanID.Text + "')}");
                            imgEdit.ImageUrl = "~/Images/monitor.gif";
                            imgEdit.ToolTip = "Review - kết thúc đợt kiểm toán";


                            imgDoanKT.ImageUrl = "~/Images/viewdetail.gif";
                            imgDoanKT.ToolTip = "Chi tiết đợt kiểm toán";
                            imgDoanKT.Attributes.Add("onclick", "{LoadDocumentSeeDotKT('" + DotKiemToanID.Text + "')}");
                        }
                        if (_action == const_hsrr)//review-ket thuc dotkt
                        {
                            imgEdit.Attributes.Add("onclick", "{PheDuyetTuChoi('" + DotKiemToanID.Text + "','tuchoi')}");
                            imgEdit.ImageUrl = "~/Images/delete.gif";
                            imgEdit.ToolTip = "Từ chối";


                            imgDoanKT.ImageUrl = "~/Images/status_16.png";
                            imgDoanKT.ToolTip = "Phê duyệt";
                            imgDoanKT.Height = 16;
                            imgDoanKT.Width = 16;
                            imgDoanKT.Attributes.Add("onclick", "{PheDuyetTuChoi('" + DotKiemToanID.Text + "','pheduyet')}");
                        }
                        if (_action == const_bbkt)//BLĐ duyet/tuchoi xuat bien bankiem toan
                        {
                            imgEdit.Attributes.Add("onclick", "{PheDuyetTuChoi('" + DotKiemToanID.Text + "','tuchoi')}");
                            imgEdit.ImageUrl = "~/Images/delete.gif";
                            imgEdit.ToolTip = "Từ chối";
                            imgEdit.Visible = false;

                            imgDoanKT.ImageUrl = "~/Images/status_16.png";
                            imgDoanKT.ToolTip = "BLĐ phê duyệt/từ chối xuất biên bản kiểm toán";
                            imgDoanKT.Height = 16;
                            imgDoanKT.Width = 16;
                            imgDoanKT.Attributes.Add("onclick", "{Duyet_XuatBBKT('" + PK_DocumentID.Text + "','" + DotKiemToanID.Text + "')}");
                        }
                    }
                    else//cong viec truong doan;-> tao nhom kiem toan + attach file
                    {
                        //tao nhom kiem toan
                        imgDoanKT.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "','" + TruongDoan.Text + "','" + DotKiemToanID.Text + "')}");
                        imgDoanKT.ToolTip = "Phân công mảng nghiệp vụ";
                        //gan file dinh kem     
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentDotKT('" + DotKiemToanID.Text + "')}");
                        imgEdit.ImageUrl = "~/Images/SharingFolder.gif";
                        imgEdit.ToolTip = "Gắn file vào đợt kiểm toán";
                    }
                    if (lblTrangThai != null)
                        CommonFunc.SetTrangThaiDotKT(lblTrangThai, DotKiemToanID.Text);
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