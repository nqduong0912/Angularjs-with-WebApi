using System;
using System.IO;

namespace VPB_KTNB.Helpers
{
    /// <summary>
    /// Summary description for FileHelper
    /// </summary>
    public class FileHelper
    {
        public FileHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// ReadFromTextFile
        /// </summary>
        /// <param name="Filename"></param>
        /// <returns></returns>
        public static string ReadFromTextFile(string Filename)
        {
            StreamReader reader = null;
            string result = string.Empty;
            try
            {
                string FilePath = System.Web.HttpContext.Current.Server.MapPath(Filename);
                reader = File.OpenText(FilePath);
                result = reader.ReadToEnd();
            }
            catch { }
            finally
            {
                if (reader != null) reader.Close();
            }
            return result;
        }
        /// <summary>
        /// WriteToTextFile
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static bool WriteToTextFile(string Filename, string Content)
        {
            bool result = false;
            StreamWriter writer = null;
            try
            {
                //string FilePath = System.Web.HttpContext.Current.Server.MapPath(Filename);
                string FilePath = Filename;
                if (!FileIsExists(FilePath))
                {
                    writer = File.CreateText(FilePath);
                    writer.WriteLine(Content);
                    writer.Flush();
                }
                else 
                {
                    writer = File.AppendText(FilePath);
                    writer.WriteLine(Content);
                    writer.Flush();
                }
            }
            catch
            {
            }
            finally
            {
                if (writer != null) writer.Close();
            }
            return result;
        }
        /// <summary>
        /// CountFilesOnFolder
        /// </summary>
        /// <param name="folderpath"></param>
        /// <returns></returns>
        public static int CountFilesOnFolder(string folderpath)
        {
            DirectoryInfo dir = new DirectoryInfo(folderpath);
            return dir.GetFiles().Length;
        }
        /// <summary>
        /// DeleteFileOnFolder
        /// </summary>
        /// <param name="folderpath"></param>
        /// <returns></returns>
        public static int DeleteFileOnFolder(string folderpath)
        {
            int err_sys = 0;
            try
            {
                foreach (string file in System.IO.Directory.GetFiles(folderpath))
                    System.IO.File.Delete(file);
            }
            catch (Exception ex)
            {
                err_sys = -1;
            }
            return err_sys;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool FileIsExists(string filepath)
        {
            return System.IO.File.Exists(filepath);
        }
    }
}
