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
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class NhomKiemToan : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid_nhomkiemtoan = DOCTYPE.NHOM_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;
        protected string _doctypeid_mangnghiepvu = DOCTYPE.MANG_NGHIEPVU;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;

        protected string _dotkt = string.Empty;
        protected string _doankt = string.Empty;
        protected bool _isTruongDoan = true;

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
            _documentid = Request["doc"];
            _doankt = Request["doankt"];
            _dotkt = Request["dotkt"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin nhóm kiểm toán", string.Empty, _doctypeid_nhomkiemtoan, 0);
            _isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            if (_isTruongDoan == true)
                _btnAddNew.Visible = true;
            else 
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
            if (!Page.IsPostBack)
            {
                //GetListNhomKiemToan(_doctypeid_nhomkiemtoan, _doankt);
                //GetListMangNghiepVu();
            }
            
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID_doankiemtoan, string dotkt)
        {
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_doankiemtoan;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }

        //private void GetListDoanKiemToan(string DocumentTypeID_doankiemtoan, string dotkt)
        //{
        //    string DocFields = "PK_DocumentID,[Name]";
        //    string PropertyFields = "Name";
        //    string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";

        //    bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
        //    DataSet ds = obj.getDocumentList(DocumentTypeID_doankiemtoan, DocFields, PropertyFields, Condition);
        //    if (ds != null)
        //    {
        //        dataCtrl.DataSource = ds;
        //        dataCtrl.DataBind();
        //    }
        //}

        private void GetListNhomKiemToan(string DocumentTypeID_nhomkiemtoan, string doankiemtoan)
        {
            if (String.IsNullOrEmpty(doankiemtoan))
                return;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + doankiemtoan + "')";
            //bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = obj.getDocumentList(DocumentTypeID_nhomkiemtoan, DocFields, PropertyFields, Condition);
            //if (isValidDataSet(ds))
            //{
            //    data_Ctrl1.DataSource = ds;
            //    data_Ctrl1.DataBind();
            //}

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_nhomkiemtoan;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            data_Ctrl1.DataBind();
        }

        void GetListMangNghiepVu()
        {
            //if (String.IsNullOrEmpty(_doankt))
            //    return;
            ////lay tat ca cac mang nghiep vu thuoc doan kiem toan
            //string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            //string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            //string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + _doankt + "')";

            //bus_Document obj_mangnghiepvu = new bus_Document(_objUserContext);
            //DataSet ds_mangnghiepvu = obj_mangnghiepvu.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);

            ////hien thi thong tin: lay tat ca thong tin truongnhom/thanhvien ma ton tai
            //if (isValidDataSet(ds_mangnghiepvu) == false)
            //    return;

            //string DocFields_nhomkt = "PK_DocumentID,[Name]";
            //string PropertyFields_nhomkt = "Name";
            //string Condition_kt = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";
            //bus_Document obj_nhomkt = bus_Document.Instance(_objUserContext);
            //DataSet ds_nhomkt = obj_nhomkt.getDocumentList(_doctypeid_nhomkiemtoan, DocFields_nhomkt, PropertyFields_nhomkt, Condition_kt);

            ////Dinh nghia table mangnghiepvu
            //DataTable dt_mangnghiepvu = ds_mangnghiepvu.Tables[0];
            //dt_mangnghiepvu.Columns.Add("NhomKiemToan_ID", typeof(string));
            //dt_mangnghiepvu.Columns.Add("NhomKiemToan", typeof(string));
            //dt_mangnghiepvu.Columns.Add("ThanhVien",typeof(string));

            ////neu ko co nhom kiem toan nao
            //if (isValidDataSet(ds_nhomkt) == false)
            //{
            //    foreach (DataRow row_mangnghiepvu in dt_mangnghiepvu.Rows)
            //    {
            //        row_mangnghiepvu["NhomKiemToan_ID"] = String.Empty;
            //        row_mangnghiepvu["NhomKiemToan"] = String.Empty;
            //        row_mangnghiepvu["ThanhVien"] = String.Empty;
            //    }
            //}
            //else
            //{
            //    bus_Doankiemtoan obj_doankt = new bus_Doankiemtoan(_objUserContext);
            //    DataSet ds_mangnghiepvu_doclink = obj_doankt.GetAllMangNghiepVuDocLink();
            //    if (isValidDataSet(ds_mangnghiepvu_doclink) == false)
            //        return;
            //    foreach (DataRow row_mangnghiepvu in dt_mangnghiepvu.Rows)
            //    {
            //       DataRow row = GetMangNghiepVuAndNhomKT(ds_nhomkt, ds_mangnghiepvu_doclink, row_mangnghiepvu["PK_DocumentID"].ToString(), row_mangnghiepvu);
            //       row_mangnghiepvu["NhomKiemToan_ID"] = row["NhomKiemToan_ID"];
            //       row_mangnghiepvu["NhomKiemToan"] = row["NhomKiemToan"];
            //       row_mangnghiepvu["ThanhVien"] = row["ThanhVien"];
            //    }
            //}

            //if (dt_mangnghiepvu.Rows.Count > 0)
            //{
            //    dataCtrl.DataSource = dt_mangnghiepvu;
            //    dataCtrl.DataBind();
            //}

            ObjectDataSource2.SelectParameters["DoanKT"].DefaultValue = _doankt;
            dataCtrl.DataBind();
        }


        DataRow GetMangNghiepVuAndNhomKT(DataSet ds_nhomkt, DataSet ds_mangnghiepvu_doclink, string mangnghiepvu, DataRow row_mangnghiepvu)
        {
            foreach (DataRow row_nhomkt in ds_nhomkt.Tables[0].Rows)
            {
                DataRow isExist = isExistMangNghiepVuAndNhomKT(ds_mangnghiepvu_doclink, mangnghiepvu, row_nhomkt["PK_DocumentID"].ToString());
                if (isExist != null)
                {
                    row_mangnghiepvu["NhomKiemToan_ID"] = row_nhomkt["PK_DocumentID"];
                    row_mangnghiepvu["NhomKiemToan"] = isExist["TruongNhom"];
                    bus_User obj_user = new bus_User(_objUserContext);
                    DataSet dsUser = obj_user.getByID(isExist["FK_DocumentID"].ToString(), String.Empty);
                    if (isValidDataSet(dsUser))
                        row_mangnghiepvu["ThanhVien"] = dsUser.Tables[0].Rows[0]["Name"];
                    return row_mangnghiepvu;
                }
            }
            row_mangnghiepvu["NhomKiemToan_ID"] = String.Empty;
            row_mangnghiepvu["NhomKiemToan"] = String.Empty;
            row_mangnghiepvu["ThanhVien"] = String.Empty;
            return row_mangnghiepvu;
        }

        DataRow isExistMangNghiepVuAndNhomKT(DataSet ds_mangnghiepvu_doclink, string mangnghiepvu, string nhomkiemtoan_ID)
        {
            DataRow[] rows = ds_mangnghiepvu_doclink.Tables[0].Select("ADDTIONAL_DATA1='" + mangnghiepvu + "' And FK_DocLinkID='" + nhomkiemtoan_ID + "'");
            return (rows.Length > 0) ? rows[0] : null;
        }


        string getThanhVien(string nhomkt)
        {

            if (String.IsNullOrEmpty(nhomkt))
                return String.Empty;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(nhomkt);
            if (!isValidDataSet(ds))
                return String.Empty;
            return ds.Tables[0].Rows[0]["UserName"].ToString();
        }


        private DataTable GetListThanhVienNhomKiemToan(string nhomkt)
        {
            if (String.IsNullOrEmpty(nhomkt))
                return null;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(nhomkt);
            if (!isValidDataSet(ds))
                return null;
            DataTable dtThanhVienKT = ds.Tables[0];
            dtThanhVienKT.Columns.Add("TenMangNghiepVu", typeof(string));
            foreach (DataRow row in dtThanhVienKT.Rows)
            {
                row["TenMangNghiepVu"] = CommonFunc.getPropertyValueOnDocument(row["MangNghiepVu"].ToString(), "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
            }
            return dtThanhVienKT;
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
                Label SLThanhVienTheoMNV = (Label)e.Item.FindControl("SLThanhVienTheoMNV") as Label;
                Label SLThanhVien = (Label)e.Item.FindControl("SLThanhVien") as Label;
                Label SLMangNghiepVu = (Label)e.Item.FindControl("SLMangNghiepVu") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                string truongnhom = String.IsNullOrEmpty(e.Item.Cells[2].Text) ? String.Empty : e.Item.Cells[2].Text;
                if (PK_DocumentID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "','" + truongnhom + "')}");
                    GetSoLuongThanhVien(PK_DocumentID.Text, SLThanhVien);
                    GetSoLuongMNV(PK_DocumentID.Text, SLMangNghiepVu);
                }
            }
        }

        private void GetSoLuongThanhVienTheoMNV(string truongnhom, Label lbl)
        {
            if (String.IsNullOrEmpty(truongnhom))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(truongnhom);
            if(isValidDataSet(ds))
                 lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }


        private void GetSoLuongThanhVien(string truongnhom, Label lbl)
        {
            if (String.IsNullOrEmpty(truongnhom))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(truongnhom);
            if (isValidDataSet(ds))
                lbl.Text = ds.Tables[0].DefaultView.ToTable(true, "PK_UserID").Rows.Count.ToString();
        }

        private void GetSoLuongMNV(string truongnhom, Label lbl)
        {
            if (String.IsNullOrEmpty(truongnhom))
                return;
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(truongnhom);
            if (isValidDataSet(ds))
                lbl.Text = ds.Tables[0].DefaultView.ToTable(true, "MangNghiepVu").Rows.Count.ToString();
        }

        #endregion

        protected void dataCtrl_ItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label NhomKiemToan_ID = (Label)e.Item.FindControl("NhomKiemToan_ID") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                string truongnhom = String.IsNullOrEmpty(e.Item.Cells[2].Text) ? String.Empty : e.Item.Cells[3].Text;
                if (NhomKiemToan_ID != null)
                {
                    if (String.IsNullOrEmpty(NhomKiemToan_ID.Text))
                        imgEdit.Visible = false;
                    else
                        imgEdit.Attributes.Add("onclick", "{LoadDocument('" + NhomKiemToan_ID.Text + "','" + truongnhom + "')}");
                    Image imgSubmit = (Image)e.Item.FindControl("imgSubmit") as Image;
                    if (imgSubmit != null)
                        if (CommonFunc.IsSubmitted(NhomKiemToan_ID.Text))
                            //imgSubmit.ImageUrl = "~/Images/check.gif";
                            imgSubmit.Visible = true;
                        
                }

            }
        }

        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            GetListNhomKiemToan(_doctypeid_nhomkiemtoan, _doankt);
            GetListMangNghiepVu();
        }
    }
}