using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc
{
    public partial class PhanLoaiBoTieuChi_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.QUANLY_PHANLOAI_BOTIEUCHI;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _year = string.Empty;
        protected string _count = string.Empty;
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
            _count = Request["count"];
            _valueactive = Request["valueactive"];
            
            #endregion

            if (string.IsNullOrEmpty(_documentid))
            {
                _viewtype = VIEWTYPE.ADDNEW;
                this.DOCSTATUS.Enabled = false;
            }
            else
            {
                _viewtype = VIEWTYPE.EDIT;
                this.ID8_51CB5CBB_9D95_423C_BF08_0FA673E6CD34.Enabled = false;
            }

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.countLoaiTC(_property, _year).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.setInactiveLoaiTC(_documentid, _year).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới phân loại bộ tiêu chí";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin phân loại bộ tiêu chí";
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
                this.ID8_DDFF81BA_1A46_426F_85D1_69C68C7F3BA3.Text = _year;
                CommonFunc.LoadStatus(this.DOCSTATUS);
                if (_count != "0") this.DOCSTATUS.SelectedValue = "2";
                CommonFunc.GetLookUpValue("51CB5CBB-9D95-423C-BF08-0FA673E6CD34", this.ID8_51CB5CBB_9D95_423C_BF08_0FA673E6CD34, 0);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                    }
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}