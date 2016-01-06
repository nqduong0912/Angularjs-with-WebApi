using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.Definition.UMS;
using CORE.UMS.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class NhomKiemToan_View : PageBase
    {
        #region initiation page variables

        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.NHOM_KIEMTOAN;
        protected string _doctypeid_mangnghiepvu = DOCTYPE.MANG_NGHIEPVU;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _username = string.Empty;
        protected string _mangnghiepvuID = string.Empty;
        protected string _pk_linkID = string.Empty;
        int _status_dotkt = 0;


        protected string _doankt = string.Empty;
        protected string _truongnhom = string.Empty;
        protected string _mangnghiepvu = string.Empty;
        protected bool _isTruongDoan = true;
        //THANGMA
        protected int _docstatus = 0;
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
            _dotkt = Request["dotkt"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            _username = Request["user"];

            _doankt = Request["doankt"];
            _truongnhom = Request["truongnhom"];
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
                {
                    FeedBackClient(XoaThanhVien(_documentid));
                }
                else if (_action == "capnhatnhomkiemtoan")
                    FeedBackClient(CapNhapNhomKiemToan());
                //thangma
                else if (_action == "submit")
                    FeedBackClient(Submit(_documentid));


            }
            //check doc status
            if (_documentid != null)
            {
                _docstatus = CommonFunc.GetDocStatus(_documentid);
            }
            #endregion

            #region init form
            string caption = "Thông tin nhóm kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler
            btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            _isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
            if (_isTruongDoan == true)
                btnThem.Visible = this.THANHVIEN.Enabled = true;
            else
                btnThem.Visible = this.THANHVIEN.Enabled = false;
            btnDongy.Attributes.Add("onclick", "{updatedocumentnhomkiemtoan('" + _documentid + "');return false;}");
            CommonFunc.SetButtonBack(_btnCloseWindow, "NhomKiemToan.aspx?doankt=" + _doankt + "&dotkt=" + _dotkt);
            _isSubmitted = CommonFunc.IsSubmitted(_documentid);
            //thangma
            _btnSave.Visible = true;
            _btnSave.Text = "Submit";
            _btnSave.Attributes.Add("onclick", "{Submit(); return false;}");
            if (_isSubmitted)
            {
                btnDongy.Visible = false;
            }

            #endregion
        }
        public string Submit(string DocID)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsThanhVienDoanKT = doanKiemToan.DanhSachThanhVienDoanKT(DocID);
            if (isValidDataSet(dsThanhVienDoanKT))
            {
                if (dsThanhVienDoanKT.Tables[0].Rows.Count > 0)
                {
                    CommonFunc.UpdateDocStatus(DocID, 4);

                    //thangma: update Trang thai Dot kiem toan 
                    //len trang thai Phan tich so bo
                    _status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                    if (_status_dotkt < 13)
                        CommonFunc.UpdateDocStatus(_dotkt, 13);
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
            //BuildDDLMangNghiepVuByDoiTuongKT();
            BuildDDLMangNghiepVuByDoanKT();
            this.DOCNAME.Text = _truongnhom;
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                        GetListThanhVien(_documentid);
            }
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

            //laythongtin doan kiem toan
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
            return FullName;
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
            //    row["MangNghiepVuID"] = row["MangNghiepVu"];
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
            //DataSet ds = doankiemtoan.DanhSachThanhVienNhomKT(_documentid);
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

            //lay danh sach nhom kiem toantheo doan kiem toan
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _doankt + "')";
            bus_Document obj_nhomkt = bus_Document.Instance(_objUserContext);
            DataSet ds_nhomkt = obj_nhomkt.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds_nhomkt) == false)
            {
                FullName = "1";
                return FullName;
            }

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
            CommonFunc.AddDocLink(thanhvien, _documentid, TYPE_OF_LINK.USER, _objUserContext, _mangnghiepvu, String.Empty);
            return FullName;
        }


        /// <summary>
        /// 1:thanhcong,2:da co thanhvien trong dot kiem toan nay
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private string XoaThanhVien(string doc)
        {
            //string doankt = _truongnhom;
            //CommonFunc.RemoveDocLink(doc, doankt, _objUserContext);
            CommonFunc.RemoveDocLink(_pk_linkID, _objUserContext);
            string errormessage = "2";
            return errormessage;
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
                    //if ((row["Loại đối tượng kiểm toán"] == null ? String.Empty : row["Loại đối tượng kiểm toán"].ToString()) == loaidoituongkiemtoan)
                    ddlMangNghiepVu.Items.Add(new ListItem(row["Tên mảng nghiệp vụ"].ToString(), row["PK_DocumentID"].ToString()));
        }

        #endregion

        #region page button processing
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_UserID = (Label)e.Item.FindControl("PK_UserID") as Label;
                Label MangNghiepVuID = (Label)e.Item.FindControl("MangNghiepVuID") as Label;

                Label FK_DOCLINKID = (Label)e.Item.FindControl("FK_DOCLINKID") as Label;
                Label PK_LINKID = (Label)e.Item.FindControl("PK_LINKID") as Label;

                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                if (PK_UserID != null && FK_DOCLINKID != null && MangNghiepVuID != null && PK_LINKID != null)
                {
                    string mangnghiepvu = e.Item.Cells[7].Text;
                    if (_isTruongDoan == false)
                        imgDelete.Visible = false;
                    if (_isSubmitted)
                        imgDelete.Visible = false;
                    imgDelete.Attributes.Add("onclick", "{xoathanhvien('" + e.Item.ClientID + "','" + PK_UserID.Text + "','" + FK_DOCLINKID.Text + "','" + MangNghiepVuID.Text + "','" + mangnghiepvu + "','" + PK_LINKID.Text + "')}");
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
            GetListThanhVien(_documentid);
        }

        private void BuildDDLMangNghiepVuByDoanKT()
        {
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
                return;

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

        private string CapNhapNhomKiemToan()
        {
            CommonFunc.UpdateDocNameDescription(_documentid, _username, String.Empty);
            string FullName = "2";
            return FullName;
        }
        #endregion
    }
}