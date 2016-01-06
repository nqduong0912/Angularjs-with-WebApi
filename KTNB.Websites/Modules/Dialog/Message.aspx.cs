using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VPB_PROMOTION.Modules.Dialog
{
    public partial class Message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Title = Request.QueryString["t"];
            string Message = Request.QueryString["m"];
            if (Title == "SysErr")
            {
                Message = "Có lỗi xảy trong quá trình thực hiện. Kiểm tra lại kết nối mạng trên máy tính của bạn. Hoặc liên hệ với Trung Tâm Tin Học VPBank.";
            }
            Label mylabel = new Label();
            mylabel.Text = Message;
            mylabel.ForeColor = System.Drawing.Color.Red;
            //rpContent.Controls.Add(mylabel);
            //rpMessage.HeaderText = Title;
            
            //rpMessage.Font.Bold = true;
            //rpMessage.HeaderStyle.VerticalAlign = VerticalAlign.Middle;
            //rpMessage.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
        }
    }
}
