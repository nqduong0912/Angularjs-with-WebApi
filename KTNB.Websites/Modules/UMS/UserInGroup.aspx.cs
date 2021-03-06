using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.C1WebGrid;
using CORE.CoreObjectContext;
using CORE.UMS.CoreBusiness;
using vpb.app.business.ktnb.Definition.UMS;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.UMS
{
    public partial class UMS_UserInGroup : PageBase
    {
        #region initiation page variables
        protected string _viewid = string.Empty;
        protected byte _viewtype = 0;
        protected string _documentid = string.Empty;
        protected string _groupid = string.Empty;
        protected string _groupname = string.Empty;
        protected string _roleid = string.Empty;
        protected string _docspace = string.Empty;
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
            _roleid = ((Role)_objUserContext.Roles[0]).RoleID;

            #region get data submit
            
            _groupid = Request["groupid"];
            _groupname = Request["name"];
            _docspace = Request["docspace"];
            if (string.IsNullOrEmpty(_groupid))
                _groupid = _m_groupid;
            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm(_groupname, "group.gif", _viewid, _viewtype);
            //if (ctxIsRole(ROLES.LANHDAO_TTD))
            //{
            //    chkSearchALL.Visible = true;
            //    AddNew.Visible = false;
            //}
            #endregion

            #region client control event handler
            AddNew.Attributes.Add("onclick", "{newuser(); return false;}");
            dataCtrl.PageSize = 200;
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
                ObjectDataSource1.SelectParameters["groupid"].DefaultValue = _groupid;
                ObjectDataSource1.SelectParameters["loginname"].DefaultValue = string.Empty;
                ObjectDataSource1.SelectParameters["searchall"].DefaultValue = false.ToString();
                ObjectDataSource1.SelectParameters["docspace"].DefaultValue = _docspace;

                if (_m_roleID == vpb.app.business.ktnb.Definition.UMS.ROLES.TRUONGPHONG_CSCC)
                    this.SearchBar.Visible = false;
            }
            if (_docspace == "sysuser")
            {
                this.chkSearchALL.Visible = false;
            }
        }
        #endregion

        #region page helper processing
        protected override void dataCtrl_OnItemDataBound(object sender, C1ItemEventArgs e)
        {
            C1ListItemType elemType = e.Item.ItemType;
            if ((elemType == C1ListItemType.AlternatingItem) || (elemType == C1ListItemType.Item))
            {
                Label PK_UserID = (Label)e.Item.FindControl("PK_UserID") as Label;
                Label UserName = (Label)e.Item.FindControl("UserName") as Label;
                Label lblStatus = (Label)e.Item.FindControl("lblStatus") as Label;
                Image imgStatus = (Image)e.Item.FindControl("imgStatus") as Image;

                if (lblStatus.Text == "0")
                    imgStatus.ImageUrl = "~/Images/unlock.png";
                else
                    imgStatus.ImageUrl = "~/Images/lock.png";

                UserName.ForeColor = System.Drawing.Color.Blue;
                string url = "userinfo.aspx?id=" + PK_UserID.Text + "&name=" + UserName.Text + "&docspace=" + _docspace;
                e.Item.Attributes.Add("ondblclick", "{popup('" + url + "');}");
            }
        }
        #endregion

        #region page button processing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DoSearch(object sender, EventArgs e)
        {
            bool TimToanCuc = this.chkSearchALL.Checked;
            string LoginName = this.txtLoginName.Text;

            if (TimToanCuc)
            {
                ObjectDataSource1.SelectParameters["groupid"].DefaultValue = _groupid; // "a50bf9f7-8c06-4965-9585-f2b832d58a0a";
                ObjectDataSource1.SelectParameters["loginname"].DefaultValue = LoginName;
                ObjectDataSource1.SelectParameters["searchall"].DefaultValue = TimToanCuc.ToString();
                ObjectDataSource1.SelectParameters["docspace"].DefaultValue = _docspace;
            }
            else
            {
                ObjectDataSource1.SelectParameters["groupid"].DefaultValue = _groupid;
                ObjectDataSource1.SelectParameters["loginname"].DefaultValue = LoginName;
                ObjectDataSource1.SelectParameters["docspace"].DefaultValue = _docspace;
            }
            dataCtrl.DataBind();
        }
        #endregion
    }
}