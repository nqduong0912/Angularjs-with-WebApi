using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class BoTieuChiDanhGiaCLKTV_Load :PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.BOTIEUCHI_DANHGIA_CLKTV;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _tenbotieuchi = string.Empty;
        protected string _loaitieuchi = string.Empty;
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
            _tenbotieuchi = Request["tenbotieuchi"];
            _loaitieuchi = Request["loaitieuchi"];
            _valueactive = Request["valueactive"];
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
                if (_action == "adddoc")
                    FeedBackClient(AddDoc(_tenbotieuchi,_loaitieuchi,_valueactive));
                if (_action == "updatedoc")
                    FeedBackClient(UpdateDoc(_tenbotieuchi, _loaitieuchi, _valueactive));
            }
            #endregion

            #region init form
            string caption = "Thêm mới bộ tiêu chí đánh giá";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin bộ tiêu chí đánh giá";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");

            _btnSave.Attributes.Add("onclick", "{adddoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedoc('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error,'Bạn chắc chắn xoá ?'); return false;}");
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                CommonFunc.GetLookUpValue("FB2F72B5-27DC-458B-BB16-D7DC09E4C7B2", this.ID8_FB2F72B5_27DC_458B_BB16_D7DC09E4C7B2, 4);
                //CommonFunc.BindUserInRoleToCombo(ID8_A4BC472E_B041_4093_8F53_D46452B1E130, ROLES.CANBO_GSTX);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion

        #region check
        public string AddDoc(string tenbotieuchi, string loaitieuchi, string valueactive)
        {
            string data = "0";
            if (valueactive == "2")//truong hop deactive thi bo qua
            {
                data = CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString();
                if (data != "0")
                {
                    data = "1";
                }
                return data;
            }
           
            data = CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString();
            //kiem tra trung ten;
            if (data != "0")
            {
                data = "1";//trungten
                return data;
            }
            //kiem tra xem tai 1 thoi diem co dc active?
            string DocFields = "PK_DocumentID,Status,[Phân loại bộ tiêu chí],[Tên bộ tiêu chí đánh giá],[Diễn giải]";
            string PropertyFields = "Phân loại bộ tiêu chí,Tên bộ tiêu chí đánh giá,Diễn giải";
            string Condition = " and [Phân loại bộ tiêu chí] =N'"+loaitieuchi+"'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.BOTIEUCHI_DANHGIA_CLKTV, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                DataRow[] rows = ds.Tables[0].Select(" Status ='4'");
                if (rows.Length > 0)
                {
                    data = "2";
                    return data;
                }
            }
            return data;
        }

        public string UpdateDoc(string tenbotieuchi, string loaitieuchi, string valueactive)
        {
            string data = "0";
            if (valueactive == "2")//truong hop deactive thi bo qua
            {
                data = CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString();
                if (data != "0")
                {
                    data = "1";
                }
                return data;
            }

            data = CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString();
            //kiem tra trung ten;
            if (data != "0")
            {
                data = "1";//trungten
                return data;
            }
            //kiem tra xem tai 1 thoi diem co dc active?
            string DocFields = "PK_DocumentID,Status,[Phân loại bộ tiêu chí],[Tên bộ tiêu chí đánh giá],[Diễn giải]";
            string PropertyFields = "Phân loại bộ tiêu chí,Tên bộ tiêu chí đánh giá,Diễn giải";
            string Condition = " and [Phân loại bộ tiêu chí] =N'" + loaitieuchi + "' and PK_DocumentID != '" + _documentid + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.BOTIEUCHI_DANHGIA_CLKTV, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                DataRow[] rows = ds.Tables[0].Select(" Status ='4'");
                if (rows.Length > 0)
                {
                    data = "2";
                    return data;
                }
            }
            return data;
        }

        #endregion
    }
}