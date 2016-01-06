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
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class PhatHienViPham_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.PHATHIEN_VIPHAM;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _congviec_docid = string.Empty;
        protected string _cv = string.Empty;
        protected string _phathienID = string.Empty;
        protected string _type = string.Empty;
        protected string _timkiem = string.Empty;
        protected string _nhanxet = string.Empty;
        protected string _dotkt = string.Empty;
        int _status = 0;
        int _status_phathien = 0;
        protected bool _isTruongDoan = false;
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
            _congviec_docid = Request["congviec_docid"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            _cv = Request["cv"];
            _phathienID = Request["phathienID"];
            _timkiem = Request["timkiem"];
            _dotkt = Request["dotkt"];
            _timkiem = Request["timkiem"];
            if (_cv == "nguoiduyet")
                _type = CommonFunc.CheckNguoiDuyet(_congviec_docid);
            if (!string.IsNullOrEmpty(_documentid))
                _status_phathien = CommonFunc.GetDocStatus(_documentid);
            if (!string.IsNullOrEmpty(_dotkt))//hien cap nhap truong la truong doan
                _isTruongDoan = CommonFunc.IsTruongDoan(_dotkt, _objUserContext.UserName);
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
                if (_action == "capnhattrangthaidone")
                    FeedBackClient(capnhattrangthaidone(_phathienID));
                if (_action == "capnhattrangthaipheduyet")
                    capnhattrangthaipheduyet(_phathienID);
                if (_action == "capnhattrangthaituchoi")
                    capnhattrangthaituchoi(_phathienID);
            }
            if (!string.IsNullOrEmpty(_documentid))
                _status = CommonFunc.GetDocStatus(_documentid);
            #endregion

            #region init form
            string caption = "Phê duyệt phát hiện";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Phê duyệt phát hiện";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnSave.Attributes.Add("onclick", "{saveNoCheck('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            // _btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updateNoCheck('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
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
                bus_Doankiemtoan doanKiemToan = new bus_Doankiemtoan(_objUserContext);
                DataSet dsDotInfo = new DataSet();
                if (!string.IsNullOrEmpty(_documentid))
                    dsDotInfo = doanKiemToan.GetDotInfoByPhatHien(_documentid);
                else
                    dsDotInfo = doanKiemToan.GetDotInfoByCongViec(_congviec_docid);
                if (isValidDataSet(dsDotInfo))
                {
                    ID8_1C98F56A_9CFE_410E_882C_7AB370D99A78.Text = dsDotInfo.Tables[0].Rows[0]["dot_kiem_toan"].ToString();
                    ID8_7222867E_EC9C_46FF_BC71_20873B7EBD37.Text = dsDotInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString();
                    txtCongViec.Text = dsDotInfo.Tables[0].Rows[0]["ten_cong_viec"].ToString();
                }
                CommonFunc.LoadDropDownList(this.ID8_A4C22F5B_2271_46D8_AD8D_B88B7399317A, 5);
                try
                {
                    BuildTrangThai();
                    if (!string.IsNullOrEmpty(_action))
                        if (_action == "loaddoc")
                        {
                            CommonFunc.LoadDocInfo(_documentid, Page.Master);
                            _btnDelete.Visible = false;
                            if (_viewtype == VIEWTYPE.EDIT)
                            {
                                if (_status_phathien == 4 || _status_phathien == 16)//ap1,ap2 thi an capnhat
                                {
                                    CommonFunc.SetEnableControl(false, Page.Master); _btnEdit.Visible = false;
                                }
                                if (_cv == "nguoithuchien")// hien thi edit,hoanthanh
                                {
                                    if (_status == 0 || _status == 32)//draft, rejected
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
                                    _btnFinish.Visible = true;
                                    _btnFinish.Text = "Phê duyệt";
                                    _btnFinish.Attributes.Add("onclick", "{capnhattrangthaipheduyet('" + _documentid + "'); return false;}");
                                    _btnFinish.CssClass = _btnSave.CssClass;
                                    _btnRemove.Text = "Từ chối";
                                    _btnRemove.Attributes.Add("onclick", "{capnhattrangthaituchoi('" + _documentid + "'); return false;}");
                                    _btnRemove.Visible = true;
                                    if (_type == "nguoiduyet1")
                                        if (_status == 4 || _status == 16)
                                        {
                                            _btnFinish.Visible = _btnRemove.Visible = false; 
                                            //trNhanXet.Visible = false;
                                        }
                                    if (_type == "nguoiduyet2")
                                        if (_status == 16)
                                        {
                                            _btnFinish.Visible = _btnRemove.Visible = false; 
                                            //trNhanXet.Visible = false;
                                        }
                                    if (!String.IsNullOrEmpty(_timkiem))
                                        if (_timkiem == "tk")
                                        {
                                            CommonFunc.SetVisiableControl(false, Page.Master, Page);
                                            if (_isTruongDoan) { CommonFunc.SetVisiableControl(true, Page.Master, Page); _btnEdit.Visible = true; }
                                        }
                                }
                            }
                        }
                }
                catch (Exception ex) { }
                //grid tu choi
                string jsonTuChoi = CommonFunc.getPropertyValueOnDocument(_documentid, "75AF3ED2-78F6-4DAE-BB20-157920BAC865");
                if (jsonTuChoi.Length > 0)
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
            }
        }



        void BuildTrangThai()
        {
            CommonFunc.LoadDropDownList(this.DOCSTATUS, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentid"></param>
        public void LoadDocInfo(string documentid, MasterPage Master)
        {
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.loadDocumentInfo(documentid);
            obj = null;
            Control frmWsp = Master.FindControl("frmWsp");
            Control FormContent = frmWsp.FindControl("FormContent");
            foreach (Control ctl in FormContent.Controls)
            {
                string PropertyID = ctl.ID;
                if (string.IsNullOrEmpty(PropertyID))
                    continue;

                #region docname & desc & docstatus
                if ("DOCNAME".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((TextBox)ctl).Text = ds.Tables[0].Rows[0]["DocName"].ToString();
                else if ("DOCDESCRIPTION".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((TextBox)ctl).Text = ds.Tables[0].Rows[0]["DocDescription"].ToString();
                else if ("DOCSTATUS".LastIndexOf(PropertyID.ToUpper()) != -1)
                    ((DropDownList)ctl).SelectedValue = ds.Tables[0].Rows[0]["DocStatus"].ToString();
                #endregion

                #region Properties
                if (PropertyID.Length == 40)
                {
                    string stype = StringHelper.Left(PropertyID, 3);
                    PropertyID = StringHelper.Right(PropertyID, 36).Replace("_", "-");
                    DataRow[] R = ds.Tables[0].Select("FK_PropertyID='" + PropertyID + "'");

                    if (R.Length == 0) continue;

                    if (stype == "ID8")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(DropDownList))
                            ((DropDownList)ctl).SelectedValue = R[0]["TextValue"].ToString();
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = R[0]["TextValue"].ToString();
                    }
                    else if (stype == "ID6")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = R[0]["NumericValue"].ToString();
                        else if (ctl.GetType() == typeof(C1WebNumericEdit))
                            ((C1WebNumericEdit)ctl).Text = Math.Round(Convert.ToDecimal(R[0]["NumericValue"]), 0).ToString();
                    }
                    else if (stype == "ID3")
                    {
                        if (ctl.GetType() == typeof(TextBox))
                            ((TextBox)ctl).Text = String.Format("{0:dd/MM/yyyy}", R[0]["DateTimeValue"]);
                    }
                }
                #endregion
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion


        #region capnhattrangthai
        public string  capnhattrangthaidone(string phathienID)
        {
            string data = "0";
            if (string.IsNullOrEmpty(phathienID))
            {
                data = "-1";
                return data;
            }
          
            //kiem tra xem phanhoi da done het chua
            if (CheckAllPhanHoiIsDoned(phathienID) == false)
            {
                data = "1";
                return data;
            }

            CommonFunc.UpdateDocStatus(phathienID, 2);//done
            data = "0";
            return data;
        }

        void capnhattrangthaipheduyet(string phathienID)
        {
            if (string.IsNullOrEmpty(phathienID))
                return;
            if (_cv == "nguoiduyet")
                if (_type == "nguoiduyet1")
                    CommonFunc.UpdateDocStatus(phathienID, 4);//A1
            if (_type == "nguoiduyet2")
                CommonFunc.UpdateDocStatus(phathienID, 16);//A2
            //thangma
            //cập nhật nhận xét
            AddNhanXet(phathienID, "PheDuyet");
        }

        void capnhattrangthaituchoi(string phathienID)
        {
            if (string.IsNullOrEmpty(phathienID))
                return;
            //CommonFunc.UpdateDocStatus(phathienID, 0);//draft
            CommonFunc.UpdateDocStatus(phathienID, 32);//Rejected
            //thangma
            //cập nhật lý do từ chối
            AddNhanXet(phathienID, "TuChoi");
        }
        public void AddNhanXet(string phathienID, string hanhDong)
        {
           //cập nhật lý do từ chối
            string nhanXetValue = _nhanxet.Trim();
            if (nhanXetValue.Length > 0)
            {
                string jsonNhanXet = CommonFunc.getPropertyValueOnDocument(phathienID, "75AF3ED2-78F6-4DAE-BB20-157920BAC865");
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
                CommonFunc.UpdateDocPropertyValue(phathienID, "75AF3ED2-78F6-4DAE-BB20-157920BAC865", jsonNhanXet);
            }
        }
        #endregion

        #region kiem tra phanhoi da het done chua
        bool CheckAllPhanHoiIsDoned(string phathienID)
        {
            DataSet ds = DataSource.getListPhanHoi(phathienID);
            if (isValidDataSet(ds) == false)
                return true;
            DataRow[] rows = ds.Tables[0].Select(" Status >= 2 And Status <= 16");
            DataRowCollection rows_All = ds.Tables[0].Rows;
            if (rows.Count() == rows_All.Count)
                return true;
            return false;
        }


        #endregion
    }
}