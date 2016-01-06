using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpb.app.business.ktnb.CoreBusiness;
using System.Text;
using C1.Web.C1WebGrid;
using System.Data;
using System.Drawing;
using System.Web.Configuration;
using System.IO;
using VPB_KTNB.Helpers;

namespace VPB_TDHS.Modules.DMS
{
    public partial class UploadFile : PageBase
    {
        #region initiation page variables
        protected string _action = string.Empty;
        protected string _documentid = string.Empty;
        protected string _documentType = string.Empty;
        protected string _rootFile = System.Configuration.ConfigurationManager.AppSettings["RootFile"].Replace("\\", "/");
        protected string _http_body = System.Configuration.ConfigurationManager.AppSettings["HTTP_BODY"];
        //thangma
        protected string _congviec_docid = string.Empty;
        protected string _cv = string.Empty;
        protected string _doankt = string.Empty;
        protected string _dotkt = string.Empty;
        #endregion

        #region Page init & load
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AuthorizeUserCtx();

            #region get data submit
            _action = Request["act"];
            _documentid = Request["doc"];
            _documentType = Request.Form["ctl00$FormContent$cboDocType"];
            //thangma
            _congviec_docid = Request["congviec_docid"];
           
            _cv = Request["cv"];
            _doankt = Request["doankt"];
            _dotkt = Request["dotkt"];
            #endregion

            #region action handler
            //if (!string.IsNullOrEmpty(_documentid))
            //{
            //    //if (_action == "uploadfiledoc")
            //        FeedBackClient(UpLoadFileDoc());
            //}

            #endregion

            #region init form
            InitForm("File đính kèm", "", string.Empty, 0);

            #endregion

            #region client control event handler

            #endregion

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        string UpLoadFileDoc()
        {
            string fullname = String.Empty;
            if (String.IsNullOrEmpty(_documentid))
            {
                fullname = "1";
                return fullname;
            }
            CommonFunc.UploadFile(FileUploadDoc, _m_groupname, _documentid, _objUserContext);
            fullname = "2";
            return fullname;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            if (FileUploadDoc.HasFile)
            {
                string extensionFile = WebConfigurationManager.AppSettings["ExtensionFile"];
                //kiem tra format file
                if (!string.IsNullOrEmpty(extensionFile))
                {
                    FileInfo fi = new FileInfo(FileUploadDoc.FileName);
                    string ext = fi.Extension.Replace(".", "");
                    foreach (string str in extensionFile.Split(','))
                    {
                        if (ext.ToUpper().Equals(str.ToUpper())) {
                            lblError.Text = "Không cho phép uppload file định dạng " + extensionFile;
                            lblError.ForeColor = Color.Red;
                            return;
                        }
                    }
                }

                string str_maxFileSize = WebConfigurationManager.AppSettings["MaxFileSize"];
                //kiem tra size file
                int fileSize = FileUploadDoc.PostedFile.ContentLength;
                int maxFileSize = string.IsNullOrEmpty(str_maxFileSize) ? 10 : Int32.Parse(str_maxFileSize);
                if (fileSize > (maxFileSize*1024*1024))
                {
                    lblError.Text = "Không cho phép dung lượng >" + maxFileSize + " MB 1 lần upload";
                    lblError.ForeColor = Color.Red;
                    return;
                }
               
                string re = CommonFunc.UploadFile(FileUploadDoc, _m_groupname, _documentid,txtDescription.Text, _objUserContext);
                if (re == "OK")
                {
                    lblError.Text = "Upload file thành công.";
                    lblError.ForeColor = Color.Blue;
                    if (!String.IsNullOrEmpty(_action))
                    {
                        if (_action == "attachfile")//redirect ve dotkiemtoan
                            Response.Redirect("DotKiemToanNam_Load.aspx?doc=" + _documentid + "&act=loaddoc");
                    }
                    else //redirect ve phathienhethong
                    {
                        Response.Redirect("PhatHienHeThong_Load.aspx?doc=" + _documentid + "&act=loaddoc&congviec_docid=" + _congviec_docid + "&cv=" + _cv + "&doankt=" + _doankt + "&dotkt=" + _dotkt);    
                    } 
                    
                }
                else
                {
                    lblError.Text = re;
                    lblError.ForeColor = Color.Red;
                }
            }
            else
            {
                lblError.Text = "Chọn 1 file để upload.";
                lblError.ForeColor = Color.Red;
            }
        }


    }
}