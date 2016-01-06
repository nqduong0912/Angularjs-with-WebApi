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
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class TieuChiDanhGiaXacSuatAnhHuong_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TIEUCHI_DANHGIA_XACSUAT_ANHHUONG;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        //protected string _property_nhomtieuchi = string.Empty;
        //protected string _propertyvalue_nhomtieuchi = string.Empty;
        protected string _tentieuchi = string.Empty;
        protected string _nhomtieuchi = string.Empty;

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
            //_property_nhomtieuchi = Request["pnhomtieuchi"];
            //_propertyvalue_nhomtieuchi = Request["vnhomtieuchi"];
            _tentieuchi = Request["tentieuchi"];
            _nhomtieuchi = Request["nhomtieuchi"];
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                {
                    //check trung nhau tren cap tieu chi va nhom tieu chi
                    //FeedBackClient(checkDuplicate("insert"));
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                }

                if (_action == "checkvalueupdate")
                {
                    //check trung nhau tren cap tieu chi va nhom tieu chi
                    //FeedBackClient(checkDuplicate("update"));
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
                }

                if (_action == "adddoc")
                {
                    FeedBackClient(AddDoc(_nhomtieuchi, _tentieuchi));
                }
                if (_action == "updatedoc")
                {
                    FeedBackClient(UpdateDoc(_nhomtieuchi, _documentid, _tentieuchi));
                }

            }
            #endregion

            #region init form
            string caption = "Thêm mới tiêu chí đánh giá ảnh hưởng xác suất/ảnh hưởng";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin tiêu chí đánh giá ảnh hưởng xác suất/ảnh hưởng";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            //_btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
            _btnSave.Attributes.Add("onclick", "{adddoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedoc('" + _documentid + "'); return false;}");

            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            #endregion
        }
        //public string checkDuplicate(string InsertOrUpdate)
        //{
        //    //check trung nhau tren cap tieu chi va nhom tieu chi
        //    string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá]";
        //    string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá";
        //    //string Condition = " and PK_DocumentID in ( select FK_DOCUMENTID from T_TYPE_DOC_PROPERTY where FK_PROPERTYID =  'DC51C8A3-8E80-4C08-AFFA-B859545B4DCB' and TEXTVALUE =N'" + sTenMangNghiepVu + "' )";
        //    string Condition = string.Empty;
        //    string result = string.Empty;
        //    bus_Document obj = bus_Document.Instance(_objUserContext);
        //    DataSet dsTieuChiDanhGiaXSAHKS = obj.getDocumentList(DOCTYPE.TIEUCHI_DANHGIA_XACSUAT_ANHHUONG, DocFields, PropertyFields, Condition);
        //    if (isValidDataSet(dsTieuChiDanhGiaXSAHKS))
        //    {
        //        DataTable dtTieuChiDanhGiaXSAHKS = dsTieuChiDanhGiaXSAHKS.Tables[0];
        //        DataRow[] foundRows;
        //        string filterCondition = "[Tên tiêu chí] = '" + _propertyvalue +
        //            "' and [Nhóm tiêu chí đánh giá] = '" + _propertyvalue_nhomtieuchi + "'";
        //        if (InsertOrUpdate.Equals("update"))
        //        {
        //            filterCondition += " and PK_DocumentID <> '" + _documentid + "'";
        //        }
        //        // Use the Select method to find all rows matching the filter.
        //        foundRows = dtTieuChiDanhGiaXSAHKS.Select(filterCondition);
        //        result = foundRows.Count().ToString();
        //    }
        //    return result;
        //}
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CommonFunc.LoadDropDownList(this.ID8_31C0688F_D708_4741_941C_1C031D9B6CAE, 1);
                //CommonFunc.GetLookUpValue("B65E8D96-F253-40AB-A645-D210E09DA504", this.ID8_B65E8D96_F253_40AB_A645_D210E09DA504, 4);
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

        #region check tieuchidanhgia
        //truong hop add
        string AddDoc(string nhomtieuchi,string tentieuchi)
        {
            string IsExist = "0";

            string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá]";
            string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá";
            string Condition = " and [Nhóm tiêu chí đánh giá]=N'" + nhomtieuchi + "'";
            Condition += " and [Tên tiêu chí]=N'" + tentieuchi + "'";

            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.TIEUCHI_DANHGIA_XACSUAT_ANHHUONG, DocFields, PropertyFields, Condition);
            doc = null;

            if(isValidDataSet(ds))
                IsExist = "1";
            return IsExist;
        }


        //truong hop edit
        string UpdateDoc(string nhomtieuchi, string docID, string tentieuchi)
        {
            string fullname = "1";
            string DocFields = "PK_DocumentID,[Tên tiêu chí],[Nhóm tiêu chí đánh giá]";
            string PropertyFields = "Tên tiêu chí,Nhóm tiêu chí đánh giá";
            string Condition = " and [Nhóm tiêu chí đánh giá]=N'" + nhomtieuchi + "' and PK_DocumentID !='" + docID + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.TIEUCHI_DANHGIA_XACSUAT_ANHHUONG, DocFields, PropertyFields, Condition);
            if(isValidDataSet(ds))
            {
                //trung ten theo nhom; 
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Tên tiêu chí"].ToString() == tentieuchi)
                    {
                        fullname = "0";
                        return fullname;
                    }
                }
            }
            return fullname;
        }

        #endregion
    }
}