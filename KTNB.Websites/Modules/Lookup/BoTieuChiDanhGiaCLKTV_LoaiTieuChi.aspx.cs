using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.OPERATORS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.DMS;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.Lookup
{
    public partial class BoTieuChiDanhGiaCLKTV_LoaiTieuChi : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.BOTIEUCHI_DANHGIA_CLKTV;
        protected string _loaitieuchi_doctypeid = DOCTYPE.LOAI_TIEUCHI_DANHGIA_CHATLUONG_KTV;
        protected byte _viewtype = 0;
        protected string _botieuchi_documentid = string.Empty;
        protected string _docspaceid = string.Empty;
        protected string _property = string.Empty;
        protected string _propertyvalue = string.Empty;
        protected string _loaitieuchi = string.Empty;
        protected string _tytrong = string.Empty;
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
            _botieuchi_documentid = Request["doc"];
            _loaitieuchi = Request["loaitieuchi"];
            _tytrong = Request["tytrong"];
            _action = Request["act"];
            _property = Request["p"];
            _propertyvalue = Request["v"];
            #endregion

            if (string.IsNullOrEmpty(_botieuchi_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region action handler
            if (!string.IsNullOrEmpty(_action))
            {
                if (_action == "checkvalue")
                    FeedBackClient(CommonFunc.CountPropertyValue(_property, _propertyvalue).ToString());
                else if (_action == "bosungloaitieuchi")
                    FeedBackClient(bosungLoaiTieuChi(_botieuchi_documentid, _loaitieuchi,_tytrong));
                else if (_action == "xoaloaitieuchi")
                    FeedBackClient(xoaLoaiTieuChi(_botieuchi_documentid, _loaitieuchi));
            }
            #endregion

            #region init form
            string caption = "Thêm các tiêu chí đánh giá cho bộ tiêu chí";
            if (!string.IsNullOrEmpty(_botieuchi_documentid))
                caption = "Thông tin chi tiết các tiêu chí đánh giá";
            base.InitForm(caption, string.Empty, _doctypeid, _viewtype);
            #endregion

            #region client control event handler
            _btnSave.Attributes.Add("onclick", "{preparesavedoc('" + System.Guid.NewGuid().ToString() + "','" + _doctypeid + "'); return false;}");
            _btnEdit.Attributes.Add("onclick", "{updatedocument('" + _botieuchi_documentid + "',update_success,update_error); return false;}");
            _btnDelete.Attributes.Add("onclick", "{deleteTieuChi('" + _botieuchi_documentid + "',delete_success,delete_error,'Bạn chắc chắn xoá ?'); return false;}");
            //btnAddTieuChi.Attributes.Add("onclick", "{addTieuChi('" + _documentid + "','" + TYPE_OF_LINK.DOCUMENT + "'); return false;}");
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
                //CommonFunc.GetLookUpValue("FB2F72B5-27DC-458B-BB16-D7DC09E4C7B2", this.ID8_FB2F72B5_27DC_458B_BB16_D7DC09E4C7B2, 4);
                //CommonFunc.BindUserInRoleToCombo(ID8_A4BC472E_B041_4093_8F53_D46452B1E130, ROLES.CANBO_GSTX);
                BindDropDownLoaiTieuChi();
                GetListLoaiTieuChiByBoTieuChi(DOCTYPE.LOAI_TIEUCHI_DANHGIA_CHATLUONG_KTV);
                if (!string.IsNullOrEmpty(_action))
                    if (_action == "loaddoc")
                    {
                        CommonFunc.LoadDocInfo(_botieuchi_documentid, Page.Master);
                        _btnDelete.Visible = false;

                    }

            }
            _btnSave.Visible = false;
            _btnEdit.Visible = false;
            _btnDelete.Visible = false;
        }
        #endregion

        #region page helper processing

        public void BindDropDownLoaiTieuChi()
        {
            DropDownLoaiTieuChi.DataSource = GetDanhSachLoaiTieuChi().Tables[0];
            DropDownLoaiTieuChi.DataTextField = "Tên loại tiêu chí đánh giá";
            DropDownLoaiTieuChi.DataValueField = "PK_DocumentID";
            DropDownLoaiTieuChi.DataBind();

            //DropDownTieuChi.Items.Add(new ListItem("key","value"));
        }
        public DataSet GetDanhSachLoaiTieuChi()
        {
            string DocFields = "PK_DocumentID,Status,[Tên loại tiêu chí đánh giá]";
            string PropertyFields = "Tên loại tiêu chí đánh giá";
            string Condition = " and Status=" + STATUS.ACTIVE.ToString();
            Condition += " And PK_DocumentID Not In (Select FK_DocumentID From T_DOCLINK Where  FK_DocLinkID='" + _botieuchi_documentid + "')";

            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_loaitieuchi_doctypeid, DocFields, PropertyFields, Condition);
            obj = null;
            return ds;

            
        }
        #endregion

        protected void btnAddTieuChi_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="botieuchi"></param>
        /// <param name="loaitieuchi"></param>
        /// <returns></returns>
        /// <auth>thangma</auth>
        protected string bosungLoaiTieuChi(string botieuchi, string loaitieuchi,string tytrong)
        {
            CommonFunc.AddDocLink(loaitieuchi, botieuchi, TYPE_OF_LINK.DOCUMENT, _objUserContext,tytrong,"");
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="botieuchi"></param>
        /// <param name="loaitieuchi"></param>
        /// <returns></returns>
        /// <auth>thangma</auth>
        protected string xoaLoaiTieuChi(string botieuchi, string loaitieuchi)
        {
            CommonFunc.RemoveDocLink(loaitieuchi, botieuchi, _objUserContext);
            return string.Empty;
        }
        private void GetListLoaiTieuChiByBoTieuChi(string DocumentTypeID)
        {
            //string DocFields = "PK_DocumentID,Status,[Tên loại tiêu chí đánh giá]";
            //string PropertyFields = "Tên loại tiêu chí đánh giá";
            //string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + _botieuchi_documentid + "')";

            //ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            //ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            //ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            //ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            //dataCtrl.DataBind();
            bus_BoTieuChiDanhGiaCLKTV BoTieuChiDanhGiaCLKTV = bus_BoTieuChiDanhGiaCLKTV.Instance(_objUserContext);
            DataSet ds = BoTieuChiDanhGiaCLKTV.DanhSachLoaiTieuChi(_botieuchi_documentid);
            dataCtrl.DataSource = ds.Tables[0];
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
                Label LoaiTieuChiID = (Label)e.Item.FindControl("LoaiTieuChiID") as Label;

                Image imgDelete = (Image)e.Item.FindControl("imgDelete") as Image;

                if (LoaiTieuChiID != null)
                {
                    imgDelete.Attributes.Add("onclick", "{deleteLoaiTieuChi('" + LoaiTieuChiID.Text + "')}");
                }

                //if (Status != null)
                //{
                //    if (Status.Text == "2")
                //        Status.Text = "Inactive";
                //    else if (Status.Text == "4")
                //        Status.Text = "Active";
                //}
            }
        }

        protected void DropDownLoaiTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindDrop
        }

        #region page button processing

        #endregion
    }
}