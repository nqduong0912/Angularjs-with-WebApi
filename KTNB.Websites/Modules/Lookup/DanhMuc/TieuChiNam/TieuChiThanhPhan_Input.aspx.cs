using KTNB.Extended.Biz;
using KTNB.Extended.Dal.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KTNB.Extended.Commons;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class TieuChiThanhPhan_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_THANHPHAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _tytrong = string.Empty;
        protected string _diengiai = string.Empty;
        protected string _valueactive = string.Empty;
        protected string _tcc = string.Empty;

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
            _diengiai = Request["diengiai"];
            _tytrong = Request["tytrong"];
            _valueactive = Request["valueactive"];
            _tcc = Request["tcc"];
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
            }
            #endregion

            #region init form
            string caption = "Thêm mới tiêu chí thành phần";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin tiêu chí thành phần";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
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
                CommonFunc.LoadStatus(this.DOCSTATUS, "4");
                CommonFunc.GetLookUpValue("D51797C1-4558-4237-A184-610F7C10D1C4", this.ID9_D51797C1_4558_4237_A184_610F7C10D1C4, 4);
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadStatus(this.DOCSTATUS);
                        //CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        LoadTieuchithanhphanInfo();
                    }
                    else
                        CommonFunc.LoadStatus(this.DOCSTATUS, "4");
                }
                else
                    CommonFunc.LoadStatus(this.DOCSTATUS, "4");

                string year = Request.QueryString["y"] ?? MiscUtils.CurrentYear;
                lblYear.Text = year;
                string ldtkt = Request.QueryString["l"] ?? "";
                lblLDTKT.Text = HttpUtility.UrlDecode(ldtkt);
                //Nếu là trang THêm mới, get dữ liệu bộ tiêu chí
                if (string.IsNullOrEmpty(_documentid))
                {
                    string _botieuchiID = Request.QueryString["tcc"] ?? "";
                    if (string.IsNullOrEmpty(_botieuchiID))
                    {
                        //redirect về trang thiết lập bộ tiêu chí cho kế hoạch năm
                    }
                    else
                    {
                        dm_li_kehoachnam_tieuchichinh info = CoreFactory<dm_li_kehoachnam_tieuchichinh>.EntityManager.GetInfo(x => x.PK_DocumentID == new Guid(_botieuchiID));
                        txtBotieuchi.Text = info.Botieuchi;
                        ID9_26C864F5_7ED3_44ED_AB0B_3C99EF198EFB.Value = info._Botieuchi.ToString();
                        txtTieuchichinh.Text = info.Ten;
                        ID9_2B01A211_9DDA_4F5F_A252_0751ED63D2B3.Value = info.PK_DocumentID.ToString();
                    }
                }
            }
        }
        #endregion

        private void LoadTieuchithanhphanInfo()
        {
            if (!string.IsNullOrEmpty(_documentid))
            {
                dm_li_kehoachnam_tieuchitp info = CoreFactory<dm_li_kehoachnam_tieuchitp>.EntityManager.GetInfo(x => x.PK_DocumentID == new Guid(_documentid));
                if (info != null)
                {
                    txtBotieuchi.Text = info.Botieuchi;
                    ID9_26C864F5_7ED3_44ED_AB0B_3C99EF198EFB.Value = info._Botieuchi.ToString();
                    txtTieuchichinh.Text = info.Tieuchichinh;
                    ID9_2B01A211_9DDA_4F5F_A252_0751ED63D2B3.Value = info._Tieuchichinh.ToString();
                    ID8_23FDFFE4_8F68_437E_B777_7DC2B88B9EB6.Text = info.Ten;
                    ID8_3031A7F5_9793_45AB_897E_AC05C60B299A.Text = info.Diengiai;
                    ID6_F5D740A0_342D_40D4_96EA_74C5DB35AE46.Text = info.Tytrong.ToString();
                    DOCSTATUS.SelectedValue = info.Status.ToString();
                    ID6_359B4356_DA02_4D60_870D_44508E638682.SelectedValue = info.Loaitieuchi.ToString();
                    ID9_D51797C1_4558_4237_A184_610F7C10D1C4.SelectedValue = info._Loaidinhtinh.ToString();
                    ID8_56B0851F_537B_4281_AD36_95909AB7FD67.Value = info.Loaidinhluong;
                }
            }
        }
    }
}