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
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class LapHoSoPhanTichSoBo : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doankt = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _hosoChiTiet = string.Empty;
        DataSet dsDotKTInfo = new DataSet();
        int _status_dotkt = 0;

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
                //thangma: update Trang thai Dot kiem toan 
                //chuyển trạng thái đợt lên: Phân tích sơ bộ
                else if (_action == "chuyentrangthaidotkt")
                {
                    FeedBackClient(TrangThaiDotKiemToan.SetStatus(_dotkt, 13));
                }
            }

            #endregion

            #region init form
            string caption = "Lập phân tích sơ bộ";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            //_btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler
            //Truongphong->tao doan kiem toan
            //if (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
            //    btnDongy.Visible = btnThem.Visible = true;
            //else
            //    btnDongy.Visible = btnThem.Visible = false;
            btnThemVDQuanTam.Attributes.Add("onclick", "{ThemVDQuanTam('" + _doctypeid + "'); return false;}");
            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            //thangma
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
            //CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            bus_LapKeHoach lapkehoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsChiTietHoSoSoBo = lapkehoach.ChiTietHoSoPhanTichSoBo(nhomkt);
            if (isValidDataSet(dsChiTietHoSoSoBo))
            {
                foreach (DataRow row in dsChiTietHoSoSoBo.Tables[0].Rows)
                {
                    CommonFunc.UpdateDocStatus(row["hoso"].ToString(), 4);
                }
                //thangma: update Trang thai Dot kiem toan 
                //len trang thai phan tich chi tiet
                //_status_dotkt = CommonFunc.GetDocStatus(_dotkt);
                //if (_status_dotkt < 14)
                //    CommonFunc.UpdateDocStatus(_dotkt, 14);
                //end
            }
            return string.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDotKiemToanInfo();
                BindMangNVDropDown();
                BindMucDoRuiRo();
                GetListHoSoPhanTichSoBo(_nhomkt);
            }

        }
        public void LoadDotKiemToanInfo()
        {

            //DataSet dsDotKTInfo = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
            if (isValidDataSet(dsDotKTInfo))
            {
                txtDoiTuongKiemToan.Text = dsDotKTInfo.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString(); //CommonFunc.getPropertyValueOnDocument(_dotkt, doiTuongKiemToan_propID);
                txtDotKiemToan.Text = dsDotKTInfo.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
            }
        }
        public void BindMangNVDropDown()
        {
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doanKiemToan.GetMangNVByNhomKT(_nhomkt);
            if (isValidDataSet(ds))
            {
                DataTable dt = ds.Tables[0].DefaultView.ToTable(true, new string[] { "mangnv", "ten_mang_nghiep_vu" });
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataSource = dt;
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataValueField = "ten_mang_nghiep_vu";//"mangnv";
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataTextField = "ten_mang_nghiep_vu";
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataBind();
            }
        }
        public void BindMucDoRuiRo()
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.DSMucDoRuiRo();
            if (isValidDataSet(ds))
            {
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataSource = ds.Tables[0];
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataValueField = "ten_muc_do_rui_ro"; //"MucDoRuiRoID";
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataTextField = "ten_muc_do_rui_ro";
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataBind();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            //string HoSoPhanTichSoBoID = hiddenNhomKT.Value;
            GetListHoSoPhanTichSoBo(_nhomkt);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetListHoSoPhanTichSoBo(string nhomKT)
        {
            if (String.IsNullOrEmpty(nhomKT))
                return;
            //bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            //DataSet ds = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKT);
            //if (isValidDataSet(ds))
            //{
            //    dataCtrl.DataSource = ds;
            //    dataCtrl.DataBind();
            //}
            ObjectDataSource1.SelectParameters["NhomKiemToan"].DefaultValue = nhomKT;
            dataCtrl.DataBind();

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
                bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);

                Label hoso = (Label)e.Item.FindControl("hoso") as Label;
                string sTenMangNghiepVu = CommonFunc.getPropertyValueOnDocument(hoso.Text, "AC4286FB-5B38-47C4-93F0-E075F171D8FC");
                bool IsSubmitted = false;
                Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                Image ImgDelete = (Image)e.Item.FindControl("ImgDelete") as Image;
                ImgSubmit.Visible = false;
                IsSubmitted = lapKeHoach.IsPhanTichSoBoSubmitted(sTenMangNghiepVu, _nhomkt);
                if (IsSubmitted)
                {
                    ImgSubmit.Visible = true;
                    ImgDelete.Visible = false;
                }

                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;

                if (hoso != null)
                {
                    imgDelete.Attributes.Add("onclick", "{deleteChiTietPhanTich('" + hoso.Text + "')}");
                }
            }
        }

    }
}