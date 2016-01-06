using System;

namespace VPB_PROMOTION.MainView
{
    public partial class MainView_Toolbar : System.Web.UI.Page
    {
        string _docspace;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _docspace = Request.QueryString["docspace"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnAddnew.Click += new EventHandler(AddNew);
        }

        protected void AddNew(object sender, EventArgs e)
        {
            string url = string.Empty;
            if ((_docspace.ToLower() == "appuser") || (_docspace.ToLower() == "sysuser"))
                url = "~/Modules/UMS/NewUser.aspx?";
            else if ((_docspace.ToLower() == "group") || (_docspace.ToLower() == "supportgroup"))
                url = "~/Modules/UMS/NewGroup.aspx?";
            else if ((_docspace.ToLower() == "appcfg") || (_docspace.ToLower() == "syscfg"))
                url = "~/Modules/CFG/Parameters.aspx?a=new";
            else if (_docspace.ToLower() == "donvi")
                url = "~/Modules/UMS/DonVi_Input.aspx?";
            url += "&docspace=" + _docspace;

            Response.Redirect(url);
        }
    }
}