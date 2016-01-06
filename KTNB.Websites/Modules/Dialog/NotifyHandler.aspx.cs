using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using System.Data;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.Dialog
{
    public partial class Modules_Dialog_NotifyHandler : PageBase
    {
        System.Text.StringBuilder _script = new System.Text.StringBuilder();
        private string _url = string.Empty;
        private string _title = string.Empty;
        private int _width = 300;
        private int _height = 150;
        string _yourmessage = string.Empty;

        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "myjs", this.ResolveUrl("~/Javascript/Popup/scrolling_popup.js"));

            _yourmessage = GetYourMessage();
            _title = "Thông báo - Công việc khác";
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region popup
            if (!string.IsNullOrEmpty(_yourmessage))
            {
                _url = "NotifyContent.aspx?msg=" + _yourmessage;
                popup(_url, _width, _height, _title);
            }

            #endregion
        }
        /// <summary>
        /// GetYourMessage
        /// </summary>
        /// <returns></returns>
        private string GetYourMessage()
        {
            string query = "Content";
            string condition = " And FK_ToUserID='" + _objUserContext.UserID + "'";
            string message = string.Empty;
            bus_Message objMess = bus_Message.Instance(_objUserContext);  //bus_Message objMess = new bus_Message(_objUserContext, _dbName);
            DataSet dsMess = objMess.getList(condition, query);
            objMess = null;
            if (!base.isValidDataSet(dsMess)) return message;
            foreach (DataRow R in dsMess.Tables[0].Rows)
                message += R["Content"].ToString() + "*";
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        private void popup(string url, int width, int height, string title)
        {
            _script.Append("<script type=\"text/javascript\">");
            _script.Append("buildPopup_Frame(" + width + ", " + height + ", '" + title + "', '" + url + "');");
            _script.Append("ShowTheBox(false, rightSide, bottomCorner, bottopUp);");
            _script.Append("</script>");
            this.script_popup.Text = _script.ToString();
        }
    }
}