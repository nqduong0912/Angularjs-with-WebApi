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
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class MucTieuKiemSoat_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.MUCTIEU_KIEMSOAT;
        protected string _doctypeid_ruiro = DOCTYPE.RUIRO_KIEMSOAT;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;

        protected string _muctieukiemsoat = string.Empty;
        protected string _mangnghiepvu = string.Empty;
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
            _muctieukiemsoat = Request["muctieukiemsoat"];
            _mangnghiepvu = Request["mangnghiepvu"];

            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    //FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                    FeedBackClient(AddDoc(_propertyvalue, _mangnghiepvu));
                if (_action == "checkvalueupdate")
                    //FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue,_documentid).ToString());
                    FeedBackClient(UpdateDoc(_propertyvalue, _mangnghiepvu,_documentid));
            }
            #endregion

            #region init form
            string caption = "Thêm mới mục tiêu kiểm soát";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin mục tiêu";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{prepareupdatedoc('" + _documentid + "'); return false;}");
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
                CommonFunc.GetLookUpValue("DC51C8A3-8E80-4C08-AFFA-B859545B4DCB", this.ID8_DC51C8A3_8E80_4C08_AFFA_B859545B4DCB, 4);
                //CommonFunc.BindUserInRoleToCombo(ID8_A4BC472E_B041_4093_8F53_D46452B1E130, ROLES.CANBO_GSTX);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        GetList(_doctypeid_ruiro);
                        _btnDelete.Visible = false;
                    }
            }
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID_RuiRo)
        {
            string DocFields = "PK_DocumentID,[Tên rủi ro kiểm soát],[Mục tiêu kiểm soát],[Diễn giải],Status";
            string PropertyFields = "Tên rủi ro kiểm soát,Mục tiêu kiểm soát,Diễn giải";
            string Condition =" And [Mục tiêu kiểm soát] = N'" + this.ID8_9D641C2E_99D2_4D3F_9034_116EFAF2C3BC.Text + "'";//" Order By [Loại đối tượng kiểm toán]";

            //bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = obj.getDocumentList(DocumentTypeID_RuiRo, DocFields, PropertyFields, Condition);
            //if (isValidDataSet(ds))
            //{
            //    //DataRow[] rows = ds.Tables[0].Select("[Mục tiêu kiểm soát]='" + _muctieukiemsoat + "'");
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Clone();
            //    DataRow[] drResults = ds.Tables[0].Select("[Mục tiêu kiểm soát]='" + this.ID8_9D641C2E_99D2_4D3F_9034_116EFAF2C3BC.Text + "'");
            //    foreach(DataRow dr in drResults)
            //    {
            //        object[] row = dr.ItemArray;
            //        dt.Rows.Add(row);
            //    } 
            //    dataCtrl.DataSource = dt;
            //    dataCtrl.DataBind();
            //}
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_RuiRo;
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

        #region check

        public string AddDoc(string ten,string mangnghiepvu)
        {
            string data = "0";
            string DocFields = "PK_DocumentID,Status,[Mảng nghiệp vụ],[Tên Mục tiêu kiểm soát],[Diễn giải]";
            string PropertyFields = "Mảng nghiệp vụ,Tên Mục tiêu kiểm soát,Diễn giải";
            string Condition = " And [Mảng nghiệp vụ]=N'" + mangnghiepvu + "' And [Tên Mục tiêu kiểm soát]=N'"+ten+"'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCTIEU_KIEMSOAT, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                data = "1";
                return data;
            }

            return data;
        }

        public string UpdateDoc(string ten,string mangnghiepvu,string docID)
        {
            string data = "0";
            string DocFields = "PK_DocumentID,Status,[Mảng nghiệp vụ],[Tên Mục tiêu kiểm soát],[Diễn giải]";
            string PropertyFields = "Mảng nghiệp vụ,Tên Mục tiêu kiểm soát,Diễn giải";
            string Condition = " And [Mảng nghiệp vụ]=N'" + mangnghiepvu + "' And PK_DocumentID !='" + docID + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.MUCTIEU_KIEMSOAT, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Tên Mục tiêu kiểm soát"].ToString() == ten)
                    {
                        data = "1";
                        return data;
                    }
                }
                data = "0";
                return data;
            }
            return data;
        }

        #endregion
    }
}