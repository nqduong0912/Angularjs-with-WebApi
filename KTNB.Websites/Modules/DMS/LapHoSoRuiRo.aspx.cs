using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using CORE.UMS.CoreBusiness;
using System.Data;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class LapHoSoRuiRo : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doankt = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _doctypeid_kiemsoat = DOCTYPE.KIEMSOAT;
        protected string _doctypeid_ruiro = DOCTYPE.RUIRO_KIEMSOAT;
        protected string _doctypeid_thutuc = DOCTYPE.THUTUC_KIEMTOAN;
        DataSet dsDotKTInfo = new DataSet();
        int _status_dotkt = 0;

        protected string _hosoChiTiet = string.Empty;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _nhomkt = Request["doc"];
            //_dotkt = Request["dotkt"];
            _action = Request["act"];
            _hosoChiTiet = Request["hosochitiet"];
            if (!string.IsNullOrEmpty(_nhomkt))
            {
                bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
                dsDotKTInfo = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
                if (isValidDataSet(dsDotKTInfo))
                {
                    _dotkt = dsDotKTInfo.Tables[0].Rows[0]["dotkt"].ToString();
                    _doankt = dsDotKTInfo.Tables[0].Rows[0]["dotkt"].ToString();
                }
            }

            #endregion

            //if (string.IsNullOrEmpty(_documentid))
            //    _viewtype = VIEWTYPE.ADDNEW;
            //else
            //    _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                else if (_action == "xoachitietphantich")
                {
                    FeedBackClient(xoaChiTietPhanTich(_hosoChiTiet, _nhomkt));
                }
                else if (_action == "submit")
                {
                    FeedBackClient(Submit(_nhomkt));
                }

            }

            #endregion

            #region init form
            string caption = "Hồ sơ rủi ro";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = _nhomkt;
            #endregion

            #region client control event handler
            //Truongphong->tao doan kiem toan
            //if (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
            //    btnDongy.Visible = btnThem.Visible = true;
            //else
            //    btnDongy.Visible = btnThem.Visible = false;
            //btnThemVDQuanTam.Attributes.Add("onclick", "{ThemVDQuanTam('" + _doctypeid + "'); return false;}");
            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            _btnSave.Visible = true;
            _btnSave.Text = "Submit";
            _btnSave.Attributes.Add("onclick", "{Submit(); return false;}");
            #endregion
        }
        public string xoaChiTietPhanTich(string hoso_chitiet, string nhomkt)
        {
            CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            return string.Empty;
        }
        public string Submit(string nhomkt)
        {
            bool IsSubmit = false;
            bus_LapKeHoach lapkehoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsChiTietRuiRo = lapkehoach.ChiTietHoSoPhanTichRuiRo(nhomkt, _objUserContext.UserName);
            if (isValidDataSet(dsChiTietRuiRo))
            {
                foreach (DataRow row in dsChiTietRuiRo.Tables[0].Rows)
                {
                    if (row["xac_suat"].ToString() != "0" && row["anh_huong"].ToString() != "0")
                    {
                        CommonFunc.UpdateDocStatus(row["hoso"].ToString(), 4);
                        IsSubmit = true;
                    }
                }
                //thangma: update Trang thai Dot kiem toan 
                //len trang thai Lap chuong trinh kiem toan
                //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                //if (_status_dotkt < 15)
                //    CommonFunc.UpdateDocStatus(_dotkt, 15);
                //end
            }
            return IsSubmit ? "submitted" : "notsubmit";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDotKiemToanInfo();
                GenerateHSRRForFirstDo();
                //GetListHoSoPhanTichSoBo(_nhomkt);
                BindHSRRGrid(_nhomkt);
            }

        }
        public void LoadDotKiemToanInfo()
        {
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
            if (isValidDataSet(ds))
            {
                txtDoiTuongKiemToan.Text = ds.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString(); //CommonFunc.getPropertyValueOnDocument(_dotkt, doiTuongKiemToan_propID);
                txtDotKiemToan.Text = ds.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
            }
        }
        public void GenerateHSRRForFirstDo()
        {
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet dsMangNVByThanhVien = doanKiemToan.GetMangNVByThanhVien(_nhomkt, _objUserContext.UserID);
            //check neu chua co rui ro thi generate
            string _doctypeid_hosoruiro = DOCTYPE.CHITIET_HOSO_RUIRO;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and CREATEDBY='" + _objUserContext.UserName + "' and PK_DocumentID in ( select hosoruiro_nhom.FK_DOCUMENTID from T_DOCLINK as hosoruiro_nhom where hosoruiro_nhom.FK_DOCLINKID = '" + _nhomkt + "')";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsHoSoRuiRo = obj.getDocumentList(_doctypeid_hosoruiro, DocFields, PropertyFields, Condition);
            if (isValidDataSet(dsMangNVByThanhVien))
            {
                BindGridVanDeQuanTam(dsMangNVByThanhVien);
            }
            //do generate
            if (isValidDataSet(dsMangNVByThanhVien))
            {
                DataTable dtMangNVByThanhVien = dsMangNVByThanhVien.Tables[0];
                //kiem tra neu phan tich so bo da submit thi moi generate
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(_nhomkt);
                if (isValidDataSet(dsPhanTichSoBo))
                {
                    if (dtMangNVByThanhVien.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtMangNVByThanhVien.Rows)
                        {
                            string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(row["mangnv"].ToString(), "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");

                            DataRow[] foundRows;
                            string expression = "STATUS = 4 and mang_nghiep_vu='" + sTenMangNghiepVu + "'";
                            foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                            //phai tat ca cac ho so phan tich so bo da duoc submit thi moi generate phan tich rui ro
                            if (foundRows.Count() > 0)
                            {
                                //check xem da generate lan 1 chua
                                bool IsGenerated = CheckGenerated(sTenMangNghiepVu, _nhomkt);
                                if (IsGenerated)
                                { //do nothing
                                }
                                else
                                {
                                    InsertHoSoRuiRo(row["mangnv"].ToString());
                                    //chuyển trạng thái đợt lên: Phân tích chi tiết
                                    //CommonFunc.ChuyenTrangThaiDotKT(_dotkt, 14);
                                    TrangThaiDotKiemToan.SetStatus(_dotkt, 14);
                                }

                            }
                        }
                    }
                }
            }
        }
        public bool CheckGenerated(string tenMangNghiepVu, string nhomkt)
        {
            bool IsGenerated = false;
            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomkt);
            if (isValidDataSet(dsPhanTichSoBo))
            {
                DataRow[] foundRows;
                string expression = "mang_nghiep_vu = '" + tenMangNghiepVu + "'";
                foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    IsGenerated = true;
            }

            return IsGenerated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            //string HoSoPhanTichSoBoID = hiddenNhomKT.Value;
            BindHSRRGrid(_nhomkt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void BindHSRRGrid(string nhomKT)
        {
            if (String.IsNullOrEmpty(nhomKT))
                return;
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichRuiRo(nhomKT, _objUserContext.UserName);
            if (isValidDataSet(ds))
            {
                //DataView dv = ds.Tables[0].DefaultView;
                //dv.Sort = "mang_nghiep_vu ASC";
                //dataCtrl.DataSource = dv.ToTable();
                //dataCtrl.DataBind();
                ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = nhomKT;
                dataCtrl.DataBind();

                DataRow[] foundRows;
                string expression = "xac_suat > 0 and anh_huong > 0";
                foundRows = ds.Tables[0].Select(expression);
                txtSoRuiRoDaDanhGia.Text = foundRows.Count().ToString() + "/" + ds.Tables[0].Rows.Count.ToString();
            }
            
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
                    ViewState["SortExpression"] = "rui_ro";
                return (string)ViewState["SortExpression"];
            }
            set { ViewState["SortExpression"] = value; }
        }
        protected void dataCtrl_OnSorting(object sender, C1SortingCommandEventArgs e)
        {
            CurrentSortExpression = e.SortExpression;

            // Ignore e.SortDirection since it is always Ascending, an apparent bug in .NET
            if (CurrentSortDirection == "asc")
                CurrentSortDirection = "desc";
            else
                CurrentSortDirection = "asc";

            string strSort = string.Empty;
            if (CurrentSortDirection == "desc")
                strSort = "DESC";
            //GetList(_doctypeid_nhomkiemtoan, _role, CurrentSortExpression + " " + strSort);
            ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = _nhomkt;
            ObjectDataSource1.SelectParameters["sort"].DefaultValue = CurrentSortExpression + " " + strSort;
        }
        public bool CheckSubmitted(string tenMangNghiepVu, string nhomKiemToanID)
        {
            bool IsSubmitted = false;
            bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsPhanTichSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKiemToanID);
            if (isValidDataSet(dsPhanTichSoBo))
            {
                DataRow[] foundRows;
                string expression = "STATUS=4 and  mang_nghiep_vu ='" + tenMangNghiepVu + "'";
                foundRows = dsPhanTichSoBo.Tables[0].Select(expression);
                if (foundRows.Count() > 0)
                    IsSubmitted = true;
            }

            return IsSubmitted;
        }
        protected void GridVanDeQuanTam_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                bool IsSubmitted = false;
                Label lblVanDeQuanTam = (Label)e.Item.FindControl("lblVanDeQuanTam") as Label;
                Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                ImgSubmit.Visible = false;

                HiddenField hiddenTenMangNghiepVu = (HiddenField)e.Item.FindControl("hiddenTenMangNghiepVu") as HiddenField;
                string TenMangNghiepVu = hiddenTenMangNghiepVu.Value;
                IsSubmitted = CheckSubmitted(TenMangNghiepVu, _nhomkt);
                if (IsSubmitted)
                    ImgSubmit.Visible = true;
                //get Van de quan tam by Mang Nghiep Vu
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                DataSet dsHoSoSoBo = lapKeHoach.ChiTietHoSoPhanTichSoBo(_nhomkt);
                if (isValidDataSet(dsHoSoSoBo))
                {
                    DataRow[] foundRows;
                    string expression = "mang_nghiep_vu ='" + TenMangNghiepVu + "'";
                    foundRows = dsHoSoSoBo.Tables[0].Select(expression);
                    lblVanDeQuanTam.Text += "<ul>";
                    foreach (DataRow row in foundRows)
                    {
                        lblVanDeQuanTam.Text += "<li>";
                        lblVanDeQuanTam.Text += row["van_de_quan_tam"].ToString();// +"<br/>";
                        lblVanDeQuanTam.Text += "</li>";

                    }
                    lblVanDeQuanTam.Text += "</ul>";
                }



            }
        }
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
                //bool IsSubmitted = false;
                Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                ImgSubmit.Visible = false;

                Label lblStatus = (Label)e.Item.FindControl("lblStatus") as Label;
                Label hoso = (Label)e.Item.FindControl("hoso") as Label;
                Label lblCommandXacSuat = (Label)e.Item.FindControl("lblCommandXacSuat") as Label;
                Label lblCommandAnhHuong = (Label)e.Item.FindControl("lblCommandAnhHuong") as Label;
                Label lblCommandRRCoHuu = (Label)e.Item.FindControl("lblCommandRRCoHuu") as Label;

                if (hoso != null)
                {
                    if (lblStatus.Text.Equals("4"))
                    {
                        ImgSubmit.Visible = true;
                    }
                    //else
                    //{
                    //    lblCommandRRCoHuu.Attributes.Add("onclick", "{LoadDocument('" + hoso.Text + "','ah')}");
                    //}
                }
                //fix issue 15 of Sprint 2
                lblCommandRRCoHuu.Attributes.Add("onclick", "{LoadDocument('" + hoso.Text + "','ah')}");
                //end
                //TreeView treeViewKiemSoat = (TreeView)e.Item.FindControl("treeViewKiemSoat") as TreeView;
                TreeView treeViewThuTuc = (TreeView)e.Item.FindControl("treeViewThuTuc") as TreeView;
                string ruiro = e.Item.Cells[3].Text == null ? String.Empty : e.Item.Cells[3].Text;
                //if (treeViewKiemSoat != null)
                //    BuidTreeView(treeViewKiemSoat, ruiro, 0);
                if (treeViewThuTuc != null)
                    BuidTreeView(treeViewThuTuc, ruiro, 1);
                if (lblCommandRRCoHuu != null)
                    BuildRRCoHuu(lblCommandXacSuat.Text, lblCommandAnhHuong.Text, lblCommandRRCoHuu);
            }
        }

        /// <summary>
        /// tinh toan rui ro co huu
        /// </summary>
        /// <param name="xacsuat"></param>
        /// <param name="anhhuong"></param>
        /// <param name="lbl"></param>
        /// author:quangna
        void BuildRRCoHuu(string xacsuat, string anhhuong, Label lbl)
        {
            if (String.IsNullOrEmpty(xacsuat) || String.IsNullOrEmpty(anhhuong))
                return;
            if (!CommonFunc.IsNumber(xacsuat) || !CommonFunc.IsNumber(anhhuong))
                return;
            if (xacsuat.Equals("0") && anhhuong.Equals("0"))
                lbl.Text = "Đánh giá";
            else
                lbl.Text = (Int32.Parse(xacsuat) * Int32.Parse(anhhuong)).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="ruiro"></param>
        /// <param name="flag"></param>
        /// author:quangna
        void BuidTreeView(TreeView treeView, string ruiro, int flag)
        {
            if (String.IsNullOrEmpty(ruiro))
                return;
            //TreeNode nodeRuiRo = new TreeNode(ruiro);
            //treeView.Nodes.Add(nodeRuiRo);
            string DocFields = "PK_DocumentID,[Tên kiểm soát],[Tên rủi ro],[Diễn giải],Status";
            string PropertyFields = "Tên kiểm soát,Tên rủi ro,Diễn giải";
            string Condition = " And [Tên rủi ro]=N'" + ruiro + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(_doctypeid_kiemsoat, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds) == false)
                return;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                TreeNode nodeKiemSoat = new TreeNode(row["Tên kiểm soát"].ToString());
                nodeKiemSoat.ToolTip = row["Tên kiểm soát"].ToString();
                nodeKiemSoat.SelectAction = TreeNodeSelectAction.None;
                treeView.Nodes.Add(nodeKiemSoat);

                if (flag == 1)
                {
                    string DocFields_TT = "PK_DocumentID,Status,[Tên kiểm soát],[Tên thủ tục kiểm toán],[Diễn giải]";
                    string PropertyFields_TT = "Tên kiểm soát,Tên thủ tục kiểm toán,Diễn giải";
                    string Condition_TT = " And [Tên kiểm soát]=N'" + row["Tên kiểm soát"].ToString() + "'";
                    DataSet dsThuTuc = doc.getDocumentList(_doctypeid_thutuc, DocFields_TT, PropertyFields_TT, Condition_TT);
                    if (isValidDataSet(dsThuTuc))
                        foreach (DataRow rowTT in dsThuTuc.Tables[0].Rows)
                        {
                            TreeNode nodeThuTuc = new TreeNode(rowTT["Tên thủ tục kiểm toán"].ToString());
                            nodeThuTuc.ToolTip = rowTT["Tên thủ tục kiểm toán"].ToString();
                            nodeThuTuc.SelectAction = TreeNodeSelectAction.None;
                            nodeKiemSoat.ChildNodes.Add(nodeThuTuc);
                        }
                }
            }
        }

        protected void BindGridVanDeQuanTam(DataSet dsMangNVByThanhVien)
        {
            if (isValidDataSet(dsMangNVByThanhVien))
            {
                DataView dv = dsMangNVByThanhVien.Tables[0].DefaultView;
                dv.Sort = "ten_mang_nghiep_vu ASC";

                GridVanDeQuanTam.DataSource = dv.ToTable();
                GridVanDeQuanTam.DataBind();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mangNghieVuID"></param>
        /// <author>thangma</author>
        protected void InsertHoSoRuiRo(string mangNghieVuID)
        {
            string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(mangNghieVuID, "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
            string sMucTieuKiemSoat_doctype = DOCTYPE.MUCTIEU_KIEMSOAT;
            //get cac muc tieu kiem soat by mang nghiep vu
            string DocFields = "PK_DocumentID,[Tên Mục tiêu kiểm soát]";
            string PropertyFields = "Tên Mục tiêu kiểm soát";
            string Condition = " and PK_DocumentID in ( select FK_DOCUMENTID from T_TYPE_DOC_PROPERTY where FK_PROPERTYID =  'DC51C8A3-8E80-4C08-AFFA-B859545B4DCB' and TEXTVALUE =N'" + sTenMangNghiepVu + "' )";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsMucTieuKiemSoatByMangNV = obj.getDocumentList(sMucTieuKiemSoat_doctype, DocFields, PropertyFields, Condition);
            DataTable dtMucTieuKiemSoatByMangNV = dsMucTieuKiemSoatByMangNV.Tables[0];
            //loop in list of muctieu kiem soat
            foreach (DataRow rowMucTieu in dtMucTieuKiemSoatByMangNV.Rows)
            {
                string sTenMucTieuKiemSoat = rowMucTieu["Tên Mục tiêu kiểm soát"].ToString();
                string sRuiRo = DOCTYPE.RUIRO_KIEMSOAT;
                //get cac muc tieu kiem soat by mang nghiep vu
                string DocFieldsRuiRo = "PK_DocumentID,[Tên rủi ro kiểm soát]";
                string PropertyFieldsRuiRo = "Tên rủi ro kiểm soát";
                string ConditionRuiRo = " and PK_DocumentID in ( select FK_DOCUMENTID from T_TYPE_DOC_PROPERTY where FK_PROPERTYID =  '236D789E-8EE2-4ACC-B960-1C47144ACF90' and TEXTVALUE =N'" + sTenMucTieuKiemSoat + "' )";
                DataSet dsRuiRoByMucTieuKS = obj.getDocumentList(sRuiRo, DocFieldsRuiRo, PropertyFieldsRuiRo, ConditionRuiRo);
                DataTable dtRuiRoByMucTieuKS = dsRuiRoByMucTieuKS.Tables[0];
                //loop in list of ruiro
                foreach (DataRow rowRuiRo in dtRuiRoByMucTieuKS.Rows)
                {
                    string sTenRuiRo = rowRuiRo["Tên rủi ro kiểm soát"].ToString();
                    //creat doc HoSoRuiRo
                    bus_Document docBiz = bus_Document.Instance(_objUserContext);
                    string HoSoRuiRoID = System.Guid.NewGuid().ToString();
                    //docBiz.upda
                    docBiz.createDocument(HoSoRuiRoID, "", "", "63090F39-2203-499E-A780-AF2EA6025BC3"
                        , "00000000-0000-0000-0000-000000000000", "", _objUserContext.UserName
                        , "SysAdmin", DateTime.Now.Year, DateTime.Now.Month, 2);
                    CommonFunc.AddDocLink(HoSoRuiRoID, _nhomkt, TYPE_OF_LINK.DOCUMENT, _objUserContext);
                    bus_Type_Doc_Property objtdp = bus_Type_Doc_Property.Instance(_objUserContext);
                    DataSet dsProperties = objtdp.getEmpty("FK_PROPERTYID,PROPERTYTYPE,VALUE=''");
                    //mang nghiep vu info
                    DataRow rowMangNVProperties = dsProperties.Tables[0].NewRow();
                    rowMangNVProperties["FK_PROPERTYID"] = "6C1F699A-C48C-4B7D-8EC7-100C6BFC733A";
                    rowMangNVProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("6C1F699A-C48C-4B7D-8EC7-100C6BFC733A");
                    rowMangNVProperties["VALUE"] = sTenMangNghiepVu;
                    dsProperties.Tables[0].Rows.Add(rowMangNVProperties);
                    //muc tieu kiem soat info
                    DataRow rowMucTieuKiemSoatProperties = dsProperties.Tables[0].NewRow();
                    rowMucTieuKiemSoatProperties["FK_PROPERTYID"] = "1335A457-680D-4503-AD26-D5E8047E4FE8";
                    rowMucTieuKiemSoatProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("1335A457-680D-4503-AD26-D5E8047E4FE8");
                    rowMucTieuKiemSoatProperties["VALUE"] = sTenMucTieuKiemSoat;
                    dsProperties.Tables[0].Rows.Add(rowMucTieuKiemSoatProperties);
                    //muc tieu kiem soat info
                    DataRow rowRuiRoProperties = dsProperties.Tables[0].NewRow();
                    rowRuiRoProperties["FK_PROPERTYID"] = "0995AAA6-A4E8-4648-A548-94DAE8507D2A";
                    rowRuiRoProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("0995AAA6-A4E8-4648-A548-94DAE8507D2A");
                    rowRuiRoProperties["VALUE"] = sTenRuiRo;
                    dsProperties.Tables[0].Rows.Add(rowRuiRoProperties);

                    DataRow rowXacSuatProperties = dsProperties.Tables[0].NewRow();
                    rowXacSuatProperties["FK_PROPERTYID"] = "26322953-20BB-4D2E-A651-289960B78202";
                    rowXacSuatProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("26322953-20BB-4D2E-A651-289960B78202");
                    rowXacSuatProperties["VALUE"] = String.Empty;
                    dsProperties.Tables[0].Rows.Add(rowXacSuatProperties);

                    DataRow rowAnhHuongProperties = dsProperties.Tables[0].NewRow();
                    rowAnhHuongProperties["FK_PROPERTYID"] = "72812D71-8A97-4BCA-B495-E0664DC1B6CF";
                    rowAnhHuongProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("72812D71-8A97-4BCA-B495-E0664DC1B6CF");
                    rowAnhHuongProperties["VALUE"] = String.Empty;
                    dsProperties.Tables[0].Rows.Add(rowAnhHuongProperties);

                    DataRow rowRuiRoConLaiProperties = dsProperties.Tables[0].NewRow();
                    rowRuiRoConLaiProperties["FK_PROPERTYID"] = "58307D3D-0507-4D91-8F21-B3AB9BDFC3E4";
                    rowRuiRoConLaiProperties["PROPERTYTYPE"] = CommonFunc.GetDataType("58307D3D-0507-4D91-8F21-B3AB9BDFC3E4");
                    rowRuiRoConLaiProperties["VALUE"] = String.Empty;
                    dsProperties.Tables[0].Rows.Add(rowRuiRoConLaiProperties);

                    DataRow rowDiemXS = dsProperties.Tables[0].NewRow();
                    rowDiemXS["FK_PROPERTYID"] = "B45EC8CF-CA21-4FD7-9820-1C4A715E1831";
                    rowDiemXS["PROPERTYTYPE"] = CommonFunc.GetDataType("B45EC8CF-CA21-4FD7-9820-1C4A715E1831");
                    rowDiemXS["VALUE"] = String.Empty;
                    dsProperties.Tables[0].Rows.Add(rowDiemXS);

                    DataRow rowDiemAH = dsProperties.Tables[0].NewRow();
                    rowDiemAH["FK_PROPERTYID"] = "DACA775F-FA70-40B0-97B5-2ED385A56474";
                    rowDiemAH["PROPERTYTYPE"] = CommonFunc.GetDataType("DACA775F-FA70-40B0-97B5-2ED385A56474");
                    rowDiemAH["VALUE"] = String.Empty;
                    dsProperties.Tables[0].Rows.Add(rowDiemAH);

                    //save doc HoSoRuiRo
                    docBiz.createDocumentProperties(HoSoRuiRoID, dsProperties);
                }
            }
        }
    }
}