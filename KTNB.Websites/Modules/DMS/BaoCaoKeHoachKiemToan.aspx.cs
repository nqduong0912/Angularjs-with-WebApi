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
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class BaoCaoKeHoachKiemToan : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _nhomkt = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _hosoChiTiet = string.Empty;

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
            }

            #endregion

            #region init form
            string caption = "Báo cáo kế hoạch kiểm toán";
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

            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
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
            }
            return string.Empty;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDotKiemToanInfo();
                GetListHoSoPhanTichSoBo(_nhomkt);
                //bus_LapKeHoach lapKeHoach = new bus_LapKeHoach(_objUserContext);
                //DataSet dsThuTucByMangNghiepVu = lapKeHoach.GetThuTucByMangNghiepVu("22cc8d92-7597-4148-8f32-4e4704caf5be");
                //bus_Document objDoc = bus_Document.Instance(_objUserContext);
                //objDoc.
            }

        }
        public void LoadDotKiemToanInfo()
        {
            bus_Doankiemtoan doanKiemToan = bus_Doankiemtoan.Instance(_objUserContext);
            DataSet ds = doanKiemToan.GetDotKTByNhomKT(_nhomkt);
            if (isValidDataSet(ds))
            {
                txtDoiTuongKiemToan.Text = ds.Tables[0].Rows[0]["doi_tuong_kiem_toan"].ToString(); //CommonFunc.getPropertyValueOnDocument(_dotkt, doiTuongKiemToan_propID);
                txtDotKiemToan.Text = ds.Tables[0].Rows[0]["ten_dot_kiem_toan"].ToString();
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
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.ChiTietHoSoPhanTichSoBo(nhomKT);
            if (isValidDataSet(ds))
            {
                
            }
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
                ImgSubmit.Visible = false;
                IsSubmitted = lapKeHoach.IsPhanTichSoBoSubmitted(sTenMangNghiepVu, _nhomkt);
                if (IsSubmitted)
                    ImgSubmit.Visible = true;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;

                if (hoso != null)
                {
                    imgDelete.Attributes.Add("onclick", "{deleteChiTietPhanTich('" + hoso.Text + "')}");
                }
            }
        }
    }
}