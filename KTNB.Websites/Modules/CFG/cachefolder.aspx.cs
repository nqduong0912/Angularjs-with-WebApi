using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VPB_KTNB.Helpers;

namespace VPB_PROMOTION.CFG
{
    public partial class Modules_CFG_cachefolder : PageBase
    {
        #region initiation page variables
        #endregion

        #region page init & load
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            base.AuthorizeUserCtx();

            #region get data submit
            #endregion

            #region action handler

            #endregion

            base.InitForm("Quản lý Cache Folder", "userman.png", string.Empty, 0);

            #region client control event handler
            this.btnResetDownloadFolder.Text = "Reset";
            this.btnResetUploadFolder.Text = "Reset";
            this.btnResetDownloadFolder.Attributes.Add("onclick", "{return resetdownloadfolder();}");
            this.btnResetUploadFolder.Attributes.Add("onclick", "{return resetuploadfolder();}");
            #endregion
        }
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblFilesDown.Text = FileHelper.CountFilesOnFolder(Server.MapPath("~/modules/Download")).ToString() + " (files)";
            this.lblFilesUp.Text = FileHelper.CountFilesOnFolder(Server.MapPath("~/modules/Upload")).ToString() + " (files)";
        }
        #endregion

        #region page helper processing
        protected void resetdownloadfolder()
        {
            FileHelper.DeleteFileOnFolder(Server.MapPath("~/modules/Download"));
        }
        protected void resetuploadfolder()
        {
            FileHelper.DeleteFileOnFolder(Server.MapPath("~/modules/Upload"));
        }
        #endregion

        #region page button processing
        /// <summary>
        /// ResetDownload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ResetDownload(object sender, EventArgs e)
        {
            resetdownloadfolder();
            this.lblFilesDown.Text = FileHelper.CountFilesOnFolder(Server.MapPath("~/modules/Download")).ToString() + " (files)";
        }
        /// <summary>
        /// ResetUpload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ResetUpload(object sender, EventArgs e)
        {
            resetuploadfolder();
            this.lblFilesUp.Text = FileHelper.CountFilesOnFolder(Server.MapPath("~/modules/Upload")).ToString() + " (files)";
        }
        #endregion
    }
}