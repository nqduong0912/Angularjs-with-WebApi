using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.Modules.UMS
{
    public partial class SendBroadCastMessage : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;

        protected string _viewid = string.Empty;
        protected byte _viewtype = 0;
        
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.AuthorizeUserCtx();
            base.OnInit(e);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnBeforeLoad()
        {
            #region get data submit

            #endregion

            #region checking data

            #endregion

            #region action handler

            #endregion

            #region init form
            base.InitForm("Gửi thông báo BroadCast","",_viewid,_viewtype);
            #endregion

            #region client control event handler

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnAfterLoad()
        {

        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}
