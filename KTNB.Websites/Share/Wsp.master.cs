using System;

namespace VPB_KTNB.Share
{
    public partial class Share_Wsp : System.Web.UI.MasterPage
    {
        protected string username;
        protected string usercode;
        protected string rolename;
        protected string groupname;
        protected string groupdesc;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CORE.CoreObjectContext.UserContext objctx = (CORE.CoreObjectContext.UserContext)Session["objUserContext"];
            if (objctx == null) return;
            username = objctx.UserName;
            usercode = objctx.UserCode;
            rolename = ((CORE.CoreObjectContext.Role)objctx.Roles[0]).RoleName;
            groupname = ((CORE.CoreObjectContext.Group)objctx.Groups[0]).GroupName;
            groupdesc = ((CORE.CoreObjectContext.Group)objctx.Groups[0]).GroupDescription;
            if (!string.IsNullOrEmpty(usercode))
                Page.Title = System.Configuration.ConfigurationManager.AppSettings["ProjectName"] + " | " + username + " | " + usercode + " | " + rolename + " | " + CORE.HELPERS.StringHelper.Right(groupname, 3) + " | " + groupdesc;
            else
                Page.Title = System.Configuration.ConfigurationManager.AppSettings["ProjectName"] + " | " + username + " | " + rolename + " | " + CORE.HELPERS.StringHelper.Right(groupname, 3) + " | " + groupdesc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}