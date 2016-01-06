using System.Web;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// UrlHelper
    /// </summary>
    /// <author>dungnt</author>
    /// <createddate>2008</createddate>
    /// <modifiedby></modifiedby>
    /// <modifieddate></modifieddate>
    public static class UrlHelper
    {
        public static string GetSiteUrl()
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return "/";
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            int end = url.IndexOf(HttpContext.Current.Request.ApplicationPath) + HttpContext.Current.Request.ApplicationPath.Length;
            url = url.Substring(0, end);

            return url;
        }
    }
}