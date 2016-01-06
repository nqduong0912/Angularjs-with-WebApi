using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class ChiTietHoSoPhanTichSoBo_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CHITIET_HOSO_PHANTICH_SOBO;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
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
            string caption = "Thêm mới loại đối tượng";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin loại đối tượng";
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
                //CommonFunc.LoadStatus(this.DOCSTATUS);
                //CommonFunc.Load
                BindDropDownMangNghiepVu();
                BindDropDownMucDoRuiRo();
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
            }
        }
        public void BindDropDownMangNghiepVu()
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.DSMangNghiepVuDuocGiaoChoNhom(_objUserContext.UserName);
            if (isValidDataSet(ds))
            {
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataSource = ds.Tables[0];
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataTextField = "ten_nghiep_vu";
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataValueField = "MangNghiepVuID";
                ID8_AC4286FB_5B38_47C4_93F0_E075F171D8FC.DataBind();
            }
            else
                return;
        }
        public void BindDropDownMucDoRuiRo()
        {
            bus_LapKeHoach lapKeHoach = bus_LapKeHoach.Instance(_objUserContext);
            DataSet ds = lapKeHoach.DSMucDoRuiRo();
            if (isValidDataSet(ds))
            {
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataSource = ds.Tables[0];
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataTextField = "ten_muc_do_rui_ro";
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataValueField = "MucDoRuiRoID";
                ID8_05B1B244_C5E3_4199_B523_96A3E131D83D.DataBind();
            }
            else
                return;
        }
        public void BindMucDoRuiRo()
        { 
            
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}