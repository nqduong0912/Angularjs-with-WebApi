using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using System.Data;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class MangNghiepVu_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.MANG_NGHIEPVU;
        protected string _doctypeid_muctieukiemsoat = DOCTYPE.MUCTIEU_KIEMSOAT;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        //protected string _property_loaidoituong = string.Empty;
        //protected string _propertyvalue_loaidoituong = string.Empty;
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
            //_property_loaidoituong = Request["ploaidoituong"];
            //_propertyvalue_loaidoituong = Request["vloaidoituong"];
            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                //if (_action == "checkvalue")
                //{
                //    //check trung nhau tren cap mang nghiep vu va loai doi tuong
                //    FeedBackClient(checkDuplicate("insert"));
                //}
                //if (_action == "checkvalueupdate")
                //{
                //    //check trung nhau tren cap mang nghiep vu va loai doi tuong
                //    FeedBackClient(checkDuplicate("update"));
                //}
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                if (_action == "checkvalueupdate")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue, _documentid).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới mảng nghiệp vụ";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin mảng nghiệp vụ";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deletedocument('" + _documentid + "',delete_success,delete_error); return false;}");
            //trLoaiDoiTuong.Visible = false;

            #endregion
        }
        //public string checkDuplicate(string InsertOrUpdate)
        //{
        //    //check trung nhau tren cap tieu chi va nhom tieu chi
        //    string DocFields = "PK_DocumentID,[Tên mảng nghiệp vụ],[Loại đối tượng kiểm toán]";
        //    string PropertyFields = "Tên mảng nghiệp vụ,Loại đối tượng kiểm toán";
        //    string Condition = string.Empty;
        //    string result = string.Empty;
        //    bus_Document obj = bus_Document.Instance(_objUserContext);
        //    DataSet dsMangNghiepVu = obj.getDocumentList(DOCTYPE.MANG_NGHIEPVU, DocFields, PropertyFields, Condition);
        //    if (isValidDataSet(dsMangNghiepVu))
        //    {
        //        DataTable dtMangNghiepVu = dsMangNghiepVu.Tables[0];
        //        DataRow[] foundRows;
        //        string filterCondition = "[Tên mảng nghiệp vụ] = '" + _propertyvalue +
        //            "' and [Loại đối tượng kiểm toán] = '" + _propertyvalue_loaidoituong + "'";
        //        if (InsertOrUpdate.Equals("update"))
        //        {
        //            filterCondition += " and PK_DocumentID <> '" + _documentid + "'";
        //        }
        //        // Use the Select method to find all rows matching the filter.
        //        foundRows = dtMangNghiepVu.Select(filterCondition);
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                //CommonFunc.GetLookUpValue("B65E8D96-F253-40AB-A645-D210E09DA504", this.ID8_B65E8D96_F253_40AB_A645_D210E09DA504, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        GetList(_doctypeid_muctieukiemsoat);
                        _btnDelete.Visible = false;
                    }
            }
        }
        private void GetList(string DocumentTypeID_MucTieuKiemSoat)
        {
            string DocFields = "PK_DocumentID,Status,[Mảng nghiệp vụ],[Tên Mục tiêu kiểm soát],[Diễn giải]";
            string PropertyFields = "Mảng nghiệp vụ,Tên Mục tiêu kiểm soát,Diễn giải";
            string Condition = " And [Mảng nghiệp vụ] = N'" + this.ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F.Text + "'";//" Order By [Loại đối tượng kiểm toán]";
            //string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _documentid + "')";

            //bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = obj.getDocumentList(DocumentTypeID_MucTieuKiemSoat, DocFields, PropertyFields, Condition);

            //if (isValidDataSet(ds))
            //{
            //    //DataRow[] rows = ds.Tables[0].Select("[Mục tiêu kiểm soát]='" + _muctieukiemsoat + "'");
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Clone();
            //    DataRow[] drResults = ds.Tables[0].Select("[Mảng nghiệp vụ]='" + this.ID8_144DFC1C_5D45_4FE7_8772_CD573CEFD04F.Text + "'");
            //    foreach (DataRow dr in drResults)
            //    {
            //        object[] row = dr.ItemArray;
            //        dt.Rows.Add(row);
            //    }
            //    dataCtrl.DataSource = dt;
            //    dataCtrl.DataBind();
            //}

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_MucTieuKiemSoat;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();

        }
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                if (PK_DocumentID != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                        Status.Text = "Active";
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