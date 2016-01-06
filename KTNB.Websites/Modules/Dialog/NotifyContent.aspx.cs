using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VPB_PROMOTION.Dialog
{
    public partial class Modules_Dialog_NotifyContent : System.Web.UI.Page
    {
        #region initiation page variables
        private string _message = string.Empty;
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            #region get data submit
            _message = Request["msg"];
            if (!string.IsNullOrEmpty(_message))
            {
                _message = _message.Replace("*", "<br/>");
                this.lblContent.Text = _message;
            }
            #endregion

            #region action handler

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

        }
        #endregion

        #region page helper processing

        #endregion

        #region page button processing

        #endregion
    }
}