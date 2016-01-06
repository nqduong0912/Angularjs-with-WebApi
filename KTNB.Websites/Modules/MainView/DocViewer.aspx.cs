using System;

namespace VPB_PROMOTION.MainView
{
    public partial class MainView_DocViewer : System.Web.UI.Page
    {
        #region initiation page variables
        protected string _doctype = string.Empty;
        protected string _wfd = string.Empty;
        protected string _url = string.Empty;
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _doctype = Request["doctype"];
            _wfd = Request["wfd"];
            if (!string.IsNullOrEmpty(_url)) Server.Transfer(_url);
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region page helper processing
        #endregion
    }
}