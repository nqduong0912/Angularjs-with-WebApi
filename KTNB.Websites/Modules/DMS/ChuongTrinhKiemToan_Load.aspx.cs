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
using CORE.CoreObjectContext;
using System.Text;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class ChuongTrinhKiemToan_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CONGVIEC_TRONG_DOTKIEMTOAN;
        protected string _doctypeid_thutuckiemtoan = DOCTYPE.THUTUC_KIEMTOAN;
        protected string _doctypeid_mangnghiepvu = DOCTYPE.MANG_NGHIEPVU;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _doankt = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _fk_doclinkID = string.Empty;
        protected string _thutuc = string.Empty;
        protected string _thanhvien = string.Empty;
        int _status_dotkt = 0;
        protected bool _isSubmitted = false;

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
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];

            _doankt = Request["doankt"];
            _dotkt = Request["dotkt"];
            _fk_doclinkID = Request["fk_doclinkID"];
            _thutuc = Request["thutuc"];
            _thanhvien = Request["thanhvien"];

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
                if (_action == "xoathutuc")
                {
                    FeedBackClient(XoaThuTuc());
                }
                if (_action == "themthutuc")
                {
                    FeedBackClient(ThemThuTuc());
                }
                if (_action == "themthutuctreeview")
                {
                    FeedBackClient(ThemThuTucTreeView());
                }
                if (_action == "submit")
                {
                    FeedBackClient(Submit(_documentid));
                }
                if (_action == "checkPTRR")
                {
                    FeedBackClient(checkPTRR(_dotkt));
                }
                ////len trang thai Lập chương trình kiểm toán
                //if (_action == "chuyentrangthaidotkt")
                //{
                //    //chuyển trạng thái đợt lên: Lập chương trình kiểm toán
                //    //FeedBackClient(CommonFunc.ChuyenTrangThaiDotKT(_dotkt, 15));
                //    FeedBackClient(TrangThaiDotKiemToan.SetStatus(_dotkt, 15));
                //}
                if (_action == "loadthutuc")
                {
                    FeedBackClient(GetThuTuc_Theo_ThanhVien(_doankt, _thanhvien));
                }
            }
            #endregion

            #region init form
            string caption = "Cập nhật chương trình kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin chương trình kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler
            btnDongy.Attributes.Add("onclick", "{ThemCongViec('" + _doctypeid + "'); return false;}");
            btnThem.Attributes.Add("onclick", "{ThemThuTuc(); return false;}");
            //btnThemThuTuc.Attributes.Add("onclick", "{ThemThuTucTreeView(); return false;}");
            CommonFunc.SetButtonBack(_btnCloseWindow, "ChuongTrinhKiemToan.aspx?doankt=" + _doankt + "&dotkt=" + _dotkt);

            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            //_btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            //thangma
            _btnSave.Visible = true;
            _btnSave.Text = "Submit";
            _btnSave.Attributes.Add("onclick", "{Submit(); return false;}");
            #endregion
        }
        public string Submit(string DocID)
        {
            DataTable dtThuTucByCongViec = ListThuTucByCongViec();
            if (dtThuTucByCongViec.Rows.Count > 0)
            {
                CommonFunc.UpdateDocStatus(DocID, 2);//sua lai tu 4->2
                //thangma: update Trang thai Dot kiem toan 
                //len trang thai Cap nhat phat hien
                //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                //if (_status_dotkt < 21)
                //    CommonFunc.UpdateDocStatus(_dotkt, 21);
                //end
            }

            else
                return "0";
            return "1";
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
                //BuildNguoiThucHien();
                if (!string.IsNullOrEmpty(_doankt))
                    BuildNguoiThucHien_Theo_MNV(_doankt);
                BuildNguoiDuyet();
                BuildTrangThai();
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion

        #region helper
        void BuildTrangThai()
        {
            //BindDropTrangThai(this.ID8_A0979435_7846_4421_97FE_4A54CF890381);
            CommonFunc.LoadDropDownList(this.DOCSTATUS, 4);
        }
        public static void BindDropTrangThai(DropDownList obj)
        {
            List<string> TrangThaiList = CommonFunc.GetDanhSachTrangThai();
            obj.Items.Add(new ListItem(TrangThaiList[0], TrangThaiList[0]));
        }
        void BuildNguoiDuyet()
        {
            bus_User_In_Group group = new bus_User_In_Group(_objUserContext);
            DataSet ds = group.getList(" And PK_ROLEID='" + ROLES.CANBO_DUYET + "'", String.Empty);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    this.ID8_C983F892_1A81_4F52_98C3_DAAD729A99E8.Items.Add(new ListItem(row["UserName"].ToString(), row["UserName"].ToString()));
                    //this.ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C.Items.Add(new ListItem(row["UserName"].ToString(), row["UserName"].ToString()));
                }
                this.ID8_B9D5B3D6_6118_4821_94F3_AB03B060FC8C.Items.Add(new ListItem("--None--", "-1"));
            }
        }


        void BuildNguoiThucHien()
        {

            if (String.IsNullOrEmpty(_doankt))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(_doankt);

            //laythem thong tin cua truongdoan kt;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID='" + _doankt + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsDoanKT = doc.getDocumentList(DOCTYPE.DOAN_KIEMTOAN, DocFields, PropertyFields, Condition);
            bus_User user = new bus_User(_objUserContext);
            string truongdoan = String.Empty;

            if (isValidDataSet(dsDoanKT))
            {
                truongdoan = dsDoanKT.Tables[0].Rows[0]["Name"].ToString();
                this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Add(new ListItem(truongdoan, truongdoan));
            }

            if (isValidDataSet(ds))
                foreach (DataRow row in ds.Tables[0].Rows)
                    if (row["UserName"].ToString() != truongdoan)
                        this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Add(new ListItem(row["UserName"].ToString(), row["UserName"].ToString()));
        }

        void BuildThuTucKiemToan()
        {
            ddlThuTuc.Items.Clear();
            //thu tuc kiem toan tu mang nghiep vu
            if (String.IsNullOrEmpty(_doankt))
                return;
            //lay tat ca cac mang nghiep vu thuoc doan kiem toan
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + _doankt + "')";
            bus_Document obj_mangnghiepvu = new bus_Document(_objUserContext);
            DataSet ds_mangnghiepvu = obj_mangnghiepvu.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds_mangnghiepvu))
            {
                string sImg = "---"; //"<img src=\"../../Images/check.gif\" />";
                //string sImg = "---"; //"<img src=\"../../Images/check.gif\" />";
                bus_LapKeHoach obj = new bus_LapKeHoach(_objUserContext);
                foreach (DataRow row in ds_mangnghiepvu.Tables[0].Rows)
                {
                    //get mangnghiepvu
                    ddlThuTuc.Items.Add(new ListItem(row["Tên mảng nghiệp vụ"].ToString(), "-1"));
                    //get thutuc
                    DataSet dsThuTuc = obj.GetThuTucByMangNghiepVu(row["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsThuTuc))
                        foreach (DataRow rowTT in dsThuTuc.Tables[0].Rows)
                            ddlThuTuc.Items.Add(new ListItem(Prefix(5) + rowTT["Tên thủ tục kiểm toán"].ToString(), rowTT["PK_DocumentID"].ToString()));
                }
            }
        }

        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            //BuildThuTucKiemToan();
            BuildThuTucKiemToan_New();
            GetListThuTucByCongViec();
        }

        void GetListThuTucByCongViec()
        {
            string documentID = idCongViec.Value;
            if (!String.IsNullOrEmpty(documentID))
            {
                ObjectDataSource1.SelectParameters["DocumentID"].DefaultValue = documentID;
                dataCtrl.DataBind();
            }
        }
        public DataTable ListThuTucByCongViec()
        {
            string documentID = _documentid;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(documentID))
            {
                string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
                string PropertyFields = "Tên thủ tục kiểm toán";
                string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + documentID + "')";
                bus_Document obj = new bus_Document(_objUserContext);
                ds = obj.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
                if (isValidDataSet(ds))
                {
                    dt = ds.Tables[0];
                    dt.Columns.Add("FK_DocLinkID", typeof(string));
                    foreach (DataRow row in dt.Rows)
                        row["FK_DocLinkID"] = documentID;
                }
            }
            return dt;
        }

        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {

            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label FK_DOCLINKID = (Label)e.Item.FindControl("FK_DOCLINKID") as Label;
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                if (PK_DocumentID != null && FK_DOCLINKID != null && imgDelete != null)
                {
                    string thutuc = e.Item.Cells[3].Text;
                    imgDelete.Attributes.Add("onclick", "{xoathutuc('" + e.Item.ClientID + "','" + PK_DocumentID.Text + "','" + FK_DOCLINKID.Text + "','" + thutuc + "')}");
                    if (CommonFunc.IsSubmitted_CV(idCongViec.Value))
                        imgDelete.Visible = false;
                }
            }
        }


        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string XoaThuTuc()
        {
            CommonFunc.RemoveDocLink(_documentid, _fk_doclinkID, _objUserContext);
            string errormessage = "2";
            return errormessage;
        }

        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string ThemThuTuc()
        {
            string FullName = "2";
            string DocFields = "PK_DocumentID";
            string PropertyFields = "PK_DocumentID";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _documentid + "')";
            bus_Document obj_thutuctheocongviec = bus_Document.Instance(_objUserContext);
            DataSet ds = obj_thutuctheocongviec.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                DataTable dt = ds.Tables[0];
                DataRow[] rows = dt.Select(" PK_DocumentID='" + _thutuc + "'");
                if (rows.Count() > 0)
                {
                    FullName = "1";
                    return FullName;
                }
            }
            //thangma: them thong tin mang nghiep vu va dot kiem toan vao bang T_DOCLINK
            //ADDTIONAL_DATA1: chua thong tin MangNghiepVuID
            //ADDTIONAL_DATA2: chua thong tin DotKiemToanID
            bus_Doankiemtoan obj = bus_Doankiemtoan.Instance(_objUserContext);
            string sTenThuTuc = CommonFunc.getPropertyValueOnDocument(_thutuc, "98F450F3-824E-4BBD-9F9D-6C9845FD8186");
            DataSet dsMangNghiepVuInfo = obj.GetMangNghiepVuByThuTuc(sTenThuTuc);
            string sMangNghiepVuID = string.Empty;
            if (isValidDataSet(dsMangNghiepVuInfo))
            {
                sMangNghiepVuID = dsMangNghiepVuInfo.Tables[0].Rows[0]["PK_DocumentID"].ToString();
            }
            CommonFunc.AddDocLink(_thutuc, _documentid, TYPE_OF_LINK.DOCUMENT, _objUserContext, sMangNghiepVuID, _dotkt);
            //thangma end
            return FullName;
        }

        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string ThemThuTucTreeView()
        {
            string FullName = "2";
            foreach (string str in _thutuc.Split('$'))
            {
                if (!String.IsNullOrEmpty(str))
                {
                    string DocFields = "PK_DocumentID";
                    string PropertyFields = "PK_DocumentID";
                    string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _documentid + "')";
                    bus_Document obj_thutuctheocongviec = bus_Document.Instance(_objUserContext);
                    DataSet ds = obj_thutuctheocongviec.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
                    if (isValidDataSet(ds))
                    {
                        DataTable dt = ds.Tables[0];
                        DataRow[] rows = dt.Select(" PK_DocumentID='" + str + "'");
                        if (rows.Count() <= 0)
                        {
                            CommonFunc.AddDocLink(str, _documentid, TYPE_OF_LINK.DOCUMENT, _objUserContext);
                        }
                    }

                }
            }
            return FullName;
        }


        void BuildTreeViewThuTuc(TreeView treeView)
        {
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + _doankt + "')";
            bus_Document obj_mangnghiepvu = new bus_Document(_objUserContext);
            DataSet ds_mangnghiepvu = obj_mangnghiepvu.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds_mangnghiepvu))
            {
                foreach (DataRow rowMNV in ds_mangnghiepvu.Tables[0].Rows)
                {
                    TreeNode nodeMNV = new TreeNode();
                    nodeMNV.Text = rowMNV["Tên mảng nghiệp vụ"].ToString();
                    nodeMNV.Value = "-1";
                    nodeMNV.SelectAction = TreeNodeSelectAction.None;
                    treeView.Nodes.Add(nodeMNV);

                    string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(rowMNV["PK_DocumentID"].ToString(), "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
                    string sMucTieuKiemSoat_doctype = DOCTYPE.MUCTIEU_KIEMSOAT;

                    //get cac muc tieu kiem soat by mang nghiep vu
                    string DocFieldsKS = "PK_DocumentID,[Mảng nghiệp vụ],[Tên Mục tiêu kiểm soát]";
                    string PropertyFieldsKS = "Mảng nghiệp vụ,Tên Mục tiêu kiểm soát";
                    string ConditionKS = " and [Mảng nghiệp vụ]=N'" + sTenMangNghiepVu + "'";
                    bus_Document obj = bus_Document.Instance(_objUserContext);
                    DataSet dsMucTieuKiemSoatByMangNV = obj.getDocumentList(sMucTieuKiemSoat_doctype, DocFieldsKS, PropertyFieldsKS, ConditionKS);
                    if (isValidDataSet(dsMucTieuKiemSoatByMangNV) == false)
                        continue;
                    //loop in list of muctieu kiem soat
                    foreach (DataRow rowMucTieu in dsMucTieuKiemSoatByMangNV.Tables[0].Rows)
                    {
                        string sTenMucTieuKiemSoat = rowMucTieu["Tên Mục tiêu kiểm soát"].ToString();
                        TreeNode nodeMTKS = new TreeNode();
                        nodeMTKS.Text = sTenMucTieuKiemSoat;
                        nodeMTKS.Value = "-1";
                        nodeMTKS.SelectAction = TreeNodeSelectAction.None;
                        nodeMNV.ChildNodes.Add(nodeMTKS);
                        string sRuiRoType = DOCTYPE.RUIRO_KIEMSOAT;
                        //get cac rui ro by muc tieu kiem soat
                        string DocFieldsRuiRo = "PK_DocumentID,[Mục tiêu kiểm soát],[Tên rủi ro kiểm soát]";
                        string PropertyFieldsRuiRo = "Mục tiêu kiểm soát,Tên rủi ro kiểm soát";
                        string ConditionRuiRo = " and [Mục tiêu kiểm soát]=N'" + sTenMucTieuKiemSoat + "'";
                        DataSet dsRuiRoByMucTieuKS = obj.getDocumentList(sRuiRoType, DocFieldsRuiRo, PropertyFieldsRuiRo, ConditionRuiRo);
                        if (isValidDataSet(dsRuiRoByMucTieuKS) == false)
                            continue;
                        //loop in list of ruiro
                        foreach (DataRow rowRuiRo in dsRuiRoByMucTieuKS.Tables[0].Rows)
                        {
                            string sTenRuiRo = rowRuiRo["Tên rủi ro kiểm soát"].ToString();
                            TreeNode nodeRR = new TreeNode();
                            nodeRR.Text = sTenRuiRo;
                            nodeRR.Value = "-1";
                            nodeRR.SelectAction = TreeNodeSelectAction.None;
                            nodeMTKS.ChildNodes.Add(nodeRR);

                            string sKiemSoatType = DOCTYPE.KIEMSOAT;
                            //get cac kiem soat by rui ro
                            string DocFieldsKiemSoat = "PK_DocumentID,[Tên rủi ro],[Tên kiểm soát]";
                            string PropertyFieldsKiemSoat = "Tên rủi ro,Tên kiểm soát";
                            string ConditionKiemSoat = " and [Tên rủi ro]=N'" + sTenRuiRo + "'";
                            DataSet dsKiemSoatByRuiRo = obj.getDocumentList(sKiemSoatType, DocFieldsKiemSoat, PropertyFieldsKiemSoat, ConditionKiemSoat);
                            if (isValidDataSet(dsKiemSoatByRuiRo) == false)
                                continue;
                            //loop in list of kiem soat
                            foreach (DataRow rowKiemSoat in dsKiemSoatByRuiRo.Tables[0].Rows)
                            {
                                string sTenKiemSoat = rowKiemSoat["Tên kiểm soát"].ToString();
                                TreeNode nodeKS = new TreeNode();
                                nodeKS.Text = sTenKiemSoat;
                                nodeKS.Value = "-1";
                                nodeKS.SelectAction = TreeNodeSelectAction.None;
                                nodeRR.ChildNodes.Add(nodeKS);
                                string sThuTucType = DOCTYPE.THUTUC_KIEMTOAN;
                                //get cac kiem soat by rui ro
                                string DocFieldsThuTuc = "PK_DocumentID,[Tên kiểm soát],[Tên thủ tục kiểm toán]";
                                string PropertyFieldsThuTuc = "Tên kiểm soát,Tên thủ tục kiểm toán";
                                string ConditionThuTuc = " and [Tên kiểm soát]=N'" + sTenKiemSoat + "'";
                                DataSet dsThuTucByKiemSoat = obj.getDocumentList(sThuTucType, DocFieldsThuTuc, PropertyFieldsThuTuc, ConditionThuTuc);
                                if (isValidDataSet(dsThuTucByKiemSoat) == false)
                                    continue;
                                foreach (DataRow rowTT in dsThuTucByKiemSoat.Tables[0].Rows)
                                {
                                    TreeNode nodeTT = new TreeNode();
                                    //nodeTT.Text = rowTT["Tên thủ tục kiểm toán"].ToString();
                                    //nodeTT.Value = rowTT["PK_DocumentID"].ToString();
                                    //nodeTT.ShowCheckBox = true;
                                    //nodeTT.Checked = false;
                                    nodeTT.Text = "<input id='" + rowTT["PK_DocumentID"].ToString() + "' type='checkbox' value='" + rowTT["PK_DocumentID"].ToString() + "'>" + rowTT["Tên thủ tục kiểm toán"].ToString() + "</input>";
                                    nodeTT.SelectAction = TreeNodeSelectAction.None;
                                    nodeKS.ChildNodes.Add(nodeTT);
                                }
                            }
                        }
                    }
                }

            }
        }

        #endregion end helper

        #region check PTRR da ton tai tren doankt?
        public string checkPTRR(string dotKT)
        {
            //get doankt
            string fullname = "1";
            string DocFields = "PK_DocumentID,[Name],STATUS";
            string PropertyFields = "Name,STATUS";
            //string Condition = string.Empty;
            string Condition = " and PK_DocumentID IN (Select FK_DocumentID From T_DOCLINK Where FK_DOCLINKID = '" + dotKT + "') and Status = 4";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsDoanKT = doc.getDocumentList(DOCTYPE.DOAN_KIEMTOAN, DocFields, PropertyFields, Condition);

            bus_Doankiemtoan doc_doankt = new bus_Doankiemtoan(_objUserContext);
            if (isValidDataSet(dsDoanKT))
            {
                foreach (DataRow rowDoanKT in dsDoanKT.Tables[0].Rows)
                {
                    DataSet dsNhomKT = doc_doankt.DanhSachNhomByDoanKT(rowDoanKT["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsNhomKT))
                    {
                        foreach (DataRow rowNhomKT in dsNhomKT.Tables[0].Rows)
                        {
                            string nhomkt = rowNhomKT["NhomID"].ToString();
                            DataSet dsMangNghiepVu = doc_doankt.GetMangNghiepVuByDoanKT(rowDoanKT["PK_DocumentID"].ToString());
                            if (isValidDataSet(dsMangNghiepVu))
                            {
                                foreach (DataRow row in dsMangNghiepVu.Tables[0].Rows)
                                {
                                    //chi can 1 PTRR la da okie
                                    bool statusPTRR = iSCheckPTRR(nhomkt, row["PK_DocumentID"].ToString());
                                    if (statusPTRR)
                                    {
                                        fullname = "1";
                                        return fullname;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            fullname = "0";//error
            return fullname;
        }

        private bool iSCheckPTRR(string nhomkt, string mangnv)
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

        #endregion


        #region sualai
        private string Prefix(int count)
        {
            if (count == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
                sb.Append("&nbsp;");
            return Server.HtmlDecode(sb.ToString());
        }


        void BuildThuTucKiemToan_New()
        {
            ddlThuTuc.Items.Clear();
            DataTable dtAll = GetDataTableThuTucByMangNghiepVu_New();
            DataTable dt = GetDataTableThuTucByCongViec();
            if (dt == null)
            {
                if (dtAll != null)
                    foreach (DataRow rowTT in dtAll.Rows)
                        ddlThuTuc.Items.Add(new ListItem(rowTT["Tên thủ tục kiểm toán"].ToString(), rowTT["PK_DocumentID"].ToString()));
            }
            else
            {
                if (dtAll != null)
                {
                    foreach (DataRow rowAll in dtAll.Rows)
                    {
                        bool isE = isExist(rowAll, dt);
                        if (!isE)
                            ddlThuTuc.Items.Add(new ListItem(rowAll["Tên thủ tục kiểm toán"].ToString(), rowAll["PK_DocumentID"].ToString()));
                    }
                }

            }

        }

        bool isExist(DataRow row, DataTable dt)
        {
            foreach (DataRow row1 in dt.Rows)
                if (row1["PK_DocumentID"].ToString() == row["PK_DocumentID"].ToString())
                    return true;
            return false;
        }


        DataTable GetDataTableThuTucByMangNghiepVu()
        {
            //lay tat ca cac mang nghiep vu thuoc doan kiem toan
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + _doankt + "')";
            bus_Document obj_mangnghiepvu = new bus_Document(_objUserContext);
            DataSet ds_mangnghiepvu = obj_mangnghiepvu.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);

            string thanhvien = this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.SelectedValue;
            if (isValidDataSet(ds_mangnghiepvu))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_DocumentID", typeof(string));
                dt.Columns.Add("Tên thủ tục kiểm toán", typeof(string));

                string sImg = "---"; //"<img src=\"../../Images/check.gif\" />";
                bus_LapKeHoach obj = new bus_LapKeHoach(_objUserContext);
                foreach (DataRow row in ds_mangnghiepvu.Tables[0].Rows)
                {
                    //them mangnghiepvu
                    DataRow rowNew_mnv = dt.NewRow();
                    rowNew_mnv["PK_DocumentID"] = "-1";
                    rowNew_mnv["Tên thủ tục kiểm toán"] = row["Tên mảng nghiệp vụ"].ToString();
                    dt.Rows.Add(rowNew_mnv);

                    DataSet dsThuTuc = obj.GetThuTucByMangNghiepVu(row["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsThuTuc))
                        foreach (DataRow rowTT in dsThuTuc.Tables[0].Rows)
                        {
                            DataRow rowNew = dt.NewRow();
                            rowNew["PK_DocumentID"] = rowTT["PK_DocumentID"].ToString();
                            rowNew["Tên thủ tục kiểm toán"] = Prefix(5) + rowTT["Tên thủ tục kiểm toán"].ToString();
                            dt.Rows.Add(rowNew);
                        }
                }
                return dt;
            }
            return null;
        }

        DataTable GetDataTableThuTucByCongViec()
        {
            _documentid = idCongViec.Value;
            if (!String.IsNullOrEmpty(_documentid))
            {
                string DocFields = "PK_DocumentID,[Tên thủ tục kiểm toán]";
                string PropertyFields = "Tên thủ tục kiểm toán";
                string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _documentid + "')";
                bus_Document obj = new bus_Document(_objUserContext);
                DataSet ds = obj.getDocumentList(_doctypeid_thutuckiemtoan, DocFields, PropertyFields, Condition);
                if (isValidDataSet(ds))
                {
                    DataTable dt = ds.Tables[0];
                    dt.Columns.Add("FK_DocLinkID", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        row["FK_DocLinkID"] = _documentid;
                    }
                    return dt;
                }
                return null;
            }
            return null;
        }

        #endregion

        #region lay mangnghiepvu-thutuc theo doankt

        void BuildNguoiThucHien_Theo_MNV(string doankt)
        {
            this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Clear();
            DataTable dtMangNV = DataSource.getListMangNghiepVu(doankt);
            
            if (dtMangNV.Rows.Count == 0)
                return;

            #region Dungnt01 REM lại và sửa như dưới để load hết các thành viên trong đoàn KT, ko phụ thuộc vào MNV
            //DataView view = new DataView(dtMangNV);
            //DataTable distinctValues = view.ToTable(true, "ThanhVien");
            //foreach (DataRow row in distinctValues.Rows)
            //    if (row["ThanhVien"] != null)
            //        if (!string.IsNullOrEmpty(row["ThanhVien"].ToString()))
            //            this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Add(new ListItem(row["ThanhVien"].ToString(), row["ThanhVien"].ToString()));
            
            DataTable thanhvien = DataSource.getListThanhVien(doankt);
            foreach (DataRow row in thanhvien.Rows)
                if (row["Name"] != null)
                    if (!string.IsNullOrEmpty(row["Name"].ToString()))
                        this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Add(new ListItem(row["Name"].ToString(), row["Name"].ToString()));
            #endregion

            if (this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Count > 0)
                BuildThuTuc_Theo_ThanhVien(this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.SelectedValue.ToString(), dtMangNV);

        }

        public void BuildThuTuc_Theo_ThanhVien(string thanhvien, DataTable dtMangNV)
        {
            if (dtMangNV.Rows.Count == 0)
                return;
            DataRow[] rows = dtMangNV.Select("ThanhVien='" + thanhvien + "'");
            bus_LapKeHoach doc = new bus_LapKeHoach(_objUserContext);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    ddlThuTuc.Items.Add(new ListItem(row["Tên mảng nghiệp vụ"].ToString(), "-1"));
                    //re = row["Tên mảng nghiệp vụ"].ToString() + "|" + "-1"+"#";
                    DataSet dsTT = doc.GetThuTucByMangNghiepVu(row["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsTT))
                    {
                        foreach (DataRow rowTT in dsTT.Tables[0].Rows)
                            ddlThuTuc.Items.Add(new ListItem(Prefix(5) + rowTT["Tên thủ tục kiểm toán"].ToString(), rowTT["PK_DocumentID"].ToString()));
                    }
                }
            }
        }


        public string GetThuTuc_Theo_ThanhVien(string doankt, string thanhvien)
        {
            string message = String.Empty;
            DataTable dtMangNV = DataSource.getListMangNghiepVu(doankt);
            if (dtMangNV.Rows.Count == 0)
                return message;
            DataRow[] rows = dtMangNV.Select("ThanhVien='" + thanhvien + "'");
            bus_LapKeHoach doc = new bus_LapKeHoach(_objUserContext);
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    ddlThuTuc.Items.Add(new ListItem(row["Tên mảng nghiệp vụ"].ToString(), "-1"));
                    message += row["Tên mảng nghiệp vụ"].ToString() + "||" + "-1" + "#";
                    DataSet dsTT = doc.GetThuTucByMangNghiepVu(row["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsTT))
                        foreach (DataRow rowTT in dsTT.Tables[0].Rows)
                            message += rowTT["Tên thủ tục kiểm toán"].ToString() + "||" + rowTT["PK_DocumentID"].ToString() + "#";
                }
            }
            return message;
        }

        DataTable GetDataTableThuTucByMangNghiepVu_New()
        {
            DataTable dtMangNV = DataSource.getListMangNghiepVu(_doankt);
            string thanhvien = string.IsNullOrEmpty(idThanhVien.Value) ?
                (this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.Items.Count > 0 ? this.ID8_7DCBBD86_FD24_4415_8EDB_531559D663F2.SelectedValue : String.Empty)
                : idThanhVien.Value;
            if (dtMangNV.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PK_DocumentID", typeof(string));
                dt.Columns.Add("Tên thủ tục kiểm toán", typeof(string));
                bus_LapKeHoach obj = new bus_LapKeHoach(_objUserContext);
                foreach (DataRow row in dtMangNV.Rows)
                {
                    //them mangnghiepvu
                    if (row["ThanhVien"].ToString() != thanhvien)
                        continue;
                    DataRow rowNew_mnv = dt.NewRow();
                    rowNew_mnv["PK_DocumentID"] = "-1";
                    rowNew_mnv["Tên thủ tục kiểm toán"] = row["Tên mảng nghiệp vụ"].ToString();
                    dt.Rows.Add(rowNew_mnv);

                    DataSet dsThuTuc = obj.GetThuTucByMangNghiepVu(row["PK_DocumentID"].ToString());
                    if (isValidDataSet(dsThuTuc))
                        foreach (DataRow rowTT in dsThuTuc.Tables[0].Rows)
                        {
                            DataRow rowNew = dt.NewRow();
                            rowNew["PK_DocumentID"] = rowTT["PK_DocumentID"].ToString();
                            rowNew["Tên thủ tục kiểm toán"] = Prefix(5) + rowTT["Tên thủ tục kiểm toán"].ToString();
                            dt.Rows.Add(rowNew);
                        }
                }
                return dt;
            }
            return null;
        }

        #endregion

    }
}