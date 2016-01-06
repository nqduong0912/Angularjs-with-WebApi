using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.Definition.DMS;
using C1.Web.C1WebGrid;
using System.Data;
using vpb.app.business.ktnb.CoreBusiness;
using CORE.CoreObjectContext;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.Lookup.DanhMuc.KeHoachNam
{
    public partial class TanSuat : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _doctypeid = DOCTYPE.TAN_SUAT;
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

            base.InitForm("Danh mục tần suất", string.Empty, _doctypeid, 0);
            _btnAddNew.Visible = true;
            _btnAddNew.Attributes.Add("onclick", "{newform(); return false;}");
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            GetList(_doctypeid);

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList(string DocumentTypeID)
        {
            string DocFields = "PK_DocumentID,Status,[Tên tần suất],[Thời gian tương ứng]";
            string PropertyFields = "Tên tần suất,Thời gian tương ứng";
            string Condition = " Order by [Thời gian tương ứng]";

            bus_Document obj = bus_Document.Instance((UserContext)System.Web.HttpContext.Current.Session["objUserContext"]);
            DataSet ds = obj.getDocumentList(_doctypeid, DocFields, PropertyFields, Condition);

            dataCtrl.DataSource = ds;
            dataCtrl.DataBind();
        }

        protected void dataCtrl_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                Literal thoigian = (Literal)e.Item.FindControl("lbThoiGian");
                DataRowView drv = e.Item.DataItem as DataRowView;
                thoigian.Text = Math.Round(Convert.ToDecimal(drv.Row["Thời gian tương ứng"]), 0).ToString() + " tháng";
            }
        }
    }
}
