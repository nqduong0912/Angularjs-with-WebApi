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
    public partial class ThongTinDoanKT : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOAN_KIEMTOAN;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;

        protected string _doclink = string.Empty;
        protected string _doctypeid_doituongkt = DOCTYPE.DOITUONG_KT;

        protected string _truongdoan = String.Empty;
        protected string _dotkt = String.Empty;
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
            _truongdoan = Request["truongdoan"];
            _dotkt = Request["dotkt"];
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
            }
            #endregion

            #region init form
            string caption = "Thông tin đoàn kiểm toán";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin đoàn kiểm toán";
            base.InitForm(caption, string.Empty, _doctypeid, 0);
            #endregion

            #region client control event handler
            //_btnSave.Visible = _btnEdit.Visible = _btnDelete.Visible = false;
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            //_btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
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
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        //CommonFunc.LoadDocInfo(_dotkt,Page.Master);
                        this.DOCNAME.Text = _truongdoan;
                        GetListThanhVienDoanKiemToan(_documentid);
                    }
                    
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetListThanhVienDoanKiemToan(string doankt)
        {
            if (String.IsNullOrEmpty(doankt))
                return;
            //bus_Doankiemtoan doankiemtoan = bus_Doankiemtoan.Instance(_objUserContext);
            //DataSet ds = doankiemtoan.DanhSachThanhVienDoanKT(doankt);
            //dataCtrl.DataSource = ds;
            //dataCtrl.DataBind();
            ObjectDataSource1.SelectParameters["DoanKT"].DefaultValue = doankt;
            dataCtrl.DataBind();
        }

        #endregion


    

        #region page helper processing

        #endregion


        #region page button processing
        #endregion
    }
}