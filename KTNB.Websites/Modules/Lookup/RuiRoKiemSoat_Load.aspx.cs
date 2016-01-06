using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.UMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class RuiRoKiemSoat_Load : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.RUIRO_KIEMSOAT;
        protected string _doctypeid_kiemsoat = DOCTYPE.KIEMSOAT;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _ruirokiemsoat = string.Empty;
        protected string _muctieukiemsoat = string.Empty;


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
            _ruirokiemsoat = Request["ruirokiemsoat"];
            _muctieukiemsoat = Request["muctieukiemsoat"];

            #endregion

            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                //if (_action == "checkvalue")
                //    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                //if (_action == "checkvalueupdate")
                //    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue,_documentid).ToString());
                if (_action == "checkvalue")
                    FeedBackClient(AddDoc(_propertyvalue, _muctieukiemsoat));
                if (_action == "checkvalueupdate")
                    FeedBackClient(UpdateDoc(_propertyvalue, _muctieukiemsoat, _documentid));


            }
            #endregion

            #region init form
            string caption = "Thêm mới rủi ro kiểm soát";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin rủi ro kiểm soát";
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                CommonFunc.GetLookUpValue("236D789E-8EE2-4ACC-B960-1C47144ACF90", this.ID8_236D789E_8EE2_4ACC_B960_1C47144ACF90, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        GetList(_doctypeid_kiemsoat);
                        _btnDelete.Visible = false;
                    }
            }
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

        private void GetList(string DocumentTypeID_muctieukiemsoat)
        {
            string DocFields = "PK_DocumentID,[Tên kiểm soát],[Tên rủi ro],[Diễn giải],Status";
            string PropertyFields = "Tên kiểm soát,Tên rủi ro,Diễn giải";
            string Condition = " And [Tên rủi ro]= N'" + this.ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6.Text + "'";//" Order By [Loại đối tượng kiểm toán]";
            //bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            //DataSet ds = obj.getDocumentList(DocumentTypeID_muctieukiemsoat, DocFields, PropertyFields, Condition);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Clone();
            //    DataRow[] drResults = ds.Tables[0].Select("[Tên rủi ro]='" + this.ID8_33686451_E1A7_4F11_9D8D_A0D697D0D5B6.Text + "'");
            //    foreach (DataRow dr in drResults)
            //    {
            //        object[] row = dr.ItemArray;
            //        dt.Rows.Add(row);
            //    }
            //    dataCtrl.DataSource = dt;
            //    dataCtrl.DataBind();
            //}

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID_muctieukiemsoat;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }

        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
        #region sua khi add,update
        public string AddDoc(string tenruiro, string muctieukiemsoat)
        {
            string data = "0";
            string DocFields = "PK_DocumentID,[Mục tiêu kiểm soát],[Tên rủi ro kiểm soát],Status";
            string PropertyFields = "Mục tiêu kiểm soát,Tên rủi ro kiểm soát";
            string Condition = " And [Mục tiêu kiểm soát]=N'" + muctieukiemsoat + "' And [Tên rủi ro kiểm soát]=N'" + tenruiro + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.RUIRO_KIEMSOAT, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                data = "1";
                return data;
            }

            return data;
        }

        public string UpdateDoc(string tenruiro, string muctieukiemsoat, string docID)
        {
            string data = "0";
            string DocFields = "PK_DocumentID,[Mục tiêu kiểm soát],[Tên rủi ro kiểm soát],Status";
            string PropertyFields = "Mục tiêu kiểm soát,Tên rủi ro kiểm soát";
            string Condition = " And [Mục tiêu kiểm soát]=N'" + muctieukiemsoat + "' And PK_DocumentID !='" + docID + "'";
            bus_Document doc = new bus_Document(_objUserContext);
            DataSet ds = doc.getDocumentList(DOCTYPE.RUIRO_KIEMSOAT, DocFields, PropertyFields, Condition);
            if (isValidDataSet(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["Tên rủi ro kiểm soát"].ToString() == tenruiro)
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