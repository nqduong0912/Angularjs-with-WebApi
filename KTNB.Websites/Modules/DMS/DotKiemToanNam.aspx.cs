using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using CORE.CoreObjectContext;
using KTNB.Extended.Commons;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class DotKiemToanNam : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.DOT_KIEMTOAN;
        protected string _doctypeid_doankiemtoan = DOCTYPE.DOAN_KIEMTOAN;

        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _docspaceid = string.Empty;
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
            _documentid = Request["doc"];
            if (string.IsNullOrEmpty(_documentid))
                _viewtype = VIEWTYPE.ADDNEW;
            else
                _viewtype = VIEWTYPE.EDIT;

            #region get data submit
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Thông tin đợt kiểm toán trong năm", string.Empty, _doctypeid, 0);
            if(_m_roleID == ROLES.TRUONGPHONG_CSCC)
                _btnAddNew.Visible = true;
            else
                _btnAddNew.Visible = false;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetList(_doctypeid, MiscUtils.CurrentYear);

        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID, string nam)
        {
            //string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán],[Phạm vi],[Mục tiêu]";
            //string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán,Phạm vi,Mục tiêu";
            string DocFields = "PK_DocumentID,[Tên đợt kiểm toán],[Đối tượng kiểm toán],[Quy mô đợt kiểm toán],[Đơn vị thực hiện],[Năm],[Thời gian dự kiến kiểm toán],[Đơn vị thực hiện],[Loại đối tượng kiểm toán],Status";
            string PropertyFields = "Tên đợt kiểm toán,Đối tượng kiểm toán,Quy mô đợt kiểm toán,Đơn vị thực hiện,Năm,Thời gian dự kiến kiểm toán,Đơn vị thực hiện,Loại đối tượng kiểm toán";

            string Condition = string.Format(" and [Năm] = '{0}' Order By [Năm],[Loại đối tượng kiểm toán]", nam);

            ObjectDataSource1.SelectParameters["DocumentTypeID"].DefaultValue = DocumentTypeID;
            ObjectDataSource1.SelectParameters["DocFields"].DefaultValue = DocFields;
            ObjectDataSource1.SelectParameters["PropertyFields"].DefaultValue = PropertyFields;
            ObjectDataSource1.SelectParameters["Condition"].DefaultValue = Condition;
            dataCtrl.DataBind();
        }
        #endregion

        #region page button processing
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
                //Label Persons = (Label)e.Item.FindControl("Persons") as Label;              
                
                Label Status = (Label)e.Item.FindControl("Status") as Label;
                if (Status != null)
                    SetStatus(Status, Status.Text);
            }
        }

        private void GetCountDoanKiemToan(string DocumentTypeID,string dotkt,Label lbl)
        {
            string DocFields = "PK_DocumentID,[Name]";
            string PropertyFields = "Name";
            string Condition = " and PK_DocumentID in (Select FK_documentID From T_DocLink Where FK_DocLinkID='" + dotkt + "')";
            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(DocumentTypeID, DocFields, PropertyFields, Condition);
             //int i = obj.getCountOnDocumentLink(Condition);
            int i = 0;
            if (ds != null && ds.Tables.Count > 0)
                lbl.Text = ds.Tables[0].Rows.Count.ToString();
        }


        void SetStatus(Label lblStatus, string text)
        {
            string result = String.Empty;
            if (string.IsNullOrEmpty(text))
                result = String.Empty;
            result = CommonFunc.GetTrangThaiDotKT(Int32.Parse(text));
            lblStatus.Text = result;

        }
        #endregion

        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetList(_doctypeid, drpYear.SelectedValue);
        }
    }
}