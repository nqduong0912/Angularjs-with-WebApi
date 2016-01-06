using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using C1.Web.C1Input;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class PhanHoiDonVi_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.THONGTIN_PHANHOI;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;//phanhoiID
        protected string _phathienid = string.Empty;//phathienID
        protected string _dotkt = string.Empty;//dotID
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _noidung = string.Empty;
        protected string _cv = string.Empty;
        protected int _status = 0;
        protected string _congviec_docid = string.Empty;
        protected string _type = string.Empty;
        protected string _timkiem = string.Empty;
        int _status_dotkt = 0;
        int _status_phanhoi = 0;
        protected string _nhanxet = String.Empty;
        protected string _role = string.Empty;
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
            _phathienid = Request["phathienid"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            _noidung = Request["noidung"];
            _cv = Request["cv"];
            _congviec_docid = Request["congviec_docid"];
            _timkiem = Request["timkiem"];
            _nhanxet = Request["nhanxet"];
            _role = Request["role"];
            if (_cv == "nguoiduyet")
                _type = CommonFunc.CheckNguoiDuyet(_congviec_docid);
            if (!string.IsNullOrEmpty(_documentid))
                _status_phanhoi = CommonFunc.GetDocStatus(_documentid);

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
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
                if (_action == "deletefile")
                    FeedBackClient(CommonFunc.deleteBody(_propertyvalue));
                if (_action == "updatenoidung")
                    FeedBackClient(UpdateNoiDung(_noidung));
                if (_action == "capnhattrangthaidone")
                    capnhattrangthaidone(_documentid);
                if (_action == "capnhattrangthaipheduyet")
                    capnhattrangthaipheduyet(_documentid);
                if (_action == "capnhattrangthaituchoi")
                    capnhattrangthaituchoi(_documentid);
                if (_action == "capnhattrangthaidot")
                    capnhattrangthaidot(_dotkt);
            }
            if (!string.IsNullOrEmpty(_documentid))
                _status = CommonFunc.GetDocStatus(_documentid);
            #endregion

            #region init form
            string caption = "Thêm thông tin phản hồi";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Sửa thông tin phản hồi";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnSave.Attributes.Add("onclick", "{saveNoCheck('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            // _btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updateNoCheck('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            //if (!string.IsNullOrEmpty(_role))
            //{
            //    if (_role.Equals("td"))
            //    {
            //        trDaNhanXet.Visible = false;
            //        trLichSu.Visible = false;
            //    }
            //}
            if (_cv == "nguoiduyet")
            {
                _btnDelete.Visible = false;
            }
            else if (_cv == "nguoithuchien")
            {
                if (_status_phanhoi == 4 || _status_phanhoi == 16)
                    _btnDelete.Visible = false;
            }

            #endregion
        }
        public string UpdateNoiDung(string noidung)
        {
            CommonFunc.UpdateDocNameDescription(_documentid, "", noidung);
            return "";
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
                bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
                DataSet dsDotInfo = new DataSet();
                if (!string.IsNullOrEmpty(_documentid))
                    dsDotInfo = doanKiemToan.GetDotInfoByPhanHoi(_documentid);
                else
                    dsDotInfo = doanKiemToan.GetDotInfoByPhatHien(_phathienid);

                if (isValidDataSet(dsDotInfo))
                {
                    txtDotKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["dot_kiem_toan"].ToString();
                    txtDoiTuongKiemToan.Text = dsDotInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
                    txtCongViec.Text = dsDotInfo.Tables[0].Rows[0]["ten_cong_viec"].ToString();
                    _phathienid = dsDotInfo.Tables[0].Rows[0]["phathienid"].ToString();
                    string propPhatHien = string.Empty;
                    string DocType = CommonFunc.GetDocType(_phathienid).ToUpper();
                    if (DocType.Equals(DOCTYPE.PHATHIEN_HETHONG))
                        propPhatHien = "E48E3FFD-FE56-4B14-AC36-2C036873E1CD";
                    else
                        propPhatHien = "D5637BAF-54F3-4724-9E3B-8543EB93509A";
                    txtPhatHien.Text = CommonFunc.getPropertyValueOnDocument(dsDotInfo.Tables[0].Rows[0]["phathienid"].ToString(), propPhatHien);
                    if (dsDotInfo.Tables[0].Columns.Contains("NoiDung"))
                        DOCDESCRIPTION.Text = dsDotInfo.Tables[0].Rows[0]["NoiDung"].ToString();
                }

                BuildTrangThai();
                try
                {
                    if (!string.IsNullOrEmpty(_action))
                    {
                        if (_action == "loaddoc")
                        {
                            //_btnDelete.Visible = false;
                            //truong hop la add
                            if (_viewtype == VIEWTYPE.ADDNEW)
                            {

                            }
                            else if (_viewtype == VIEWTYPE.EDIT)
                            {
                                if (_status_phanhoi == 4 || _status_phanhoi == 16)
                                {
                                    _btnEdit.Visible = false;
                                    CommonFunc.SetEnableControl(false, Page.Master);
                                }
                                if (_cv == "nguoithuchien")// hien thi edit,hoanthanh
                                {
                                    if (_status == 0 || _status == 32)//draft va rejected
                                    {
                                        _btnFinish.Visible = true;
                                        _btnFinish.Text = "Hoàn tất";
                                        _btnFinish.Attributes.Add("onclick", "{capnhattrangthaidone('" + _documentid + "'); return false;}");
                                    }
                                }
                                else//hoanthanh va tuchoi
                                {
                                    CommonFunc.SetEnableControl(false, Page.Master);
                                    _btnEdit.Visible = false;

                                    _btnFinish.Text = "Phê duyệt";
                                    _btnFinish.CssClass = _btnSave.CssClass;
                                    _btnFinish.Attributes.Add("onclick", "{capnhattrangthaipheduyet('" + _documentid + "'); return false;}");

                                    _btnRemove.Text = "Từ chối";
                                    _btnRemove.Attributes.Add("onclick", "{capnhattrangthaituchoi('" + _documentid + "'); return false;}");

                                    _btnFinish.Visible = _btnRemove.Visible = false;
                                    if (_type == "nguoiduyet1")
                                        if (_status == 2) _btnFinish.Visible = _btnRemove.Visible = true;
                                        //else
                                        //    trNhanXet.Visible = false;
                                    if (_type == "nguoiduyet2")
                                        if (_status == 4) _btnFinish.Visible = _btnRemove.Visible = true;
                                        //else
                                        //    trNhanXet.Visible = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { }
                //grid tu choi
                bus_Document docTuChoi = new bus_Document(_objUserContext);
                DataSet dsTuchoi = docTuChoi.getByID(_documentid, "NAME");
                if (isValidDataSet(dsTuchoi))
                {
                    string jsonTuChoi = dsTuchoi.Tables[0].Rows[0]["NAME"].ToString();
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
                                string nguoiduyetX = CommonFunc.CheckNguoiDuyetOfCongViec(_congviec_docid, nhanxet.NguoiNhanXet);
                                if (nguoiduyetX.Equals("nguoiduyet1"))
                                    DaNhanXetNguoiDuyet1.Text = nhanxet.LyDo;
                                //else
                                //    DaNhanXetNguoiDuyet2.Text = nhanxet.LyDo;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
        }

        void BuildTrangThai()
        {
            CommonFunc.LoadDropDownList(this.DOCSTATUS, 6);
            //this.DOCSTATUS.ClearSelection();
            //this.DOCSTATUS.Items.FindByValue(_status.ToString()).Selected = true;
        }

        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label FileID = (Label)e.Item.FindControl("FileID") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                if (FileID != null)
                    imgDelete.Attributes.Add("onclick", "{DeleteFile('" + FileID.Text + "')}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentid"></param>
        public void LoadDocInfo(string documentid, MasterPage Master)
        {
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getByID(documentid, "");
            string sDocTypeID = string.Empty;
            if (isValidDataSet(ds))
            {
                DOCDESCRIPTION.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion


        #region capnhattrangthai
        void capnhattrangthaidone(string phanhoiID)
        {
            if (string.IsNullOrEmpty(phanhoiID))
                return;
            CommonFunc.UpdateDocStatus(phanhoiID, 2);//done
        }

        void capnhattrangthaipheduyet(string phanhoiID)
        {
            if (string.IsNullOrEmpty(phanhoiID))
                return;
            if (_cv == "nguoiduyet")
            {
                if (_type == "nguoiduyet1")
                {
                    CommonFunc.UpdateDocStatus(phanhoiID, 4);//A1
                    
                }
                if (_type == "nguoiduyet2")
                    CommonFunc.UpdateDocStatus(phanhoiID, 16);//A2
                //cập nhật nhận xét
                AddNhanXet(phanhoiID, "PheDuyet");
            }
        }

        void capnhattrangthaituchoi(string phanhoiID)
        {
            if (string.IsNullOrEmpty(phanhoiID))
                return;
            //CommonFunc.UpdateDocStatus(phanhoiID, 0);//draft
            CommonFunc.UpdateDocStatus(phanhoiID, 32);//rejected
            //cập nhật lý do từ chối
            AddNhanXet(phanhoiID, "TuChoi");
        }
        void capnhattrangthaidot(string DotKiemToanID)
        {
            //thangma: update Trang thai Dot kiem toan 
            //chuyển trạng thái đợt lên: Cập nhật phản hồi
            _status_dotkt = CommonFunc.GetDocStatus(DotKiemToanID);
            //if (_status_dotkt < 25)
            //    CommonFunc.UpdateDocStatus(_dotkt, 25);
            TrangThaiDotKiemToan.SetStatus(_dotkt, 25);
            //end
        }
        public void AddNhanXet(string phanhoiID, string hanhDong)
        {
            string nhanXetValue = _nhanxet.Trim();
            if (nhanXetValue.Length > 0)
            {
                bus_Document docPhanhoi = new bus_Document(_objUserContext);
                DataSet dsPhanHoi = docPhanhoi.getByID(phanhoiID, "NAME,DESCRIPTION");
                if (isValidDataSet(dsPhanHoi))
                {
                    string jsonNhanXet = dsPhanHoi.Tables[0].Rows[0]["NAME"].ToString();
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
                    CommonFunc.UpdateDocNameDescription(phanhoiID, jsonNhanXet, dsPhanHoi.Tables[0].Rows[0]["DESCRIPTION"].ToString());
                }
            }
        }
        #endregion
    }
}