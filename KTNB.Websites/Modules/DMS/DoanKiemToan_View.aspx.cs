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
    public partial class DoanKiemToan_View : PageBase
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


        protected string _truongdoan = string.Empty;
        protected bool _isTruongPhong = true;
        protected string _dsmangNghiepVu = string.Empty;
        //thangma
        protected int _docstatus = 0;
        protected bool _isSubmitted = false;
        protected int _status_dotkt = 0;
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
            if (!string.IsNullOrEmpty(_dotkt))
                _status_dotkt = CommonFunc.GetDocStatus(_dotkt);

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
                else if (_action == "capnhatdoankiemtoan")
                    FeedBackClient(CapNhapDoanKiemToan());
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
            string caption = "Thông tin đoàn kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler
            btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            _isTruongPhong = (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN ? true : false);

            if (_isTruongPhong == true)
                btnThem.Visible = this.THANHVIEN.Enabled = btnDongy.Visible = true;
            else
                btnThem.Visible = this.THANHVIEN.Enabled = btnDongy.Visible = false;
            if (_status_dotkt >= 23)//ma tran
                btnThem.Visible = false;

            if (_isTruongPhong)
            {
                //ve sau se kiem tra ca truong hop da submmited chua ?
                btnDongy.Attributes.Add("onclick", "{updatedocumentdoankiemtoan('" + _documentid + "');return false;}");
                _isSubmitted = CommonFunc.IsSubmitted(_documentid);
                if (_isSubmitted)
                {
                    //neu submitted ko dc sua, xoa thanh vien
                    // dc them thanh vien
                    btnDongy.Visible = false;
                }

                //thangma
                _btnSave.Visible = true;
                _btnSave.Text = "Submit";
                _btnSave.Attributes.Add("onclick", "{Submit(); return false;}");

            }
            CommonFunc.SetButtonBack(_btnCloseWindow, "DoanKiemToan.aspx?dotkt=" + _dotkt);
            #endregion
        }
        public string Submit(string DocID)
        {
            bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
            DataSet dsThanhVienDoanKT = doanKiemToan.DanhSachThanhVienDoanKT(DocID);
            DataSet dsMangNghiepVuMapped = doanKiemToan.GetMangNghiepVuByDoanKT(DocID);
            if (isValidDataSet(dsMangNghiepVuMapped))
            {
                if (dsMangNghiepVuMapped.Tables[0].Rows.Count == 0)
                    return "2";
            }
            else
                return "2";
            if (isValidDataSet(dsThanhVienDoanKT))
            {
                if (dsThanhVienDoanKT.Tables[0].Rows.Count > 0)
                {
                    CommonFunc.UpdateDocStatus(DocID, 4);
                    //thangma: update Trang thai Dot kiem toan 
                    //len trang thai Nhap TT dot kiem toan
                    //int dotktStatus = CommonFunc.GetDocStatus(_dotkt);
                    //if (dotktStatus < 12)
                    //    CommonFunc.UpdateDocStatus(_dotkt, 12);
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
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        this.DOCNAME.Text = _truongdoan;
                        if (!btnDongy.Visible) 
                            this.DOCNAME.Enabled = false;
                        GetListThanhVien(_documentid);
                    }
                }
            }
        }
        public string MappingMangNghiepVu(string strDSMangNghiepVu)
        {
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
            //lay theo ROLES
            DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='"
                + ROLES.THANHVIEN_KIEMTOAN + "' OR FK_RoleID='" + ROLES.TRUONGPHONG_DONVI_KIEMTOAN + "' OR FK_RoleID='" + ROLES.CANBO_DUYET + "')"
                , "FullName=FullName+' - '+GroupName,PK_UserID");
            // lay theo phong
            //string groupName = GetGroupNameByDotKiemToan();
            //bus_User_In_Group group = new bus_User_In_Group(_objUserContext);
            //DataSet ds = group.getList(" AND GROUPNAME=N'" + groupName + "' And UserName='" + username + "'", "FullName=FullName+' - '+GroupName,PK_UserID");
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
            //string groupName = GetGroupNameByDotKiemToan();
            //lay theo RPLES
            //DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='" + ROLES.THANHVIEN_KIEMTOAN + "')", "FullName=FullName+' - '+GroupName");
            DataSet ds = CommonFunc.getUserInfo(" AND UserName='" + username + "' AND PK_UserID IN (Select FK_UserID From T_USER_IN_ROLE Where  FK_RoleID='"
              + ROLES.THANHVIEN_KIEMTOAN + "' OR FK_RoleID='" + ROLES.TRUONGPHONG_DONVI_KIEMTOAN + "' OR FK_RoleID='" + ROLES.CANBO_DUYET + "')"
              , "FullName=FullName+' - '+GroupName,PK_UserID");

            //bus_User_In_Group group = new bus_User_In_Group(_objUserContext);
            //DataSet ds = group.getList(" AND GROUPNAME=N'" + groupName + "' And UserName='" + username + "'", "FullName=FullName+' - '+GroupName,PK_UserID");
            if (isValidDataSet(ds))
                FullName = ds.Tables[0].Rows[0]["FullName"].ToString();
            return FullName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetListThanhVien(string truongdoan)
        {
            if (String.IsNullOrEmpty(truongdoan))
                return;
            //bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(truongdoan);
            //if (isValidDataSet(ds))
            //{
            //    dataCtrl.DataSource = ds;
            //    dataCtrl.DataBind();
            //}

            ObjectDataSource1.SelectParameters["Status"].DefaultValue = "1";
            ObjectDataSource1.SelectParameters["TruongDoan"].DefaultValue = truongdoan;
            ObjectDataSource1.SelectParameters["TruongNhom"].DefaultValue = String.Empty;
            dataCtrl.DataBind();
        }


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

        private string CapNhapDoanKiemToan()
        {
            CommonFunc.UpdateDocNameDescription(_documentid, _username, String.Empty);
            string FullName = "2";
            return FullName;
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
                    imgDelete.Attributes.Add("onclick", "{xoathanhvien('" + e.Item.ClientID + "','" + PK_UserID.Text + "','" + FK_DOCLINKID.Text + "')}");
                    if (_isTruongPhong == false)
                        imgDelete.Visible = false;
                    if (_isSubmitted)//ko dc phep xoa
                        imgDelete.Visible = false;
                    //if(_status_dotkt >= 14)//matran doan nay vo nghia vi doankt fai dc subitted roi chu?
                    //    imgDelete.Visible = false;
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
        #endregion
    }
}