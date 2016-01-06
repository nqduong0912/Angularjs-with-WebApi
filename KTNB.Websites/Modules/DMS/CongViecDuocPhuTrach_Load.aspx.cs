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
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class CongViecDuocPhuTrach_Load : PageBase
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
        protected string _trangthai = string.Empty;
        protected string _nhanxet = String.Empty;
        protected string _cv = string.Empty;
        int _status_dotkt = 0;
        int _status = 0;
        string _type;

        protected bool _isSubmitted = false;
        //thangma
        protected int _docstatus = 0;
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
            _trangthai = Request["trangthai"];
            _nhanxet = Request["nhanxet"];
            _cv = Request["cv"];
            if (!string.IsNullOrEmpty(_documentid))
                _status = CommonFunc.GetDocStatus(_documentid);
            if (_cv == "nguoiduyet")
                _type = CommonFunc.CheckNguoiDuyet(_documentid);
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
                if (_action == "capnhattrangthai")
                {
                    FeedBackClient(CapNhatTrangThai(_documentid));
                }
                if (_action == "capnhattrangthaituchoi")
                {
                    FeedBackClient(CapNhatTrangThaiTuChoi(_documentid));
                }
            }
            //check doc status
            if (_documentid != null)
            {
                _docstatus = CommonFunc.GetDocStatus(_documentid);
            }
            #endregion

            #region init form
            string caption = "Cập nhật chương trình kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin chương trình kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnEdit.Attributes.Add("onclick", "{updatedocumentcongviec('" + _documentid + "'); return false;}");
            _btnDelete.Visible = false;
            _btnCloseWindow.Visible = false;
            if (!String.IsNullOrEmpty(_action))
            {

                _btnEdit.Visible = false;
            }
            //thangma
            _btnSave.Visible = true;
            _btnSave.Text = "Cập nhật";
            _btnSave.Attributes.Add("onclick", "{CapNhatTrangThai(); return false;}");
            _btnRemove.Attributes.Add("onclick", "{CapNhatTrangThaiTuChoi(); return false;}");


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
                UpdateInprogress();
                BuildTrangThai();
                if (!String.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        BuildTreeViewThuTucKT(treeViewThuTucKT);
                        try
                        {
                            _btnCloseWindow.Visible = true;
                            CommonFunc.SetButtonBack(_btnCloseWindow, "CongViecDuocPhuTrach.aspx?doankt=" + _doankt + "&dotkt=" + _dotkt + "&cv=" + _cv);
                            if (_cv == "nguoithuchien")
                            {
                                if (_status == 4 || _status == 2 || _status == 8)//inprogress va rejected
                                {
                                    _btnSave.Text = "Hoàn tất"; _btnSave.Visible = true;
                                }
                                else _btnSave.Visible = false;
                            }
                            if (_cv == "nguoiduyet")
                            {
                                //kiemtra la nguoiduyet1 hay nguoiduyet2
                                if (_type == "nguoiduyet1")
                                {
                                    _btnSave.Visible = _btnRemove.Visible = false;
                                    _btnSave.Text = "Phê duyệt";
                                    _btnRemove.Text = "Từ chối";
                                    if (_status == 16) _btnRemove.Visible = _btnSave.Visible = true;
                                    else trNhanXet.Visible = false;
                                }
                                if (_type == "nguoiduyet2")
                                {
                                    _btnSave.Visible = _btnRemove.Visible = false;
                                    _btnSave.Text = "Phê duyệt";
                                    _btnRemove.Text = "Từ chối";
                                    if (_status == 32) _btnRemove.Visible = _btnSave.Visible = true;
                                    else trNhanXet.Visible = false;
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                //grid tu choi
                string jsonTuChoi = CommonFunc.getPropertyValueOnDocument(_documentid, "AE50F25A-A04E-4541-8266-CE8C5033E0E5");
                if (jsonTuChoi.Length > 0)
                {
                    try
                    {
                        //bind Lich su tu choi
                        List<NhanXet> nhanXetTuChoiList = JSONHelper.Deserialize<List<NhanXet>>(jsonTuChoi);
                        List<NhanXet> tuChoiList = nhanXetTuChoiList.Where(s => s.HanhDong == "TuChoi").ToList();
                        gridTuChoi.DataSource = tuChoiList;
                        gridTuChoi.DataBind();
                        //bind da nhan xet
                        List<NhanXet> nhanXetList = nhanXetTuChoiList.Where(s => s.HanhDong == "PheDuyet").ToList();
                        foreach (NhanXet nhanxet in nhanXetList)
                        {
                            string nguoiduyetX = CommonFunc.CheckNguoiDuyetOfCongViec(_documentid, nhanxet.NguoiNhanXet);
                            if (nguoiduyetX.Equals("nguoiduyet1"))
                                DaNhanXetNguoiDuyet1.Text = nhanxet.LyDo;
                            //else
                            //    DaNhanXetNguoiDuyet2.Text = nhanxet.LyDo;
                        }
                    }
                    catch (Exception ex) { }
                }
            }
        }


        void UpdateInprogress()
        {
            int status = CommonFunc.GetDocStatus(_documentid);
            //chi o trang thai not started thi moi update inprogress
            if (_status == 2)
            {
                CommonFunc.UpdateDocStatus(_documentid, 4);
                //thangma
                //chuyển trạng thái đợt lên: Cập nhật phát hiện
                TrangThaiDotKiemToan.SetStatus(_dotkt, 21);
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
            CommonFunc.LoadDropDownList(this.DOCSTATUS, 4);
        }
        public static void LoadDropTrangThai(DropDownList obj)
        {
            List<string> TrangThaiList = CommonFunc.GetDanhSachTrangThai();
            foreach (string trangthai in TrangThaiList)
                obj.Items.Add(new ListItem(trangthai, trangthai));
        }

        public string CapNhatTrangThai(string DocID)
        {
            if (_cv == "nguoithuchien")
            {
                if (_status == 4 || _status == 8)
                {
                    string result = "1";
                    //kiem tra cac phathien da done 2<= status <=16   
                    if (CheckAllPhatHienIsDoned(DocID) == false)
                    {
                        result = "2";
                        return result;
                    }
                    CommonFunc.UpdateDocStatus(DocID, 16);//done;
                    result = "1";
                    //thangma: update Trang thai Dot kiem toan 
                    //chuyển trạng thái đợt lên: Cập nhật công việc (chuyển duyệt)
                    //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                    //if (_status_dotkt < 22)
                    //    CommonFunc.UpdateDocStatus(_dotkt, 22);
                    string resultUpdStatusDot = TrangThaiDotKiemToan.SetStatus(_dotkt, 22);
                    //trường hợp đặc biệt:
                    //Công việc không có phát hiện nào
                    if (resultUpdStatusDot.Equals("0"))
                    {
                        int currentStatus = CommonFunc.GetDocStatus(_dotkt);
                        bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
                        DataSet dsPhatHien = doankiemtoan.GetTatCaDanhSachPhatHien(DocID);

                        if (currentStatus == 15 && !isValidDataSet(dsPhatHien))
                        {
                            CommonFunc.UpdateDocStatus(_dotkt, 22);
                        }
                    }
                    //end
                    return result;
                }
                //else
                //{
                //    CommonFunc.UpdateDocStatus(DocID, Int32.Parse(_trangthai));
                //    string result = "1";
                //    return result;
                //}
            }
            if (_cv == "nguoiduyet")
            {
                if (_type == "nguoiduyet1")
                {
                    if (isCheckPhatHienApproved(DocID) == false)
                    {
                        string result = "0";
                        return result;
                    }
                    else
                    {
                        if (_status == 16)
                        {
                            CommonFunc.UpdateDocStatus(DocID, 32);//Approved1
                            //thangma: update Trang thai Dot kiem toan 
                            //chuyển trạng thái đợt lên: Đánh giá rr còn lại
                            //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                            //if (_status_dotkt < 23)
                            //    CommonFunc.UpdateDocStatus(_dotkt, 23);
                            TrangThaiDotKiemToan.SetStatus(_dotkt, 23);
                            //end
                            //cập nhật nhận xét
                            AddNhanXet(DocID, "PheDuyet");
                            string result = "1";
                            return result;
                        }
                    }
                }
                if (_type == "nguoiduyet2")
                {
                    //check xem phan hoi theo congviecid da dc approved2 het chua?
                    if (isCheckPhatHienApproved(DocID) == false)
                    {
                        string result = "0";
                        return result;
                    }
                    else
                    {
                        if (_status == 32)
                        {
                            CommonFunc.UpdateDocStatus(DocID, 64);//Approved2
                            string result = "1";
                            //thangma: update Trang thai Dot kiem toan 
                            //chuyển trạng thái đợt lên: Đánh giá rr còn lại
                            //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                            //if (_status_dotkt < 23)
                            //    CommonFunc.UpdateDocStatus(_dotkt, 23);
                            TrangThaiDotKiemToan.SetStatus(_dotkt, 23);
                            //end
                            //cập nhật nhận xét
                            AddNhanXet(DocID, "PheDuyet");
                            return result;
                        }
                    }

                }
            }
            return String.Empty;
        }

        /// <summary>
        /// sua lai chi can app1 la okie
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        bool isCheckPhatHienApproved(string DocID)
        {
            bus_Document doc = new bus_Document(_objUserContext);
            //get danh sach phat hien hethong theo congviecid
            string DocFields_HT = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_HT = "Phát hiện";
            string Condition_HT = " and PK_DocumentID IN (SELECT FK_DocumentID From T_DOCLINK WHERE FK_DOCLINKID ='" + DocID + "')";
            DataSet dsPhatHien_HT = doc.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT);

            //get danh sach phat hien vipham theo congviecid
            string DocFields_VP = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_VP = "Phát hiện";
            string Condition_VP = " and PK_DocumentID IN (SELECT FK_DocumentID From T_DOCLINK WHERE FK_DOCLINKID ='" + DocID + "')";
            DataSet dsPhatHien_VP = doc.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP);

            string Condition_HT_Approved = " and PK_DocumentID IN (SELECT FK_DocumentID From T_DOCLINK WHERE FK_DOCLINKID ='" + DocID + "') and (Status=4 Or Status=16)";//=16
            string Condition_VP_Approved = " and PK_DocumentID IN (SELECT FK_DocumentID From T_DOCLINK WHERE FK_DOCLINKID ='" + DocID + "') and (Status=4 Or Status=16)";//=16

            DataSet dsPhatHien_HT_Approved = doc.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT_Approved);
            DataSet dsPhatHien_VP_Approved = doc.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP_Approved);

            if (dsPhatHien_HT != null)
                if (dsPhatHien_VP != null)
                    dsPhatHien_VP.Merge(dsPhatHien_HT, true, MissingSchemaAction.Add);//tat ca phathien theo congviecid;

            if (dsPhatHien_HT_Approved != null)
                if (dsPhatHien_VP_Approved != null)
                    dsPhatHien_VP_Approved.Merge(dsPhatHien_HT_Approved, true, MissingSchemaAction.Add);//tat ca phathien theo congviecid;

            if (dsPhatHien_VP.Tables[0].Rows.Count == dsPhatHien_VP_Approved.Tables[0].Rows.Count)
                return true;
            return false;
        }


        public string CapNhatTrangThaiTuChoi(string DocID)
        {
            //CommonFunc.UpdateDocStatus(DocID, 2);//Not started
            CommonFunc.UpdateDocStatus(DocID, 8);//Rejected
            string result = "1";
            //cập nhật lý do từ chối
            AddNhanXet(DocID, "TuChoi");
            return result;
        }
        public void AddNhanXet(string DocID, string hanhDong)
        {
            string nhanXetValue = _nhanxet.Trim();
            if (nhanXetValue.Length > 0)
            {
                //cập nhật lý do từ chối
                string jsonNhanXet = CommonFunc.getPropertyValueOnDocument(DocID, "AE50F25A-A04E-4541-8266-CE8C5033E0E5");
                List<NhanXet> tuChoiList;
                if (jsonNhanXet.Length > 0)
                    tuChoiList = JSONHelper.Deserialize<List<NhanXet>>(jsonNhanXet);
                else
                    tuChoiList = new List<NhanXet>();

                NhanXet tuchoi = new NhanXet(_nhanxet, _objUserContext.UserName, DateTime.Now.ToString(), hanhDong);
                //remove nhan xet cu
                List<NhanXet> nhanXetPheDuyetList = tuChoiList.Where(s => s.HanhDong == "PheDuyet").ToList();
                if (nhanXetPheDuyetList.Count > 0 && hanhDong.Equals("PheDuyet"))
                {
                    var itemToRemove = tuChoiList.SingleOrDefault(r => r.HanhDong == "PheDuyet");
                    if (itemToRemove != null)
                        tuChoiList.Remove(itemToRemove);
                }
                tuChoiList.Add(tuchoi);
                jsonNhanXet = JSONHelper.Serialize<List<NhanXet>>(tuChoiList);
                CommonFunc.UpdateDocPropertyValue(DocID, "AE50F25A-A04E-4541-8266-CE8C5033E0E5", jsonNhanXet);
            }
        }
        bool isExist(DataRow row, DataTable dt)
        {
            foreach (DataRow row1 in dt.Rows)
                if (row1["PK_DocumentID"].ToString() == row["PK_DocumentID"].ToString())
                    return true;
            return false;
        }


        void BuildTreeViewThuTucKT(TreeView treeViewThuTucKT)
        {
            DataTable dt = GetDataTableThuTucByCongViec();
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                    return;
                TreeNode nodeRoot = new TreeNode();
                nodeRoot.Text = "Thủ tục kiểm toán";
                nodeRoot.SelectAction = TreeNodeSelectAction.None;
                treeViewThuTucKT.Nodes.Add(nodeRoot);
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["Tên thủ tục kiểm toán"].ToString();
                    node.ToolTip = row["Tên thủ tục kiểm toán"].ToString();
                    node.SelectAction = TreeNodeSelectAction.None;
                    nodeRoot.ChildNodes.Add(node);
                }
            }
        }

        DataTable GetDataTableThuTucByCongViec()
        {
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
                    if (_isSubmitted)
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
            CommonFunc.AddDocLink(_thutuc, _documentid, TYPE_OF_LINK.DOCUMENT, _objUserContext);
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
                    CommonFunc.AddDocLink(str, _documentid, TYPE_OF_LINK.DOCUMENT, _objUserContext);
                }
            }
            return FullName;
        }

        #endregion end helper

        #region kiem tra phathien da het done chua
        bool CheckAllPhatHienIsDoned(string congviecID)
        {
            string DocFields_HT = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_HT = "Phát hiện";
            string Condition_HT = "  and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + congviecID + "') and (Status >= 2 And Status <=16)";

            bus_Document obj = new bus_Document(_objUserContext);
            DataSet ds_HT = obj.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT, PropertyFields_HT, Condition_HT);

            string DocFields_VP = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_VP = "Phát hiện";
            string Condition_VP = "  and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + congviecID + "') and (Status >= 2 And Status <=16)";

            DataSet ds_VP = obj.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP, PropertyFields_VP, Condition_VP);
            if (ds_HT != null)
                if (ds_VP != null)
                    ds_HT.Merge(ds_VP);

            string DocFields_HT_ALL = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_HT_ALL = "Phát hiện";
            string Condition_HT_ALL = "  and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + congviecID + "')";
            DataSet ds_HT_ALL = obj.getDocumentList(DOCTYPE.PHATHIEN_HETHONG, DocFields_HT_ALL, PropertyFields_HT_ALL, Condition_HT_ALL);

            string DocFields_VP_ALL = "PK_DocumentID,[Phát hiện],Status";
            string PropertyFields_VP_ALL = "Phát hiện";
            string Condition_VP_ALL = "  and PK_DocumentID In (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + congviecID + "')";
            DataSet ds_VP_ALL = obj.getDocumentList(DOCTYPE.PHATHIEN_VIPHAM, DocFields_VP_ALL, PropertyFields_VP_ALL, Condition_VP_ALL);

            if (ds_HT_ALL != null)
                if (ds_VP_ALL != null)
                    ds_HT_ALL.Merge(ds_VP_ALL);
            if (ds_HT.Tables.Count > 0 && ds_HT_ALL.Tables.Count > 0)
                if (ds_HT.Tables[0].Rows.Count == ds_HT_ALL.Tables[0].Rows.Count)
                    return true;
            return false;
        }


        #endregion
    }
}