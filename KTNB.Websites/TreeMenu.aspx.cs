using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using VPB_KTNB.Helpers;

public partial class TreeMenu : PageBase
{
    #region Initiation page variables
    private string _appID;
    #endregion

    #region Page Init & Load
    /// <summary>
    /// OnInit
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        base.AuthorizeUserCtx();
    }
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region menu
        MenuHelper objMenu = new MenuHelper();
        XMenuHelper XobjMenu = new XMenuHelper();
        try
        {
            _appID = Request.QueryString["appid"].ToString();
        }
        catch
        {
            _appID = string.Empty;
        }
        this.boxmenu.Text = XobjMenu.LoadMenuBox(string.Empty, _appID, string.Empty, "Red");
        #endregion
    }
    #endregion

    #region Page heper processing
    /// <summary>
    /// openURL
    /// </summary>
    /// <param name="url"></param>
    protected void openURL(string url)
    {
        Response.Write("<script type=\"text/javascript\">");
        Response.Write("window.open('" + url + "','fraDetail');");
        Response.Write("</script>");
    }
    #endregion
}
