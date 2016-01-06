using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class PhanHoiDonVi : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.THONGTIN_PHANHOI;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;//phathienid
        protected string _phanhoiid = string.Empty;//
        protected string _docspaceid = string.Empty;
        protected string _cv = string.Empty;
        protected string _congviec_docid = string.Empty;
        protected int _status_dotkt = 0;
        protected string _type = string.Empty;
        public string _IsCoTheThemPhanHoi = string.Empty;
        public string _role = string.Empty;
        public string _dotkt = string.Empty;
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
            _documentid = Request["doc"];//phathienid
            _dotkt = Request["dotkt"];
            _phanhoiid = Request["phanhoiid"];//phathienid
            _cv = Request["cv"];
            _congviec_docid = Request["congviec_docid"];
            _role = Request["role"];
            if (_cv == "nguoiduyet")
                _type = CommonFunc.CheckNguoiDuyet(_congviec_docid);
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            
            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "xoaphanhoi")
                {
                    if (!string.IsNullOrEmpty(_phanhoiid) && !string.IsNullOrEmpty(_documentid))
                    {
                        FeedBackClient(XoaPhanHoi(_phanhoiid, _documentid));
                    }
                }
            }
            #endregion

            #region init form
            base.InitForm("Danh sách phản hồi", string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler

            #endregion
        }
        private string XoaPhanHoi(string PhanHoiID, string PhatHienID)
        {
            CommonFunc.RemoveDocLink(PhanHoiID, PhatHienID, _objUserContext);
            string errormessage = "2";
            return errormessage;
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsDotInfo = new DataSet();
            if (!string.IsNullOrEmpty(_documentid))
            {
                dsDotInfo = doanKiemToan.GetDotInfoByPhatHien(_documentid);
            }

            if (isValidDataSet(dsDotInfo))
            {
                txtDotKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["dot_kiem_toan"].ToString();
                txtDoiTuongKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
                txtCongViec.Text = dsDotInfo.Tables[0].Rows[0]["ten_cong_viec"].ToString();
                string propPhatHien = string.Empty;
                string DocType = CommonFunc.GetDocType(_documentid).ToUpper();
                if (DocType.Equals(DOCTYPE.PHATHIEN_HETHONG))
                    propPhatHien = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
                else
                    propPhatHien = "D5637BAF-54F3-4724-9E3B-8543EB93509A";
                txtPhatHien.Text = CommonFunc.getPropertyValueOnDocument(dsDotInfo.Tables[0].Rows[0]["phathienid"].ToString(), propPhatHien);
                _status_dotkt = CommonFunc.GetDocStatus(dsDotInfo.Tables[0].Rows[0]["dotid"].ToString());
                _dotkt = dsDotInfo.Tables[0].Rows[0]["dotid"].ToString();
            }
            GetList(_doctypeid);
            //Kiểm tra trạng thái đợt kiểm toán
            if (!string.IsNullOrEmpty(_role))
            {
                if (_role.Equals("td"))
                {
                    _IsCoTheThemPhanHoi = TrangThaiDotKiemToan.IsCoTheThemPhanHoiBienBan(_status_dotkt).ToString();
                }
            }
            else
            {
                _IsCoTheThemPhanHoi = TrangThaiDotKiemToan.IsCoTheThemPhanHoiDonVi(_status_dotkt).ToString();
            }

        }
        #endregion

        #region page helper processing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            //string DocFields = "PK_DocumentID,[Phát hiện]";
            //string PropertyFields = "Phát hiện";
            string Condition = String.Empty;//" Order By [Loại đối tượng kiểm toán]";

            //ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            //ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            //ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["phatHienID"].DefaultValue = _documentid;
            if(_cv == "nguoithuchien")
                ObjectDataSource1.SelectParameters["UserType"].DefaultValue = _cv;
            if(_cv == "nguoiduyet")
                ObjectDataSource1.SelectParameters["UserType"].DefaultValue = _type;
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
                //Label lblLoaiPhatHien = (Label)e.Item.FindControl("lblLoaiPhatHien") as Label;
                //Label lblLoaiPhatHienText = (Label)e.Item.FindControl("lblLoaiPhatHienText") as Label;
                //Label Status = (Label)e.Item.FindControl("lblTongSoPhanHoi") as Label;
                Label lblRole = (Label)e.Item.FindControl("lblRole") as Label;
                Label lblNguoiNhap = (Label)e.Item.FindControl("lblNguoiNhap") as Label;
                Label phanhoiid = (Label)e.Item.FindControl("phanhoiid") as Label;
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                imgDelete.Attributes.Add("onclick", "{xoaPhanHoi('" + phanhoiid.Text + "')}");

                //if ( Convert.ToInt32(Status.Text) < 2 && _status_dotkt <= 26)
                //    imgDelete.Visible = true;
                //else
                //    imgDelete.Visible = false;
                //Kiểm tra trạng thái đợt kiểm toán
                _status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                bool isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, lblNguoiNhap.Text);
                if (isTruongDoan)
                    lblRole.Text = "Trưởng Đoàn";
                else
                    lblRole.Text = "Thành Viên";
                if (!string.IsNullOrEmpty(_role))
                {
                    if (_role.Equals("td"))
                    {
                        bool isCotheXoaPhanHoiBienBan = TrangThaiDotKiemToan.IsCoTheXoaPhanHoiBienBan(_status_dotkt);
                        //imgDelete.Visible = isCotheXoaPhanHoiBienBan;
                        if (Convert.ToInt32(Status.Text) < 2 && isCotheXoaPhanHoiBienBan)
                            imgDelete.Visible = true;
                        else
                            imgDelete.Visible = false;
                    }
                }
                else
                {
                    bool isCotheXoaPhanHoiDonVi = TrangThaiDotKiemToan.IsCoTheXoaPhanHoiDonVi(_status_dotkt);
                    if (Convert.ToInt32(Status.Text) < 2 && isCotheXoaPhanHoiDonVi)
                        imgDelete.Visible = true;
                    else
                        imgDelete.Visible = false;
                }
                if (isTruongDoan)
                    imgDelete.Visible = false;
                if (phanhoiid != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocumentPhanHoi('" + phanhoiid.Text + "')}");
                if (Status != null)
                    CommonFunc.SetTrangThaiPhanHoi(Status, Status.Text);
            }
        }

        #endregion
    }
}