using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace VPB_CRM.Helper
{
    public static class SqlSpReturnValue
    {
        public const int SP_PROCESS_STATUS_DONE = 4;
        public const int SP_PROCESS_STATUS_ABORT = 16;
        public const int SP_PROCESS_STATUS_ERROR = 32;
    }
}