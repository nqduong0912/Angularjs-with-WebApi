using vpb.app.business.ktnb.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Data;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons;
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Dal;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class BoTieuChiNam_Input : PageBase
    {
        #region initiation page variables
        private const string propertyIdTCN = "3952C713-E304-443D-9CC7-C55D51408A5F";
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.CACBOTIEUCHI_NAM;
        protected string _doctypetcc = DOCTYPE.TIEUCHI_CHINH;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _NewDocumentId = string.Empty;
        protected string isUpdate = string.Empty;
        protected string NewDocumentId
        {
            get
            {
                if (!string.IsNullOrEmpty(_documentid))
                    return _documentid;
                if(string.IsNullOrEmpty(_NewDocumentId))
                    _NewDocumentId = System.Guid.NewGuid().ToString();
                return _NewDocumentId;
            }
        }
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
            string caption = "Thêm mới bộ tiêu chí năm";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin bộ tiêu chí năm";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{adddoc('" + NewDocumentId + "','" + _doctypeid + "'); return false;}");
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
                
                if (!string.IsNullOrEmpty(_action))
                {
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadStatus(this.DOCSTATUS);
                        CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        _btnDelete.Visible = false;
                    }
                    else
                        CommonFunc.LoadStatus(this.DOCSTATUS, "2");
                }
                else
                    CommonFunc.LoadStatus(this.DOCSTATUS, "2");
                

                string year = Request.QueryString["y"] ?? MiscUtils.CurrentYear;
                CommonFunc.GetYear2Dropdownlist(drpYears, year);
                string ldtkt = Request.QueryString["l"] ?? "";
                List<dm_loaidoituongkiemtoan> lst = CoreFactory<dm_loaidoituongkiemtoan>.EntityManager.GetList(x => x.Nam == Int32.Parse(year));
                foreach (var info in lst)
                {
                    drpLoaidoituongkiemtoan.Items.Add(new ListItem(info.Ten, info.SourceId.ToString()));
                }
                drpLoaidoituongkiemtoan.SelectedValue = ldtkt;
            }
            GetList(_doctypetcc);            
        }
        #endregion      
      
        private void GetList(string DocumentTypeID)
        {
            if (!string.IsNullOrEmpty(_documentid))
            {
                List<dm_li_kehoachnam_tieuchichinh> lst = CoreFactory<dm_li_kehoachnam_tieuchichinh>.EntityManager.GetList(x => x.Botieuchi == ID8_078EF917_FC13_41CC_88CB_EFC5B8C47773.Text);
                drpTCC.Items.Clear();
                drpTCC.Items.Add(new ListItem("Chọn tiêu chí chính", "0"));
                if (lst.Count > 0)
                {
                    rptTCC.DataSource = lst;
                    rptTCC.DataBind();

                    
                    foreach (dm_li_kehoachnam_tieuchichinh info in lst.FindAll(x => x.Status == 4))
                    {
                        drpTCC.Items.Add(new ListItem(info.Ten, info.Tytrong.ToString()));
                    }
                }
            }
            else
                isUpdate = " dontshow";
        }
        protected void rptTCC_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                dm_li_kehoachnam_tieuchichinh item = (dm_li_kehoachnam_tieuchichinh)e.Item.DataItem;
                Literal Status = (Literal)e.Item.FindControl("Status");
                LinkButton btnInactive = (LinkButton)e.Item.FindControl("btnInactive");
                Status.Text = item.Status == 4 ? "Active" : "Inactive";
                if (item.Status == 4)
                    btnInactive.Visible = true;
            }
        }
        protected void rptTCC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string documentId = (string)e.CommandArgument;
            int status = 4;
            switch(e.CommandName)
            {                
                case "inactive":
                    status = 2;
                    break;
                default:
                    status = 2;
                    break;
            }
            CommonFunc.UpdateDocStatus(documentId, status);
            GetList(_doctypetcc);        
        }

    }
}