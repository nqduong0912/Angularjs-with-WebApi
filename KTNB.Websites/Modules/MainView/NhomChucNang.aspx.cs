using System;
using System.Data;
using System.Web.UI.WebControls;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.CoreBusiness;
using C1.Web.C1WebGrid;
using vpb.app.business.ktnb.Definition.DMS;
using vpb.app.business.ktnb.Definition.WFS;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_KTNB.Modules.MainView
{
    public partial class NhomChucNang : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
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
            base.InitForm("Nhóm chức năng", string.Empty, string.Empty, 0);
            _btnAddNew.Visible = true;
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
            GetList();
            
        }
        #endregion

        #region page helper processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DocumentTypeID"></param>
        private void GetList()
        {
            bus_Role objRole = bus_Role.Instance(_objUserContext);
            DataSet ds = objRole.getList(" and ISEXPIRED = 0 and ORDERINDEX = 1 ORDER BY NAME", "PK_ROLEID,NAME, DESCRIPTION");
            dataCtrl.DataSource = ds;
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
                Label PK_DocumentID = (Label)e.Item.FindControl("PK_ROLEID") as Label;
                Image imgEdit = (Image)e.Item.FindControl("imgEdit") as Image;
                DataRowView drv = e.Item.DataItem as DataRowView;
                if (PK_DocumentID != null)
                {
                    imgEdit.Attributes.Add("onclick", "{LoadDocument('" + PK_DocumentID.Text + "')}");
                }
            }
        }
        #endregion
    }
}
