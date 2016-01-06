using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using System.Data;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class NhomKiemToan_Load : PageBase
    {
        #region initiation page variables

        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.NHOM_KIEMTOAN;
        protected string _doctypeid_dotkiemtoan = DOCTYPE.DOT_KIEMTOAN;

        protected string _doctypeid_mangnghiepvu = DOCTYPE.MANG_NGHIEPVU;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _username = string.Empty;

        protected string _pk_linkID = string.Empty;

        protected string _doankt = string.Empty;
        protected string _mangnghiepvu = string.Empty;
        protected string _mangnghiepvuID = string.Empty;
        int _status_dotkt = 0;

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
            _doankt = Request["doankt"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            _username = Request["user"];

            _mangnghiepvu = Request["mangnghiepvu"];
            _mangnghiepvuID = Request["mangnghiepvuID"];
            _pk_linkID = Request["pk_linkID"];
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
                    FeedBackClient(ThemThanhVien(_username, _mangnghiepvu));
                else if (_action == "xoathanhvien")
                    FeedBackClient(XoaThanhVien(_documentid));
                //thangma
                else if (_action == "submit")
                    FeedBackClient(Submit(_documentid));
                else if (_action == "chuyentrangthaidotkt")
                    //chuyển trạng thái đợt lên: Nhập TT đợt KT
                    FeedBackClient(TrangThaiDotKiemToan.SetStatus(_dotkt,12));
            }
            #endregion

            #region init form
            string caption = "Thông tin nhóm kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler

            btnDongy.Attributes.Add("onclick", "{ThemTruongNhom('" + _doctypeid + "'); return false;}");
            btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            CommonFunc.SetButtonBack(_btnCloseWindow, "NhomKiemToan.aspx?doankt=" + _doankt + "&dotkt=" + _dotkt);
            //thangma
            _btnSave.Visible = true;
            _btnSave.Text = "Submit";
            _btnSave.Attributes.Add("onclick", "{Submit(); return false;}");
            #endregion
        }
        public string Submit(string DocID)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsThanhVienNhomKT = doanKiemToan.DanhSachThanhVienNhomKT(DocID);
            if (isValidDataSet(dsThanhVienNhomKT))
            {
                if (dsThanhVienNhomKT.Tables[0].Rows.Count > 0)
                {
                    CommonFunc.UpdateDocStatus(DocID, 4);
                    //thangma: update Trang thai Dot kiem toan 
                    //chuyển trạng thái đợt lên: Phân tích sơ bộ
                    //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                    //if (_status_dotkt < 13)
                    //    CommonFunc.UpdateDocStatus(_dotkt, 13);
                    TrangThaiDotKiemToan.SetStatus(_dotkt, 13);
                    //end
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
                return "0";
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
                //BuildDDLMangNghiepVuByDoiTuongKT();  
                BuildDDLMangNghiepVuByDoanKT();

            }

        }
        #endregion

        #region page helper processing
        protected string getUserFullNameThanhVien(string username)
        {
            string FullName = "";
            //DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='" + ROLES.THANHVIEN_KIEMTOAN + "')", "FullName=FullName+' - '+GroupName,PK_UserID");
            //if (isValidDataSet(ds))
            //    FullName = ds.Tables[0].Rows[0]["FullName"].ToString() + "#" + ds.Tables[0].Rows[0]["PK_UserID"].ToString();
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(_doankt);

            //laythem thong tin cua truongdoan kt;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID='" + _doankt + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsDoanKT = doc.getDocumentList(DOCTYPE.DOAN_KIEMTOAN, DocFields, PropertyFields, Condition);
            bus_User user = new bus_User(_objUserContext);
            //user.

            if (isValidDataSet(dsDoanKT))
            {
                foreach (DataRow rowDoanKT in dsDoanKT.Tables[0].Rows)
                {
                    if ((rowDoanKT["Name"] == null ? String.Empty : rowDoanKT["Name"].ToString()) == username)
                    {
                        DataSet dsUSer = user.GetByName(rowDoanKT["Name"].ToString(), "PK_UserID,FullName");
                        if (isValidDataSet(dsUSer))
                        {
                            FullName = dsUSer.Tables[0].Rows[0]["FullName"].ToString() + "#" + dsUSer.Tables[0].Rows[0]["PK_UserID"].ToString();
                            return FullName;
                        }

                    }
                }
            }

            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if ((row["UserName"] == null ? String.Empty : row["UserName"].ToString()) == username)
                    {
                        FullName = row["FullName"].ToString() + "#" + row["PK_UserID"].ToString();
                        return FullName;
                    }
                }
            }

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
            //DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='" + ROLES.THANHVIEN_KIEMTOAN + "')", "FullName=FullName+' - '+GroupName");
            // if (isValidDataSet(ds))
            //    FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(_doankt);

            //laythem thong tin cua truongdoan kt;
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID='" + _doankt + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet dsDoanKT = doc.getDocumentList(DOCTYPE.DOAN_KIEMTOAN, DocFields, PropertyFields, Condition);
            bus_User user = new bus_User(_objUserContext);
            if (isValidDataSet(dsDoanKT))
            {
                foreach (DataRow rowDoanKT in dsDoanKT.Tables[0].Rows)
                {
                    if ((rowDoanKT["Name"] == null ? String.Empty : rowDoanKT["Name"].ToString()) == username)
                    {
                        DataSet dsUSer = user.GetByName(rowDoanKT["Name"].ToString(), "PK_UserID,FullName");
                        if (isValidDataSet(dsUSer))
                        {
                            FullName = dsUSer.Tables[0].Rows[0]["FullName"].ToString();
                            return FullName;
                        }
                    }
                }
            }

            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if ((row["UserName"] == null ? String.Empty : row["UserName"].ToString()) == username)
                    {
                        FullName = row["FullName"].ToString();
                        return FullName;
                    }
                }
            }
            //FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            return FullName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID, string dotkt)
        {
            string DocFields = "PK_DocumentID,[Tên đoàn kiểm toán],[Tên trưởng đoàn]";
            string PropertyFields = "Tên đoàn kiểm toán,Tên trưởng đoàn";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }


        private string GetDoiTuongKiemToanByDotKT()
        {
            string str = CommonFunc.getPropertyValueOnDocument(_dotkt, "737694DF-DE17-4FF9-AE15-70E197C83593");
            return str;
        }

        private void BuildDDLMangNghiepVuByDoiTuongKT()
        {
            string loaidoituongkiemtoan = GetDoiTuongKiemToanByDotKT();
            if (String.IsNullOrEmpty(loaidoituongkiemtoan))
                return;
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Loại đối tượng kiểm toán],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Loại đối tượng kiểm toán,Diễn giải";
            string Condition = " And [Loại đối tượng kiểm toán]=N'" + loaidoituongkiemtoan + "'";//" Order By [Loại đối tượng kiểm toán]";
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
                foreach (DataRow row in ds.Tables[0].Rows)
                    //if((row["Loại đối tượng kiểm toán"] == null ? String.Empty : row["Loại đối tượng kiểm toán"].ToString()) == loaidoituongkiemtoan)
                    ddlMangNghiepVu.Items.Add(new ListItem(row["Tên mảng nghiệp vụ"].ToString(), row["PK_DocumentID"].ToString()));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetListThanhVien(string nhomkt)
        {
            if (String.IsNullOrEmpty(nhomkt))
                return;
            //bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(nhomkt);
            //if (!isValidDataSet(ds))
            //    return;
            //DataTable dtDoanKT = ds.Tables[0];
            //dtDoanKT.Columns.Add("TenMangNghiepVu", typeof(string));
            //dtDoanKT.Columns.Add("MangNghiepVuID", typeof(string));
            //foreach (DataRow row in dtDoanKT.Rows)
            //{ 
            //    //DataRow rowNew = dtDoanKT.NewRow();
            //    row["TenMangNghiepVu"] = CommonFunc.getPropertyValueOnDocument(row["MangNghiepVu"].ToString(), "144DFC1C-5D45-4FE7-8772-CD573CEFD04F");
            //    row["MangNghiepVuID"] = row["MangNghiepVu"].ToString();
            //}

            //dataCtrl.DataSource = dtDoanKT;
            //dataCtrl.DataBind();

            ObjectDataSource1.SelectParameters["Status"].DefaultValue = "2";
            ObjectDataSource1.SelectParameters["TruongDoan"].DefaultValue = String.Empty;
            ObjectDataSource1.SelectParameters["TruongNhom"].DefaultValue = nhomkt;
            dataCtrl.DataBind();
        }


        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string ThemThanhVien(string thanhvien, string mangnghiepvu)
        {
            string FullName = "2";
            //bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(_doankt);
            //if (isValidDataSet(ds))
            //{
            //    DataTable dt = ds.Tables[0];
            //    //DataRow[] rows = dt.Select("PK_UserID ='" + thanhvien + "' AND MangNghiepVu='"+mangnghiepvu+"'");
            //    DataRow[] rows = dt.Select(" AND MangNghiepVu=N'" + mangnghiepvu + "'");
            //    if (rows.Count() > 0)
            //    {
            //        FullName = "1";
            //        return FullName;
            //    }
            //}
            string DocFields_nhomkt = "PK_DocumentID,[Name]";
            string PropertyFields_nhomkt = "Name";
            string Condition_kt = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";
            bus_Document obj_nhomkt = bus_Document.Instance(_objUserContext);
            DataSet ds_nhomkt = obj_nhomkt.getDocumentList(_doctypeid, DocFields_nhomkt, PropertyFields_nhomkt, Condition_kt);
            //mang nghiep vu da ton tai trong doan kiem toan nay roi thi ko cho phep
            bus_Doankiemtoan obj_doankt = new bus_Doankiemtoan(_objUserContext);
            DataSet ds_mangnghiepvu_doclink = obj_doankt.GetAllMangNghiepVuDocLink();
            if (isValidDataSet(ds_mangnghiepvu_doclink))
            {
                foreach (DataRow row_nhomkt in ds_nhomkt.Tables[0].Rows)
                {
                    DataRow[] rows = ds_mangnghiepvu_doclink.Tables[0].Select("ADDTIONAL_DATA1='" + mangnghiepvu + "' And FK_DocLinkID='" + row_nhomkt["PK_DocumentID"] + "'");
                    if (rows.Length > 0)
                    {
                        FullName = "1";
                        return FullName;
                    }
                }
            }
            CommonFunc.AddDocLink(thanhvien, _doankt, TYPE_OF_LINK.USER, _objUserContext, _mangnghiepvu, String.Empty);
            return FullName;
        }




        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string XoaThanhVien(string doc)
        {
            //string truongnhom = _doankt;
            //CommonFunc.RemoveDocLink(doc, truongnhom, _objUserContext);
            CommonFunc.RemoveDocLink(_pk_linkID, _objUserContext);
            string errormessage = "2";
            return errormessage;
        }

        #endregion

        #region page button processing
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {

            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_UserID = (Label)e.Item.FindControl("PK_UserID") as Label;
                Label PK_LINKID = (Label)e.Item.FindControl("PK_LINKID") as Label;
                Label FK_DOCLINKID = (Label)e.Item.FindControl("FK_DOCLINKID") as Label;
                Label MangNghiepVuID = (Label)e.Item.FindControl("MangNghiepVuID") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;


                if (PK_UserID != null && FK_DOCLINKID != null && MangNghiepVuID != null && PK_LINKID != null)
                {
                    string mangnghiepvu = e.Item.Cells[6].Text;
                    imgDelete.Attributes.Add("onclick", "{xoathanhvien('" + e.Item.ClientID + "','" + PK_UserID.Text + "','" + FK_DOCLINKID.Text + "','" + PK_LINKID.Text + "','" + MangNghiepVuID.Text + "','" + mangnghiepvu + "')}");
                    if (CommonFunc.IsSubmitted(idTruongDoan.Value))
                    {
                        imgDelete.Visible = false;

                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            string truongnhom = idTruongDoan.Value;
            GetListThanhVien(truongnhom);
        }

        private void BuildDDLMangNghiepVuByDoanKT()
        {
            //lay mang nghiep vu theo doan kiem toan
            string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Diễn giải],Status";
            string PropertyFields = "Tên mảng nghiệp vụ,Diễn giải";
            string Condition = " And PK_DocumentID In (Select FK_DocumentID from T_DocLink where FK_DocLinkID ='" + _doankt + "')";
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds_mangnghiepvu = obj.getDocumentList(_doctypeid_mangnghiepvu, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds_mangnghiepvu) == false)
                return;

            //chi lay nhung mang nghiep vu nao chua ton tai trong nhom
            string DocFields_nhomkt = "PK_DocumentID,[Name]";
            string PropertyFields_nhomkt = "Name";
            string Condition_kt = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";
            bus_Document obj_nhomkt = bus_Document.Instance(_objUserContext);
            DataSet ds_nhomkt = obj_nhomkt.getDocumentList(_doctypeid, DocFields_nhomkt, PropertyFields_nhomkt, Condition_kt);

            if (isValidDataSet(ds_nhomkt) == false)
            {
                foreach (DataRow row_mangnghiepvu in ds_mangnghiepvu.Tables[0].Rows)
                {
                    ddlMangNghiepVu.Items.Add(new ListItem(row_mangnghiepvu["Tên mảng nghiệp vụ"].ToString(), row_mangnghiepvu["PK_DocumentID"].ToString()));
                }
                return;
            }

            bus_Doankiemtoan obj_doankt = new bus_Doankiemtoan(_objUserContext);
            DataSet ds_mangnghiepvu_doclink = obj_doankt.GetAllMangNghiepVuDocLink();

            if (isValidDataSet(ds_mangnghiepvu_doclink) == false)
            {
                if (isValidDataSet(ds_mangnghiepvu))
                    foreach (DataRow row_mangnghiepvu in ds_mangnghiepvu.Tables[0].Rows)
                        ddlMangNghiepVu.Items.Add(new ListItem(row_mangnghiepvu["Tên mảng nghiệp vụ"].ToString(), row_mangnghiepvu["PK_DocumentID"].ToString()));
                return;
            }

            foreach (DataRow row_mangnghiepvu in ds_mangnghiepvu.Tables[0].Rows)
                if (isExistMangNghiepVuByDoanKT(ds_mangnghiepvu_doclink, ds_nhomkt, row_mangnghiepvu["PK_DocumentID"].ToString()) == false)
                    ddlMangNghiepVu.Items.Add(new ListItem(row_mangnghiepvu["Tên mảng nghiệp vụ"].ToString(), row_mangnghiepvu["PK_DocumentID"].ToString()));

        }

        bool isExistMangNghiepVuByDoanKT(DataSet ds_mangnghiepvu_doclink, DataSet ds_nhomkt, string mangnghiepvu)
        {
            foreach (DataRow row_nhomkt in ds_nhomkt.Tables[0].Rows)
            {
                //ton tai roi thi bo qua
                DataRow[] rows = ds_mangnghiepvu_doclink.Tables[0].Select("ADDTIONAL_DATA1 ='" + mangnghiepvu + "' And FK_DocLinkID='" + row_nhomkt["PK_DocumentID"] + "'");
                if (rows.Length > 0)
                    return true;
            }
            return false;
        }

        #endregion
    }
}