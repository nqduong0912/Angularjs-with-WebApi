using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.UMS;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using vpb.app.business.ktnb.Definition.OPERATORS;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class KiemSoat_Load :PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.KIEMSOAT;
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
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue,_documentid).ToString());
            }
            #endregion

            #region init form
            string caption = "Thêm mới kiểm soát";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin kiểm soát";
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
                CommonFunc.LoadStatus(this.DOCSTATUS);
                CommonFunc.GetLookUpValue("26CEC2EE-9636-4EFF-A104-8B8189984B7F", this.ID8_26CEC2EE_9636_4EFF_A104_8B8189984B7F, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
                BindThuTucGrid();
            }

        }
        public void BindThuTucGrid()
        {
            //DataSet ds = GetDanhSachThuTuc();
            //if (isValidDataSet(ds))
            //{
            //    DataTable dt = new DataTable();
            //    dt = ds.Tables[0].Clone();
            //    DataRow[] drResults = ds.Tables[0].Select("[Tên kiểm soát]='" + ID8_4138B099_7CBD_443E_A12B_9A1FF5D1E08F.Text + "'");
            //    foreach (DataRow dr in drResults)
            //    {
            //        object[] row = dr.ItemArray;
            //        dt.Rows.Add(row);
            //    }
            //    dataCtrl.DataSource = dt;
            //    dataCtrl.DataBind();
            //}
            
            string DocFields = "PK_DocumentID,Status,[Tên kiểm soát],[Tên thủ tục kiểm toán],[Diễn giải]";
            string PropertyFields = "Tên kiểm soát,Tên thủ tục kiểm toán,Diễn giải";
            string Condition = " And [Tên kiểm soát] = N'" + ID8_4138B099_7CBD_443E_A12B_9A1FF5D1E08F.Text + "'";//" and Status=" + STATUS.ACTIVE.ToString();
            
            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DOCTYPE.THUTUC_KIEMTOAN;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
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
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_DocumentID") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                if(PK_DocumentID != null && imgEdit != null)
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                if (Status != null)
                {
                    if (Status.Text == "2")
                        Status.Text = "Inactive";
                    else if (Status.Text == "4")
                        Status.Text = "Active";
                }
            }
        }

        public DataSet GetDanhSachThuTuc()
        {
            string DocFields = "PK_DocumentID,Status,[Tên kiểm soát],[Tên thủ tục kiểm toán],[Diễn giải]";
            string PropertyFields = "Tên kiểm soát,Tên thủ tục kiểm toán,Diễn giải";
            string Condition = String.Empty;//" and Status=" + STATUS.ACTIVE.ToString();
            //Condition += " And 

            bus_Document obj = bus_Document.Instance(_objUserContext);
            DataSet ds = obj.getDocumentList(DOCTYPE.THUTUC_KIEMTOAN, DocFields, PropertyFields, Condition);
            obj = null;
            return ds;
        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}