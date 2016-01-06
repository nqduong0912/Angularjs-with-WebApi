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
using KTNB.Extended.Dal.Lib;
using KTNB.Extended.Biz;
using KTNB.Extended.Commons;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.TieuChiNam
{
    public partial class TieuChiChinh_Input : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        private const string propertyIdTCC = "2B01A211-9DDA-4F5F-A252-0751ED63D2B3";
        private const string _doctypetctp = DOCTYPE.TIEUCHI_THANHPHAN;
        protected string _doctypeid = DOCTYPE.TIEUCHI_CHINH;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _valueactive = string.Empty;
        protected string _NewDocumentId = string.Empty;
        protected string isUpdate = string.Empty;
        protected string NewDocumentId
        {
            get
            {
                if (!string.IsNullOrEmpty(_documentid))
                    return _documentid;
                if (string.IsNullOrEmpty(_NewDocumentId))
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
            }
            #endregion

            #region init form
            string caption = "Thêm mới tiêu chí chính";
            if (!string.IsNullOrEmpty(_documentid))
                caption = "Thông tin tiêu chí chính";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{adddoc('" + NewDocumentId + "','" + _doctypeid + "'); return false;}");
            //_btnEdit.Attributes.Add("onclick", "{updatedocument('" + _documentid + "',update_success,update_error); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedoc('" + _documentid + "'); return false;}");
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
                //CommonFunc.GetLookUpValue("3952C713-E304-443D-9CC7-C55D51408A5F", this.ID8_3952C713_E304_443D_9CC7_C55D51408A5F, 4);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadStatus(this.DOCSTATUS);                        
                        _btnDelete.Visible = false;
                        //CommonFunc.LoadDocInfo(_documentid, Page.Master);
                        LoadTieuchichinhInfo();
                    }
                    else
                        CommonFunc.LoadStatus(this.DOCSTATUS, "2");
                else
                    CommonFunc.LoadStatus(this.DOCSTATUS, "2");
                string year = Request.QueryString["y"] ?? MiscUtils.CurrentYear;
                lblYear.Text = year;
                string ldtkt = Request.QueryString["l"] ?? "";
                lblLDTKT.Text = HttpUtility.UrlDecode(ldtkt);
                //Nếu là trang THêm mới, get dữ liệu bộ tiêu chí
                if (string.IsNullOrEmpty(_documentid))
                {
                    string _botieuchiID = Request.QueryString["btc"] ?? "";
                    if(string.IsNullOrEmpty(_botieuchiID))
                    {
                        //redirect về trang thiết lập bộ tiêu chí cho kế hoạch năm
                    }
                    else
                    {
                        dm_li_kehoachnam_botieuchi info = CoreFactory<dm_li_kehoachnam_botieuchi>.EntityManager.GetInfo(x => x.PK_DocumentID == new Guid(_botieuchiID));
                        txtBotieuchi.Text = info.Ten;
                        ID9_3952C713_E304_443D_9CC7_C55D51408A5F.Value = info.PK_DocumentID.ToString();
                    }
                }
            }
            GetList(_doctypetctp);
        }
        #endregion
        private void LoadTieuchichinhInfo()
        {
            if (!string.IsNullOrEmpty(_documentid))
            {
                dm_li_kehoachnam_tieuchichinh info = CoreFactory<dm_li_kehoachnam_tieuchichinh>.EntityManager.GetInfo(x => x.PK_DocumentID == new Guid(_documentid));
                if(info != null)
                {
                    txtBotieuchi.Text = info.Botieuchi;
                    ID9_3952C713_E304_443D_9CC7_C55D51408A5F.Value = info._Botieuchi.ToString();
                    ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.Text = info.Ten;
                    ID8_346D63E4_2747_4DF7_81F3_4A95CAEF9E13.Text = info.Diengiai;
                    ID6_2E3ECB61_E8C2_433A_8FF9_8B19BB8204CE.Text = info.Tytrong.ToString();
                    ID8_D2ABD1F4_CE31_4424_93DD_C217ECA2A835.Value = info.Congthuc;
                    DOCSTATUS.SelectedValue = info.Status.ToString();
                }
            }
        }
        private void GetList(string DocumentTypeID)
        {
            if (!string.IsNullOrEmpty(_documentid))
            {
                List<dm_li_kehoachnam_tieuchitp> lst = CoreFactory<dm_li_kehoachnam_tieuchitp>.EntityManager.GetList(x => x.Tieuchichinh == ID8_67E8EBEA_3C55_4EF4_9F4F_90D457DEB495.Text);
                drpTCC.Items.Clear();
                drpTCC.Items.Add(new ListItem("Chọn tiêu chí thành phần", "0"));
                if (lst.Count > 0)
                {
                    rptTCC.DataSource = lst;
                    rptTCC.DataBind();


                    foreach (dm_li_kehoachnam_tieuchitp info in lst.FindAll(x => x.Status == 4))
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
                dm_li_kehoachnam_tieuchitp item = (dm_li_kehoachnam_tieuchitp)e.Item.DataItem;
                Literal Status = (Literal)e.Item.FindControl("Status");
                LinkButton btnActive = (LinkButton)e.Item.FindControl("btnActive");
                LinkButton btnInactive = (LinkButton)e.Item.FindControl("btnInactive");
                Status.Text = item.Status == 4 ? "Active" : "Inactive";

                if (item.Status == 2)
                    btnActive.Visible = true;
                else
                    btnInactive.Visible = true;
            }
        }
        protected void rptTCC_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string documentId = (string)e.CommandArgument;
            int status = 4;
            switch (e.CommandName)
            {
                case "active":
                    status = 4;
                    break;
                case "inactive":
                    status = 2;
                    break;
                default:
                    status = 4;
                    break;
            }
            CommonFunc.UpdateDocStatus(documentId, status);
            GetList(_doctypetctp);
        }
    }
}