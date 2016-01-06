using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DotKiemToan_MangNghiepVu : PageBase
    {
        #region initiation page variables

        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOAN_KIEMTOAN;
        protected string _doctypeid_dotkiemtoan = DOCTYPE.DOT_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _username = string.Empty;
        int _status_dotkt = 0;

        protected string _truongdoan = string.Empty;
        protected bool _isTruongPhong = true;
        protected string _dsmangNghiepVu = string.Empty;
        protected DataSet _dataSetMangNghiepVuMapped = new DataSet();
        protected int _status_doankt = 0;
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

            #region get data submit
            _documentid = Request["doc"];
            _dotkt = Request["dotkt"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            _username = Request["user"];
            _dsmangNghiepVu = Request["dsmangnghiepvu"];

            _truongdoan = Request["truongdoan"];

            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                else if (_action == "getfullname")
                    FeedBackClient(getUserFullName(_username));
                else if (_action == "getfullnamethanhvien")
                    FeedBackClient(getUserFullNameThanhVien(_username));
                else if (_action == "themthanhvien")
                    FeedBackClient(ThemThanhVien(_username));
                else if (_action == "xoathanhvien")
                    FeedBackClient(XoaThanhVien(_documentid));
                else if (_action == "mappingmangnghiepvu")
                    FeedBackClient(MappingMangNghiepVu(_dsmangNghiepVu));

            }
            //check doc status
            if (_documentid != null)
            {
                _status_doankt = CommonFunc.GetDocStatus(_documentid);
            }
            #endregion

            #region init form
            string caption = "Chọn Mảng Nghiệp Vụ Cho Đợt Kiểm Toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler
            _isTruongPhong = (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN ? true : false);
            if (_isTruongPhong == true)
                _btnAddNew.Visible = true;
            else
                _btnAddNew.Visible = false;
            CommonFunc.SetButtonBack(_btnCloseWindow, "DoanKiemToan.aspx?dotkt=" + _dotkt);
            _btnAddNew.Text = "Save";
            _btnAddNew.Attributes.Add("onclick", "javascript:SaveMangNghiepVu(); return false;");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _dataSetMangNghiepVuMapped = GetMangNghiepVuMapped(_documentid);
                GetListMangNghiepVuByLoaiDoiTuong();
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        this.DOCNAME.Text = _truongdoan;
                    }
                }
            }
        }
        public string MappingMangNghiepVu(string strDSMangNghiepVu)
        {
            //if (_docstatus != 4)
            //{
            //delete all old mapping befor add
            DataSet dataSetMangNghiepVu = GetList();
            if (isValidDataSet(dataSetMangNghiepVu))
            {
                foreach (DataRow row in dataSetMangNghiepVu.Tables[0].Rows)
                {
                    CommonFunc.RemoveDocLink(row["PK_DocumentID"].ToString(), _documentid, _objUserContext);
                }
            }
            //}

            string[] dsMangNghiepVu = strDSMangNghiepVu.Split(new string[] { "__" }, StringSplitOptions.None);
            foreach (string mangNghiepVu in dsMangNghiepVu)
            {
                if (mangNghiepVu != "")
                {
                    CommonFunc.AddDocLink(mangNghiepVu, _documentid, TYPE_OF_LINK.DOCUMENT, _objUserContext);
                }
            }
            return string.Empty;
        }
        #endregion

        #region page helper processing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected string getUserFullNameThanhVien(string username)
        {
            string FullName = "";
            //DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='" + ROLES.THANHVIEN_KIEMTOAN + "')", "FullName=FullName+' - '+GroupName,PK_UserID");
            string groupName = GetGroupNameByDotKiemToan();
            bus_User_In_Group group = new bus_User_In_Group(_objUserContext);
            DataSet ds = group.getList(" AND GROUPNAME=N'" + groupName + "' And UserName='" + username + "'", "FullName=FullName+' - '+GroupName,PK_UserID");
            if (isValidDataSet(ds))
                FullName = ds.Tables[0].Rows[0]["FullName"].ToString() + "#" + ds.Tables[0].Rows[0]["PK_UserID"].ToString();
            return FullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        protected string getUserFullName(string username)
        {
            string FullName = "";
            string groupName = GetGroupNameByDotKiemToan();
            //DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='" + ROLES.THANHVIEN_KIEMTOAN + "')", "FullName=FullName+' - '+GroupName");
            bus_User_In_Group group = new bus_User_In_Group(_objUserContext);
            DataSet ds = group.getList(" AND GROUPNAME=N'" + groupName + "' And UserName='" + username + "'", "FullName=FullName+' - '+GroupName,PK_UserID");
            if (isValidDataSet(ds))
                FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            return FullName;
        }
        private void GetListMangNghiepVuByLoaiDoiTuong()
        {
            string tenLoaiDoiTuong = CommonFunc.getPropertyValueOnDocument(_dotkt, "737694DF-DE17-4FF9-AE15-70E197C83593");
            string LoaiDoiTuongID = string.Empty;
            bus_Doankiemtoan doankiemtoan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsLoaiDoiTuong = doankiemtoan.GetLoaiDoiTuongByName(tenLoaiDoiTuong);
            if (isValidDataSet(dsLoaiDoiTuong))
                LoaiDoiTuongID = dsLoaiDoiTuong.Tables[0].Rows[0]["PK_DOCUMENTID"].ToString();

            string DocFields = "PK_DocumentID,Status,[Tên mảng nghiệp vụ]";
            string PropertyFields = "Tên mảng nghiệp vụ";

            string Condition = " and Status = 4 and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + LoaiDoiTuongID + "')";
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DOCTYPE.MANG_NGHIEPVU;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            GridPhamViMangNghiepVu.DataBind();


        }
        private DataSet GetList()
        {
            string tenLoaiDoiTuong = CommonFunc.getPropertyValueOnDocument(_dotkt, "737694DF-DE17-4FF9-AE15-70E197C83593");
            string LoaiDoiTuongID = string.Empty;
            bus_Doankiemtoan doankiemtoan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsLoaiDoiTuong = doankiemtoan.GetLoaiDoiTuongByName(tenLoaiDoiTuong);
            if (isValidDataSet(dsLoaiDoiTuong))
                LoaiDoiTuongID = dsLoaiDoiTuong.Tables[0].Rows[0]["PK_DOCUMENTID"].ToString();

            string DocFields = "PK_DocumentID,Status,[Tên mảng nghiệp vụ]";
            string PropertyFields = "Tên mảng nghiệp vụ";

            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + LoaiDoiTuongID + "')";

            bus_Document obj = new bus_Document(_objUserContext);
            DataSet ds = obj.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);
            return ds;
        }
        //private DataSet GetL
        protected string GetGroupNameByDotKiemToan()
        {
            if (String.IsNullOrEmpty(_dotkt))
                return null;
            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán]";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";
            string Condition = String.Empty;
            bus_Document obj = new bus_Document(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid_dotkiemtoan, DocFields, PropertyFields, String.Empty);
            //bus_Document obj = bus_Document.Instance(_objUserContext);
            //DataSet ds = obj.loadDocumentInfo(_dotkt);
            if (isValidDataSet(ds))
            {
                DataRow[] rows = ds.Tables[0].Select("[PK_DocumentID]='" + _dotkt + "'");
                if (rows.Length > 0)
                {
                    return rows[0]["Đơn vị thực hiện"].ToString();
                }
            }
            return String.Empty;
        }
        public DataSet GetMangNghiepVuMapped(string DoanKiemToanID)
        {
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.GetMangNghiepVuByDoanKT(DoanKiemToanID);
            return ds;
        }
        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string ThemThanhVien(string thanhvien)
        {
            string FullName = "2";
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(_truongdoan);
            if (isValidDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select("PK_UserID ='" + thanhvien + "'");
                if (rows.Count() > 0)
                {
                    FullName = "1";
                    return FullName;
                }
            }
            CommonFunc.AddDocLink(thanhvien, _truongdoan, TYPE_OF_LINK.USER, _objUserContext);
            return FullName;
        }


        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string XoaThanhVien(string doc)
        {
            string truongdoan = _truongdoan;
            CommonFunc.RemoveDocLink(doc, truongdoan, _objUserContext);
            string errormessage = "2";
            return errormessage;
        }



        #endregion

        #region page button processing
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            //var car = Json.Decode(json);
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_UserID = (Label)e.Item.FindControl("PK_UserID") as Label;
                Label FK_DOCLINKID = (Label)e.Item.FindControl("FK_DOCLINKID") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                if (PK_UserID != null && FK_DOCLINKID != null)
                {
                    if (_isTruongPhong == false)
                        imgDelete.Visible = false;
                    imgDelete.Attributes.Add("onclick", "{xoathanhvien('" + e.Item.ClientID + "','" + PK_UserID.Text + "','" + FK_DOCLINKID.Text + "')}");
                }

            }
        }
        protected void GridPhamViMangNghiepVu_OnItemDataBound(object sender, C1ItemEventArgs e)
        {

            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                CheckBox chkMangNghiepVu = (CheckBox)e.Item.FindControl("chkMangNghiepVu") as CheckBox;
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                if (chkMangNghiepVu != null)
                {
                    //imgDelete.Attributes.Add("onclick", "{xoathanhvien('" + e.Item.ClientID + "','" + PK_UserID.Text + "','" + FK_DOCLINKID.Text + "')}");
                    //chkMangNghiepVu.ClientID = PK_DocumentID.Text;
                    chkMangNghiepVu.Attributes.Add("mangnghiepvuid", PK_DocumentID.Text);

                    //binding mapped mang nghiep vu
                    if (isValidDataSet(_dataSetMangNghiepVuMapped))
                    {
                        DataRow[] foundRows;
                        string expression = "PK_DocumentID = '" + PK_DocumentID.Text + "'";
                        // Use the Select method to find all rows matching the filter.
                        foundRows = _dataSetMangNghiepVuMapped.Tables[0].Select(expression);
                        if (foundRows.Count() > 0)
                        {
                            chkMangNghiepVu.Checked = true;
                        }
                        //bool IsHaveMangNghiepVuInPTSB = IsHavePhanTichSoBoMangNghiepVu(PK_DocumentID.Text, _documentid);
                        //bool IsHaveNhomMangNV = IsHaveNhomMangNghiepVu(PK_DocumentID.Text, _documentid);
                        //if (_docstatus == 4 && chkMangNghiepVu.Checked)
                        //    chkMangNghiepVu.Enabled = false;
                        _status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                        //check can remove ?
                        bool IsCotheThem = TrangThaiDotKiemToan.IsCotheThemMangNghiepVu(_status_dotkt);
                        bool IsCotheXoa = TrangThaiDotKiemToan.IsCoTheXoaMangNghiepVu (_status_dotkt);
                        if (chkMangNghiepVu.Checked) // code cu: _status_dotkt >= 12
                        {
                            if(IsCotheXoa)
                                chkMangNghiepVu.Enabled = true;
                            else
                                chkMangNghiepVu.Enabled = false;
                        }
                        //check can add ?
                        if (chkMangNghiepVu.Checked == false) // code cu: _status_dotkt >= 31
                        {
                            if(IsCotheThem)
                                chkMangNghiepVu.Enabled = true;
                            else
                                chkMangNghiepVu.Enabled = true;
                        }
                        
                    }
                }

            }
        }
        public bool IsHavePhanTichSoBoMangNghiepVu(string MangNghiepVuID, string DoanID)
        {
            bool IsHave = false;
            string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(MangNghiepVuID, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsNhom = doanKiemToan.DanhSachNhomByDoanKT(DoanID);
            if (isValidDataSet(dsNhom))
            {
                if (dsNhom.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow rowNhom in dsNhom.Tables[0].Rows)
                    {
                        string NhomID = rowNhom["NhomID"].ToString();
                        bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                        IsHave = lapKeHoach.IsHavePhanTichSoBoMangNghiepVu(sTenMangNghiepVu, NhomID);
                    }
                }
            }
            return IsHave;
        }
        public bool IsHaveNhomMangNghiepVu(string MangNghiepVuID, string DoanID)
        {
            bool IsHave = false;
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsNhom = doanKiemToan.DanhSachNhomByDoanKT(DoanID);
            if (isValidDataSet(dsNhom))
            {
                if (dsNhom.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow rowNhom in dsNhom.Tables[0].Rows)
                    {
                        string NhomID = rowNhom["NhomID"].ToString();
                        //bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                        //IsHave = lapKeHoach.IsHavePhanTichSoBoMangNghiepVu(sTenMangNghiepVu, NhomID);
                        DataSet dsThanhVien = doanKiemToan.DanhSachThanhVienNhomKT(NhomID);
                        if (isValidDataSet(dsThanhVien))
                        {
                            DataRow[] foundRows;
                            string expression = "MangNghiepVu ='" + MangNghiepVuID + "'";
                            foundRows = dsThanhVien.Tables[0].Select(expression);
                            if (foundRows.Count() > 0)
                                return true;

                        }
                    }
                }
            }
            return IsHave;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
        }
        #endregion
    }
}