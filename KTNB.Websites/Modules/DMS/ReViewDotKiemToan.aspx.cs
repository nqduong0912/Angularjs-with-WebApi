using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class ReViewDotKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected string _doctypeid_thutuckiemtoan = DOCTYPE.THUTUC_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;


        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;

        protected bool _isTruongDoan = false;
        protected int _status_dotkt = 0;
        protected string _v_thoigian = string.Empty;
        protected string _v_lydo = string.Empty;

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
            _dotkt = Request["dotkt"];
            _doankt = Request["doankt"];
            _action = Request["act"];
            _v_thoigian = Request["v_thoigian"];
            _v_lydo = Request["v_lydo"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;
            if (!string.IsNullOrEmpty(_dotkt))
                _status_dotkt = CommonFunc.GetDocStatus(_dotkt);
            #region get data submit
            #endregion
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checksumuarydotkt")
                    FeedBackClient(CheckSumDotKT(_doankt));
                if (_action == "updatestatus")
                    FeedBackClient(UpdateStatus(_dotkt));
                if (_action == "updatestatus_pheduyet")
                    FeedBackClient(UpdateStatus_PheDuyet(_dotkt));
                if (_action == "updatestatus_tuchoi")
                    FeedBackClient(UpdateStatus_TuChoi(_dotkt));

                if (_action == "updatestatus_bbkt")
                    FeedBackClient(UpdateStatus_BBKT(_dotkt));
                if (_action == "updatestatus_pheduyet_bbkt")
                    FeedBackClient(UpdateStatus_PheDuyet_BBKT(_dotkt));
                if (_action == "updatestatus_tuchoi_bbkt")
                    FeedBackClient(UpdateStatus_TuChoi_BBKT(_dotkt));
              
            }

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin review - kết thúc đợt kiểm toán", string.Empty, _doctypeid, 0);
            //_isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            //if (_isTruongDoan)
            //    _btnAddNew.Visible = true;
            //else
            //    _btnAddNew.Visible = false;
            //if (_status_dotkt >= 31 || _status_dotkt <= 14)//matran
            //    _btnAddNew.Visible = false;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "td")
                {
                    if ((_status_dotkt >= 21 && _status_dotkt <= 26) || _status_dotkt == 231)
                    {
                        btnAction.Visible = true;
                    }
                }
                if (_action == "bld")
                {
                    btnPheDuyet.Visible = btnTuChoi.Visible = true;
                }
                if (_action == "td_bbkt")
                {
                    btnAction_BBKT.Visible = true;
                    //tbGiaHan.Visible = true;
                }
                if (_action == "bld_bbkt")
                {
                    btnPheDuyet_BBKT.Visible = btnTuChoi_BBKT.Visible = true;
                }
            }
            

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
                if (!String.IsNullOrEmpty(_dotkt))
                {
                    CommonFunc.LoadDocInfo(_dotkt, Page.Master);
                }
            }
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            if (String.IsNullOrEmpty(_doankt))
                return;
            string DocFields = "PK_DocumentID,[Tên công việc],[Người thực hiện],[Người duyệt 1],[Ngày bắt đầu],[Ngày kết thúc],Status";
            string PropertyFields = "Tên công việc,Người thực hiện,Người duyệt 1,Ngày bắt đầu,Ngày kết thúc";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = _doctypeid;
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
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgCongViec = (Image)e.Item.FindControl("imgCongViec") as Image;
                if (PK_DocumentID != null)
                {
                    if (imgCongViec != null)
                        imgCongViec.Attributes.Add("onclick", "{LoadDocumentCongViec('" + PK_DocumentID.Text + "')}");
                    if (imgEdit != null)
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentHoSoRR('" + PK_DocumentID.Text + "','"+_doankt+"','"+_dotkt+"')}");
                    if (lblTrangThai != null)
                        CommonFunc.SetTrangThaiCongViec(lblTrangThai, lblTrangThai.Text);
                }
                Label NgayBatDau = (Label)e.Item.FindControl("NgayBatDau") as Label;
                Label NgayKetThuc = (Label)e.Item.FindControl("NgayKetThuc") as Label;
                if (NgayBatDau != null && NgayKetThuc != null)
                {
                    NgayBatDau.Text = CommonFunc.formatDateTimeDDMMYYY(NgayBatDau.Text);
                    NgayKetThuc.Text = CommonFunc.formatDateTimeDDMMYYY(NgayKetThuc.Text);
                }
            }
        }


        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetList();
            GetList_PhatHien();
        }
        #endregion

        #region phathien-phe duyet review ketthuc kt
        private void GetList_PhatHien()
        {
            ObjectDataSource2.SelectParameters["TenDotKiemToan"].DefaultValue = this.ID8_63A0C4B1_2088_4994_B891_2FF65EB20265.Text;
            ObjectDataSource2.SelectParameters["DotKiemToan_ID"].DefaultValue = _dotkt;
            ObjectDataSource2.SelectParameters["DoanKiemToan_ID"].DefaultValue = _doankt;
            dataCtrl1.DataBind();
        }

        protected void dataCtrl1_ItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Label DotKiemToanID = (Label)e.Item.FindControl("DotKiemToanID") as Label;
                Label Kieu = (Label)e.Item.FindControl("Kieu") as Label;
                Label lblLoai = (Label)e.Item.FindControl("lblLoai") as Label;
                Label lblTrangThai = (Label)e.Item.FindControl("lblTrangThai") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                
                if (PK_DocumentID != null && Kieu != null)
                {
                    if (Kieu.Text == "HT")
                    {
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentHT('" + PK_DocumentID.Text + "')}");
                        lblLoai.Text = "Hệ thống";
                    }
                    else if (Kieu.Text == "VP")
                    {
                        imgEdit.Attributes.Add("onclick", "{LoadDocumentVP('" + PK_DocumentID.Text + "')}");
                        lblLoai.Text = "Vi phạm";
                    }
                }
                if (lblTrangThai != null)
                    CommonFunc.SetTrangThaiPhatHien(lblTrangThai, lblTrangThai.Text);
            }
        }

        public string CheckSumDotKT(string doankt)
        {
            //string message = "1";
            //CommonFunc.UpdateDocStatus(dotkt, 26);
            //return message;
            //string message = "1";
            //if (_status_dotkt == 25)
            //{
            //    CommonFunc.UpdateDocStatus(dotkt, 26);
            //    return message;
            //}
            //message = "KHÔNG ĐƯỢC PHÉP TRÌNH BLĐ PHÊ DUYỆT VÌ CHƯA CẬP NHẬT PHẢN HỒI";
            //return message;
            //string message = TrangThaiDotKiemToan.SetStatus(dotkt, 26);
            string message = GetSumDotKT(doankt);
            return message;
        }

        public string UpdateStatus(string dotkt)
        {
            //string message = "1";
            //CommonFunc.UpdateDocStatus(dotkt, 26);
            //return message;
            //string message = "1";
            //if (_status_dotkt == 25)
            //{
            //    CommonFunc.UpdateDocStatus(dotkt, 26);
            //    return message;
            //}
            //message = "KHÔNG ĐƯỢC PHÉP TRÌNH BLĐ PHÊ DUYỆT VÌ CHƯA CẬP NHẬT PHẢN HỒI";
            //return message;
            //string message = TrangThaiDotKiemToan.SetStatus(dotkt, 26);
            string message = "1";
            if (CommonFunc.CheckAllCongViecApprovedByDoanKT(_doankt) == false)
            {
                message = "Không được phép trình duyệt BLĐ đóng đợt kiểm toán vì Công việc chưa đc approved hết.";
                return message;
            }
            if (isDanhGiaRRConLai_New(_doankt) == false)
            {
                message = "Không được phép trình duyệt BLĐ đóng đợt kiểm toán vì tồn tại RR cố hữu chưa được đánh giá RR còn lại.";
                return message;
            }

            CommonFunc.UpdateDocStatus(dotkt, 26);
            return message;
        }

        public string UpdateStatus_PheDuyet(string dotkt)
        {
            string message = "1";
            if (CommonFunc.CheckAllCongViecApprovedByDoanKT(_doankt) == false)
            {
                message = "Không được phép Phê duyệt đóng đợt kiểm toán vì Công việc chưa được approved hết.";
                return message;
            }
            if (isDanhGiaRRConLai_New(_doankt) == false)
            {
                message = "Không được phép trình duyệt BLĐ đóng đợt kiểm toán vì tồn tại RR cố hữu chưa được đánh giá RR còn lại.";
                return message;
            }

            CommonFunc.UpdateDocStatus(dotkt, 31);
            return message;
        }

        public string UpdateStatus_TuChoi(string dotkt)
        {
            string message = "0";
            //if (_status_dotkt == 26)
            {
                CommonFunc.UpdateDocStatus(dotkt, 25);
                message = "1";
                return message;
            }
            return message;
        }
        #endregion

        #region pheduyet xuat BBKT
        public string UpdateStatus_BBKT(string dotkt)
        {
            //string message = isDanhGiaRRConLai(_doankt);
            //CommonFunc.UpdateDocStatus(dotkt, 231);
            //return message;
            //string message = "1";
            //if (_status_dotkt == 23)//o trang thai danh gia rui ro moi dc cap nhat trang thai phe duyet/tuchoi BLD
            //{
            //    message = isDanhGiaRRConLai(_doankt);
            //    CommonFunc.UpdateDocStatus(dotkt, 231);
            //    return message;
            //}
            //message = "KHÔNG ĐƯỢC PHÉP TRÌNH BLĐ PHÊ DUYỆT VÌ CHƯA ĐÁNH GIÁ RỦI RO CÒN LẠI";
            //return message;
            string message = "1";
            if (CommonFunc.CheckAllCongViecApprovedByDoanKT(_doankt) == false)
            {
                message = "Không thể chuyển BLĐ duyệt xuất BBKT vì toàn bộ công việc chưa được APPROVED hết.";
                return message;
            }
            CommonFunc.UpdateDocStatus(dotkt, 231);
            //message = isDanhGiaRRConLai(_doankt);
            //update thoigian,lydo
            if (!string.IsNullOrEmpty(_v_thoigian))
                CommonFunc.UpdateDocPropertyValue(dotkt, "F2C18E15-64AF-44CC-86C8-2CD84A774B76", _v_thoigian);
            if (!string.IsNullOrEmpty(_v_lydo))
                CommonFunc.UpdateDocPropertyValue(dotkt, "9FB60511-17CD-4F5C-B0F9-7BEDF93A5C43", _v_lydo);
            return message;
        }

        public string UpdateStatus_PheDuyet_BBKT(string dotkt)
        {
            //return message;
            string message = "1";
            if (CommonFunc.CheckAllCongViecApprovedByDoanKT(_doankt) == false)
            {
                message = "Không thể chuyển BLĐ duyệt xuất BBKT vì toàn bộ công việc chưa được APPROVED hết.";
                return message;
            }

            CommonFunc.UpdateDocStatus(dotkt, 24);
            return message;
        }

        public string UpdateStatus_TuChoi_BBKT(string dotkt)
        {
            string message = "1";
            CommonFunc.UpdateDocStatus(dotkt, 23);
            return message;
        }

        string isDanhGiaRRConLai(string doankt)
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(doankt))
                return "1";
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "')";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsNhomKT = doc.getDocumentList(DOCTYPE.NHOM_KIEMTOAN, DocFields, PropertyFields, Condition);
            //get danhgiarr by nhomkt
            if (isValidDataSet(dsNhomKT) == false)
                return "1";
            int countRR = 0;
            foreach (DataRow rowNhomKT in dsNhomKT.Tables[0].Rows)
            {
                DocFields = "PK_DocumentID,[Xác xuất],[Điểm kiểm soát],[Ảnh hưởng]";
                PropertyFields = "Xác xuất,Điểm kiểm soát,Ảnh hưởng";
                Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + rowNhomKT["PK_DocumentID"].ToString() + "')";
                DataSet dsDanhGiaRR = doc.getDocumentList(DOCTYPE.CHITIET_HOSO_RUIRO, DocFields, PropertyFields, Condition);
                if (isValidDataSet(dsDanhGiaRR))
                {
                    foreach (DataRow rowDanhGiaRR in dsDanhGiaRR.Tables[0].Rows)
                    {
                        if (rowDanhGiaRR["Xác xuất"] == null ||
                            rowDanhGiaRR["Điểm kiểm soát"] == null ||
                            rowDanhGiaRR["Ảnh hưởng"] == null)
                        {
                            countRR++;
                            continue;
                        }
                        if (string.IsNullOrEmpty(rowDanhGiaRR["Xác xuất"].ToString()) ||
                            string.IsNullOrEmpty(rowDanhGiaRR["Điểm kiểm soát"].ToString()) ||
                            string.IsNullOrEmpty(rowDanhGiaRR["Ảnh hưởng"].ToString()))
                        {
                            countRR++;   
                        }
                    }
                }
            }
            if (countRR > 0)
                return string.Format("Cập nhật trạng thái thành công.CHÚ Ý:ĐỢT KT CÒN {0} RỦI RO CHƯA ĐƯỢC ĐÁNH GIÁ.",countRR);
            return "1";
        }


        bool isDanhGiaRRConLai_New(string doankt)
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(doankt))
                return false;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "')";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsNhomKT = doc.getDocumentList(DOCTYPE.NHOM_KIEMTOAN, DocFields, PropertyFields, Condition);
            //get danhgiarr by nhomkt
            if (isValidDataSet(dsNhomKT) == false)
                return true;
            foreach (DataRow rowNhomKT in dsNhomKT.Tables[0].Rows)
            {
                DocFields = "PK_DocumentID,[Xác xuất],[Điểm kiểm soát],[Ảnh hưởng]";
                PropertyFields = "Xác xuất,Điểm kiểm soát,Ảnh hưởng";
                Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + rowNhomKT["PK_DocumentID"].ToString() + "')";
                DataSet dsDanhGiaRR = doc.getDocumentList(DOCTYPE.CHITIET_HOSO_RUIRO, DocFields, PropertyFields, Condition);
                if (isValidDataSet(dsDanhGiaRR))
                {
                    foreach (DataRow rowDanhGiaRR in dsDanhGiaRR.Tables[0].Rows)
                    {
                        if (rowDanhGiaRR["Xác xuất"] != null && rowDanhGiaRR["Ảnh hưởng"] != null)
                        {
                            if (!string.IsNullOrEmpty(rowDanhGiaRR["Xác xuất"].ToString()) &&
                            !string.IsNullOrEmpty(rowDanhGiaRR["Ảnh hưởng"].ToString()))
                            {
                                if (rowDanhGiaRR["Điểm kiểm soát"] == null)
                                    return false;
                                if (string.IsNullOrEmpty(rowDanhGiaRR["Điểm kiểm soát"].ToString()))
                                    return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region Tinh toan sum congviec,phathien,HSRR hoan thanh

        string GetSumDotKT(string doankt)
        {
            string re = string.Empty;
            string congviec = SumCongViec(doankt);
            if (congviec != "1")
                re = congviec;

            string phathien = SumPhatHien();
            if (phathien != "1")
                re += phathien;

            string rrconlai = SumDanhGiaRRConLai(doankt);
            if(rrconlai != "1")
                re += rrconlai;
            return re;
        }
        
        string SumDanhGiaRRConLai(string doankt)
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(doankt))
                return "1";
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "')";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsNhomKT = doc.getDocumentList(DOCTYPE.NHOM_KIEMTOAN, DocFields, PropertyFields, Condition);
            //get danhgiarr by nhomkt
            if (isValidDataSet(dsNhomKT) == false)
                return "1";
            int countRR = 0;
            int countRR_NotDoned = 0;
            foreach (DataRow rowNhomKT in dsNhomKT.Tables[0].Rows)
            {
                DocFields = "PK_DocumentID,[Xác xuất],[Điểm kiểm soát],[Ảnh hưởng]";
                PropertyFields = "Xác xuất,Điểm kiểm soát,Ảnh hưởng";
                Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + rowNhomKT["PK_DocumentID"].ToString() + "')";
                DataSet dsDanhGiaRR = doc.getDocumentList(DOCTYPE.CHITIET_HOSO_RUIRO, DocFields, PropertyFields, Condition);
                if (isValidDataSet(dsDanhGiaRR))
                {
                    foreach (DataRow rowDanhGiaRR in dsDanhGiaRR.Tables[0].Rows)
                    {
                        countRR++;
                        if (rowDanhGiaRR["Xác xuất"] == null ||
                            rowDanhGiaRR["Điểm kiểm soát"] == null ||
                            rowDanhGiaRR["Ảnh hưởng"] == null)
                        {
                            countRR_NotDoned++;
                            continue;
                        }
                        if (string.IsNullOrEmpty(rowDanhGiaRR["Xác xuất"].ToString()) ||
                            string.IsNullOrEmpty(rowDanhGiaRR["Điểm kiểm soát"].ToString()) ||
                            string.IsNullOrEmpty(rowDanhGiaRR["Ảnh hưởng"].ToString()))
                        {
                            countRR_NotDoned++;
                        }
                    }
                }
            }
            if (countRR > 0 && countRR_NotDoned > 0)
                return string.Format(".{0}/{1} RỦI RO CHƯA ĐC ĐÁNH GIÁ.", countRR_NotDoned,countRR);
            return "1";
        }

        string SumCongViec(string doankt)
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(doankt))
                return "1";
            string DocFields = "PK_DocumentID,Status";
            string PropertyFields = "";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankt + "')";
            bus_Document bus = new bus_Document(_objUserContext);
            DataSet dsCV = bus.getDocumentList(DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsCV) == false)
                return "1";
            int sumCV = dsCV.Tables[0].Rows.Count;
            int sumCV_NotDoned = dsCV.Tables[0].Select(" Status < 16").Count();

            if (sumCV > 0 && sumCV_NotDoned > 0)
                return String.Format("{0}/{1} CÔNG VIỆC CHƯA ĐC HOÀN THÀNH.", sumCV_NotDoned, sumCV);
            return "1";
        }

        string SumPhatHien()
        {
            //get nhomkt by doankt
            if (String.IsNullOrEmpty(_doankt))
                return "1";
            string tendotkt = CommonFunc.getPropertyValueOnDocument(_dotkt, "63A0C4B1-2088-4994-B891-2FF65EB20265");
            DataTable dt = DataSource.getListPhatHien(tendotkt, _dotkt, _doankt);
            if(dt != null)
                if (dt.Rows.Count > 0)
                {
                    

                    int sumPH = dt.Rows.Count;
                    int sumPH_NotDoned = dt.Select("Status = 0 Or Status = 32").Count();
                    if (sumPH > 0 && sumPH_NotDoned > 0)
                        return String.Format(".{0}/{1} PHÁT HIỆN CHƯA ĐC HOÀN THÀNH.", sumPH_NotDoned, sumPH);
                }
            return "1";
        }

        #endregion
    }
}