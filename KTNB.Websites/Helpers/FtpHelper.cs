using System;

namespace VPB_KTNB.Helpers
{
    public static class FtpHelper
    {
        private static EnterpriseDT.Net.Ftp.FTPConnection _objFTP;

        public static bool FTPFolderIsExists(string folder, string parentpath, EnterpriseDT.Net.Ftp.FTPConnection oFTP)
        {
            if (parentpath == "/") return true;

            try
            {
                string[] files = oFTP.GetFiles(parentpath);
                if (files.Length == 0) return false;
                for (int i = 0; i < files.Length; i++)
                {
                    string f = files[i];
                    if (!string.IsNullOrEmpty(f))
                    {
                        f = f.Replace("\\", "/");
                        string[] foldersName = f.Split(new char[] { '/' });
                        string folderexists = foldersName[foldersName.Length - 1];
                        if (folderexists.ToUpper().Equals(folder.ToUpper())) return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                string ErrDetail = e.Message;
                ErrDetail += "<br/>folder: " + folder;
                ErrDetail += "<br/>parentpath: " + parentpath;
                ErrDetail += "<br/>ftp IsConnected: " + oFTP.IsConnected.ToString();
                ErrDetail += "<br/>ftp ServerAddress: " + oFTP.ServerAddress;
                ErrDetail += "<br/>ftp ServerDirectory: " + oFTP.ServerDirectory;
                ErrDetail += "<br/>ftp LocalDirectory: " + oFTP.LocalDirectory;

                FormHelper.FormWarning("Lỗi kết nối", ErrDetail, "Red");
                return false;
            }
        }
        public static string GetRoot(string app)
        {
            string root;
            //if (app.Equals(VPB_CRM.Helper.Constant.AMS.APP_REPORT))
            //    root = System.Web.HttpContext.Current.Application["REPORT_ROOT"].ToString();
            //else if (app.Equals(VPB_CRM.Helper.Constant.AMS.APP_SWIFT))
            //    root = System.Web.HttpContext.Current.Application["SWIFT_ROOT"].ToString();
            //else if (app.Equals(VPB_CRM.Helper.Constant.AMS.APP_REUTER))
            //    root = System.Web.HttpContext.Current.Application["REUTER_ROOT"].ToString();
            //else
                root = string.Empty;

            return root;
        }
        public static string VerifyFTPFolder(string folder, string root0, EnterpriseDT.Net.Ftp.FTPConnection oFTP)
        {
            string root = root0;

            //System.Web.HttpContext.Current.Response.Write("<br/>------------------");
            //System.Web.HttpContext.Current.Response.Write("<br/>folder:" + folder);
            //System.Web.HttpContext.Current.Response.Write("<br/>root0:" + root0);
            //System.Web.HttpContext.Current.Response.Write("<br/>------------------");

            string ftpFolder = string.Empty;

            if (!FTPFolderIsExists(folder, root, oFTP))
            {
                ftpFolder = root;
                if (!StringHelper.Right(ftpFolder, 1).Equals("/")) ftpFolder += "/";
                ftpFolder += folder;
                try
                {
                    oFTP.CreateDirectory(ftpFolder);
                }
                catch (EnterpriseDT.Net.Ftp.FTPException ex)
                {
                    string messerr = ex.Message + "<br/>ftpFolder:" + ftpFolder;
                    messerr += "<br/>root: " + root;
                    messerr += "<br/>folder: " + folder;
                    FormHelper.FormWarning("FTP Create Folder Error", messerr, "red");
                }
            }
            else
            {
                ftpFolder = root;
                if (!StringHelper.Right(ftpFolder, 1).Equals("/")) ftpFolder += "/";
                ftpFolder += folder;
            }
            return ftpFolder;
        }
        public static string VerifyFTPMultiFolder(string folder, string root, EnterpriseDT.Net.Ftp.FTPConnection oFTP)
        {
            string retvalue = root;
            string[] arrFolder = folder.Split(new char[1] { '/' });
            foreach (string sfolder in arrFolder)
            {
                if (sfolder.Length > 0)
                {
                    retvalue = VerifyFTPFolder(sfolder, retvalue, oFTP);
                }
            }
            return retvalue;
        }

        public static string Upload(string filename, string IP, int port, string username, string password, string ftpfolder)
        {
            string ErrorMessage = string.Empty;

            #region Connect FTP server
            _objFTP = new EnterpriseDT.Net.Ftp.FTPConnection();
            _objFTP.ServerAddress = IP;
            _objFTP.ServerPort = port;
            _objFTP.UserName = username;
            _objFTP.Password = password;
            _objFTP.UseGuiThreadIfAvailable = true;
            _objFTP.TransferBufferSize = 512;
            _objFTP.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.PASV;
            _objFTP.Timeout = 90000;
            try
            {
                _objFTP.Connect();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "FtpHelper.Upload(): " + ex.Message;
                _objFTP.Dispose();
                return ErrorMessage;
            }
            if (!_objFTP.IsConnected)
            {
                ErrorMessage = "FtpHelper.Upload(): FTP server is not connected.";
                _objFTP.Dispose();
                return ErrorMessage;
            }
            #endregion

            #region Upload file
            try
            {
                _objFTP.UploadFile(filename, ftpfolder + "/123456.csv");
            }
            catch (Exception ex)
            {
                ErrorMessage = "FtpHelper.Upload(): " + ex.Message;
                _objFTP.Dispose();
                return ErrorMessage;
            }
            #endregion

            return ErrorMessage;
        }
    }
}