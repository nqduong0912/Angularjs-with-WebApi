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
using KTNB.Extended.Dal;
using KTNB.Extended.Biz;
using KTNB.Extended.Core;
using C1.Web.C1WebGrid;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam
{
    public partial class QuanLyBoTieuChiKeHoachNam_ThemTCC : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_CHINH;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _year = string.Empty;
        protected string _loaiDTKT = string.Empty;
        protected string _boTCN = string.Empty;
        protected string _valueactive = string.Empty;
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
            _year = Request["y"];
            _loaiDTKT = Request["loaiDT"];
            _boTCN = Request["btc"];
            _valueactive = Request["valueactive"];

            #endregion

            if (string.IsNullOrEmpty(_documentid))
            {
                _viewtype = VIEWTYPE.ADDNEW;
            }
            else
            {
                _viewtype = VIEWTYPE.EDIT;
            }

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(insert(int.Parse(_year), _loaiDTKT, _boTCN).ToString());
                //if (_action == "checkvalueupdate")
                //    FeedBackClient(CommonFunc.setInactiveLoaiTC(_documentid, _year).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
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
                //this.txtNgay.Text = _year;
                //this.txtNgay.Text = DateTime.Now.ToString("d/M/yyyy");
                //CommonFunc.GetLookUpValue("BA3A0E4C-33BC-475B-B547-4580CD602D68", this.ID8_BA3A0E4C_33BC_475B_B547_4580CD602D68, 4);
                //CommonFunc.GetLookUpValue("FE3C31CC-4A33-4712-86B4-A037D68173E7", this.ID8_FE3C31CC_4A33_4712_86B4_A037D68173E7, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                    }
                Binddrp(drpLoaiDTKT, DOCTYPE.LOAI_DOITUONG_KT, "Tên loại đối tượng kiểm toán");
                //Binddrp(drpBoTieuChi, DOCTYPE.CACBOTIEUCHI_NAM, "Tên bộ tiêu chí năm");
                List<String> listYears = MiscUtils.GetAllYear();
                drpYears.DataSource = listYears;
                drpYears.DataBind();
                //_btnEdit.Visible = false;
                //GetList(_doctypeid);
                tbBoTieuChi.Text = _boTCN;
            }
        }
        #endregion
        //private void GetList(string DocumentTypeID)
        //{
        //    string DocFields = "PK_DocumentID,Status,[Tên],[Tỷ trọng]";
        //    string PropertyFields = "Tên,Tỷ trọng";
        //    string Condition = string.Empty;

        //    bus_Document obj = bus_Document.Instance(_objUserContext);
        //    DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
        //    obj = null;

        //    dsTCC.DataSource = ds.Tables[0];
        //    dsTCC.DataBind();
        //}

        public void Binddrp(DropDownList drp, String doctypeid, String docfield)
        {
            string DocFields = "PK_DocumentID,Status,[" + docfield + "]";
            string PropertyFields = docfield;
            string Condition = " and Status= 4 order by [" + docfield + "]";
            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(doctypeid, DocFields, PropertyFields, Condition);
            obj = null;

            drp.DataSource = ds.Tables[0];
            drp.DataTextField = docfield;
            drp.DataValueField = docfield;
            drp.DataBind();
        }

        protected int insert(int nam, String loaiDTKT, String tenBTC)
        {
            List<qt_qlybotieuchi> lst = ManagerFactory.qlybotieuchi_manager.GetList(x => x.Nam == nam && x.LoaiDTKT == loaiDTKT && x.BoTieuChiNam == tenBTC);
            if (lst.Count > 0) return 1;
            qt_qlybotieuchi qlyBoTC = new qt_qlybotieuchi();
            qlyBoTC.Nam = nam;
            qlyBoTC.LoaiDTKT = loaiDTKT;
            qlyBoTC.BoTieuChiNam = tenBTC;
            qlyBoTC.isActive = false;
            qlyBoTC.TrangThai = 1;
            qlyBoTC.NgayCapNhat = DateTime.Now;
            ManagerFactory.qlybotieuchi_manager.Insert(qlyBoTC);
            return 0;
        }

        protected void dsTCC_ItemDataBound(object sender, C1.Web.C1WebGrid.C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;
                Label TrongSo = (Label)e.Item.FindControl("TyTrong") as Label;
                DataRowView drv = e.Item.DataItem as DataRowView;
                TrongSo.Text = Math.Round(Convert.ToDecimal(drv.Row["Tỷ trọng"]), 0).ToString();
                if (PK_DocumentID != null)
                {
                    imgDelete.Attributes.Add("onclick", "{DeleteDocument('" + PK_DocumentID.Text + "')}");

                }
            }
        }

        protected void btnCong_Click(object sender, EventArgs e)
        {

        }

        protected void btnTru_Click(object sender, EventArgs e)
        {

        }

        protected void btnNhan_Click(object sender, EventArgs e)
        {

        }

        protected void btnChia_Click(object sender, EventArgs e)
        {

        }

        protected void btnMoNgoac_Click(object sender, EventArgs e)
        {

        }

        protected void btnDongNgoac_Click(object sender, EventArgs e)
        {

        }

        protected void btnLamLai_Click(object sender, EventArgs e)
        {

        }

        protected void drpLoaiTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = (DropDownList)sender;
            string selected = (string)drp.SelectedValue;
            
            if (selected.Equals("1"))
            {
                this.pnTest.Visible = true;
            }
            else this.pnTest.Visible = false;
        }
    }
}