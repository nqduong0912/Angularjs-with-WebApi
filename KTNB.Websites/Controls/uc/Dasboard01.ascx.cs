using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VPB_CE.Controls.uc
{
    public partial class Dasboard01 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.Attributes.Add("onclick","{submitform(); return false;}");
            
        }
    }
}