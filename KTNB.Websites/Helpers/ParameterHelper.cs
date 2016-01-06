using System;
using System.Data;
using CORE.CoreObjectContext;
using vpb.app.business.ktnb.CoreBusiness;

namespace VPB_KTNB.Helpers
{
    public static class ParameterHelper
    {
        #region constant

        public const string PARAM_TYPE_DOWNLOAD = "DOWNLOAD";
        public const string PARAM_TYPE_UPLOAD = "UPLOAD";

        #endregion constant

        /// <summary>
        /// GetValue
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objUserContext"></param>
        /// <returns></returns>
        public static string GetValue(string name, UserContext objUserContext)
        {
            if (string.IsNullOrEmpty(name)) return "";
            string svalue = "";
            bus_Project_Parameter objParam = bus_Project_Parameter.Instance(objUserContext);
            svalue = objParam.getByID(name, "value").Tables[0].Rows[0]["Value"].ToString();
            return svalue;
        }

        /// <summary>
        /// ResolveNamingPattern
        /// </summary>
        /// <param name="NamingPattern"></param>
        /// <param name="company"></param>
        /// <param name="mnemonic"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ResolveNamingPattern(string NamingPattern, string company, string mnemonic, string date, string fpt_root)
        {
            string folder_resolved = NamingPattern;

            if (StringHelper.isSubstring(folder_resolved, "{ROOT}"))
            {
                folder_resolved = folder_resolved.Replace("{ROOT}", fpt_root);
                if (folder_resolved == "/") folder_resolved = "";
            }
            if (StringHelper.isSubstring(folder_resolved, "{MNEMONIC}"))
            {
                folder_resolved = folder_resolved.Replace("{MNEMONIC}", mnemonic);
            }
            if (StringHelper.isSubstring(folder_resolved, "{COMPANY}"))
            {
                folder_resolved = folder_resolved.Replace("{COMPANY}", company);
            }
            if (StringHelper.isSubstring(folder_resolved, "{DATE}"))
            {
                folder_resolved = folder_resolved.Replace("{DATE}", date);
            }
            if (string.IsNullOrEmpty(folder_resolved)) folder_resolved = NamingPattern;
            return folder_resolved;
        }

        /// <summary>
        /// ResolveNamingPattern
        /// </summary>
        /// <param name="NamingPattern"></param>
        /// <param name="company"></param>
        /// <param name="mnemonic"></param>
        /// <param name="date"></param>
        /// <param name="fpt_root"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ResolveNamingPattern(string NamingPattern, string company, string mnemonic, string date, string fpt_root, string prefix)
        {
            string folder_resolved = NamingPattern;

            if (StringHelper.isSubstring(folder_resolved, "{" + prefix + "_ROOT}"))
            {
                folder_resolved = folder_resolved.Replace("{" + prefix + "_ROOT}", fpt_root);
                if (folder_resolved == "/") folder_resolved = "";
            }
            if (StringHelper.isSubstring(folder_resolved, "{MNEMONIC}"))
            {
                folder_resolved = folder_resolved.Replace("{MNEMONIC}", mnemonic);
            }
            if (StringHelper.isSubstring(folder_resolved, "{COMPANY}"))
            {
                folder_resolved = folder_resolved.Replace("{COMPANY}", company);
            }
            if (StringHelper.isSubstring(folder_resolved, "{DATE}"))
            {
                folder_resolved = folder_resolved.Replace("{DATE}", date);
            }
            if (string.IsNullOrEmpty(folder_resolved)) folder_resolved = NamingPattern;
            return folder_resolved;
        }

        /// <summary>
        /// ResolveNamingPattern
        /// </summary>
        /// <param name="NamingPattern"></param>
        /// <returns></returns>
        public static string ResolveNamingPattern(string NamingPattern)
        {
            string folder_resolved = NamingPattern;
            string dbname = System.Configuration.ConfigurationManager.AppSettings["dbname"].ToString();
            string date = string.Format("{0: yyyyMMdd.HHmm}", DateTime.Now);
            if (StringHelper.isSubstring(folder_resolved, "{DATE}"))
            {
                folder_resolved = folder_resolved.Replace("{DATE}", date);
            }
            return dbname + "." + folder_resolved.Trim();
        }

        /// <summary>
        /// GetApplicationDownloadFolder
        /// </summary>
        /// <param name="applicationid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetApplicationDownloadFolder(string applicationid, UserContext objUserContext, string dbName)
        {
            string dowloadfolder = "";
            string query = "Name,Value";
            string condition = " and fk_applicationid='" + applicationid + "' and parameter_type='" + PARAM_TYPE_DOWNLOAD + "'";
            bus_Project_Parameter objpara = new bus_Project_Parameter(objUserContext, dbName);
            DataSet ds = objpara.getList(condition, query);
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;
            foreach (DataRow R in ds.Tables[0].Rows)
            {
                dowloadfolder += R["value"].ToString() + ",";
            }
            return dowloadfolder;
        }

        /// <summary>
        /// GetApplicationUploadFolder
        /// </summary>
        /// <param name="applicationid"></param>
        /// <param name="objUserContext"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string GetApplicationUploadFolder(string applicationid, UserContext objUserContext, string dbName)
        {
            string uploadfolder = "";
            string query = "Name,Value";
            string condition = " and fk_applicationid='" + applicationid + "' and parameter_type='" + PARAM_TYPE_UPLOAD + "'";
            bus_Project_Parameter objpara = new bus_Project_Parameter(objUserContext, dbName);
            DataSet ds = objpara.getList(condition, query);
            if (ds == null) return string.Empty;
            if (ds.Tables[0].Rows.Count == 0) return string.Empty;

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                uploadfolder += R["value"].ToString() + ",";
            }
            return uploadfolder;
        }
    }
}