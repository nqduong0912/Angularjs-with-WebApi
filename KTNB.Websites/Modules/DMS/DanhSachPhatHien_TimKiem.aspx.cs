using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.OPERATORS;
using System.Globalization;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DanhSachPhatHien_TimKiem : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

        protected string _tungay = string.Empty;
        protected string _denngay = string.Empty;
        protected string _loaidoituong = string.Empty;

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
            _tungay = Request["tungay"];
            _denngay = Request["denngay"];
            _loaidoituong = Request["loaidoituong"];

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "timkiemdotkt")
                {
                    //GetList();
                    //updatepanel1_OnLoad(null, null);
                }
            }
            #endregion

            #region init form
            base.InitForm("Tìm kiếm phát hiện", string.Empty, _doctypeid, 0);
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
            //GetList(_doctypeid);

            if (!IsPostBack)
            {
                LoadDoiTuongKiemToan();
                BuildThangNam();
                BuildTrangThai();
            }
        }

        void BuildTrangThai()
        {
            CommonFunc.LoadDropDownList(this.ddlTrangThai, 7);
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            string tungay = hdTuNgay.Value;
            string denngay = hdDenNgay.Value;
            string doituongkt = hdLoaiDoiTuong.Value;
            string trangthai = hdTrangThai.Value;
            if (String.IsNullOrEmpty(tungay) || String.IsNullOrEmpty(denngay) || String.IsNullOrEmpty(doituongkt))
                return;
            ObjectDataSource1.SelectParameters["DoiTuongKiemToan"].DefaultValue = doituongkt;
            ObjectDataSource1.SelectParameters["TuNgay"].DefaultValue = tungay;
            ObjectDataSource1.SelectParameters["DenNgay"].DefaultValue = denngay;
            ObjectDataSource1.SelectParameters["TrangThai"].DefaultValue = trangthai;
            dataCtrl.DataBind();

        }

        void LoadDoiTuongKiemToan()
        {
            string DocFields = "PK_DocumentID,Status,[Tên đối tượng kiểm toán],[Tên hiển thị],[Loại đối tượng kiểm toán],[Email],[Cán bộ GSTX]";
            string PropertyFields = "Tên đối tượng kiểm toán,Tên hiển thị,Loại đối tượng kiểm toán,Email,Cán bộ GSTX";
            string Condition = " Order By [Loại đối tượng kiểm toán]";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.DOITUONG_KT, DocFields, PropertyFields, Condition);
            ddlLoaiDoiTuong.Items.Add(new ListItem("--- Tất cả ---", "-1"));
            if (isValidDataSet(ds))
                foreach (DataRow row in ds.Tables[0].Rows)
                    ddlLoaiDoiTuong.Items.Add(new ListItem(row["Tên đối tượng kiểm toán"].ToString(), row["Tên đối tượng kiểm toán"].ToString()));
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
                Label Kieu = (Label)e.Item.FindControl("Kieu") as Label;
                Label lblLoai = (Label)e.Item.FindControl("lblLoai") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Image imgDotKT = (Image)e.Item.FindControl("imgDotKT") as Image;
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
                if(DotKiemToanID != null && imgDotKT != null)
                    imgDotKT.Attributes.Add("onclick", "{LoadDocumentDotKT('" + DotKiemToanID.Text + "')}");

                Label Status = (Label)e.Item.FindControl("Status") as Label;
                if (Status != null)
                    SetStatus(Status, Status.Text);
               
            }
        }

        void SetStatus(Label lblStatus, string text)
        {
            string result = String.Empty;
            if (string.IsNullOrEmpty(text))
                result = String.Empty;
            result = CommonFunc.GetTrangThaiDotKT(Int32.Parse(text));
            lblStatus.Text = result;
        }

        private void GetCountDoanKiemToan(string DocumentTypeID, string dotkt, Label lbl)
        {
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
            //int i = obj.getCountOnDocumentLink(Condition);
            int i = 0;
            if (ds != null && ds.Tables.Count > 0)
                lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }

        #endregion

        #region xuly grid
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetList();
            //DefinedDataTableDanhSachPhatHien();
        }

        void BuildThangNam()
        {
            for (int i = 1; i <= 12; i++)
            {
                ddlTuNgayThang.Items.Add(new ListItem("Tháng " + i.ToString(), i.ToString()));
                ddlDenNgayThang.Items.Add(new ListItem("Tháng " + i.ToString(), i.ToString()));
            }
            CommonFunc.LoadDropDownList(ddlTuNgayNam, 3);
            CommonFunc.LoadDropDownList(ddlDenNgayNam, 3);
        }
        #endregion end xuly grid

        #region DanhSachPhatHien

        //dinh nghia datatable
        void DefinedDataTableDanhSachPhatHien()
        {
            string tungay = hdTuNgay.Value;
            string denngay = hdDenNgay.Value;
            string doituongkt = hdLoaiDoiTuong.Value;

            if (String.IsNullOrEmpty(tungay) || String.IsNullOrEmpty(denngay) || String.IsNullOrEmpty(doituongkt))
                return;
            DateTime dtTuNgay = Convert.ToDateTime(tungay);
            DateTime dtDenNgay = Convert.ToDateTime(denngay);

            DataTable dt = new DataTable();
            dt.Columns.Add("PK_DocumentID", typeof(string));
            dt.Columns.Add("PhatHien", typeof(string));
            dt.Columns.Add("Kieu", typeof(string));//Phan biet phan biet phat hien HeThong(HT),ViPham(VP)
            dt.Columns.Add("MucDo", typeof(string));
            dt.Columns.Add("ChiTiet", typeof(string));
            dt.Columns.Add("AnhHuong", typeof(string));
            dt.Columns.Add("GhiChu", typeof(string));
            dt.Columns.Add("KhuyenNghi", typeof(string));
            dt.Columns.Add("FileDinhKem", typeof(string));

            dt.Columns.Add("DotKiemToan", typeof(string));
            dt.Columns.Add("LoaiDoiTuongKiemToan", typeof(string));
            dt.Columns.Add("DoiTuongKiemToan", typeof(string));
            dt.Columns.Add("Nam", typeof(string));
            dt.Columns.Add("ThoiGianDuKien", typeof(string));

            dt.Columns.Add("ThoiGian", typeof(DateTime));

            //lay tat ca HeThong(co file dinhkem)
            string DocFields_HT = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiêt],[Khuyến nghị],[Đợt kiểm toán]";
            string PropertyFields_HT = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiêt,Khuyến nghị,Đợt kiểm toán";
            string Condition_HT = doituongkt == "-1" ? String.Empty :  " And [Đối tượng kiểm toán]=N'" + doituongkt + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsHT = doc.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT);
            if (isValidDataSet(dsHT))
            {
                foreach (DataRow rowHT in dsHT.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowHT["PK_DocumentID"];
                    newRow["PhatHien"] = rowHT["Phát hiện"];
                    newRow["Kieu"] = "HT";
                    newRow["MucDo"] = rowHT["Mức độ"];
                    newRow["ChiTiet"] = rowHT["Chi tiêt"];
                    newRow["AnhHuong"] = rowHT["Ảnh hưởng"];
                    newRow["GhiChu"] = rowHT["Ghi chú"];
                    newRow["KhuyenNghi"] = rowHT["Khuyến nghị"];

                    newRow["DotKiemToan"] = rowHT["Đợt kiểm toán"];
                    newRow["LoaiDoiTuongKiemToan"] = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Loại đối tượng kiểm toán");
                    newRow["DoiTuongKiemToan"] = rowHT["Đối tượng kiểm toán"];
                    string nam = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Năm");
                    string thoigiandukienkiemtoan = GetThongTinDotKiemToan(rowHT["Đợt kiểm toán"].ToString(), "Thời gian dự kiến kiểm toán");

                    newRow["Nam"] = nam;
                    newRow["ThoiGianDuKien"] = thoigiandukienkiemtoan;

                    if (!String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(thoigiandukienkiemtoan))
                    {
                        string thoigian = String.Format("1/{0}/{1}", thoigiandukienkiemtoan.Replace("Tháng", "").Trim(), nam);
                        newRow["ThoiGian"] = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(thoigian));
                    }
                    dt.Rows.Add(newRow);
                }
            }
            //lay tat ca ViPham(ko file dinhkem)
            string DocFields_VP = "PK_DocumentID,[Nguyên nhân],[Phát hiện],[Đối tượng kiểm toán],[Mức độ],[Ảnh hưởng],[Ghi chú],[Chi tiết],[Khuyến nghị],[Đợt kiểm toán]";
            string PropertyFields_VP = "Nguyên nhân,Phát hiện,Đối tượng kiểm toán,Mức độ,Ảnh hưởng,Ghi chú,Chi tiết,Khuyến nghị,Đợt kiểm toán";
            string Condition_VP = doituongkt == "-1" ? String.Empty : " And [Đối tượng kiểm toán]=N'" + doituongkt + "'";
            DataSet dsVP = doc.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP);
            if (isValidDataSet(dsVP))
            {
                foreach (DataRow rowVP in dsVP.Tables[0].Rows)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["PK_DocumentID"] = rowVP["PK_DocumentID"];
                    newRow["PhatHien"] = rowVP["Phát hiện"];
                    newRow["Kieu"] = "VP";
                    newRow["MucDo"] = rowVP["Mức độ"];
                    newRow["ChiTiet"] = rowVP["Chi tiết"];
                    newRow["AnhHuong"] = rowVP["Ảnh hưởng"];
                    newRow["GhiChu"] = rowVP["Ghi chú"];
                    newRow["KhuyenNghi"] = rowVP["Khuyến nghị"];

                    newRow["DotKiemToan"] = rowVP["Đợt kiểm toán"];
                    newRow["LoaiDoiTuongKiemToan"] = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Loại đối tượng kiểm toán");
                    newRow["DoiTuongKiemToan"] = rowVP["Đối tượng kiểm toán"];
                    string nam = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Năm");
                    string thoigiandukienkiemtoan = GetThongTinDotKiemToan(rowVP["Đợt kiểm toán"].ToString(), "Thời gian dự kiến kiểm toán");

                    newRow["Nam"] = nam;
                    newRow["ThoiGianDuKien"] = thoigiandukienkiemtoan;

                    if (!String.IsNullOrEmpty(nam) && !String.IsNullOrEmpty(thoigiandukienkiemtoan))
                    {
                        string thoigian = String.Format("1/{0}/{1}", thoigiandukienkiemtoan.Replace("Tháng", "").Trim(), nam);
                        newRow["ThoiGian"] = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(thoigian));
                    }
                    dt.Rows.Add(newRow);
                }
            }

            string filter = "ThoiGian >= '" + String.Format("{0:M/d/yyyy}", dtTuNgay) + "' AND ThoiGian <= '" + String.Format("{0:M/d/yyyy}", dtDenNgay) + "'";
            DataRow[] drs = dt.Select(filter);
            //make a new "results" datatable via clone to keep structure
            DataTable dt2 = dt.Clone();
            //Import the Rows
            foreach (DataRow d in drs)
                dt2.ImportRow(d);

            dataCtrl.DataSource = dt2;
            dataCtrl.DataBind();
        }

        string GetThongTinDotKiemToan(string tenDotKT,string columnName)
        {
            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán]";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";
            string Condition = " And  [Tên đợt kiểm toán]=N'" + tenDotKT + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.DOT_KIEMTOAN, DocFields,PropertyFields,Condition);
            if (isValidDataSet(ds))
                return ds.Tables[0].Rows[0][columnName].ToString();
            return String.Empty;
        }


        #endregion end DanhSachPhatHien
    }
}