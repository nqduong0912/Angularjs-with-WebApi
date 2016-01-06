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
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class TinhDiemKiemSoat : PageBase
    {
        protected string _action = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _hosoruiroid = string.Empty;
        protected string _dotkt = string.Empty;
        protected string _ruiroid = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected string _value = string.Empty;

        protected string RuiRoConLaiPropertyID = "58307D3D-0507-4D91-8F21-B3AB9BDFC3E4";

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            _action = Request["act"];
            _ruiroid = Request["ruiroid"];
            _hosoruiroid = Request["hosoruiroid"];
            _dotkt = Request["dotkt"];
            _value = Request["value"];

            #endregion

            //if (string.IsNullOrEmpty(_documentid))
            //    _viewtype = VIEWTYPE.ADDNEW;
            //else
            //    _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "tinhdiem")
                    FeedBackClient(TinhDiem(_value, _hosoruiroid));
            }

            #endregion

            #region init form
            string caption = "Tính điểm kiểm soát";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            //_btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler
            //Truongphong->tao doan kiem toan
            //if (_m_roleID == ROLES.TRUONGPHONG_DONVI_KIEMTOAN)
            //    btnDongy.Visible = btnThem.Visible = true;
            //else
            //    btnDongy.Visible = btnThem.Visible = false;
            //btnThemVDQuanTam.Attributes.Add("onclick", "{ThemVDQuanTam('" + _doctypeid + "'); return false;}");
            //btnThem.Attributes.Add("onclick", "{ThemThanhVien(); return false;}");
            _btnSave.Visible = true;
            _btnSave.Text = "Lưu";
            _btnSave.Attributes.Add("onclick", "{TinhDiem(); return false;}");
            #endregion
        }
        public string xoaChiTietPhanTich(string hoso_chitiet, string nhomkt)
        {
            CommonFunc.RemoveDocLink(hoso_chitiet, nhomkt, _objUserContext);
            return string.Empty;
        }
        public string TinhDiem(string DiemKiemSoat, string hosoruiroid)
        {
            string sResult = CommonFunc.UpdateDocPropertyValue(hosoruiroid, RuiRoConLaiPropertyID, DiemKiemSoat);
            if (sResult.Length == 0)
            {
                //chuyển trạng thái đợt lên: Đánh giá rr còn lại
                //CommonFunc.ChuyenTrangThaiDotKT(_dotkt, 23);
                TrangThaiDotKiemToan.SetStatus(_dotkt, 23);
            }
            return sResult;
        }
        public int GetRRCoHuu(string HoSoRuiRoID)
        {
            string anhhuong = CommonFunc.getPropertyValueOnDocument(HoSoRuiRoID, "72812D71-8A97-4BCA-B495-E0664DC1B6CF");
            string xacsuat = CommonFunc.getPropertyValueOnDocument(HoSoRuiRoID, "26322953-20BB-4D2E-A651-289960B78202");
            if (anhhuong != "" && xacsuat != "")
                return Int32.Parse(xacsuat) * Int32.Parse(anhhuong);
            return 0;
        }
        public string Submit(string nhomkt)
        {
            bus_LapKeHoach lapkehoach = new bus_LapKeHoach(_objUserContext);
            DataSet dsChiTietRuiRo = lapkehoach.ChiTietHoSoPhanTichRuiRo(nhomkt, _objUserContext.UserName);
            if (isValidDataSet(dsChiTietRuiRo))
            {
                foreach (DataRow row in dsChiTietRuiRo.Tables[0].Rows)
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
                BindKiemSoat();
                //GetListHoSoPhanTichSoBo(_nhomkt);
            }

        }

        public void BindKiemSoat()
        {
            //
            string sTenRuiRo = string.Empty;
            sTenRuiRo = CommonFunc.getPropertyValueOnDocument(_ruiroid, "33686451-E1A7-4F11-9D8D-A0D697D0D5B6");
            string _doctypeid_kiemsoat = DOCTYPE.KIEMSOAT;
            string DocFields = "PK_DocumentID,[Tên rủi ro],[Tên kiểm soát]";
            string PropertyFields = "Tên rủi ro,Tên kiểm soát";
            string Condition = " and [Tên rủi ro]=N'" + sTenRuiRo + "'";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet dsKiemSoat = obj.getDocumentList(_doctypeid_kiemsoat, DocFields, PropertyFields, Condition);
            dataCtrl.DataSource = dsKiemSoat.Tables[0];
            dataCtrl.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updatepanel1_OnLoad(object sender, EventArgs e)
        {
            //string HoSoPhanTichSoBoID = hiddenNhomKT.Value;

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
                Label lblKiemSoatID = (Label)e.Item.FindControl("lblKiemSoatID") as Label;

                Label lblFormatRadio = (Label)e.Item.FindControl("lblFormatRadio") as Label;
                lblFormatRadio.Text = "<input type=\"radio\" name=\"" + lblKiemSoatID.Text + "\" value=\"1\">1";
                lblFormatRadio.Text += "<input type=\"radio\" name=\"" + lblKiemSoatID.Text + "\" value=\"2\">2";
                lblFormatRadio.Text += "<input type=\"radio\" name=\"" + lblKiemSoatID.Text + "\" value=\"3\">3";
                lblFormatRadio.Text += "<input type=\"radio\" name=\"" + lblKiemSoatID.Text + "\" value=\"4\">4";
                lblFormatRadio.Text += "<input type=\"radio\" name=\"" + lblKiemSoatID.Text + "\" value=\"5\">5";
                //bool IsSubmitted = false;
                //Image ImgSubmit = (Image)e.Item.FindControl("ImgSubmit") as Image;
                //ImgSubmit.Visible = false;


                //Label hoso = (Label)e.Item.FindControl("hoso") as Label;
                //Label lblCommandXacSuat = (Label)e.Item.FindControl("lblCommandXacSuat") as Label;
                //Label lblCommandAnhHuong = (Label)e.Item.FindControl("lblCommandAnhHuong") as Label;
                //Label lblCommandRRCoHuu = (Label)e.Item.FindControl("lblCommandRRCoHuu") as Label;

                //if (hoso != null)
                //{
                //    if (lblStatus.Text.Equals("4"))
                //    {
                //        ImgSubmit.Visible = true;
                //    }
                //    else
                //    {
                //        lblCommandRRCoHuu.Attributes.Add("onclick", "{LoadDocument('" + hoso.Text + "','ah')}");
                //    }

                //}
                ////TreeView treeViewKiemSoat = (TreeView)e.Item.FindControl("treeViewKiemSoat") as TreeView;
                //TreeView treeViewThuTuc = (TreeView)e.Item.FindControl("treeViewThuTuc") as TreeView;
                //string ruiro = e.Item.Cells[3].Text == null ? String.Empty : e.Item.Cells[3].Text;
                ////if (treeViewKiemSoat != null)
                ////    BuidTreeView(treeViewKiemSoat, ruiro, 0);
            }
        }
    }
}