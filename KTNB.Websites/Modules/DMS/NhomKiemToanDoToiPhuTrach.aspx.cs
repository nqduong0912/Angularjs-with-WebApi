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
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class NhomKiemToanDoToiPhuTrach : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid_nhomkiemtoan = DOCTYPE.NHOM_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _task = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;
        protected bool _isTruongDoan = true;
        protected string _role = string.Empty;
        protected string caption = string.Empty;
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
            _doankt = Request["doankt"];
            _dotkt = Request["dotkt"];
            _task = Request["task"];
            if (Request["role"] != null)
                _role = Request["role"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form

            if (_role.Equals("tv"))
            {
                caption = "Lập hồ sơ rủi ro";
                lblActor.Text = "Thành viên";
            }
            else
            {
                lblActor.Text = "Trưởng nhóm";
                caption = "Lập phân tích sơ bộ";
            }
            if (!string.IsNullOrEmpty(_task))
            {
                caption = "Đánh giá rủi ro còn lại";
            }
            txtActor.Text = _objUserContext.UserName;

            base.InitForm(caption, string.Empty, _doctypeid_nhomkiemtoan, 0);
            //_isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            //if (_isTruongDoan == true)
            //    _btnAddNew.Visible = true;
            //else
            //    _btnAddNew.Visible = false;
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
            if (!Page.IsPostBack) {
                //GetList(_doctypeid_nhomkiemtoan, _role, "");
            }
            
        }

        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetList(_doctypeid_nhomkiemtoan, _role, "");
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        //private void GetList(string DocumentTypeID_doankiemtoan, string dotkt)
        //{
        //    string DocFields = "PK_DocumentID,[Name]";
        //    string PropertyFields = "Name";
        //    string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";

        //    ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_doankiemtoan;
        //    ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
        //    ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
        //    ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
        //    dataCtrl.DataBind();
        //}

        private void GetListDoanKiemToan(string DocumentTypeID_doankiemtoan, string dotkt)
        {
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";

            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(DocumentTypeID_doankiemtoan, DocFields, PropertyFields, Condition);
            if (ds != null)
            {
                dataCtrl.DataSource = ds;
                dataCtrl.DataBind();
            }
        }

        //private void GetListNhomKiemToan(string DocumentTypeID_nhomkiemtoan)
        //{
        //    string DocFields = "PK_DocumentID,[Name],STATUS";
        //    string PropertyFields = "Name,STATUS";
        //    //string Condition = string.Empty;
        //    string Condition = string.Empty;
        //    //truong nhom
        //    if (_role.Equals(""))
        //    {
        //        Condition = " and STATUS=4 and PK_DocumentID in (Select PK_DocumentID From T_Document Where name='" + _objUserContext.UserName + "')";
        //        lblActor.Text = "Trưởng nhóm";
        //    }
        //    //thanh vien
        //    else
        //    {
        //        Condition = " and STATUS=4  and PK_DocumentID in (select thanhvien_nhom.FK_DOCLINKID from T_DOCLINK as thanhvien_nhom where thanhvien_nhom.FK_DOCUMENTID ='" + _objUserContext.UserID + "')";
        //        lblActor.Text = "Thành viên";
        //    }
        //    txtActor.Text = _objUserContext.UserName;
        //    bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
        //    DataSet dsNhomKiemToan = obj.getDocumentList(DocumentTypeID_nhomkiemtoan, DocFields, PropertyFields, Condition);
        //    DataTable dtNhomKiemToan = dsNhomKiemToan.Tables[0];
        //    dtNhomKiemToan.Columns.Add("doi_tuong_kiem_toan", typeof(String));
        //    dtNhomKiemToan.Columns.Add("ten_dot_kiem_toan", typeof(String));
        //    dtNhomKiemToan.Columns.Add("dotkt", typeof(String));

        //    bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);

        //    foreach (DataRow row in dtNhomKiemToan.Rows)
        //    {
        //        DataSet dsDotKT = doanKiemToan.GetDotKTByNhomKT(row["PK_DocumentID"].ToString());
        //        if (isValidDataSet(dsDotKT))
        //        {
        //            row["doi_tuong_kiem_toan"] = dsDotKT.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
        //            row["ten_dot_kiem_toan"] = dsDotKT.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
        //            row["dotkt"] = dsDotKT.Tables[0].Rows[0]["dotkt"].ToString();
        //        }
        //    }

        //    if (isValidDataSet(dsNhomKiemToan))
        //    {
        //        dataCtrl.DataSource = dtNhomKiemToan;
        //        dataCtrl.DataBind();
        //    }
        //}

        #endregion
        protected void dataCtrl_OnSorting(object sender, C1SortingCommandEventArgs e)
        {
            CurrentSortExpression = e.SortExpression;

            // Ignore e.SortDirection since it is always Ascending, an apparent bug in .NET
            if (CurrentSortDirection == "asc")
                CurrentSortDirection = "desc";
            else
                CurrentSortDirection ="asc";

            string strSort = string.Empty;
            if (CurrentSortDirection == "desc")
                strSort = "DESC";
            //GetList(_doctypeid_nhomkiemtoan, _role, CurrentSortExpression + " " + strSort);
            ObjectDataSource1.SelectParameters["DocTypeNhomKT"].DefaultValue = _doctypeid_nhomkiemtoan;
            ObjectDataSource1.SelectParameters["role"].DefaultValue = _role;
            ObjectDataSource1.SelectParameters["currentUserID"].DefaultValue = _objUserContext.UserID;
            ObjectDataSource1.SelectParameters["currentUserName"].DefaultValue = _objUserContext.UserName;
            ObjectDataSource1.SelectParameters["task"].DefaultValue = _task;
            ObjectDataSource1.SelectParameters["sort"].DefaultValue = CurrentSortExpression + " " + strSort;
        }
        private string CurrentSortDirection
        {
            get
            {
                if (sortDirection.Value == null)
                    sortDirection.Value = "asc";
                return sortDirection.Value;
            }
            set { sortDirection.Value = value; }
        }

        private string CurrentSortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                    ViewState["SortExpression"] = "ten_dot_kiem_toan";
                return (string)ViewState["SortExpression"];
            }
            set { ViewState["SortExpression"] = value; }
        }
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
                Label lblMangNghiepVu = (Label)e.Item.FindControl("lblMangNghiepVu") as Label;
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                //Label SLThanhVienTheoMNV = (Label)e.Item.FindControl("SLThanhVienTheoMNV") as Label;
                Label SLThanhVien = (Label)e.Item.FindControl("SLThanhVien") as Label;
                Label SLMangNghiepVu = (Label)e.Item.FindControl("SLMangNghiepVu") as Label;

                Label lblCommand = (Label)e.Item.FindControl("lblCommand") as Label;
                //Button btnPhanTich = (Button)e.Item.FindControl("btnPhanTich") as Button;
                string truongnhom = String.IsNullOrEmpty(e.Item.Cells[2].Text) ? String.Empty : e.Item.Cells[2].Text;
                if (PK_DocumentID != null)
                {
                    //imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "','" + _role + "','" + truongnhom + "')}");
                    lblCommand.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "','" + _role + "','" + truongnhom + "','" + _task + "')}");
                    lblCommand.Text = caption;
                    //btnPhanTich.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "','" + _role + "','" + truongnhom + "')}");
                    //btnPhanTich.Text = "Phân tích";
                    //btnPhanTich.CausesValidation = false;
                    //GetSoLuongThanhVienTheoMNV(PK_DocumentID.Text, SLThanhVienTheoMNV);
                    //GetSoLuongThanhVien(PK_DocumentID.Text, SLThanhVien);
                    //GetSoLuongMNV(PK_DocumentID.Text, SLMangNghiepVu);
                    lblMangNghiepVu.Text = BuildMangNghiepVuInfo(PK_DocumentID.Text, lblMangNghiepVu);
                }
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Label dotkt = (Label)e.Item.FindControl("dotkt") as Label;
                if (imgEdit != null && dotkt != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocumentDotKT('" + dotkt.Text + "')}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocTypeNhomKT, string role, string sortColumn)
        {
            string Condition = String.Empty;//" Order By [Loại đối tượng kiểm toán]";
            ObjectDataSource1.SelectParameters["DocTypeNhomKT"].DefaultValue = DocTypeNhomKT;
            ObjectDataSource1.SelectParameters["role"].DefaultValue = role;
            ObjectDataSource1.SelectParameters["currentUserID"].DefaultValue = _objUserContext.UserID;
            ObjectDataSource1.SelectParameters["currentUserName"].DefaultValue = _objUserContext.UserName;
            ObjectDataSource1.SelectParameters["task"].DefaultValue = _task;
            ObjectDataSource1.SelectParameters["sort"].DefaultValue = sortColumn;
            dataCtrl.DataBind();
        }
        private string BuildMangNghiepVuInfo(string nhomkt, Label label)
        {
            string html = string.Empty;
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsMangNghiepVu = doanKiemToan.GetMangNVByNhomKT(nhomkt);
            if (isValidDataSet(dsMangNghiepVu))
            {

                html += "<table class=\"table-nhom\" width='100%' style=\"border:1px solid black;border-collapse:collapse;\">";
                html += "   <col width=\"207px\" />";
                html += "   <col width=\"50px\" />";
                html += "   <col width=\"50px\" />";
                html += "   <col width=\"50px\" />";
                html += "   <tr>";
                html += "       <th style=\"border:1px solid black;\">Mảng Nghiệp vụ";
                html += "       </th>";
                html += "       <th style=\"border:1px solid black;\">Thành Viên";
                html += "       </th>";
                html += "       <th style=\"border:1px solid black;\">PTSB";
                html += "       </th>";
                html += "       <th style=\"border:1px solid black;\">PTRR";
                html += "       </th>";
                html += "   </tr>";

                foreach (DataRow row in dsMangNghiepVu.Tables[0].Rows)
                {
                    //kiem tra trang thai mang nghiep vu da duoc PTSB va PTRR chua?
                    bool statusPTSB = CheckPTSB(nhomkt, row["mangnv"].ToString());
                    bool statusPTRR = CheckPTRR(nhomkt, row["mangnv"].ToString());
                    string sImg = "<img src=\"../../Images/check.gif\" />";
                    html += "   <tr>";
                    html += "       <td style=\"border:1px solid black;\">" + row["ten_mang_nghiep_vu"].ToString();
                    html += "       </td>";
                    html += "       <td style=\"border:1px solid black;\">" + GetThanhVienThuocNhomByMangNV(nhomkt, row["mangnv"].ToString());
                    html += "       </td>";
                    html += "       <td align=\"center\" style=\"border:1px solid black;\">";
                    if (statusPTSB)
                        html += sImg;
                    html += "       </td>";
                    html += "       <td align=\"center\" style=\"border:1px solid black;\">";
                    if (statusPTRR)
                        html += sImg;
                    html += "       </td>";
                    html += "   </tr>";
                }
                html += "</table>";
            }
            return html;
        }
        private string GetThanhVienThuocNhomByMangNV(string nhomkt, string mangnv)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsNhomKT = doanKiemToan.DanhSachThanhVienNhomKT(nhomkt);
            if (isValidDataSet(dsNhomKT))
            {
                DataRow[] foundRows;
                string expression = "MangNghiepVu = '" + mangnv + "'";
                foundRows = dsNhomKT.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    return foundRows[0]["username"].ToString();

            }
            return string.Empty;
        }
        private bool CheckPTSB(string nhomkt, string mangnv)
        {
            bool status = false;
            string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(mangnv, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");

            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomkt);

            if (isValidDataSet(dsPhanTichSoBo))
            {
                DataRow[] foundRows;
                string expression = "Status = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";
                foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    status = true;
            }

            return status;
        }
        private bool CheckPTRR(string nhomkt, string mangnv)
        {
            bool status = false;
            string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(mangnv, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");

            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichRuiRo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomkt);

            if (isValidDataSet(dsPhanTichRuiRo))
            {
                DataRow[] foundRowsByMangNghiepVu;
                DataRow[] foundRowsDaDanhGia;

                string expressionByMangNghiepVu = "Status = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";
                string expressionDaDanhGia = "xac_suat > 0 and anh_huong > 0 and Status = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";

                foundRowsByMangNghiepVu = dsPhanTichRuiRo.Tables[0].Select(expressionByMangNghiepVu);
                foundRowsDaDanhGia = dsPhanTichRuiRo.Tables[0].Select(expressionDaDanhGia);

                if ((foundRowsDaDanhGia.Count() == foundRowsByMangNghiepVu.Count()) && foundRowsDaDanhGia.Count() > 0)
                    status = true;
            }

            return status;
        }
        private void GetSoLuongThanhVienTheoMNV(string truongnhom, Label lbl)
        {
            if (String.IsNullOrEmpty(truongnhom))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(truongnhom);
            if (isValidDataSet(ds))
                lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }
        #endregion
    }
}